using UnityEngine;

public class QuitPanel : BasePanel
{
    public UIButton BtnSure;
    public UIButton BtnBack;

    #region UIEVents

    public void BtnSureEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        HidePanel();
        GameSystem.Instance.GamePanel.HidePanel();
        StartSystem.Instance.InitStartScene();
        Time.timeScale = 1;
    }

    public void BtnBackEvent()
    {
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
        HidePanel();
        Time.timeScale = 1;
    }

    #endregion
}