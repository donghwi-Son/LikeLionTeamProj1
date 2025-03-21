using UnityEngine;

public class LYJ_GameManager : MonoBehaviour
{
    public static LYJ_GameManager Instance { get; private set; }

    #region Objects
    [SerializeField]
    public LYJ_Aiming Aim;
    [SerializeField]
    public LYJ_Player Player;
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
