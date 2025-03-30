using UnityEngine;

public class sdh_Shield : MonoBehaviour
{
    AudioSource mys;
    private void Start()
    {
        mys = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            mys.Play();
        }
    }
}
