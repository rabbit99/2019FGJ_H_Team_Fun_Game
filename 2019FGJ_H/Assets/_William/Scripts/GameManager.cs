using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour, INotification
{
    public GameOverUI GameOverUI;
    public ThankMessage m_ThankMessage;

    private bool flag_over = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject player1ani;
    public GameObject player2ani;

    public Transform player1Transform;
    public Transform player2Transform;

    // Start is called before the first frame update
    void Start()
    {
        AddNotificationObserver();
        GameOverUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //    Reset_flag_over();
    }

    public void Reset_flag_over()
    {
        flag_over = false;

        player1.transform.localPosition = player1Transform.localPosition;
        player2.transform.localPosition = player2Transform.localPosition;
        Debug.Log("player1.transform.localPosition" + player1.transform.localPosition);
        Debug.Log("player1Transform.localPosition" + player1Transform.localPosition);
        player1ani.transform.localPosition = player1Transform.localPosition;
        player2ani.transform.localPosition = player2Transform.localPosition;
    }

    #region Notification

    void AddNotificationObserver()
    {
        NotificationCenter.Default.AddObserver(this, NotificationKeys.GameOver);
    }

    void RemoveNotificationObserver()
    {
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.GameOver);
    }

    public void OnNotify(Notification _noti)
    {
        if (_noti.name == NotificationKeys.GameOver)
        {
            if (flag_over)
                return;

            flag_over = true;

            //TO DO
            Debug.Log("GameOver " + (string)_noti.data);
            if(GameOverUI != null)
            {
                GameOverUI.gameObject.SetActive(true);
                GameOverUI.SetWinnerText((string)_noti.data);
            }
            m_ThankMessage.GetRandomThankMessage();
        }
    }
    #endregion

}
