using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ascensorScript : MonoBehaviour
{
    public Transform upTarget;
    public Transform downTarget;
    public bool isGoingUp = true;
    //usar bool

    void Start()
    {


    }

    void Update()
    {
        if(isGoingUp){
            transform.DOMove(upTarget.position, 2).SetEase(Ease.Linear).OnComplete(() => {isGoingUp = false;});
        }else{
            transform.DOMove(downTarget.position, 2).SetEase(Ease.Linear).OnComplete(() => {isGoingUp = true;});
        }
        //Debug.Log("target position: " + target.position + "/ up position: " + upTarget.position);
    }
}
