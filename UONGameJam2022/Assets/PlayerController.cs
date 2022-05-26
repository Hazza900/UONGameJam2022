using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ID { get; set; }
    public int Score { get; set; }

    public bool Ready { get; set; }
    public bool immune;

    public GameObject prefab;
    public Transform[] rails;

    public void InitPawn()
    {
        Debug.Log(GameController.instance.bottomPlayer[1].name);

        if (ID == 1)
        {
            GameObject go = Instantiate(prefab, GameController.instance.bottomPlayer[1].position, Quaternion.identity, transform);
            go.GetComponent<PlayerMovement>().railPositions = GameController.instance.bottomPlayer;
        }
        else
        {
            GameObject go = Instantiate(prefab, GameController.instance.topPlayer[1].position, Quaternion.identity, transform);
            go.GetComponent<PlayerMovement>().railPositions = GameController.instance.topPlayer;
        }

    }
}
