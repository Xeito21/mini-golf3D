using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{

    public void ClearBtn()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        PlayerPrefs.DeleteAll();
    }

}

