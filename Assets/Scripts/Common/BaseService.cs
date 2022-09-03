using UnityEngine;

public class BaseService<T> : MonoBehaviour where T : class
{
    public static T Instance { private set; get; }

    public virtual void InitService()
    {
        Instance = this as T;
    }
}