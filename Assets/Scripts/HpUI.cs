using UnityEngine;
using UnityEngine.UI;

public class CoreHPUI : MonoBehaviour
{
    public Core core;           // Core�X�N���v�g�ւ̎Q��
    public Slider hpSlider;     // UI��Slider

    void Start()
    {
        if (core == null || hpSlider == null)
        {
            Debug.LogWarning("Core�܂���Slider�����ݒ�ł�");
            return;
        }

        // �����l�̐ݒ�
        hpSlider.maxValue = core.maxHp;
        hpSlider.value = core.hp;
    }

    void Update()
    {
        if (core != null && hpSlider != null)
        {
            hpSlider.value = core.hp;
        }
    }
}
