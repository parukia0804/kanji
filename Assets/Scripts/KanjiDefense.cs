using UnityEngine;
using System.Collections.Generic;

public class KanjiDefense : MonoBehaviour
{
    public int durability = 10;      // �ϋv�l
    public int fireDamage = 1;       // �Α����_���[�W
    public float attackRange = 3f;   // �͈�
    public float attackInterval = 1f; // �U���Ԋu

    private float attackTimer;

    [SerializeField] Sprite[] possibleSprites;//�`�̈Ⴄ�X�v���C�g��o�^

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            durability--;
            if (durability <= 0)
            {
                Destroy(gameObject); // ����
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
