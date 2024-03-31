using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    public TextMeshProUGUI[] Rankings;
    public TextMeshProUGUI[] ReTrys;

    void Start()
    {
        ImputRank();
    }

    private void Update()
    {
        for(int i = 0; i < Rankings.Length; i++)
        {
            Rankings[i].text = GameInstence.instence.RankingEntry[i].Recode.ToString("F2");
            ReTrys[i].text = GameInstence.instence.RankingEntry[i].ReTrys.ToString();
        }
    }

    private void ImputRank()
    {
        for(int i = 0; GameInstence.instence.Recodes.Length > i; i++)
        {
            GameInstence.instence.AllRecode += GameInstence.instence.Recodes[i];
        }

        GameInstence.instence.RankingEntry.Add(new RankingEntry(GameInstence.instence.AllRecode, GameInstence.instence.ReTry));

        RankSet();
    }

    private void RankSet()
    {
        GameInstence.instence.RankingEntry.Sort((x,y) => {
            if(x.ReTrys != y.ReTrys) return x.ReTrys.CompareTo(y.ReTrys);
            else return x.Recode.CompareTo(y.Recode);
        });

        GameInstence.instence.RankingEntry.RemoveAt(5);
    }
}
