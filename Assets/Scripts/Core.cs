using UnityEngine;

public class Core : MonoBehaviour
{
    public int hp = 20;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Debug.Log("コアにダメージ！ 残りHP: " + hp);
        if (hp <= 0)
        {
            Debug.Log("ゲームオーバー");
            // ここでリトライやシーンリセット処理を追加
        }
    }
}
