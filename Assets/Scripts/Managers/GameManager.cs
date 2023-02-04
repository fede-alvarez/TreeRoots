using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fruit Resources")]
    [SerializeField] private Transform _resourcesSpawnPoints;
    [SerializeField] private FruitResource _fruitPrefab;

    private int _spawnPointsAmount;

    private static GameManager _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }

    private void Start() 
    {
        HideResourcesSpawn();
    }

    private void HideResourcesSpawn()
    {
        _spawnPointsAmount = _resourcesSpawnPoints.childCount;

        foreach(Transform spawn in _resourcesSpawnPoints)
        {
            if (spawn.TryGetComponent(out SpriteRenderer render))
                render.enabled = false;
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, _resourcesSpawnPoints.childCount);
            Transform randomPoint = _resourcesSpawnPoints.GetChild(randomIndex);
            if (randomPoint.TryGetComponent(out FruitTrigger trigger))
            {
                FruitResource fruit = Instantiate(_fruitPrefab, randomPoint.position, Quaternion.identity);
                if (fruit != null)
                    trigger.SetFruit(fruit);
            }
        }
    }
    
    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
    }
    
    public static GameManager GetInstance => _instance;
}
