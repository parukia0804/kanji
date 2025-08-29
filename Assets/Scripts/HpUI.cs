using UnityEngine;
using UnityEngine.UI;

public class CoreHPUI : MonoBehaviour
{
    public Core core;           // Coreスクリプトへの参照
    public Slider hpSlider;     // UIのSlider

    void Start()
    {
        if (core == null || hpSlider == null)
        {
            Debug.LogWarning("CoreまたはSliderが未設定です");
            return;
        }

        // 初期値の設定
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
