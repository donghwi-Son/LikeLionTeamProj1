using UnityEngine;

public class LYJ_GameManager : MonoBehaviour
{
    public static LYJ_GameManager Instance { get; private set; }

    #region Objects
    public LYJ_Aiming Aim ;
    public LYJ_Player Player ;
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
