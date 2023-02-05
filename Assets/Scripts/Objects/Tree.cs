using UnityEngine;

public class Tree : MonoBehaviour
{
    private int _health = 100;
    private bool _isDead = false;

    public void Damage(int amount)
    {
        if (_isDead) return;

        _health -= amount;

        if (_health <= 0)
        {
            EventManager.OnGameOver();
            _isDead = true;
        }
    }

    public int GetHealth => _health;
}
