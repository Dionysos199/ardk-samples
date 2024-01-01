using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    private static SceneManagerSingleton _instance;

    public static SceneManagerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneManagerSingleton>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneManagerSingleton");
                    _instance = singletonObject.AddComponent<SceneManagerSingleton>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
