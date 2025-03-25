using UnityEngine;

public class MiniMonster : MonoBehaviour
{
    public int health = 3;
    public float moveSpeed = 1f; 
    private Transform player; 
    public float chaseDistance = 7f;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    
        if (distanceToPlayer < chaseDistance)
        {            
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            
            TakeDamage(1);
            
        }
    }
    }