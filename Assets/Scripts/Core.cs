using UnityEngine;

public class Core : MonoBehaviour
{
    public int hp = 20;
    public int maxHp = 20; // �ő�HP��ݒ�

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        hp = Mathf.Max(hp, 0); // HP���}�C�i�X�ɂȂ�Ȃ��悤��
        Debug.Log("�R�A�Ƀ_���[�W�I �c��HP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("�Q�[���I�[�o�[");
            // �����Ń��g���C��V�[�����Z�b�g������ǉ�
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
        hp = Mathf.Min(hp, maxHp); // �ő�HP�𒴂��Ȃ��悤��
        Debug.Log("�R�A���񕜁I ����HP: " + hp);
    }
}

