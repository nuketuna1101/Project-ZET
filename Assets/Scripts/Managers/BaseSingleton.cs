using UnityEngine;

public abstract class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"[Singleton] {typeof(T).Name} Instance NOT initialized yet");
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogError($"[Singleton] {typeof(T).Name} Instance already exists.");
            Destroy(gameObject);
            return;
        }
    }
}
