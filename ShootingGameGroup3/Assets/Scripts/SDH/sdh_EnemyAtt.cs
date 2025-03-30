using UnityEngine;

public class sdh_EnemyAtt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Attacked");
        }
    }
}
