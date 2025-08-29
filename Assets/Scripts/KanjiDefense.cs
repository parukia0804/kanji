using UnityEngine;
using System.Collections.Generic;

public class KanjiDefense : MonoBehaviour
{
    public int durability = 10;      // 耐久値
    public int fireDamage = 1;       // 火属性ダメージ
    public float attackRange = 3f;   // 範囲
    public float attackInterval = 1f; // 攻撃間隔

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        // 火属性攻撃（範囲内の敵にダメージ）
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

    // 敵がぶつかってきたときに耐久値を減らす
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            durability--;
            Debug.Log("防御オブジェクトが攻撃された！ 残り耐久値: " + durability);

            if (durability <= 0)
            {
                Debug.Log("防御オブジェクトが破壊された！");
                Destroy(gameObject);
            }
        }
    }

    // 範囲表示（デバッグ用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
