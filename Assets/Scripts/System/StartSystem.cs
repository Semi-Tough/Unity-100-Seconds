public class StartSystem : BaseSystem<StartSystem>
{
    public StartPanel StartPanel;
    public SettingPanel SettingPanel;
    public RankPanel RankPanel;
    public ChoosePanel ChoosePanel;


    public void InitStartScene()
    {
        ResourceService.Instance.LoadSceneAsync("StartScene", () =>
        {
            PoolService.Instance.Clear();
            StartPanel.ShowPanel();
            AudioService.Instance.PlayBgMusic(PathDefine.START_BG_MUSIC);
        });
    }
}