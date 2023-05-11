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
    public void AddPlayerBtn()
    {
        playerRecord.AddPlayer(InputPlayerName.text);
        startBtn.interactable = true;
        InputPlayerName.text = "";
        if(playerRecord.playerList.Count == playerRecord.playerColours.Length)
        {
            addPlayerBtn.interactable = false;
        }
    }

    public void StartBtn()
    {
        SceneManager.LoadScene(playerRecord.levels[0]);
    }
}
