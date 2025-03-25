using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Const Numbers
    float MOVE_SPEED = 3f;
    float ROLLING_POWER = 10f;
    float ROLLING_TIME = 0.2f;
    float ROLLING_COOLDOWN = 2.8f; // 구르는 시간과 합쳐서 3초
    #endregion

    #region Player Stats
    float moveSpeed;
    #endregion

    #region Player Actions
    bool isRolling;
    bool isKickBacked;
    bool readyToRoll;
    WaitForSeconds rollingTime;
    WaitForSeconds rollingCooldown;
    #endregion

    #region Components (Include Input System Vector)
    Vector2 inputVec;
    Rigidbody2D rb;
    SpriteRenderer playerSpriteRenderer;
    #endregion

    void Awake()
    {
        rollingTime = new WaitForSeconds(ROLLING_TIME);
        rollingCooldown = new WaitForSeconds(ROLLING_COOLDOWN);
        moveSpeed = MOVE_SPEED;
        InitAllActions();
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }

    void OnJump()
    {
        if (isRolling || !readyToRoll) { return; }
        StartCoroutine(Rolling());
    }

    void FixedUpdate()
    {
        if (isRolling) {return;}
        Move();
        FlipPlayer();
    }

    void Move()
    {
        Vector2 nextVec = inputVec * moveSpeed;
        rb.linearVelocity = nextVec;
    }

    void InitAllActions()
    {
        isRolling = false;
        isKickBacked = false;
        readyToRoll = true;
    }

    void FlipPlayer()
    {
        if (GameManager.Instance.MouseManager.GetMousePos().x < transform.position.x)
        {
            playerSpriteRenderer.flipX = true;
            // 무기 뒤집기는 따로 분리
        }
        else
        {
            playerSpriteRenderer.flipX = false;
            // 무기 뒤집기는 따로 분리
        }
    }

    IEnumerator Rolling()
    {
        isRolling = true;
        readyToRoll = false;
        Vector3 rollDir = new Vector3(inputVec.x, inputVec.y, 0).normalized;
        if (rollDir == Vector3.zero)
        {
            Vector3 rollToMouseVec = GameManager.Instance.MouseManager.GetMousePos() - transform.position;
            rollDir = new Vector3(rollToMouseVec.x, rollToMouseVec.y, 0).normalized;
        }
        rb.AddForce(rollDir * ROLLING_POWER, ForceMode2D.Impulse);
        yield return rollingTime;
        rb.linearVelocity = Vector2.zero;
        isRolling = false;
        yield return rollingCooldown;
        readyToRoll = true;
    }

}
