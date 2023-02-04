using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance;
    public Animator _transition;

    void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void GoNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoLevelIndex(int idx)
    {
        StartCoroutine(LoadLevel(idx));
    }

    public void GoMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void ResetLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        _transition.SetTrigger("Start"); 
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    public static LevelLoader GetInstance 
    {
        get { return _instance; }
    }
}
