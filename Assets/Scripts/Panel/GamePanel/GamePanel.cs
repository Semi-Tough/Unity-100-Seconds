using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel
{
    public UIButton BtnBack;
    public UILabel LabTime;
    public List<GameObject> HpList;
    public List<GameObject> MaxHpList;

    public float Time { private set; get; }

    public override void ShowPanel()
    {
        base.ShowPanel();
        Time = 0f;
    }

    public void ChangeMaxHp(int maxHp)
    {
        for (int i = 0; i < HpList.Count; i++)
        {
            MaxHpList[i].SetActive(i < maxHp);
        }

        ChangeHp(maxHp);
    }

    public void ChangeHp(int hp)
    {
        for (int i = 0; i < HpList.Count; i++)
        {
            HpList[i].SetActive(i < hp);
        }
    }

    private void Update()
    {
        Time += UnityEngine.Time.deltaTime;
        LabTime.text = "Time: " + Time.ToString("0.0") + " S";
    }

    #region UIEvents

    public void BtnBackEvent()
    {
        GameSystem.Instance.QuitPanel.ShowPanel();
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        UnityEngine.Time.timeScale = 0;
    }

    #endregion
}