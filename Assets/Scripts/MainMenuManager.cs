using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI highScoreText;

    public GameObject diamondsPanel;

    public GameObject settingsPanel;
    public AudioSource sfx;
    

    private void Start()
    {

     

        Time.timeScale = 1;
        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore", 0);
    }


    private void Update()
    {
        diamondText.text = "" + PlayerPrefs.GetInt("Coins");
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Game");

    }

    public void BuyDiamonds()
    {
       
        diamondsPanel.SetActive(true);
    }
    public void CancelDiamonds()
    {

        diamondsPanel.SetActive(false);
    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }


   

}
