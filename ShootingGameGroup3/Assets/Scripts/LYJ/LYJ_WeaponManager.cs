using System.Collections.Generic;
using UnityEngine;

public class LYJ_WeaponManager : MonoBehaviour
{
    enum WeaponName { /*샷건, 라이플*/ };
    List<GameObject> allWeapons;
    Queue<GameObject> selectableWeapons;
    WeaponName currentWeaponName;
    GameObject currentWeapon;

    void SetSelectableWeapon()
    {
        // 스테이지별 가능한 웨펀 Enqueue
        ChangeWeapon(); // 최초 무기 할당
    }

    public void ChangeWeapon()
    {
        currentWeapon = selectableWeapons.Dequeue();
        selectableWeapons.Enqueue(currentWeapon);
    }

    public void NormalShoot()
    {
        switch (currentWeaponName)
        {
            // case WeaponName.샷건:
            // break;
            default:
            break;
        }
    }

    public void SpecialSkill()
    {
        switch (currentWeaponName)
        {
            default:
            break;
        }
    }

    public void ChargeSkill()
    {
        switch (currentWeaponName)
        {
            default:
            break;
        }
    }

    public void ChangeBullet()
    {
        switch (currentWeaponName)
        {
            default:
            break;
        }
    }

}
