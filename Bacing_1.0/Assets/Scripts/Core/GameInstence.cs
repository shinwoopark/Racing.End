using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingEntry
{
    public float Recode { get; set; }
    public int ReTrys { get; set; }

    public RankingEntry(float recode, int reTrys)
    {
        Recode = recode;
        ReTrys = reTrys;
    }
}

public class GameInstence : MonoBehaviour
{
    public static GameInstence instence;

    public List<RankingEntry> RankingEntry = new List<RankingEntry>();

    public bool bPlay;
    public bool bRacing;
    public float CurrentTime;
    public float[] Recodes;
    public float AllRecode;
    public int ReTry;

    public int CurrentStage;
    public int[] CurrentInventory;
    public int CurrentMoney;
    public int CurrentEngineLevel;

    public bool bDesertWheel, bMountainWheel, bCityWheel;
    public bool bDesertOther, bMountainOther, bCityOther;

    private void Awake()
    {
        if(instence == null)
            instence = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
