using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI mainUI;

    public TextMeshProUGUI Recode, CurrentTime, CurrentMoney;

    public Image SandStorm_img, DesertOher_img, MountainOther_img, CityOther_img, Engine_img, Wheel_img;
    public GameObject SandStorm_gb, DesertOhter_gb, MountainOther_gb, CityOther_gb;
    public Color Yellow, Orange, Red, Green, Gray;

    private void Start()
    {
        if (GameInstence.instence.bPlay)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(mainUI);
            mainUI = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        Recode.text = GameInstence.instence.Recodes[GameInstence.instence.CurrentStage - 1].ToString("F2");

        CurrentTime.text = GameInstence.instence.CurrentTime.ToString("F2");
        CurrentMoney.text = GameInstence.instence.CurrentMoney.ToString("N0");

        UpdateEngineLevel();
        UpdateWheelLevel();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void NextStage()
    {
        switch(GameInstence.instence.CurrentStage)
        {
            case 1:
                SceneManager.LoadScene("Stage2");
                break;
            case 2:
                SceneManager.LoadScene("Stage3");
                break;
            case 3:
                SceneManager.LoadScene("Ranking");
                break;
        }
    }

    public void Retry()
    {
        GameInstence.instence.ReTry++;

        switch (GameInstence.instence.CurrentStage)
        {
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
        }
    }

    public void SandStorm(bool bsandStorm, bool bwhiper)
    {
        if(bsandStorm)
        {
            SandStorm_gb.SetActive(true);
            if (!bwhiper)
            {
                if (SandStorm_img.color.a < 0.95)
                    SandStorm_img.color += new Color(0, 0, 0, 0.3f * Time.deltaTime);
            }
            else
            {
                if (SandStorm_img.color.a > 0)
                    SandStorm_img.color -= new Color(0, 0, 0, 0.3f * Time.deltaTime);               
            }           
        }
        else
        {
            SandStorm_gb.SetActive(false);
            if (SandStorm_img.color.a > 0)
                SandStorm_img.color -= new Color(0, 0, 0, 0.3f * Time.deltaTime);
        }
    }

    public void UpdateEngineLevel()
    {
        switch (GameInstence.instence.CurrentEngineLevel)
        {
            case 1:
                Engine_img.color = Yellow;
                break;
            case 2:
                Engine_img.color = Orange;
                break;
            case 3:
                Engine_img.color = Red;
                break;
        }
    }

    public void UpdateWheelLevel()
    {
        if(GameInstence.instence.bDesertWheel)
            Wheel_img.color = Yellow;
        if(GameInstence.instence.bMountainWheel)
            Wheel_img.color = Green;
        if (GameInstence.instence.bCityWheel)
            Wheel_img.color = Gray;
    }

    public void OtherItem(int currentStage, bool on)
    {
        switch (currentStage)
        {
            case 1:
                if (on)
                    DesertOher_img.color = Color.white;
                else
                    DesertOher_img.color = Color.black;
                break;
            case 2:
                if (on)
                    MountainOther_img.color = Color.white;
                else
                    MountainOther_img.color = Color.black;
                break;
            case 3:
                if (on)
                    CityOther_img.color = Color.yellow;
                else
                    CityOther_img.color = Color.black;
                break;
        }
    }
}
