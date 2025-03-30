using System.Collections.Generic;
using UnityEngine;

public class LYJ_UIManager : MonoBehaviour
{
    enum UIType { WeaponSlot, HP }

    List<GameObject> Hearts;
    GameObject HeartIcon;
    UIType type;

    void Awake()
    {
        switch (type)
        {
            case UIType.HP:
                Hearts = new List<GameObject>();
                for (int i = 0; i < LYJ_GameManager.Instance.Player.BasePlayerHp; ++i)
                {
                    GameObject heart = Instantiate(HeartIcon, transform);
                    Hearts.Add(heart);
                }
                SetHpUI();
                break;
            case UIType.WeaponSlot:
            
                break;
        }

    }

    public void SetHpUI()
    {
        foreach (var item in Hearts)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < LYJ_GameManager.Instance.Player.CurrentPlayerHp; ++i)
        {
            Hearts[i].SetActive(true);
        }
    }
}
