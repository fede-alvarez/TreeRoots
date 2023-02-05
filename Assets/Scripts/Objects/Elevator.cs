using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    public Elevator otherElevator; //usar esto para que se muevan asincronicamente //usar gamecomponet

    public Transform upTarget;
    public Transform downTarget;
    public bool isGoingUp = true;

    public Transform playerParent;
    private bool _isMoving = false;
    private bool _isIn = false;
    private PlayerController _controller;

    private void Update() 
    {
        if (_controller == null) return;

        if (_controller.InteractionPressed && _isIn)
        {
            print("Esto se ejecuta mucho?");
            _controller.DisablePhysics(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isMoving || !other.CompareTag("Player")) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            _controller = player;
            _isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isMoving || !other.CompareTag("Player")) return;

        if (_controller != null) _controller = null;
        _isIn = false;
    }

    public void Move()
    {
        _isMoving = true;

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
