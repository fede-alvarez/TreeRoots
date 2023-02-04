using UnityEngine;
using DG.Tweening;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private Transform _trajectoryPivot;

    [Header("Trajectory properties")]
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _maxRotationAngle = 50f;
    private PlayerController _controller;
    
    private float _horizontalMovement;
    private int _fruits = 0;
    private float _rotation = 0;
    private bool _shootMode = false;

    private void Awake() 
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Start() 
    {
        EventManager.FruitCollected += OnFruitCollected;  
        _trajectoryLine.gameObject.SetActive(false);
    }

    private void Update() 
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        bool eKeyPressed = Input.GetKeyDown(KeyCode.E);

        if (!_shootMode && eKeyPressed)
            Shoot();

        CalculateTrajectory();
    }

    private void CalculateTrajectory()
    {
        if (!_shootMode) return;

        if (_horizontalMovement > 0)
            _rotation += _rotationSpeed * Time.deltaTime;
        else if (_horizontalMovement < 0)
            _rotation -= _rotationSpeed * Time.deltaTime;

        _rotation = Mathf.Clamp(_rotation, -_maxRotationAngle, _maxRotationAngle);
        _trajectoryPivot.transform.rotation = Quaternion.Euler(0,0, _rotation);
    }

    private void Shoot()
    {
        print("Shoot Mode");
        _shootMode = true;
        _controller.DisableMovement = true;
        _trajectoryLine.gameObject.SetActive(true);
    }

    private void NormalMode()
    {
        print("Normal Mode");
        _shootMode = false;
        _controller.DisableMovement = false;
        _trajectoryLine.gameObject.SetActive(false);
    }

    private void OnFruitCollected()
    {
        _fruits += 1;
    }

    private void OnDestroy() 
    {
        EventManager.FruitCollected -= OnFruitCollected;
    }
}
