using System.Collections.Generic;
using UnityEngine;

public class LYJ_SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;
    List<Transform> spawnPoints;
    float waveInterval;
    int currentWave;

    void Awake()
    {
        foreach (var item in enemies)
        {
            LYJ_PoolManager.Instance.CreatePool(item, 10);
        }
    }
    void Update()
    {
        if (LYJ_GameManager.Instance.CurrentStageTime >= waveInterval)
        {
            StartWave();
        }
    }

    void StartWave()
    {

        currentWave++;
    }
}
