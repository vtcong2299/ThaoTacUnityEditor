using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider;
    public static UIManager instance;
    public Text GameOver;
    public GameObject PanelGameOver;
    public GameObject PanelGameStart;
    public GameObject PanelHienThi;
    public GameObject PanelDisplayPause;
    public Text txtHP;
    public Text txtLaps;
    public Text txtCoins;
    public bool isStart = true;
    private void Awake()
    {
        PanelGameStart.SetActive(true);
        PanelHienThi.SetActive(false);
        PanelGameOver.SetActive(false);
        PanelDisplayPause.SetActive(false);
    }
    public void ClickPause()
    {
        PanelDisplayPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClickPlay()
    {
        PanelDisplayPause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickExit()
    {
        Application.Quit();
    }
    private void Start()
    {
        Time.timeScale = 0;
    }
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    public void OnHpChange(int hp)
    {
        txtHP.text = "Durability: " + hp;
    }
    public void OnCoinsChange(int coins)
    {
        txtCoins.text = "Coins: " + coins;  
    }
    public void OnLapsChange(int countDich)
    {
        txtLaps.text = "Laps: " + (countDich + 1); 
    }
    public void OnFuleChange(float capacity)
    {
        slider.value = capacity;
    }
    public void EndGame(int hp)
    {
        if (hp<=0)
        {
            PanelHienThi.SetActive(false);
            PanelGameOver.SetActive(true);
            if (Input.GetMouseButtonDown(0) && isStart)
            {
                StartGame();
            }
        }
    }    
    void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ClickRestart()
    {
        StartGame();
    }
    public void ClickStart()
    {
        PanelGameStart.SetActive(false);
        PanelHienThi.SetActive(true);
        Time.timeScale = 1;
    }
}
