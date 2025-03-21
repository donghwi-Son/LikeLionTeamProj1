using UnityEngine;

public class LYJ_MoneyGun : MonoBehaviour
{
    #region Basic Stats
    private float _damage;
    public float Damage => _damage;

    private bool _readyToShoot;
    public bool ReadyToShoot => _readyToShoot;

    private int _maxBullet;
    public int MaxBullet => _maxBullet;

    private WaitForSeconds _attackDelay;
    public WaitForSeconds AttackDelay => _attackDelay;
    #endregion
    
    bool isMoneyMode;
    [SerializeField]
    GameObject money;
    [SerializeField]
    GameObject bullet; // 게임매니저에 추가?

    SpriteRenderer spriteRenderer;
    void Awake()
    {
        isMoneyMode = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        if (Input.GetMouseButtonDown(1))
        {
            isMoneyMode = !isMoneyMode;
            switch (isMoneyMode)
            {
                case true:
                    spriteRenderer.color = Color.red;
                    break;
                case false:
                    spriteRenderer.color = Color.blue;
                    break;
            }
        }
    }

    void Fire()
    {
        switch(isMoneyMode)
        {
            case true:
                FireMoney();
                break;
            case false:
                FireNormal();
                break;
        }
    }

    void FireMoney()
    {
        Vector2 directionVec = LYJ_GameManager.Instance.Aim.GetMousePos() - transform.position;
        GameObject currentMoney = Instantiate(money, transform.position, Quaternion.identity);
        currentMoney.GetComponent<LYJ_Money>().ShootMoney(directionVec, 3f/*temp*/);
    }

    void FireNormal()
    {
        Vector2 directionVec = (LYJ_GameManager.Instance.Aim.GetMousePos() - transform.position).normalized;
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<LYJ_Bullet>().ShootBullet(directionVec, 10f/*temp*/);
    }
}
