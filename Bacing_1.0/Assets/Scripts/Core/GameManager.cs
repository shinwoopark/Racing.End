using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public GameObject GameClearUI, GameOverUI, ShopUI;

    public AudioSource CountDownSound, StartUpSound, ReadySound, WinSound, LoseSound;

    private void Awake()
    {
        if (manager != null)
            manager = this;
        else
        {
            Destroy(manager);
            manager = this;
        }
    }

    void Start()
    {
        StartCoroutine(StartRacing());       
    }

    void Update()
    {
        if (GameInstence.instence.bRacing)
            GameInstence.instence.CurrentTime += Time.deltaTime;
    }

    public void StartGameScene()
    {
       GameInstence.instence.CurrentStage = 1;
       GameInstence.instence.bPlay = true;
       SceneManager.LoadScene("Stage1");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RankingScene()
    {
        SceneManager.LoadScene("Ranking");
    }

    private IEnumerator StartRacing()
    {
        yield return new WaitForSeconds(2);
        StartUpSound.Play();
        yield return new WaitForSeconds(0.5f);
        ReadySound.Play();
        CountDownSound.Play();
        yield return new WaitForSeconds(3);
        ReadySound.Stop();
        GameInstence.instence.bRacing = true;
    }

    public void EndGame()
    {
        for (int i = 0; i < GameInstence.instence.Recodes.Length; i++)
        {
            GameInstence.instence.Recodes[i] = 0;
        }
        for (int i = 0; i < GameInstence.instence.CurrentInventory.Length; i++)
        {
            GameInstence.instence.CurrentInventory[i] = 0;
        }
        GameInstence.instence.bPlay = false;
        GameInstence.instence.bRacing = false;
        GameInstence.instence.CurrentStage = 0;
        GameInstence.instence.CurrentMoney = 0;
        GameInstence.instence.CurrentEngineLevel = 0;
        GameInstence.instence.bDesertWheel = false;
        GameInstence.instence.bMountainWheel = false;
        GameInstence.instence.bCityWheel = false;
        GameInstence.instence.bDesertOther = false;
        GameInstence.instence.bMountainOther = false;
        GameInstence.instence.bCityOther = false;
    }

    public IEnumerator FinishRacing(bool bPlayer)
    {
        if (GameInstence.instence.bRacing)
        {
            GameInstence.instence.bRacing = false;

            if (bPlayer)
            {
                WinSound.Play();

                switch (GameInstence.instence.CurrentStage)
                {
                    case 1:
                        GameInstence.instence.Recodes[0] = GameInstence.instence.CurrentTime;
                        yield return new WaitForSeconds(2);
                        GameClearUI.SetActive(true);
                        break;
                    case 2:
                        GameInstence.instence.Recodes[1] = GameInstence.instence.CurrentTime;
                        yield return new WaitForSeconds(2);
                        GameClearUI.SetActive(true);
                        break;
                    case 3:
                        GameInstence.instence.Recodes[2] = GameInstence.instence.CurrentTime;
                        yield return new WaitForSeconds(2);
                        GameClearUI.SetActive(true);
                        break;
                }
            }
            else
            {
                LoseSound.Play();
                yield return new WaitForSeconds(2);
                GameOverUI.SetActive(true);
            }
        }       
    }

    public void ShopOpen()
    {
        Time.timeScale = 0;
        ShopUI.SetActive(true);
    }
}
