using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject waitScreen;

    public void RemoveConnectionScreen()
    {
        waitScreen.SetActive(false);
    }
}
