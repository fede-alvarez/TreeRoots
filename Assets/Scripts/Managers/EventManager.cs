using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction FruitCollected;
    public static void OnFruitCollected() => FruitCollected?.Invoke();
}