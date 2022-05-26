using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private float iframetime;

    private void Start()
    {
        controller = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller.immune)
            return;

        if (controller.ID == 1)
        {
            GameController.instance.ScorePlayer(1);
        }
        else
        {
            GameController.instance.ScorePlayer(0);
        }
    }

    IEnumerator recovery()
    {
        controller.immune = true;
        yield return new WaitForSeconds(iframetime);
        controller.immune = false;
    }
}
