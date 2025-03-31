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
    }
    public void ThrowBurner()
    {
        if (!ReadyToShoot) { return; }
        PoolManager.Instance.GetGameObject(burner);
        StartCoroutine(DelayFire());
    }



    IEnumerator DelayFire()
    {
        _readyToShoot = false;
        yield return AttackDelay;
        _readyToShoot = true;
    }
}
