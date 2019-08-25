﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text winner;
    public GameObject WinA;
    public GameObject WinB;
    public ThankMessage m_ThankMessage;
    public GameObject Body;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWinnerText(string player_name)
    {
        winner.text = player_name;
        switch(player_name)
        {
            case "Player 1":
                WinA.SetActive(true);
                WinB.SetActive(false);
                break;
            case "Player 2":
                WinA.SetActive(false);
                WinB.SetActive(true);
                break;
        }
        StartCoroutine(ShowThankMessage());
    }

    IEnumerator ShowThankMessage()
    {
        yield return new WaitForSeconds(3);
        m_ThankMessage.gameObject.SetActive(true);
        Body.SetActive(false);
    }
}
