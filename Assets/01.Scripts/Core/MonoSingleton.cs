using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
            }

            if (_instance == null)
            {
                Debug.LogError($"Can't Find Instance!!");
            }
            return _instance;
        }
    }

    public abstract void Init();
}
