using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int enemyKillScore = 100;

    [Header("Fruit Resources")]
    [SerializeField]
    private Transform _resourcesSpawnPoints;

    [SerializeField]
    private FruitResource _fruitPrefab;

    [Header("Player")]
    [SerializeField]
    private PlayerController _playerController;

    [Header("Elevators")]
    [SerializeField]
    private float _elevatorsTimer = 5.0f;

    [SerializeField]
    private Elevator _elevatorLeft;

    [SerializeField]
    private Elevator _elevatorRight;

    private int _spawnPointsAmount;

    private int _prevRandIndex = 0;

    private int score = 0;

    private static GameManager _instance;

    [SerializeField]
    private TMP_Text scoreText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        HideResourcesSpawn();
        GenerateRandomResources();

        EventManager.GameOver += OnGameOver;
        EventManager.EnemyDied += OnEnemyDied;

        InvokeRepeating("StartElevators", 4.0f, _elevatorsTimer);
        InvokeRepeating("IncrementScore", 2, 2);
    }

    private void StartElevators()
    {
        _elevatorLeft.Move();
        _elevatorRight.Move();
    }

    private void IncrementScore()
    {
        UpdateScore(score + 10);
    }

    private void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = newScore.ToString().PadLeft(8, '0');
    }

    private void HideResourcesSpawn()
    {
        _spawnPointsAmount = _resourcesSpawnPoints.childCount;

        foreach (Transform spawn in _resourcesSpawnPoints)
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
                FruitResource fruit =
                    Instantiate(_fruitPrefab,
                    child.position,
                    Quaternion.identity);
                if (fruit != null) trigger.SetFruit(fruit);
            }
        }
    }

    private void OnEnemyDied(SimpleEnemy _)
    {
        UpdateScore(score + enemyKillScore);
    }

    private void OnGameOver()
    {
        CancelInvoke("StartElevators");
        CancelInvoke("IncrementScore");
    }

    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
        EventManager.EnemyDied -= OnEnemyDied;
        EventManager.GameOver -= OnGameOver;
    }

    public PlayerController GetPlayer => _playerController;

    public static GameManager GetInstance => _instance;
}
