using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fruit Resources")]
    [SerializeField] private Transform _resourcesSpawnPoints;
    [SerializeField] private FruitResource _fruitPrefab;

    [Header("Player")]
    [SerializeField] private PlayerController _playerController;

    [Header("Elevators")]
    [SerializeField] private float _elevatorsTimer = 5.0f;
    [SerializeField] private Elevator _elevatorLeft;
    [SerializeField] private Elevator _elevatorRight;

    private int _spawnPointsAmount;
    private int _prevRandIndex = 0;

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
        GenerateRandomResources();

        InvokeRepeating("StartElevators", 4.0f, _elevatorsTimer);
    }

    private void StartElevators()
    {
        _elevatorLeft.Move();
        _elevatorRight.Move();
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

    private void GenerateRandomResources()
    {
        for (int i = 0; i < _resourcesSpawnPoints.childCount; i++)
        {
            Transform child = _resourcesSpawnPoints.GetChild(i);
            if (child.TryGetComponent(out FruitTrigger trigger))
            {
                FruitResource fruit = Instantiate(_fruitPrefab, child.position, Quaternion.identity);
                if (fruit != null)
                    trigger.SetFruit(fruit);
            }
        }
    }
    
    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
    }
    
    public PlayerController GetPlayer => _playerController;
    public static GameManager GetInstance => _instance;
}
