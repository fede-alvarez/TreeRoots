using UnityEngine;

public class FruitResource : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
        EventManager.OnFruitCollected();
        Destroy(gameObject);
    }
}
