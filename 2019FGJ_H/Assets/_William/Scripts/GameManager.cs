using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour, INotification
{
    public GameOverUI GameOverUI;
    public ThankMessage m_ThankMessage;

    // Start is called before the first frame update
    void Start()
    {
        AddNotificationObserver();
        GameOverUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
