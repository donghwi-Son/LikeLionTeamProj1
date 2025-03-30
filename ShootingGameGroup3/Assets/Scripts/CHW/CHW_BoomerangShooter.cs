using UnityEngine;

public class CHW_BoomerangShooter : MonoBehaviour
{
    public Transform player;
    public GameObject boomerangPrefab;
    public Vector2 offset = new Vector2(0.5f, 0f);
    private bool facingRight = true;

    [Header("Boomerang Settings")]
    private bool canThrowBoomerang = true;
    private GameObject currentBoomerang;

    private void Update()
    {
        FollowPlayer();
        if (Input.GetMouseButtonDown(0) && canThrowBoomerang)
        {
            ThrowBoomerang();
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 playerPosition = player.position;
        float direction = player.localScale.x;

        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            facingRight = !facingRight;
            offset.x = -offset.x; // ÁÂ¿ì ¹ÝÀü
        }

        transform.position = new Vector3(playerPosition.x + offset.x, playerPosition.y + offset.y, 0f);
    }

    private void ThrowBoomerang()

     
    {
        if (!canThrowBoomerang) return;
        canThrowBoomerang = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 throwDirection = (mousePosition - transform.position).normalized;
        GameObject boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);

        CHW_Boomerang boomerangScript = boomerang.GetComponent<CHW_Boomerang>();
        if (boomerangScript != null)
        {
            boomerangScript.Initialize(transform, throwDirection);
        }
        else
        {
            Debug.LogError("Boomerang script not found on prefab!");
        }
    }


    public void RetrieveBoomerang()
    {
        canThrowBoomerang = true;
    }
}