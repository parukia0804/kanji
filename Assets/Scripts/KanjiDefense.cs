using UnityEngine;
using System.Collections.Generic;

public class KanjiDefense : MonoBehaviour
{
    public int durability = 10;      // �ϋv�l
    public int fireDamage = 1;       // �Α����_���[�W
    public float attackRange = 3f;   // �͈�
    public float attackInterval = 1f; // �U���Ԋu

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        // �Α����U���i�͈͓��̓G�Ƀ_���[�W�j
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

    // �G���Ԃ����Ă����Ƃ��ɑϋv�l�����炷
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            durability--;
            Debug.Log("�h��I�u�W�F�N�g���U�����ꂽ�I �c��ϋv�l: " + durability);

            if (durability <= 0)
            {
                Debug.Log("�h��I�u�W�F�N�g���j�󂳂ꂽ�I");
                Destroy(gameObject);
            }
        }
    }

    // �͈͕\���i�f�o�b�O�p�j
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
