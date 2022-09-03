using System.Collections.Generic;

public class MusicData
{
    public bool OpenMusic;
    public bool OpenSound;
    public float MusicValue;
    public float SoundValue;
    public bool HaveData;
}

public class RankData
{
    public readonly List<RankInfo> RankInfos = new();
}

public class RankInfo
{
    public string Name;
    public float Time;

    public RankInfo()
    {
    }

    public RankInfo(string name, float time)
    {
        Name = name;
        Time = time;
    }
}

public class PlaneData
{
    public List<PlaneInfo> PlaneInfos = new();
}

public class PlaneInfo
{
    public int Hp;
    public int Speed;
    public int Volume;
    public string ResPath;
    public float LocalScale;
}

public class BulletData
{
    public List<BulletInfo> BulletInfos = new();
}

public class BulletInfo
{
    public int Id;
    public int Type;
    public float ForwardSpeed;
    public float RightSpeed;
    public float RotateSpeed;
    public string BulletPath;
    public string EffectPath;
    public float LifeTime;
}

public class FirePointData
{
    public List<FirePointInfo> FirePointInfos = new();
}

public class FirePointInfo
{
    public int Id;
    public int Type;
    public int Num;
    public float Cd;
    public string Ids;
    public float Delay;
}

public class DataService : BaseService<DataService>
{
    public MusicData MusicData { private set; get; }
    public RankData RankData { private set; get; }
    public PlaneData PlaneData { private set; get; }
    public BulletData BulletData { private set; get; }
    public FirePointData FirePointData { private set; get; }

    public override void InitService()
    {
        base.InitService();
        LoadMusicData();
        LoadRankData();
        LoadPlaneData();
        LoadBulletData();
        LoadFirePointData();
    }

    private void LoadMusicData()
    {
        MusicData = ResourceService.Instance.LoadXmlData(typeof(MusicData), nameof(MusicData)) as MusicData;
        if (MusicData is { HaveData: false })
        {
            MusicData.OpenMusic = true;
            MusicData.OpenSound = true;
            MusicData.MusicValue = 0.5f;
            MusicData.SoundValue = 0.5f;

            MusicData.HaveData = true;
        }
    }

    private void LoadRankData()
    {
        RankData = ResourceService.Instance.LoadXmlData(typeof(RankData), nameof(RankData)) as RankData;
    }

    private void LoadPlaneData()
    {
        PlaneData = ResourceService.Instance.LoadXmlData(typeof(PlaneData), nameof(PlaneData)) as PlaneData;
    }

    private void LoadBulletData()
    {
        BulletData = ResourceService.Instance.LoadXmlData(typeof(BulletData), nameof(BulletData)) as BulletData;
    }

    private void LoadFirePointData()
    {
        FirePointData = ResourceService.Instance.LoadXmlData(typeof(FirePointData), nameof(FirePointData)) as FirePointData;
    }

    public void SaveMusicData(MusicData musicData)
    {
        MusicData = musicData;
        ResourceService.Instance.SaveXmlData(MusicData, nameof(MusicData));
    }

    public void AddRankData(RankInfo rankInfo)
    {
        RankData.RankInfos.Add(rankInfo);

        RankData.RankInfos.Sort((a, b) => a.Time > b.Time ? -1 : 1);

        if (RankData.RankInfos.Count > 20)
        {
            RankData.RankInfos.RemoveAt(20);
        }

        ResourceService.Instance.SaveXmlData(RankData, nameof(RankData));
    }

    public PlaneInfo GetNowPlaneData(int index)
    {
        return PlaneData.PlaneInfos[index];
    }
}