using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private SimpleEnemy Enemy;

    [SerializeField]
    private Transform EnemySpawnPoints;

    [SerializeField]
    private EnemyWavesScriptable EnemyWaves;

    private ObjectPool<SimpleEnemy> enemies;

    [SerializeField]
    private Water Water;

    private ObjectPool<Water> waters;

    void Start()
    {
        enemies = new ObjectPool<SimpleEnemy>(InstantiatePrefab<SimpleEnemy>(Enemy));
        waters = new ObjectPool<Water>(InstantiatePrefab<Water>(Water));
        EventManager.EnemyDied += OnEnemyDied;
        EventManager.WaterCollected += OnWaterCollected;
        StartCoroutine("SpawnEnemyWaves");
    }

    Func<T> InstantiatePrefab<T>(T prefab) where T : Component
    {
        return () => Instantiate(prefab, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(7);

        foreach (var wave in EnemyWaves.Waves)
        {
            for (int i = 0; i < wave.EnemiesFromSpawnPoints.Length; i += 1)
            {
                for (int j = 0; j < wave.EnemiesFromSpawnPoints[i]; j += 1)
                {
                    enemies.Get().ConfigureFor(EnemySpawnPoints.GetChild(i).position);

                    float randomWait = UnityEngine.Random.Range(wave.MinWaitBetweenEnemies, wave.MaxWaitBetweenEnemies);
                    yield return new WaitForSeconds(randomWait);
                }
            }
            yield return new WaitForSeconds(wave.WaitForNextWave);
            //print("Waited for Next Wave => " + wave.WaitForNextWave.ToString());
        }
    }

    void OnEnemyDied(SimpleEnemy enemy)
    {
        waters.Get().ConfigureFor(enemy.transform.position);
        enemy.gameObject.SetActive(false);
        enemies.Release (enemy);
    }

    void OnWaterCollected(PlayerController player, Water water)
    {
        water.gameObject.SetActive(false);
        waters.Release (water);
    }
}
