using System.Collections.Generic;
using UnityEngine;

public class ChoosePanel : BasePanel
{
    public Camera UICamera;
    public UIButton BtnLeft;
    public UIButton BtnRight;
    public UIButton BtnStart;
    public UIButton BtnClose;

    public Transform PlanePos;

    public List<GameObject> HpList;
    public List<GameObject> SpeedList;
    public List<GameObject> VolumeList;

    private GameObject _currentPlane;
    private int _planeIndex;
    private float _time;
    private bool _select;

    public override void ShowPanel()
    {
        base.ShowPanel();
        _planeIndex = 0;
        ChangePlane();
    }

    private void ChangePlane()
    {
        GameSystem.Instance.PlaneIndex = _planeIndex;

        PlaneInfo planeInfo = DataService.Instance.GetNowPlaneData(_planeIndex);
        if (_currentPlane)
        {
            Destroy(_currentPlane);
            _currentPlane = null;
        }

        _currentPlane = Instantiate(ResourceService.Instance.Load<GameObject>(planeInfo.ResPath), PlanePos, false);
        _currentPlane.layer = LayerMask.NameToLayer("UI");
        _currentPlane.transform.localPosition = Vector3.zero;
        _currentPlane.transform.localRotation = Quaternion.identity;
        _currentPlane.transform.localScale = Vector3.one * planeInfo.LocalScale;

        for (int i = 0; i < 10; i++)
        {
            HpList[i].SetActive(i < planeInfo.Hp);
            SpeedList[i].SetActive(i < planeInfo.Speed);
            VolumeList[i].SetActive(i < planeInfo.Volume);
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;
        PlanePos.Translate(Vector3.up * (Mathf.Sin(_time) * 0.0001f), Space.World);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(UICamera.ScreenPointToRay(Input.mousePosition), 1000, 1 << LayerMask.NameToLayer("UI")))
            {
                _select = true;
            }
        }

        if (Input.GetMouseButton(0) && _select)
        {
            PlanePos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -10, Vector3.up);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _select = false;
        }
    }

    #region UIEvents

    public void BtnStartEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        HidePanel();
        GameSystem.Instance.InitGameScene();
    }

    public void BtnLeftEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        _planeIndex--;
        if (_planeIndex < 0)
        {
            _planeIndex = DataService.Instance.PlaneData.PlaneInfos.Count - 1;
        }


        ChangePlane();
    }

    public void BtnRightEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        _planeIndex++;
        if (_planeIndex > DataService.Instance.PlaneData.PlaneInfos.Count - 1)
        {
            _planeIndex = 0;
        }


        ChangePlane();
    }

    public void BtnCloseEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        StartSystem.Instance.StartPanel.ShowPanel();
        HidePanel();
    }

    #endregion
}