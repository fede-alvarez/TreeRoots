using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Vector3 targetPosition;

    private readonly float speed = 4.5f;

    public void ConfigureFor(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        MoveToBottomFloor();
    }

    private void MoveToBottomFloor()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down,
                    Mathf.Infinity, LayerMask.GetMask("BottomFloor"));
        if (hit) targetPosition = hit.point;
    }

    private void Update() 
    {
        if (transform.position != targetPosition)
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EventManager.OnWaterCollected(other.gameObject.GetComponent<PlayerController>(), this);
    }
}
