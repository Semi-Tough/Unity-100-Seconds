public class GameSystem : BaseSystem<GameSystem>
{
    public GamePanel GamePanel;
    public QuitPanel QuitPanel;
    public OverPanel OverPanel;
    public int PlaneIndex;
    public PlaneInfo PlaneInfo { private set; get; }

    public void InitGameScene()
    {
        ResourceService.Instance.LoadSceneAsync("GameScene", () =>
        {
            PlaneInfo = DataService.Instance.GetNowPlaneData(PlaneIndex);
            GamePanel.ShowPanel();
            GamePanel.ChangeMaxHp(PlaneInfo.Hp);
            PlayerController.Instance.InitPlane(PlaneInfo);
            AudioService.Instance.PlayBgMusic(PathDefine.GAME_BG_MUSIC);
        });
    }
}