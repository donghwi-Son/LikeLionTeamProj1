using UnityEngine;

public class CHW_Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            gm.isSceneCleared = true;
            /*if (gm != null)
            {
                gm.isSceneCleared = true;
                Debug.Log("Player와 충돌: 씬 클리어 처리됨.");
            }
            else
            {
                Debug.LogWarning("GameManager를 찾을 수 없습니다.");
            }*/
        }
    }
}
