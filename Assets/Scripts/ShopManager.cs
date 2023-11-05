using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] playersModels;
    public Player[] players;
    public int currentPlayer;

    public Button buyButton;
    public Button playButton;

    public AudioSource sfx;
    public AudioClip click;
    public AudioClip swoosh;
    void Start()
    {
        currentPlayer = PlayerPrefs.GetInt("CurrentPlayer", 0);

        foreach(GameObject p in playersModels)
        {

            p.SetActive(false);
        }

        playersModels[currentPlayer].SetActive(true);

        foreach(Player c in players)
        {
            if(c.price == 0)
            {
                c.isLocked = false;
            }
            else
            {
                c.isLocked = PlayerPrefs.GetInt(c.name, 1) == 1 ? true : false;
            }

        }
    }

    private void Update()
    {
        UpdateBuyButton();
    }

    public void ChangeNext()
    {
        sfx.PlayOneShot(swoosh);
        playersModels[currentPlayer].SetActive(false);
        currentPlayer++;
        if(currentPlayer == playersModels.Length)
        {
            currentPlayer = 0;
        }
        playersModels[currentPlayer].SetActive(true);

        Player p = players[currentPlayer];
        if(p.isLocked)
        {
            return;
        }

        PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
    }


    public void ChangePrevious()
    {
        sfx.PlayOneShot(swoosh);
        playersModels[currentPlayer].SetActive(false);
        currentPlayer--;
        if (currentPlayer == -1)
        {
            currentPlayer = playersModels.Length -1;
        }

        playersModels[currentPlayer].SetActive(true);

        Player p = players[currentPlayer];
        if (p.isLocked)
        {
            return;
        }

        PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
    }

    public void UpdateBuyButton()
    {
        Player p = players[currentPlayer];

        
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy- " + p.price;

        if(p.isLocked)
        {
            buyButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("Coins") >= p.price)
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }

        }

        else
        {
            buyButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }

       

    }

    public void buyItem()
    {
        sfx.PlayOneShot(click);
        Player p = players[currentPlayer];
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - p.price);
        PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
        PlayerPrefs.SetInt(p.name, 0);
        p.isLocked = false;

    }


}
