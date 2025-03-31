using System.Collections;
using UnityEngine;

public class LYJ_AlcoholBurnerHand : MonoBehaviour
{
    #region Basic Stats
    private bool _readyToShoot;
    public bool ReadyToShoot => _readyToShoot;
    
    private WaitForSeconds _attackDelay;
    public WaitForSeconds AttackDelay => _attackDelay;
    #endregion

    [SerializeField]
    GameObject burner;

    void Awake()
    {
        _readyToShoot = true;
        _attackDelay = new WaitForSeconds(0.5f);
        PoolManager.Instance.CreatePool(burner, 10);
    }
    public void ThrowBurner()
    {
        if (!ReadyToShoot) { return; }
        // PoolManager.Instance.GetGameObject(burner);
        Instantiate(burner);
        burner.transform.position = GameManager.Instance.Player.transform.position;
        StartCoroutine(DelayFire());
    }
    // public void ReturnBurner()
    // {
    //     PoolManager.Instance.ReturnGameObject(burner);
    // }



    IEnumerator DelayFire()
    {
        _readyToShoot = false;
        yield return AttackDelay;
        _readyToShoot = true;
    }
}
