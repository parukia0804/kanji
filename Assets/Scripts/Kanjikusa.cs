using UnityEngine;

public class KanjiDefenseGrass : MonoBehaviour
{
    public int durability = 10;          // 耐久値
    public int healAmount = 1;           // 回復量
    public int attackDamage = 1;         // 攻撃ダメージ
    public float effectRange = 3f;       // 範囲（回復と攻撃共通）
    public float effectInterval = 1f;    // 間隔（回復と攻撃共通）

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
                // 回復対象（Coreなど）
                if (hit.CompareTag("Core"))
                {
                    Core core = hit.GetComponentInParent<Core>();
                    if (core != null)
                    {
                        core.Heal(healAmount);
                    }
                }

                // 攻撃対象（Enemy）
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

