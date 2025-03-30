using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Stage Maps")]
    [SerializeField]
    private GameObject stage1MapPrefab;

    [SerializeField]
    private GameObject stage2MapPrefab;

    private GameObject currentMap;
    private int currentStage = 1;
    private bool isStageCleared = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartStage1();
        //GameManager.Instance.Player.SpdChange(5f);
    }

    void Update()
    {
        // 디버깅용 스테이지 전환 키
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"Stage {currentStage} Clear!");
            ClearCurrentStage();
        }
    }

    public void StartStage1()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        currentStage = 1;
        isStageCleared = false;
        currentMap = Instantiate(stage1MapPrefab);
        // 스테이지 1 초기화 로직
    }

    public void StartStage2()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        currentStage = 2;
        isStageCleared = false;
        currentMap = Instantiate(stage2MapPrefab);
        // 스테이지 2 초기화 로직
    }

    public void ClearCurrentStage()
    {
        isStageCleared = true;

        if (currentStage == 1)
        {
            StartStage2();
        }
        else if (currentStage == 2)
        {
            GameManager.Instance.ClearScene(); // 게임 클리어 처리
            Debug.Log("Game Clear!");
        }
    }

    public int GetCurrentStage()
    {
        return currentStage;
    }
}
