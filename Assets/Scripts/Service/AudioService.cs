using UnityEngine;

public class  AudioService : BaseService<AudioService>
{
    public AudioSource BgMusic;
    public AudioSource UiSound;
    
    public void PlayBgMusic(string path, bool loop = true)
    {
        ResourceService.Instance.LoadAsync<AudioClip>(path, audioClip =>
        {
            BgMusic.clip = audioClip;
            BgMusic.loop = loop;
            BgMusic.volume = DataService.Instance.MusicData.MusicValue;
            BgMusic.mute = !DataService.Instance.MusicData.OpenMusic;
            BgMusic.Play();
        });
    }

    public void SetBgMusicValue(float value)
    {
        BgMusic.volume = value;
    }

    public void SetBgMusicState(bool open)
    {
        BgMusic.mute = !open;
    }

    public void PlayUiSound(string path)
    {
        AudioClip audioClip = ResourceService.Instance.Load<AudioClip>(path);
        UiSound.volume = DataService.Instance.MusicData.SoundValue;
        UiSound.mute = !DataService.Instance.MusicData.OpenSound;
        UiSound.PlayOneShot(audioClip);
    }
}