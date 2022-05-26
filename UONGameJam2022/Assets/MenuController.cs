using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject logoScreen;
    public GameObject waitScreen;

    public GameObject finalTimer;
    public TextMeshProUGUI finalTimerText;

    public GameObject player1;
    public TextMeshProUGUI player1text;
    public GameObject player2;
    public TextMeshProUGUI player2text;

    public TextMeshProUGUI player1score;
    public TextMeshProUGUI player2score;

    public GameObject bluewins;
    public GameObject redwins;

    public void RemoveConnectionScreen()
    {
        waitScreen.SetActive(false);
    }
}
