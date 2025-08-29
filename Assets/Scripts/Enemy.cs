using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    void Update()
    {
        // テスト用にスペースでダメージ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("敵が倒された");
        WaveManager.Instance.OnEnemyKilled();
        Destroy(gameObject);
    }
}
