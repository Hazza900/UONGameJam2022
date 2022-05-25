using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    PlayerInputManager playerInputManager;

    [SerializeField] public List<PlayerController> Players;
    private int maxPlayers = 2;
    private int nextIndex = 1;

    private bool hasStarted = false;

    [SerializeField] public Transform[] bottomPlayer;
    [SerializeField] public Transform[] topPlayer;

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

        Players = new List<PlayerController>(2);
    }

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();

        StartCoroutine(WaitingForPlayers());
    }
    public void onPlayerJoined(PlayerInput input)
    {
       
        GameObject player = input.gameObject;
        Debug.Log("Player joined called, " + player.name + ". ID: " + input.playerIndex);

        PlayerController pc = player.GetComponent<PlayerController>();
        pc.ID = nextIndex++;
        pc.Score = 0;
        Players.Add(pc);
    }

    public void ScorePlayer(int index)
    {
        Players[index].Score++;
    }

    private void StartGame()
    {
        Debug.Log("starting game");

        for (int i = 0; i < 2; i++)
        {
            Debug.Log("pawn spawn");

            if (i == 0)
            {
                Players[i].rails = bottomPlayer;
            }
            else
            {
                Players[i].rails = topPlayer;
            }

            Players[i].InitPawn();
        }
    }

    IEnumerator WaitingForPlayers()
    {
        while (Players.Count < 2)
        {
            Debug.Log("Waiting for players");
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(1f);
        StartGame();
    }
}
