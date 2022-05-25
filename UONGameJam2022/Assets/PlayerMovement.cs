using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    private InputActionMap iaa;

    public Transform[] railPositions;
    private int railIndex = 1;

    [SerializeField] private float swapRailDuration;
    private Vector3 origin;
    private Vector3 target;
    private float t = 0;
    private bool moving;

    private void Start()
    {
        input = transform.parent.GetComponent<PlayerInput>();
        iaa = input.actions.FindActionMap("Gameplay");
        iaa.FindAction("Left").performed += Left;
        iaa.FindAction("Right").performed += Right;
    }

    private void Left(InputAction.CallbackContext context)
    {
        if (moving)
            return;

        if (railIndex > 0)
        {
            MoveBetweenRail(railPositions[--railIndex].position);
        }  
    }

    private void Right(InputAction.CallbackContext context)
    {
        if (moving)
            return;

        if (railIndex < 2)
        {
            MoveBetweenRail(railPositions[++railIndex].position);
        }
    }

    private void MoveBetweenRail(Vector3 pos)
    {
        moving = true;
        origin = transform.position;
        target = pos;
    }

    private void Update()
    {
        if (moving)
        {
            if (t < swapRailDuration)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(origin, target, t / swapRailDuration);
            }
            else
            {
                moving = false;
                t = 0;
            }
        }
    }
}
