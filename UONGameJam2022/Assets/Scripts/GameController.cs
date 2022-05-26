using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PlayerInputManager playerInputManager;
    public MenuController mc;
    public PatternSpawner ps;

    public List<PlayerController> Players;
    private int maxPlayers = 2;
    private int nextIndex = 1;

    private bool hasStarted = false;
    private bool hasEnded = false;


    private bool alreadySpawned = false;

    [SerializeField] public Transform[] bottomPlayer;
    [SerializeField] public Transform[] topPlayer;

    [SerializeField] private float gameLength;
    [SerializeField] private float secretTime;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _quitButton;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        #endregion

        Players = new List<PlayerController>(2);
    }

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();

        StartCoroutine(GameLoad());

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

    public void Update()
    {
        if (hasStarted)
        {
            mc.player1score.text = Players[0].Score.ToString();
            mc.player2score.text = Players[1].Score.ToString();
        }
    }

    IEnumerator GameLoad()
    {
        yield return new WaitForSeconds(3);
        mc.logoScreen.SetActive(false);
        playerInputManager.EnableJoining();
        StartCoroutine(WaitingForPlayers());
    }

    public void Restart()
    {

        hasStarted = false;
        hasEnded = false;
        Players[0].Score = 0;
        Players[1].Score = 0;

        mc.player1score.text = Players[0].Score.ToString();
        mc.player2score.text = Players[1].Score.ToString();

        mc.bluewins.SetActive(false);
        mc.redwins.SetActive(false);
        mc.draw.SetActive(false);

        _restartButton.SetActive(false);
        _quitButton.SetActive(false);

        StartCoroutine(WaitingForPlayers());
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator WaitingForPlayers()
    {
        while (Players.Count < 2)
        {
            Debug.Log("Waiting for players");
            yield return new WaitForSeconds(2f);
        }

        mc.waitScreen.SetActive(false);

        Debug.Log("Displaying Names");
        mc.player1.SetActive(true);
        mc.player2.SetActive(true);

        mc.player1text.text = Players[0].GetComponent<PlayerInput>().devices[0].displayName.Substring(0, 4);
        mc.player2text.text = Players[1].GetComponent<PlayerInput>().devices[0].displayName.Substring(0, 4);
        yield return new WaitForSeconds(4f);
        mc.player1.SetActive(false);
        mc.player2.SetActive(false);

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Debug.Log("Starting Game!");
        if (!alreadySpawned)
        {
            Players[0].rails = bottomPlayer;
            Players[0].InitPawn();
            Players[1].rails = topPlayer;
            Players[1].InitPawn();

            Debug.Log("Players Spawned, Beginning Countdown...");
            alreadySpawned = true;

        }
        else
        {
            foreach (PlayerController player in Players)
            {
                player.GetComponent<PlayerInput>().ActivateInput();
                player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                player.immune = false;
            }
        }


        yield return new WaitForSeconds(1f);

        ps.StartSpawn();
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        hasStarted = true;
        yield return new WaitForSeconds(gameLength);

        //block score
        mc.finalTimer.SetActive(true);

        mc.finalTimerText.text = secretTime.ToString();
        while (secretTime > 0)
        {
            yield return new WaitForSeconds(1);
            secretTime--;
            mc.finalTimerText.text = secretTime.ToString();
        }

        foreach (PlayerController player in Players)
        {
            player.GetComponent<PlayerInput>().DeactivateInput();
            player.immune = true;
        }

        ps.StopSpawn();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        yield return new WaitForSeconds(1);
        mc.finalTimerText.text = "";

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.75f);
            mc.finalTimerText.text += ".";
        }

        mc.finalTimer.SetActive(false);

        int p1 = Players[0].Score;
        int p2 = Players[1].Score;

        if (p1 > p2)
        {
            mc.bluewins.SetActive(true);
        }
        else if (p1 == p2)
        {
            mc.draw.SetActive(true);
        }
        else
        {
            mc.redwins.SetActive(true);

        }

        yield return new WaitForSeconds(5f);
        hasEnded = true;
        _restartButton.SetActive(true);
        _quitButton.SetActive(true);
    }
}
