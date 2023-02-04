using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class elevator : MonoBehaviour
{
    public Transform otherElevator; //usar esto para que se muevan asincronicamente //usar gamecomponet
    public Transform upTargetLeft;
    public Transform downTargetRight;
    public Transform upTargetRight;
    public Transform downTargetLeft;
    private Transform playerParent;
    public bool isGoingUp = true;

    private void OnTriggerEnter2D(Collider2D playerCollider){ //el movimiento tiene que salir del evento OnTrigger (y pasar a ser por timer), las referencias al jugador se tienen que actualizar
        playerParent = playerCollider.transform.parent;
        playerCollider.transform.SetParent(transform);

        playerCollider.GetComponent<PlayerController>().enabled = false;
        playerCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        playerCollider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(isGoingUp){
            transform.DOMove(upTargetLeft.position, 2)
            .SetEase(Ease.Linear)
            .OnComplete(() =>{
                isGoingUp = false;
                playerCollider.transform.SetParent(playerParent);
                playerCollider.GetComponent<PlayerController>().enabled = true;
                playerCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            });
            otherElevator.transform.DOMove(downTargetRight.position, 2)
            .SetEase(Ease.Linear);
        }else{
            transform.DOMove(downTargetLeft.position, 2)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                isGoingUp = true;
                playerCollider.transform.SetParent(playerParent);
                playerCollider.GetComponent<PlayerController>().enabled = true;
                playerCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            });
            otherElevator.transform.DOMove(upTargetRight.position, 2)
            .SetEase(Ease.Linear);
        }
    }
}
