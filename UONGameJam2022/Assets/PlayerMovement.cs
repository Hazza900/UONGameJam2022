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

    private bool leftDown;
    private bool rightDown;

    private void Start()
    {
        input = transform.parent.GetComponent<PlayerInput>();
        iaa = input.actions.FindActionMap("Gameplay");
        iaa.FindAction("Left").performed += Left;
        iaa.FindAction("Left").canceled += LeftUp;
        iaa.FindAction("Right").performed += Right;
        iaa.FindAction("Right").canceled += RightUp;
    }

    private void Left(InputAction.CallbackContext context)
    {
        leftDown = true;

        if (moving)
            return;

        if (railIndex > 0)
        {
            MoveBetweenRail(railPositions[--railIndex].position);
        }  
    }

    private void LeftUp(InputAction.CallbackContext context)
    {
        leftDown = false;
    }    

    private void Right(InputAction.CallbackContext context)
    {
        rightDown = true;

        if (moving)
            return;

        if (railIndex < 2)
        {
            MoveBetweenRail(railPositions[++railIndex].position);
        }
    }

    private void RightUp(InputAction.CallbackContext context)
    {
        rightDown = false;
    }

    private void MoveBetweenRail(Vector3 pos)
    {
        t = 0;
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
                if (leftDown && railIndex != 0)
                {
                    MoveBetweenRail(railPositions[--railIndex].position);
                }
                else if (rightDown && railIndex !=2)
                {
                    MoveBetweenRail(railPositions[++railIndex].position);
                }
                else
                {
                    moving = false;
                }
            }
        }
    }
}
