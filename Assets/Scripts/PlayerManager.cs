using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameStarted;
    public static bool isGameOver;
    public GameObject startText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;
    public static int coins;
    public float score;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreTextPanel;
    public TextMeshProUGUI diamondsTextPanel;

    public GameObject pausePanel;
    public Image pause;

    public int currentPlayer;
    public GameObject[] playersModels;

    public bool isSoundChanged;

    

    public AudioClip startGameOverSound;

    void Start()
    {
       
        pause.enabled = false;
        currentPlayer = PlayerPrefs.GetInt("CurrentPlayer", 0);

        foreach (GameObject p in playersModels)
        {

            p.SetActive(false);
        }
        playersModels[currentPlayer].SetActive(true);


        coins = PlayerPrefs.GetInt("Coins", 0);
        Time.timeScale = 1;
        isGameStarted = false;
        isGameOver = false;
        isSoundChanged = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(SwipeManager.tap)
        {
            isGameStarted = true;
            startText.SetActive(false);
            pause.enabled = true;


        }
        if (isGameStarted)
        {
            
            score += Time.deltaTime;

            if( !isSoundChanged)
            {
                FindObjectOfType<AudioManager>().StopSound("Waiting");
                FindObjectOfType<AudioManager>().PlaySound("MainTheme");
                isSoundChanged = true;
            }
           
           
        }

        if(isGameOver)
        {
            
            isGameStarted = false;
            isGameOver = false;
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
            StartCoroutine(GameOverMusic());

            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;

            
            

            FindObjectOfType<SpawnManager>().enabled = false;


            //Time.timeScale = 0;


            PlayerPrefs.SetInt("Coins", coins);
            if(score > PlayerPrefs.GetInt("HighScore",0))
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
            }
            gameOverPanel.SetActive(true);
            scoreTextPanel.text = "" + (int)score;
            diamondsTextPanel.text = "" + coins;
            pausePanel.SetActive(false);
            pause.enabled = false;
        }



        coinsText.text = "" + coins;
        scoreText.text = "" + (int)score;
    }

    IEnumerator GameOverMusic()
    {
        FindObjectOfType<AudioManager>().PlaySound("GameOverStart");

        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().StopSound("GameOverStart");
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
    }




    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {

        pausePanel.SetActive(true);
        Time.timeScale = 0;
        FindObjectOfType<AudioManager>().PauseSound("MainTheme");

    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        FindObjectOfType<AudioManager>().PlaySound("MainTheme");
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
