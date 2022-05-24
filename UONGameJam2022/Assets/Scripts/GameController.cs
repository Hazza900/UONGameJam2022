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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerJoinedEvent(PlayerInput pi)
    {
        
    }

    public void PlayerLeftEvent()
    {

    }
}
