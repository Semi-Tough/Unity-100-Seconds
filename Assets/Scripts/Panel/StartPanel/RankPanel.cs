using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel
{
    public Transform ScrollView;
    public UIButton BtnClose;

    private readonly List<RankItem> _rankItems = new();

    public override void ShowPanel()
    {
        base.ShowPanel();
        List<RankInfo> rankInfos = DataService.Instance.RankData.RankInfos;
        for (int i = 0; i < rankInfos.Count; i++)
        {
            if (_rankItems.Count > i)
            {
                _rankItems[i].SetRankInfo(i + 1, rankInfos[i].Name, rankInfos[i].Time);
            }
            else
            {
                GameObject go = Instantiate(ResourceService.Instance.Load<GameObject>(PathDefine.RANK_ITEM), ScrollView, false);
                go.transform.localPosition = new Vector3(0, 160 - (i * 50), 0);
                RankItem rankItem = go.GetComponent<RankItem>();
                rankItem.SetRankInfo(i + 1, rankInfos[i].Name, rankInfos[i].Time);
                _rankItems.Add(rankItem);
            }
        }
    }

    #region UIEvents

    public void BtnCloseEvent()
    {
        HidePanel();
        AudioService.Instance.PlayUiSound(PathDefine.CLICK_BUTTON);
    }

    #endregion
}