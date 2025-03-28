using UnityEngine;

public class LYJ_GameManager : MonoBehaviour
{
    public static LYJ_GameManager Instance { get; private set; }

    #region Objects
    public LYJ_Aiming Aim ;
    public LYJ_Player Player ;
    public LYJ_WeaponManager WeaponManager;
    #endregion


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    void Update()
    {
        
    }

    #region 아직 메인으로 안 옮긴 부분
    

    #endregion
}
