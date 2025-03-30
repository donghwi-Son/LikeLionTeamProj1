using System.Collections;
using UnityEngine;

public class LSM_CardQueen : MonoBehaviour
{
    private LSM_Monster monsterScript;

    public GameObject Player;

    public GameObject CardSoldier;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public bool canSpawn = true;
    public float spawnInterval = 15f;

    public GameObject CardBullet;
    public Transform CardBulletSpawnPoint;
    public bool canShoot = true;
    public float shootInterval = 3f;

    public GameObject RuinLazer;
    public GameObject LazerWarning;
    public Transform RuinLazerSpawnPoint;

    private bool pattern450Triggered = false;
    private bool pattern60Triggered = false;
    private bool pattern40Triggered = false;
    private bool pattern10Triggered = false;

    void Awake()
    {
        monsterScript = GetComponent<LSM_Monster>();
    }

    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        monsterScript.SetInvincibility(false);
        monsterScript.isTracking = false;
        monsterScript.isSmells = false;
        monsterScript.health = 500;
    }

    void Update()
    {
        SpawnSolider();
        CardAttack();
        PatternManage();
    }

    void PatternManage()
    {
        int health = monsterScript.health;

        if (health <= 450 && !pattern450Triggered)
        {
            pattern450Triggered = true;
            Pattern450();
        }

        if (health <= 60 && !pattern60Triggered)
        {
            pattern60Triggered = true;
            Pattern60();
        }

        if (health <= 40 && !pattern40Triggered)
        {
            pattern40Triggered = true;
            Pattern40();
        }

        if (health <= 10 && !pattern10Triggered)
        {
            pattern10Triggered = true;
            Pattern10();
        }
    }

    void Pattern450()
    {
        Debug.Log("450줄 패턴 실행");
        Big_CardAttack();
        // 패턴 구현
    }

    void Pattern60()
    {
        Debug.Log("300줄 패턴 실행");
        // 패턴 구현
    }

    void Pattern40()
    {
        Debug.Log("40% 패턴 실행");
        // 패턴 구현
    }

    void Pattern10()
    {
        Debug.Log("100줄 패턴 실행");
        Ruined_Lazer();
        // 패턴 구현
    }

    void SpawnSolider()
    {
        if (!canSpawn)
            return;
        GameObject soldier = Instantiate(CardSoldier, spawnPoint.position, Quaternion.identity);
        GameObject soldier2 = Instantiate(CardSoldier, spawnPoint2.position, Quaternion.identity);
        canSpawn = false;
        StartCoroutine(SpawnDelay());
    }

    void CardAttack()
    {
        if (!canShoot)
            return;
        GameObject cardBullet = Instantiate(
            CardBullet,
            CardBulletSpawnPoint.position,
            Quaternion.identity
        );
        Rigidbody2D crb = cardBullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (Player.transform.position - CardBulletSpawnPoint.position).normalized;
        crb.linearVelocity = direction * 8f; // 속도 조정
        canShoot = false;
        StartCoroutine(ShootDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
    }

    void Big_CardAttack() { }

    void Ruined_Lazer()
    {
        StartCoroutine(LazerSequence());
    }

    IEnumerator LazerSequence()
    {
        // Warning 오브젝트 생성
        GameObject warning = Instantiate(
            LazerWarning,
            RuinLazerSpawnPoint.position,
            Quaternion.identity
        );
        Transform targetTransform = Player.transform; // 기본 타겟은 플레이어
        Debug.Log("조준시작");

        float warningDuration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < warningDuration)
        {
            // Muffin 태그 오브젝트 검색
            GameObject[] muffins = GameObject.FindGameObjectsWithTag("Muffin");
            if (muffins.Length > 0)
            {
                // 가장 가까운 머핀 찾기
                float minDistance = float.MaxValue;
                GameObject nearestMuffin = null;

                foreach (GameObject muffin in muffins)
                {
                    float distance = Vector2.Distance(
                        RuinLazerSpawnPoint.position,
                        muffin.transform.position
                    );
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestMuffin = muffin;
                    }
                }

                if (nearestMuffin != null)
                {
                    targetTransform = nearestMuffin.transform;
                }
            }

            // Warning 오브젝트 방향 업데이트
            if (warning != null)
            {
                Vector2 direction = (
                    RuinLazerSpawnPoint.position - targetTransform.position
                ).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                warning.transform.rotation = Quaternion.Euler(0, 0, angle);

                // 자식 오브젝트의 크기와 위치 조절
                Transform warningEffect = warning.transform.GetChild(0);
                if (warningEffect != null)
                {
                    float distance = Vector2.Distance(
                        RuinLazerSpawnPoint.position,
                        targetTransform.position
                    );
                    Vector3 effectScale = warningEffect.localScale;
                    effectScale.x = distance;
                    warningEffect.localScale = effectScale;

                    // 로컬 포지션 조절
                    Vector3 effectPosition = warningEffect.localPosition;
                    effectPosition.x = -distance / 2f; // 왼쪽 방향으로 절반 거리만큼 이동
                    warningEffect.localPosition = effectPosition;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Warning 오브젝트 제거
        if (warning != null)
        {
            Destroy(warning);
        }

        // 실제 레이저 발사
        GameObject ruinLazer = Instantiate(
            RuinLazer,
            RuinLazerSpawnPoint.position,
            Quaternion.identity
        );
        //LSM_RuinLazer ruinLazerScript = ruinLazer.GetComponent<LSM_RuinLazer>();
        Vector2 finalDirection = (
            targetTransform.position - RuinLazerSpawnPoint.position
        ).normalized;
        Debug.Log("발사");
        //ruinLazerScript.SetDirection(finalDirection);

        // 3초 후 레이저 삭제
        yield return new WaitForSeconds(2f);
        Destroy(ruinLazer);
    }
}
