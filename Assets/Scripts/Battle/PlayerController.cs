using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }
    private int _maxHp;
    private int _nowHp;

    private int _moveSpeed;
    private float _roteSpeed;

    private float _h;
    private float _v;
    private Vector3 _lastPos;
    private Vector3 _nowPos;
    private Quaternion _targetQuaternion;
    private Camera _camera;
    private bool _isDead;

    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;
    }

    public void InitPlane(PlaneInfo planeInfo)
    {
        _maxHp = planeInfo.Hp;
        _nowHp = _maxHp;
        _moveSpeed = planeInfo.Speed * 8;
        _roteSpeed = 10;
        Instantiate(ResourceService.Instance.Load<GameObject>(planeInfo.ResPath), transform);
    }

    public void Hurt()
    {
        if (_isDead) return;
        _nowHp -= 1;
        GameSystem.Instance.GamePanel.ChangeHp(_nowHp);
        if (_nowHp <= 0) Dead();
    }

    private void Dead()
    {
        _isDead = true;
        GameSystem.Instance.OverPanel.ShowPanel();
    }

    private void Update()
    {
        if (_isDead) return;
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_h == 0)
        {
            _targetQuaternion = Quaternion.identity;
        }
        else
        {
            _targetQuaternion = _h < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetQuaternion, _roteSpeed * Time.deltaTime);

        _lastPos = transform.position;

        transform.Translate(Vector3.forward * (_moveSpeed * _v * Time.deltaTime));
        transform.Translate(Vector3.right * (_moveSpeed * _h * Time.deltaTime), Space.World);

        _nowPos = _camera.WorldToScreenPoint(transform.position);


        if (_nowPos.x <= 0 || _nowPos.x >= Screen.width)
        {
            transform.position = new Vector3(_lastPos.x, transform.position.y, transform.position.z);
        }

        if (_nowPos.y <= 0 || _nowPos.y >= Screen.height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _lastPos.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit, 1000, 1 << LayerMask.NameToLayer("Bullet")))
            {
                raycastHit.transform.GetComponent<Bullet>().ReleaseBullet();
            }
        }
    }
}