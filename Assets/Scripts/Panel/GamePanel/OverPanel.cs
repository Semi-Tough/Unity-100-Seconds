using UnityEngine;

public class OverPanel : BasePanel
{
    public UILabel LabTime;
    public UIButton BtnOk;
    public UIInput InputName;


    public override void ShowPanel()
    {
        base.ShowPanel();
        Time.timeScale = 0;
        LabTime.text = "You Lasted " + GameSystem.Instance.GamePanel.Time.ToString("0.00") + " Seconds";
    }

    #region UIEvents

    public void BtnOkEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        DataService.Instance.AddRankData(new RankInfo(InputName.value, GameSystem.Instance.GamePanel.Time));
        HidePanel();
        GameSystem.Instance.GamePanel.HidePanel();
        StartSystem.Instance.InitStartScene();
        Time.timeScale = 1;
    }

    #endregion
}