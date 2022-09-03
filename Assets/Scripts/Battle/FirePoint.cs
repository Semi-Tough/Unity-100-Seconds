using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public enum EScreenPoint
    {
        LeftUp,
        LeftCenter,
        LeftBottom,
        CenterUp,
        CenterBottom,
        RightUp,
        RightCenter,
        RightBottom,
    }

    public EScreenPoint ScreenPoint;
    private Vector3 _screenPos;
    private Vector3 _defaultDir;
    private Camera _camera;

    private List<FirePointInfo> _firePointInfos;
    private FirePointInfo _firePointInfo;
    private BulletInfo _bulletInfo;

    private int _nowNum;
    private float _nowCd;
    private float _nowDelay;

    private float _changeAngle;

    private void Start()
    {
        _camera = Camera.main;
        SetFirePointPos();
        StartCoroutine(FireCoroutine());
    }

    private void SetFirePointPos()
    {
        _screenPos.z = 200;
        switch (ScreenPoint)
        {
            case EScreenPoint.LeftUp:
                _screenPos.x = 0;
                _screenPos.y = Screen.height;
                _defaultDir = Vector3.right;
                break;
            case EScreenPoint.LeftCenter:
                _screenPos.x = 0;
                _screenPos.y = (float)Screen.height / 2;
                _defaultDir = Vector3.forward;
                break;
            case EScreenPoint.LeftBottom:
                _screenPos.x = 0;
                _screenPos.y = 0;
                _defaultDir = Vector3.forward;
                break;
            case EScreenPoint.CenterUp:
                _screenPos.x = (float)Screen.width / 2;
                _screenPos.y = Screen.height;
                _defaultDir = Vector3.right;
                break;
            case EScreenPoint.CenterBottom:
                _screenPos.x = (float)Screen.width / 2;
                _screenPos.y = 0;
                _defaultDir = Vector3.left;
                break;
            case EScreenPoint.RightUp:
                _screenPos.x = Screen.width;
                _screenPos.y = Screen.height;
                _defaultDir = Vector3.back;
                break;
            case EScreenPoint.RightCenter:
                _screenPos.x = Screen.width;
                _screenPos.y = (float)Screen.height / 2;
                _defaultDir = Vector3.back;
                break;
            case EScreenPoint.RightBottom:
                _screenPos.x = Screen.width;
                _screenPos.y = 0;
                _defaultDir = Vector3.left;
                break;
        }

        transform.position = _camera.ScreenToWorldPoint(_screenPos);
    }

    private void SetFireInfo()
    {
        _firePointInfos = DataService.Instance.FirePointData.FirePointInfos;
        _firePointInfo = _firePointInfos[Random.Range(0, _firePointInfos.Count)];
        _nowNum = _firePointInfo.Num;
        _nowCd = _firePointInfo.Cd;
        _nowDelay = _firePointInfo.Delay;

        string[] strings = _firePointInfo.Ids.Split(',');
        int beginId = int.Parse(strings[0]);
        int endId = int.Parse(strings[1]);

        _bulletInfo = DataService.Instance.BulletData.BulletInfos[Random.Range(beginId, endId + 1)];
        if (_firePointInfo.Type == 2)
        {
            switch (ScreenPoint)
            {
                case EScreenPoint.LeftCenter:
                case EScreenPoint.CenterUp:
                case EScreenPoint.CenterBottom:
                case EScreenPoint.RightCenter:
                    _changeAngle = 180f / (_nowNum + 1);
                    break;
                case EScreenPoint.LeftUp:
                case EScreenPoint.LeftBottom:
                case EScreenPoint.RightUp:
                case EScreenPoint.RightBottom:
                    _changeAngle = 90f / (_nowNum + 1);
                    break;
            }
        }
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            SetFireInfo();
            yield return new WaitForSeconds(_nowCd);

            switch (_firePointInfo.Type)
            {
                case 1:
                {
                    _nowNum--;
                    GameObject bullet = PoolService.Instance.Get(_bulletInfo.BulletPath);
                    bullet.GetComponent<Bullet>().InitBullet(_bulletInfo);
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = Quaternion.LookRotation(PlayerController.Instance.transform.position - transform.position);
                    if (_nowNum <= 0)
                    {
                        yield return new WaitForSeconds(_nowDelay);
                        SetFireInfo();
                    }

                    _nowCd = _firePointInfo.Cd;
                    break;
                }
                case 2:
                {
                    if (_nowCd == 0)
                    {
                        for (int i = 0; i < _firePointInfo.Num; i++)
                        {
                            _nowNum--;
                            GameObject bullet = PoolService.Instance.Get(_bulletInfo.BulletPath);
                            bullet.GetComponent<Bullet>().InitBullet(_bulletInfo);
                            bullet.transform.position = transform.position;
                            bullet.transform.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(_changeAngle * (i+1), Vector3.up) * _defaultDir);
                        }

                        yield return new WaitForSeconds(_nowDelay);
                        SetFireInfo();
                    }
                    else
                    {
                        _nowNum--;
                        GameObject bullet = PoolService.Instance.Get(_bulletInfo.BulletPath);
                        bullet.GetComponent<Bullet>().InitBullet(_bulletInfo);
                        bullet.transform.position = transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(_changeAngle * (_firePointInfo.Num - _nowNum+1), Vector3.up) * _defaultDir);
                        if (_nowNum <= 0)
                        {
                            yield return new WaitForSeconds(_nowDelay);
                            SetFireInfo();
                        }

                        _nowCd = _firePointInfo.Cd;
                    }
                }
                    break;
            }
        }
    }
}