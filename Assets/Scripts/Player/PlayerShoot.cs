using UnityEngine;
using DG.Tweening;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private Transform _trajectoryPivot;

    [Header("Trajectory properties")]
    [SerializeField] private Transform _fruitBulletPrefab;
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _maxRotationAngle = 50f;
    private PlayerController _controller;
    
    private float _horizontalMovement;
    private int _fruits = 0;
    private float _rotation = 0;
    private bool _shootMode = false;
    private bool _canShoot = false;
    private bool _actionPressed = false;

    private PlayerInput _input;

    private void Awake() 
    {
        _controller = GetComponent<PlayerController>();
        _input = GetComponent<PlayerInput>();
    }

    private void Start() 
    {
        EventManager.FruitCollected += OnFruitCollected;  
        _trajectoryLine.gameObject.SetActive(false);
    }

    private void Update() 
    {
        _horizontalMovement = _input.GetMovement;
        _actionPressed = _input.GetInteraction;
        
        if (!_canShoot) return;

        if (!_shootMode && _actionPressed)
            ShootMode();

        if (_fruits > 0 && _shootMode && _actionPressed)
            Shoot();

        CalculateTrajectory();
    }

    private void CalculateTrajectory()
    {
        if (!_shootMode) return;

        bool isTopPlayer = _controller.GetPlayerType == PlayerController.PlayerType.TopTree;

        if (_horizontalMovement > 0)
            _rotation += _rotationSpeed * Time.deltaTime;
        else if (_horizontalMovement < 0)
            _rotation -= _rotationSpeed * Time.deltaTime;

        _rotation = Mathf.Clamp(_rotation, -_maxRotationAngle, _maxRotationAngle);

        if (!isTopPlayer)
        {
            _trajectoryPivot.transform.rotation = Quaternion.Euler(0,0, -_rotation);    
        }else{
            _trajectoryPivot.transform.rotation = Quaternion.Euler(0,0, _rotation);
        }
    }

    private void Shoot()
    {
        if (_fruits <= 0) return;
        
        Transform bullet = Instantiate(_fruitBulletPrefab, transform.position, Quaternion.identity);
        if (!bullet) return;
        if (bullet.TryGetComponent(out FruitBullet bulletFruit))
        {
            bulletFruit.Shoot(_trajectoryPivot.transform.rotation);
            _fruits -= 1;
        }
    }

    private void ShootMode()
    {
        _shootMode = true;
        _controller.DisableMovement = true;
        _trajectoryLine.gameObject.SetActive(true);
    }

    private void NormalMode()
    {
        _shootMode = false;
        _controller.DisableMovement = false;
        _trajectoryLine.gameObject.SetActive(false);
    }

    private void OnFruitCollected()
    {
        _fruits += 1;
    }

    public bool CanShoot
    {
        get { return _canShoot; }
        set { _canShoot = value; }
    }

    private void OnDestroy() 
    {
        EventManager.FruitCollected -= OnFruitCollected;
    }
}
