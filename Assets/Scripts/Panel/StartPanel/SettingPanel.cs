public class SettingPanel : BasePanel
{
    public UIButton BtnClose;

    public UISlider SliderMusic;
    public UISlider SliderSound;

    public UIToggle ToggleMusic;
    public UIToggle ToggleSound;

    private MusicData _musicData;

    public override void ShowPanel()
    {
        base.ShowPanel();

        _musicData = DataService.Instance.MusicData;

        SliderMusic.value = _musicData.MusicValue;
        SliderSound.value = _musicData.SoundValue;
        ToggleMusic.value = _musicData.OpenMusic;
        ToggleSound.value = _musicData.OpenSound;
    }


    #region UIEvents

    public void BtnCloseEvent()
    {
        HidePanel();
        DataService.Instance.SaveMusicData(_musicData);
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
    }

    public void SliderMusicEvent()
    {
        _musicData.MusicValue = SliderMusic.value;
        AudioService.Instance.SetBgMusicValue(SliderMusic.value);
    }

    public void SliderSoundEvent()
    {
        _musicData.SoundValue = SliderSound.value;
    }

    public void ToggleMusicEvent()
    {
        _musicData.OpenMusic = ToggleMusic.value;
        AudioService.Instance.SetBgMusicState(ToggleMusic.value);
    }

    public void ToggleSoundEvent()
    {
        _musicData.OpenSound = ToggleSound.value;
    }

    #endregion
}