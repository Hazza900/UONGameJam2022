using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    PlayerInputManager playerInputManager;

    public List<Player> Players { get; set; }
    private int maxPlayers = 2;
    private int nextIndex = 1;

    private bool hasStarted = false;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        playerInputManager = GetComponent<PlayerInputManager>();
        Players = new List<Player>(2);
    }

    public void PlayerJoined(PlayerInput input)
    {
        Player player = input.gameObject.GetComponent<Player>();
        player.ID = nextIndex;
        player.Score = 0;
    }

    private void Start()
    {
        StartCoroutine(WaitingForPlayers());
    }

    public void ScorePlayer(int index)
    {
        Players[index].Score++;
    }

    private void StartGame()
    {
        foreach(Player player in Players)
        {
        }
    }

    IEnumerator WaitingForPlayers()
    {
        if (Players.Count != 2)
            yield return new WaitForSeconds(0.5f);

        StartGame();
    }
}
