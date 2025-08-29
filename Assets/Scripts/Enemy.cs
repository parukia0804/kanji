using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 5;
    public float speed = 2f;
    private Transform core;
    bool isAttacking = false;

    void Start()
    {
        GameObject coreObj = GameObject.FindWithTag("Core");
        if (coreObj != null)
        {
            core = coreObj.transform;
        }
    }

    void Update()
    {
        if (core == null || isAttacking == true) return;

        Collider2D col = GetComponent<Collider2D>();
        Vector2 origin = transform.position;

        if (col != null)
        {
            origin = col.bounds.center;
        }

        Vector2 dir = ((Vector2)core.position - origin).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Coreにぶつかった場合
        if (other.CompareTag("Defence"))
        {
            isAttacking = true;
        }

        if (other.CompareTag("Core"))
        {
            Core coreComp = other.GetComponentInParent<Core>();
            if (coreComp != null)
            {
                coreComp.TakeDamage(1);
            }
            Destroy(gameObject);
        }

        // 防御オブジェクトにぶつかった場合
        if (other.CompareTag("Defense"))
        {
            KanjiDefenseGrass defenseComp = other.GetComponentInParent<KanjiDefenseGrass>();
            if (defenseComp != null)
            {
                defenseComp.durability--;
                if (defenseComp.durability <= 0)
                {
                    Destroy(defenseComp.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
