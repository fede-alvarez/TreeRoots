using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    private readonly float secondsBetweenEnemies = 0.7F;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private Transform EnemySpawnPoints;

    [SerializeField]
    private EnemyWavesScriptable EnemyWaves;

    private ObjectPool<GameObject> Enemies;

    void Start()
    {
        Enemies = new ObjectPool<GameObject>(InstantiateEnemy);
        StartCoroutine("SpawnEnemyWaves");
    }

    GameObject InstantiateEnemy()
    {
        return Instantiate(Enemy, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnEnemyWaves()
    {
        foreach (var wave in EnemyWaves.Waves)
        {
            for (int i = 0; i < wave.EnemiesFromSpawnPoints.Length; i += 1)
            {
                for (int j = 0; j < wave.EnemiesFromSpawnPoints[i]; j += 1)
                {
                    GameObject enemy = Enemies.Get();
                    enemy.transform.position = EnemySpawnPoints.GetChild(i).position;
                    enemy.SetActive(true);
                    yield return new WaitForSeconds(secondsBetweenEnemies);
                }
            }
            yield return new WaitForSeconds(wave.WaitForNextWave);
        }
    }
}
