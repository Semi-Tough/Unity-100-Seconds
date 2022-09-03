using UnityEngine;

public class StartPanel : BasePanel
{
    public UIButton BtnStart;
    public UIButton BtnRank;
    public UIButton BtnSetting;
    public UIButton BtnQuit;

    #region UIEvents

    public void BtnStartEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        StartSystem.Instance.ChoosePanel.ShowPanel();
        HidePanel();
    }

    public void BtnRankEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        StartSystem.Instance.RankPanel.ShowPanel();
    }

    public void BtnSettingEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        StartSystem.Instance.SettingPanel.ShowPanel();
    }

    public void BtnQuitEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        Application.Quit();
    }

    #endregion
}