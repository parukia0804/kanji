using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 5;
    public float speed = 2f;
    private Transform core;
    bool isAttacking = false;

    void Start()
    {
        core = GameObject.FindWithTag("Core").transform;
    }

    void Update()
    {
        if (core == null || isAttacking == true) return;

        Vector2 dir = (core.position - transform.position).normalized;
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
    }
}
