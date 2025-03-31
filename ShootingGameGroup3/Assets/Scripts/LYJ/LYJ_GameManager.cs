using UnityEngine;

public class LYJ_GameManager : MonoBehaviour
{
    public static LYJ_GameManager Instance { get; private set; }

    #region Objects
    public LYJ_Aiming Aim ;
    public LYJ_Player Player ;
    public LYJ_WeaponManager WeaponManager;
    public LYJ_SpawnManager SpawnManager;
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

    

    #region 아직 메인으로 안 옮긴 부분
    
    float currentStageTime;
    public float CurrentStageTime => currentStageTime;
    bool isTimeGoing;
    void Update()
    {
        currentStageTime += Time.deltaTime;
    }
    
    public void StopGame()
    {
        isTimeGoing = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isTimeGoing = true;
        Time.timeScale = 1;
    }

    #endregion
}
