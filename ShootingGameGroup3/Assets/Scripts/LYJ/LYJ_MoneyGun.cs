using System.Collections;
using UnityEngine;

public class LYJ_MoneyGun : MonoBehaviour
{
    #region Basic Stats
    private float _damage;
    public float Damage => _damage;

    private bool _readyToShoot;
    public bool ReadyToShoot => _readyToShoot;

    private WaitForSeconds _attackDelay;
    public WaitForSeconds AttackDelay => _attackDelay;
    #endregion
    [SerializeField]
    GameObject money;
    [SerializeField]
    GameObject bullet;

    void Awake()
    {
        _readyToShoot = true;
        _damage = 1.5f;
        _attackDelay = new WaitForSeconds(0.3f);
    }


    public void FireBuck()
    {
        Vector2 directionVec = LYJ_GameManager.Instance.Aim.GetMousePos() - transform.position;
        GameObject currentMoney = Instantiate(money, transform.position, Quaternion.identity);
        currentMoney.GetComponent<LYJ_Money>().ShootMoney(directionVec, 3f/*temp*/);
    }

    public void FireCoin()
    {
        Vector2 directionVec = (LYJ_GameManager.Instance.Aim.GetMousePos() - transform.position).normalized;
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<LYJ_Bullet>().ShootBullet(directionVec, 10f/*temp*/, _damage);
        StartCoroutine(DelayFire());
    }



    IEnumerator DelayFire()
    {
        _readyToShoot = false;
        yield return AttackDelay;
        _readyToShoot = true;
    }
}
