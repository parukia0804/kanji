using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KanjiDefense : MonoBehaviour
{
    public int durability = 10;       // 耐久値
    public int fireDamage = 1;        // 火属性ダメージ
    public float attackRange = 3f;    // 範囲
    public float attackInterval = 1f; // 攻撃間隔

    public float damageInterval = 1.0f; // ダメージを受ける間隔（秒）
    private List<Enemy> enemies = new List<Enemy>();
    private Coroutine damageCoroutine;

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        // 範囲攻撃（炎）
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

    // 敵がぶつかってきたとき
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);

                // ぶつかった瞬間にもダメージ
                if (enemy.isAttacking)
                {
                    ApplyDamage(enemy);
                }

                // コルーチン開始
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

                // 敵がいなくなったら止める
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
        Debug.Log("壁の耐久力: " + durability + " (攻撃: " + enemy.attack + ")");

        if (durability <= 0)
        {
            Debug.Log("壁が破壊された！");
            Destroy(gameObject);
        }
    }

    // 範囲表示（デバッグ用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}