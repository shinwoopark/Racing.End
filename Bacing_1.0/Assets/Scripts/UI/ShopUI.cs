using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    static ShopUI shopUI;

    public LockOn[] LockOns;

    public GameObject[] SoldOuts_gb;

    public AudioSource BuySound;

    private void Start()
    {
        if(GameInstence.instence.CurrentEngineLevel == 1)
        {
            SoldOuts_gb[0].SetActive(true);
            LockOns[0].bLockOff = true;
        }
        if (GameInstence.instence.CurrentEngineLevel == 2)
        {
            SoldOuts_gb[0].SetActive(true);
            LockOns[0].bLockOff = true;
            SoldOuts_gb[1].SetActive(true);
            LockOns[1].bLockOff = true;
        }
        if (GameInstence.instence.CurrentEngineLevel == 3)
        {
            SoldOuts_gb[0].SetActive(true);
            LockOns[0].bLockOff = true;
            SoldOuts_gb[1].SetActive(true);
            LockOns[1].bLockOff = true;
            SoldOuts_gb[2].SetActive(true);
            LockOns[2].bLockOff = true;
        }
        if (GameInstence.instence.bDesertWheel)
        {
            SoldOuts_gb[3].SetActive(true);
        }
        if (GameInstence.instence.bMountainWheel)
        {
            SoldOuts_gb[4].SetActive(true);
        }
        if (GameInstence.instence.bCityWheel)
        {
            SoldOuts_gb[5].SetActive(true);
        }
        if (GameInstence.instence.bDesertOther)
        {
            SoldOuts_gb[6].SetActive(true);
            MainUI.mainUI.DesertOhter_gb.SetActive(true);
            MainUI.mainUI.OtherItem(1, false);
        }
        if (GameInstence.instence.bMountainOther)
        {
            SoldOuts_gb[7].SetActive(true);
            MainUI.mainUI.MountainOther_gb.SetActive(true);
            MainUI.mainUI.OtherItem(2, false);
        }
        if (GameInstence.instence.bCityOther)
        {
            SoldOuts_gb[8].SetActive(true);
            MainUI.mainUI.CityOther_gb.SetActive(true);
            MainUI.mainUI.OtherItem(3, false);
        }
    }

    void Update()
    {
        ExitShop();
    }

    public void ExitShop()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }    
    }

    public void BuyEngine1()
    {
        if (GameInstence.instence.CurrentMoney >= 3000000 && GameInstence.instence.CurrentEngineLevel == 0)
        {
            GameInstence.instence.CurrentMoney -= 3000000;
            GameInstence.instence.CurrentEngineLevel = 1;
            SoldOuts_gb[0].SetActive(true);
            LockOns[0].bLockOff = true;
            BuySound.Play();
        }
    }

    public void BuyEngine2()
    {
        if (GameInstence.instence.CurrentMoney >= 6000000 && GameInstence.instence.CurrentEngineLevel == 1)
        {
            GameInstence.instence.CurrentMoney -= 6000000;
            GameInstence.instence.CurrentEngineLevel = 2;
            SoldOuts_gb[1].SetActive(true);
            LockOns[1].bLockOff = true;
            BuySound.Play();
        }
    }

    public void BuyEngine3()
    {
        if (GameInstence.instence.CurrentMoney >= 9000000 && GameInstence.instence.CurrentEngineLevel == 2)
        {
            GameInstence.instence.CurrentMoney -= 9000000;
            GameInstence.instence.CurrentEngineLevel = 3;
            SoldOuts_gb[2].SetActive(true);
            BuySound.Play();
        }
    }

    public void BuyDesertWheel()
    {
        if (GameInstence.instence.CurrentMoney >= 3000000 && !GameInstence.instence.bDesertWheel)
        {
            GameInstence.instence.CurrentMoney -= 3000000;
            GameInstence.instence.bDesertWheel = true;
            SoldOuts_gb[3].SetActive(true);
            BuySound.Play();
        }
    }

    public void BuyMountainWheel()
    {
        if (GameInstence.instence.CurrentMoney >= 3000000 && !GameInstence.instence.bMountainWheel)
        {
            GameInstence.instence.CurrentMoney -= 3000000;
            GameInstence.instence.bMountainWheel = true;
            SoldOuts_gb[4].SetActive(true);
            BuySound.Play();
        }
    }

    public void BuyCityWheel()
    {
        if (GameInstence.instence.CurrentMoney >= 3000000 && !GameInstence.instence.bCityWheel)
        {
            GameInstence.instence.CurrentMoney -= 3000000;
            GameInstence.instence.bCityWheel = true;
            SoldOuts_gb[5].SetActive(true);
            BuySound.Play();
        }
    }

    public void BuyDesertOther()
    {
        if (GameInstence.instence.CurrentMoney >= 3000000 && !GameInstence.instence.bDesertOther)
        {
            GameInstence.instence.CurrentMoney -= 3000000;
            GameInstence.instence.bDesertOther = true;
            SoldOuts_gb[6].SetActive(true);
            BuySound.Play();
            MainUI.mainUI.DesertOhter_gb.SetActive(true);
            MainUI.mainUI.OtherItem(1, false);
        }
    }

    public void BuyMountainOther()
    {
        if (GameInstence.instence.CurrentMoney >= 4000000 && !GameInstence.instence.bMountainOther)
        {
            GameInstence.instence.CurrentMoney -= 4000000;
            GameInstence.instence.bMountainOther = true;
            SoldOuts_gb[7].SetActive(true);
            BuySound.Play();
            MainUI.mainUI.MountainOther_gb.SetActive(true);
            MainUI.mainUI.OtherItem(2, false);
        }
    }

    public void BuyCityOther()
    {
        if (GameInstence.instence.CurrentMoney >= 5000000 && !GameInstence.instence.bCityOther)
        {
            GameInstence.instence.CurrentMoney -= 5000000;
            GameInstence.instence.bCityOther = true;
            SoldOuts_gb[8].SetActive(true);
            BuySound.Play();
            MainUI.mainUI.CityOther_gb.SetActive(true);
            MainUI.mainUI.OtherItem(3, false);
        }
    }
}
