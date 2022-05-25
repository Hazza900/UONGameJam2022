using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    PlayerInputManager pim;

    // Start is called before the first frame update
    void Start()
    {
        pim = GetComponent<PlayerInputManager>();

        pim.onPlayerJoined += test;
    }


    public void test(PlayerInput pi)
    {
        Debug.Log(pi.playerIndex);
    }
}
