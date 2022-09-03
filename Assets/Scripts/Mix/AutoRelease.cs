using UnityEngine;

public class AutoRelease : MonoBehaviour
{
    public float DelayTime;

    private void OnEnable()
    {
        Invoke(nameof(Release), DelayTime);
    }

    private void Release()
    {
        PoolService.Instance.Release(gameObject);
    }
}