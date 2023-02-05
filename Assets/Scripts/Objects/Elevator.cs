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
    private PlayerController _controller;

    //el movimiento tiene que salir del evento OnTrigger (y pasar a ser por timer), las referencias al jugador se tienen que actualizar
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isMoving || !other.CompareTag("Player")) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            _controller = player;
            player.DisablePhysics(transform);
        }
    }

    public void Move()
    {
        _isMoving = true;

        transform.DOMove( (isGoingUp) ? upTarget.position : downTarget.position, 2.0f)
                 .SetEase(Ease.Linear)
                 .OnComplete(() => 
                 {
                    _isMoving = false;
                    isGoingUp = !isGoingUp;
                    _controller.EnablePhysics(playerParent);
                 });
    }
}
