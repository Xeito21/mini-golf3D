using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardManager : MonoBehaviour
{
    public TextMeshProUGUI nameText, puttsText;

    private PlayerRecord playerRecord;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("Finish");
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        playerRecord = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        nameText.text = "";
        puttsText.text = "";
        foreach(var player in playerRecord.GetScoreBoardList())
        {
            nameText.text += player.name + "\n";
            puttsText.text += player.totalPutts + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        puttsText.fontSize = nameText.fontSize;
    }

    public void MenuBtn()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        Destroy(playerRecord.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
