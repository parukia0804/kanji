using UnityEngine;

public class KanjiBlock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("“G‚ªŠ¿š‚É“–‚½‚Á‚½I");
            // “G‚Ì“®‚«‚ğ~‚ß‚éˆ—‚È‚Ç‚ğ‘‚­
        }
    }
}
