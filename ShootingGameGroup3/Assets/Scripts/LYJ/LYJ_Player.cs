using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LYJ_Player : MonoBehaviour
{
    Vector2 inputVec;
    float moveSpeed = 3f; //temp
    Rigidbody2D _rb;
    bool isKickBacked = false;
    [SerializeField]
    GameObject alcohol;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isKickBacked) {return;}
        Vector2 nextVec = inputVec * moveSpeed;
        _rb.linearVelocity = nextVec;
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }

    public void KickBackRequest(Vector3 vec, float amount)
    {
        StartCoroutine(KickBack(vec, amount));
    }

    IEnumerator KickBack(Vector3 vec, float Amount)
    {
        isKickBacked = true;
        _rb.linearVelocity = vec * Amount;
        yield return new WaitForSeconds(0.2f);
        isKickBacked = false;
        
    }

    // 알코올램프 던지기
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Instantiate(alcohol, transform);
    //     }
    // }

void Update()
    {
        ControlWeapon();
    }

    void ControlWeapon()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LYJ_GameManager.Instance.WeaponManager.ChangeBullet();
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LYJ_GameManager.Instance.WeaponManager.ChangeWeapon();
        }

        if (Input.GetMouseButtonDown(0))
        {
            LYJ_GameManager.Instance.WeaponManager.NormalShoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            LYJ_GameManager.Instance.WeaponManager.SpecialSkill();
        }

        if (Input.GetMouseButton(1))
        {
            LYJ_GameManager.Instance.WeaponManager.ChargeSkill();
        }
    }


    #region 아직 메인으로 안 옮긴 부분
    int maxHp = 7; // temp
    int baseHp = 5;
    int currentHp;
    public int BasePlayerHp => baseHp;
    public int CurrentPlayerHp => currentHp;
    bool isMujuk = false;
    WaitForSeconds mujukTime = new WaitForSeconds(3f);
    void Hitted()
    {
        if (isMujuk) { return; }
        currentHp--;
        StartCoroutine(NotHittedNow());
    }

    IEnumerator NotHittedNow()
    {
        isMujuk = true;
        // 스프라이트 렌더러로 깜빡깜빡 할지
        yield return mujukTime;
        isMujuk = false;
    }
    #endregion


}
