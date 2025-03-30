using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Objects")]
    #region Objects
    public MouseManager MouseManager;
    public Player Player;
    public WeaponManager WeaponManager;
    #endregion


    [Header("Scene Claer")]
    // 씬 클리어 상태 변수
    public bool isSceneCleared = false;

    // 씬 전환 순서 배열
    public string[] sceneOrder = new string[] { "MainScene", "CHW", "LHG", "LSM", "LYJ", "SDH" };

    public int stage { get; private set; } = 0;

    // 씬이 로드될 때 호출되는 이벤트 등록
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 새로운 씬이 로드된 후 isSceneCleared를 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneCleared = false;
        UpdateStage(scene.name);
    }

    private void UpdateStage(string sceneName)
    {
        switch (sceneName)
        {
            case "MainScene":
                stage = 0;
                break;
            case "CHW":
                stage = 1;
                break;
            case "LHG":
                stage = 2;
                break;
            case "LSM":
                stage = 3;
                break;
            case "LYJ":
                stage = 4;
                break;
            case "SDH":
                stage = 5;
                break;
            default:
                stage = 0;
                break;
        }
    }

    void Update()
    {
        // 매 프레임 씬 클리어 여부를 확인
        if (isSceneCleared)
        {
            LoadNextScene();
        }
    }

    // 외부에서 씬 클리어를 호출할 때 사용
    public void ClearScene()
    {
        isSceneCleared = true;
    }

    // 현재 씬의 다음 씬으로 이동하는 함수
    void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentIndex = System.Array.IndexOf(sceneOrder, currentSceneName);

        // 현재 씬이 배열에 없으면 에러 메시지 출력
        if (currentIndex < 0)
        {
            Debug.LogError("현재 씬이 sceneOrder 배열에 존재하지 않습니다: " + currentSceneName);
            return;
        }

        int nextIndex = currentIndex + 1;

        // 배열 내에서 다음 씬이 존재하면 로드, 아니면 완료 메시지 출력
        if (nextIndex < sceneOrder.Length)
        {
            SceneManager.LoadScene(sceneOrder[nextIndex]);
        }
        else
        {
            Debug.Log("모든 씬을 완료했습니다.");
            // 모든 씬 완료 후 내용 추가
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
}
