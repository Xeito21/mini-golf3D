using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private string playerName;

    public void ChangeScene(int sceneIndex)
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        PlayerPrefs.SetString("PlayerName", playerName);
        SceneManager.LoadScene(sceneIndex);
    }

    public void BackBtn()
    {
        SceneManager.LoadScene(0);
    }
}
