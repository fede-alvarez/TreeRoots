using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    [Header("Elevator")]
    public Elevator otherElevator;

    [Header("Targets")]
    public Transform upTarget;
    public Transform downTarget;

    [Header("Direction")]
    public bool isGoingUp = true;

    [Header("Required")]
    public Transform playerParent;

    [Header("UI")]
    [SerializeField] private CanvasGroup _uiTextGroup;

    private PlayerController _controller;
    private bool _isMoving = false;
    private bool _isIn = false;
    
    private void Update()
    {
        if (_controller == null || !_isIn) return;
        if (!_controller.GetInput.GetInteract) return;
        _controller.GetInput.ResetInteract();
        
        _controller.DisablePhysics(transform);
        _controller.transform.localPosition = new Vector3(0,-0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isMoving || !other.CompareTag("Player")) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            _controller = player;
            _isIn = true;
            _uiTextGroup.DOFade(1, 0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (_isMoving || !other.CompareTag("Player") || _isIn) return;
        if (other.TryGetComponent(out PlayerController player))
        {
            _controller = player;
            _isIn = true;
            _uiTextGroup.DOFade(1, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isMoving || !other.CompareTag("Player")) return;

        if (_controller != null) _controller = null;
        _isIn = false;
        _uiTextGroup.DOFade(0, 0.5f);
    }

    public void Move()
    {
        _isMoving = true;
        _uiTextGroup.DOFade(0, 0.5f);

        transform.DOMove( (isGoingUp) ? upTarget.position : downTarget.position, 2.0f)
                 .SetEase(Ease.Linear)
                 .OnComplete(() => 
                 {
                    isGoingUp = !isGoingUp;
                    
                    _isMoving = false;
                    _isIn = false;
                    
                    if (_controller != null)
                    {
                        _controller.EnablePhysics(playerParent);
                        _controller = null;
                    }
                 });
    }
}
