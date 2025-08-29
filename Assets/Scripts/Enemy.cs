using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    void Update()
    {
        // �e�X�g�p�ɃX�y�[�X�Ń_���[�W
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
        Debug.Log("�G���|���ꂽ");
        WaveManager.Instance.OnEnemyKilled();
        Destroy(gameObject);
    }
}
