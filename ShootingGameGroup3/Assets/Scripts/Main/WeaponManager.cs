using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    enum WeaponName { /*Triump*/
    }; // 추후 다른 총 완성 되면 추가 하겠습니다.

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
            // case Triump:
            // LSM_Triump().Triump_Left_Click();
            // break;
            default:
                break;
        }
    }

    public void SpecialSkill()
    {
        switch (currentWeaponName)
        {
            // case Triump:
            // LSM_Triump().Triump_Right_Click();
            // break;
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
            // case Triump:
            // LSM_Triump().Triump_Left_Shift();
            // break;
            default:
                break;
        }
    }
}
