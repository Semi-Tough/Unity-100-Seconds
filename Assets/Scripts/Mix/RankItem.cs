using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel LabRank;
    public UILabel LabName;
    public UILabel LabTime;

    public void SetRankInfo(int rank, string name, float time)
    {
        LabRank.text = rank.ToString();

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "未知用户";
        }

        LabName.text = name;

        LabTime.text = time.ToString("0.00") + "S";
    }
}