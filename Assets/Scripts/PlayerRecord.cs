using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour
{
    public List<Player> playerList;
    public string[] levels;
    public Color[] playerColours;

    [HideInInspector] public int levelIndex;
    private void OnEnable()
    {
        playerList = new List<Player>();
        DontDestroyOnLoad(gameObject);
    }
    public void AddPlayer(string name)
    {
        playerList.Add(new Player(name, playerColours[playerList.Count], levels.Length));

    }

    public void AddPutts(int playerIndex, int puttCount)
    {
        playerList[playerIndex].putts[levelIndex] = puttCount;
    }
    public class Player
    {
        public string name;
        public Color colour;
        public int[] putts;

        public Player (string newName, Color newColor, int levelCount)
        {
            name = newName;
            colour = newColor;
            putts = new int[levelCount];
        }
    }
}
