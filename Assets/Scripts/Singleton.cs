using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    GameObject obj = new(typeof(T).Name);

                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Debug.LogWarning($"Duplicate Singleton<{typeof(T).Name}> 감지 - 파괴 대상: {gameObject.name}, 원본: {_instance.gameObject.name}");
            Destroy(gameObject);
        }
    }
}
