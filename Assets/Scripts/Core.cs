using UnityEngine;

public class Core : MonoBehaviour
{
    public int hp = 20;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Debug.Log("�R�A�Ƀ_���[�W�I �c��HP: " + hp);
        if (hp <= 0)
        {
            Debug.Log("�Q�[���I�[�o�[");
            // �����Ń��g���C��V�[�����Z�b�g������ǉ�
        }
    }
}
