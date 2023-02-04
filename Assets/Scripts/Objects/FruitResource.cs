using UnityEngine;

public class FruitResource : MonoBehaviour
{
    public void Remove()
    {
        EventManager.OnFruitCollected();
        Destroy(gameObject);
    }
}
