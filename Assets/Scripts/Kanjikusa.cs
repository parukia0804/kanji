using UnityEngine;

public class KanjiDefenseGrass : MonoBehaviour
{
    public int durability = 10;          // �ϋv�l
    public int healAmount = 1;           // �񕜗�
    public int attackDamage = 1;         // �U���_���[�W
    public float effectRange = 3f;       // �͈́i�񕜂ƍU�����ʁj
    public float effectInterval = 1f;    // �Ԋu�i�񕜂ƍU�����ʁj

    private float effectTimer;

    void Update()
    {
        effectTimer += Time.deltaTime;

        if (effectTimer >= effectInterval)
        {
            effectTimer = 0f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, effectRange);
            foreach (Collider2D hit in hits)
            {
                // �񕜑ΏہiCore�Ȃǁj
                if (hit.CompareTag("Core"))
                {
                    Core core = hit.GetComponentInParent<Core>();
                    if (core != null)
                    {
                        core.Heal(healAmount);
                    }
                }

                // �U���ΏہiEnemy�j
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(attackDamage);
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            durability--;
            if (durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, effectRange);
    }
}

