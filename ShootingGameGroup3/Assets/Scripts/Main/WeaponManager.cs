using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    enum WeaponName { /*샷건, 라이플, 차지라이플*/ };
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

    public void NormalShoot() //mousebuttondown 0
    {
        switch (currentWeaponName)
        {
            // case WeaponName.샷건:
            // break;
            // case WeaponName.차지라이플:
            // sdh_Gun.Shoot(GameManager.Instance.MouseManager.GetMousePos());

            default:
            break;
        }
    }

    public void SpecialSkill() //mousebuttondown 1
    {
        switch (currentWeaponName)
        {
            // case WeaponName.차지라이플:
            // sdh_Gun.flashRoutineStart();
            default:
            break;
        }
    }

    public void ChargeSkill() // mousebutton 1
    {
        switch (currentWeaponName)
        {
            // case WeaponName.차지라이플:
            // sdh_Gun.Charging();
            default:
            break;
        }
    }

    public void ChangeBullet() //left shift
    {
        switch (currentWeaponName)
        {
            default:
            break;
        }
    }

    // mousebuttonup 1
    // case WeaponName.차지라이플:
    // sdh_Gun.ShootOut(GameManager.Instance.MouseManager.GetMousePos());

}
