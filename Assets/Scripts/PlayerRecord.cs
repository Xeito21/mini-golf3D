using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRecord : MonoBehaviour
{
    public List<Player> playerList;
    public string[] levels;
    public Color[] playerColours;

    [HideInInspector] public int levelIndex;

    private const string playerNameKeyPrefix = "PlayerName_";
    private const string playerPuttsKeyPrefix = "PlayerPutts_";

    private string playerName;


    private void Awake()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
    }

private void OnEnable()
    {
        playerList = new List<Player>();

        // Load saved player data from PlayerPrefs
        for (int i = 0; i < playerColours.Length; i++)
        {
            string playerNameKey = playerNameKeyPrefix + i.ToString();
            string playerPuttsKey = playerPuttsKeyPrefix + i.ToString();

            if (PlayerPrefs.HasKey(playerNameKey) && PlayerPrefs.HasKey(playerPuttsKey))
            {
                string playerName = PlayerPrefs.GetString(playerNameKey);
                string[] puttsStr = PlayerPrefs.GetString(playerPuttsKey).Split(',');

                int[] putts = new int[levels.Length];
                for (int j = 0; j < puttsStr.Length; j++)
                {
                    int.TryParse(puttsStr[j], out putts[j]);
                }

                playerList.Add(new Player(playerName, playerColours[i], putts));
            }
            else
            {
                break;
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(string name)
    {
        int index = playerList.Count;

        if (index < playerColours.Length)
        {
            playerList.Add(new Player(name, playerColours[index], levels.Length));

            // Save player data to PlayerPrefs
            string playerNameKey = playerNameKeyPrefix + index.ToString();
            string playerPuttsKey = playerPuttsKeyPrefix + index.ToString();

            PlayerPrefs.SetString(playerNameKey, name);
            PlayerPrefs.SetString(playerPuttsKey, string.Join(",", Enumerable.Repeat(0, levels.Length).ToArray()));
            PlayerPrefs.Save();
        }
    }

    public void AddPutts(int playerIndex, int puttCount)
    {
        playerList[playerIndex].putts[levelIndex] = puttCount;

        // Save player data to PlayerPrefs
        string playerPuttsKey = playerPuttsKeyPrefix + playerIndex.ToString();
        PlayerPrefs.SetString(playerPuttsKey, string.Join(",", playerList[playerIndex].putts));
        PlayerPrefs.Save();
    }

    public List<Player> GetScoreBoardList()
    {
        foreach (var player in playerList)
        {
            foreach (var puttScore in player.putts)
            {
                player.totalPutts += puttScore;
            }
        }
        return (from p in playerList orderby p.totalPutts select p).ToList();
    }

    public class Player
    {
        public string name;
        public Color colour;
        public int[] putts;
        public int totalPutts;

        public Player(string newName, Color newColor, int levelCount)
        {
            name = newName;
            colour = newColor;
            putts = new int[levelCount];
        }

        public Player(string newName, Color newColor, int[] putts)
        {
            name = newName;
            colour = newColor;
            this.putts = putts;
        }
    }
}
