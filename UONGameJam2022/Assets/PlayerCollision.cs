using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (controller.ID == 1)
        {
            GameController.instance.ScorePlayer(2);
        }
        else
        {
            GameController.instance.ScorePlayer(1);
        }

        
        //reduce score by one, give iframes
    }
}
