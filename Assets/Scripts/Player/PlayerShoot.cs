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

    [SerializeField] private GameObject _handsFilled;
    private PlayerController _controller;

    
    private float _horizontalMovement;
    private int _fruits = 0;
    private float _rotation = 0;
    private bool _shootMode = false;
    private bool _canShoot = false;
    private bool _actionPressed = false;
    private bool _isDelayed = false;

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
        
        if (_isDelayed && _canShoot && _fruits > 0 && _actionPressed)
        {
            _canShoot = false;
            _isDelayed = false;
            Shoot();
            NormalMode();
        }

        if (!_canShoot) return;
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
        
        Quaternion finalRotation = Quaternion.Slerp(_trajectoryPivot.transform.rotation, Quaternion.Euler(0,0, _rotation), _rotationSpeed * Time.deltaTime);
        _trajectoryPivot.transform.rotation = finalRotation;
    }

    public void Shoot()
    {
        if (_fruits <= 0) return;

        Transform bullet = Instantiate(_fruitBulletPrefab, transform.position, Quaternion.identity);
        if (!bullet) return;
        if (bullet.TryGetComponent(out FruitBullet bulletFruit))
        {
            AudioManager.GetInstance.PlaySound(AudioManager.AudioList.ThrowFruite);
            bulletFruit.Shoot(_trajectoryPivot.transform.rotation);
            _fruits -= 1;
            ResetRotation();
        }
    }

    private void ResetRotation()
    {
        _rotation = 0;
        _trajectoryPivot.transform.rotation = Quaternion.Euler(0,0,0); 
    }

    private void ShootMode()
    {
        _handsFilled.SetActive(true);
        _shootMode = true;
        _controller.DisableMovement = true;
        _trajectoryLine.gameObject.SetActive(true);

        Invoke("SetDelay", 0.5f);
    }

    private void SetDelay()
    {
        _isDelayed = true;
    }

    private void NormalMode()
    {
        _handsFilled.SetActive(false);
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
        set {
            if (value) ShootMode();
            _canShoot = value;
        }
    }

    private void OnDestroy() 
    {
        EventManager.FruitCollected -= OnFruitCollected;
    }
}
