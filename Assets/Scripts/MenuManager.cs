using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public TMP_InputField InputPlayerName;
    public PlayerRecord playerRecord;
    public Button startBtn;
    public Button addPlayerBtn;
    public void Start()
    {
        // check if PlayerName exists in PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            startBtn.interactable = true;
        }
        else
        {
            startBtn.interactable = false;
        }
    }

    public void AddPlayerBtn()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            playerRecord.AddPlayer(InputPlayerName.text);
            PlayerPrefs.SetString("PlayerName", InputPlayerName.text);
        }
        startBtn.interactable = true;
        InputPlayerName.text = "";
        if (playerRecord.playerList.Count == playerRecord.playerColours.Length)
        {
            addPlayerBtn.interactable = false;
        }
    }

    public void StartBtn()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        SceneManager.LoadScene(1);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }



}
