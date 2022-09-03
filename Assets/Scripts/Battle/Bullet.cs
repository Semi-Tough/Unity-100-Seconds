using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletInfo _bulletInfo;
    private float _time;

    public void InitBullet(BulletInfo bulletInfo)
    {
        _time = 0;
        _bulletInfo = bulletInfo;
        Invoke(nameof(ReleaseBullet), _bulletInfo.LifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_bulletInfo.ForwardSpeed * Time.deltaTime));
        switch (_bulletInfo.Type)
        {
            case 1:
                //直线运动
                break;
            case 2:
                //曲线运动
                _time += Time.deltaTime;
                transform.Translate(Vector3.right * (Mathf.Sin(_time * _bulletInfo.RotateSpeed) * _bulletInfo.RightSpeed * Time.deltaTime));
                break;
            case 3:
                //右抛物线
                transform.rotation *= Quaternion.AngleAxis(_bulletInfo.RotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                //左抛物线
                transform.rotation *= Quaternion.AngleAxis(-_bulletInfo.RotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                //跟踪
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(PlayerController.Instance.transform.position - transform.position),
                    _bulletInfo.RotateSpeed * Time.deltaTime);
                break;
        }
    }

    public void ReleaseBullet()
    {
        PoolService.Instance.Release(gameObject);
        GameObject fx = PoolService.Instance.Get(_bulletInfo.EffectPath);
        fx.transform.position = transform.position;
        CancelInvoke(nameof(ReleaseBullet));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.Hurt();
            ReleaseBullet();
        }
    }
}