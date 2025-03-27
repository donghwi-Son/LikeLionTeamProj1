using System;
using UnityEngine;

public class LHG_Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public int bounceCount = 2;

    private Vector2 direction;
    private int currentBounces = 0;

    public GameObject Effect;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            LHG_Monster monster = collision.GetComponent<LHG_Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                CreateEffect();
                Bounce(collision.transform.position);
            }
        }
        else if (collision.CompareTag("MiniMonster"))
        {
            LHG_MiniMonster miniMonster = collision.GetComponent<LHG_MiniMonster>();
            if (miniMonster != null)
            {
                miniMonster.TakeDamage(damage);
                CreateEffect();
                Bounce(collision.transform.position);
            }
        }
    }

    void CreateEffect()
    {
        GameObject go = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f);
    }

    void Bounce(Vector2 hitPoint)
    {
        if (currentBounces < bounceCount)
        {
            currentBounces++;
            Vector2 bounceDirection = (Vector2)transform.position - hitPoint;
            direction = bounceDirection.normalized;

            
            FindClosestMonster();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FindClosestMonster()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 10f);
        Transform closestMonsterTransform = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in monsters)
        {
            if (collider.CompareTag("Monster") || collider.CompareTag("MiniMonster"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestMonsterTransform = collider.transform; 
                }
            }
        }

        if (closestMonsterTransform != null)
        {
            Vector2 targetDirection = (closestMonsterTransform.position - transform.position).normalized;
            direction = targetDirection;
        }
    }
}