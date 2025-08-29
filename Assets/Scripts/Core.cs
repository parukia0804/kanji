using UnityEngine;

public class Core : MonoBehaviour
{
    public int hp = 20;
    public int maxHp = 20; // 最大HPを設定

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        hp = Mathf.Max(hp, 0); // HPがマイナスにならないように
        Debug.Log("コアにダメージ！ 残りHP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("ゲームオーバー");
            // ここでリトライやシーンリセット処理を追加
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
        hp = Mathf.Min(hp, maxHp); // 最大HPを超えないように
        Debug.Log("コアを回復！ 現在HP: " + hp);
    }
}

