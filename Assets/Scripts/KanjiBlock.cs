using UnityEngine;

public class KanjiBlock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�G�������ɓ��������I");
            // �G�̓������~�߂鏈���Ȃǂ�����
        }
    }
}
