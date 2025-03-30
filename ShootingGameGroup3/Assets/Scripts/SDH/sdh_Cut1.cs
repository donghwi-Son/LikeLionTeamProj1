using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class sdh_Cut1 : MonoBehaviour
{
    PlayableDirector pd;
    Collider2D col;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pd.Play();
            col.enabled = false;
        }
    }

    
}
