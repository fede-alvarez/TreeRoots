using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }
    
    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
    }
    
    public static GameManager GetInstance => _instance;
}
