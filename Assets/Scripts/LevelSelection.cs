using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] arenaBtn;
    // Start is called before the first frame update
    void Start()
    {
        int arenaAt = PlayerPrefs.GetInt("ArenaAt", 2);
        for (int i = 0; i < arenaBtn.Length; i++)
        {
            if (i + 2 > arenaAt)
                arenaBtn[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
