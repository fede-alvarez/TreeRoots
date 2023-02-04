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

    void Start()
    {
        enemies = new ObjectPool<SimpleEnemy>(InstantiateEnemy);
        EventManager.EnemyDied += OnEnemyDied;
        StartCoroutine("SpawnEnemyWaves");
    }

    SimpleEnemy InstantiateEnemy()
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
                    var enemy = enemies.Get();
                    enemy.ConfigureFor(EnemySpawnPoints.GetChild(i).position);
                    yield return new WaitForSeconds(secondsBetweenEnemies);
                }
            }
            yield return new WaitForSeconds(wave.WaitForNextWave);
        }
    }

    void OnEnemyDied(SimpleEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemies.Release(enemy);
    }
}
