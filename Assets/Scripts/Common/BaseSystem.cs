using UnityEngine;

public abstract class BaseSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { private set; get; }

    public virtual void InitSystem()
    {
        Instance = this as T;
    }
}