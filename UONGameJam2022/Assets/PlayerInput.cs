using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    MasterInput input;

    private void Awake()
    {
        input = new MasterInput();

        input.Gameplay.Left.performed += MoveLeft;
        input.Gameplay.Right.performed += MoveRight;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void MoveLeft(InputAction.CallbackContext context)
    {

    }

    private void MoveRight(InputAction.CallbackContext context)
    {

    }
}
