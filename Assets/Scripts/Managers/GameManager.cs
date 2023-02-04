using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fruit Resources")]
    [SerializeField] private Transform _resourcesSpawnPoints;
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
    }
    
    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
    }
    
    public static GameManager GetInstance => _instance;
}
