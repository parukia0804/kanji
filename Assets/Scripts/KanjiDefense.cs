using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KanjiDefense : MonoBehaviour
{
    public int durability = 10;       // �ϋv�l
    public int fireDamage = 1;        // �Α����_���[�W
    public float attackRange = 3f;    // �͈�
    public float attackInterval = 1f; // �U���Ԋu

    public float damageInterval = 1.0f; // �_���[�W���󂯂�Ԋu�i�b�j
    private List<Enemy> enemies = new List<Enemy>();
    private Coroutine damageCoroutine;

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        // �͈͍U���i���j
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(fireDamage);
                    }
                }
            }
        }
    }

    // �G���Ԃ����Ă����Ƃ�
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);

                // �Ԃ������u�Ԃɂ��_���[�W
                if (enemy.isAttacking)
                {
                    ApplyDamage(enemy);
                }

                // �R���[�`���J�n
                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(DamageOverTime());
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemies.Contains(enemy))
            {
                enemies.Remove(enemy);

                // �G�����Ȃ��Ȃ�����~�߂�
                if (enemies.Count == 0 && damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                    damageCoroutine = null;
                }
            }
        }
    }

    IEnumerator DamageOverTime()
    {
        while (true)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null && enemy.isAttacking)
                {
                    ApplyDamage(enemy);
                }
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }

    void ApplyDamage(Enemy enemy)
    {
        durability -= enemy.attack;
        Debug.Log("�ǂ̑ϋv��: " + durability + " (�U��: " + enemy.attack + ")");

        if (durability <= 0)
        {
            Debug.Log("�ǂ��j�󂳂ꂽ�I");
            Destroy(gameObject);
        }
    }

    // �͈͕\���i�f�o�b�O�p�j
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}