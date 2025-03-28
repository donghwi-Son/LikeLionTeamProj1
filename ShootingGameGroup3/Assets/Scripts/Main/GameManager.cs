using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header ("Game Objects")]
    #region Objects
    public MouseManager MouseManager;
    public Player Player ;
    public WeaponManager WeaponManager;
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
}
