using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    private readonly float secondsBetweenEnemies = 0.7F;

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
        StartCoroutine("SpawnEnemyWaves");
    }

    Func<T> InstantiatePrefab<T>(T prefab) where T : Component
    {
        return () => Instantiate(prefab, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnEnemyWaves()
    {
        foreach (var wave in EnemyWaves.Waves)
        {
            for (int i = 0; i < wave.EnemiesFromSpawnPoints.Length; i += 1)
            {
                for (int j = 0; j < wave.EnemiesFromSpawnPoints[i]; j += 1)
                {
                    enemies.Get().ConfigureFor(EnemySpawnPoints.GetChild(i).position);
                    yield return new WaitForSeconds(UnityEngine.Random.Range(wave.MinWaitBetweenEnemies, wave.MaxWaitBetweenEnemies));
                }
            }
            yield return new WaitForSeconds(wave.WaitForNextWave);
        }
    }

    void OnEnemyDied(SimpleEnemy enemy)
    {
        waters.Get().ConfigureFor(enemy.transform.position);
        enemy.gameObject.SetActive(false);
        enemies.Release (enemy);
    }
}
