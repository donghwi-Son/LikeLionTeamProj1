using System.Collections;
using UnityEngine;

public class LHG_Monster : MonoBehaviour
{
    public int health = 3;
    public GameObject MiniMonsterPrefab;
    public int numberOfMiniMonster = 3;

    public float moveSpeed = 1f; 
    private Transform player;
    public float chaseDistance = 7f; 

    private Vector3 randomDirection;
    private float changeDirectionTime = 2f;
    private float timer;

    private SpriteRenderer spriteRenderer; 
    private Color originalColor;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
        else
        {
            transform.position += randomDirection * moveSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                SetRandomDirection();
                timer = 0f;
            }
        }
    }

    private void SetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, randomY, 0).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SplitIntoMiniMonster();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(BecomeTransparent(2f));
        }
    }

    private IEnumerator BecomeTransparent(float duration)
    {
        Color transparentColor = originalColor;
        transparentColor.a = 0.1f;
        spriteRenderer.color = transparentColor;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = originalColor;
    }

    private void SplitIntoMiniMonster()
    {
        for (int i = 0; i < numberOfMiniMonster; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Instantiate(MiniMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}