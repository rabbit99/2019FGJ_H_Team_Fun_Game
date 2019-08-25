using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, INotification
{
    public GameOverUI GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        AddNotificationObserver();
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
            Debug.Log("GameOver");
            if(GameOverUI != null)
                GameOverUI.gameObject.SetActive(true);
            Hook hook = (Hook)_noti.sender;
            Debug.Log("sender = " + hook.m_player.playerTransform.gameObject.name + "  被打到的是"+(string)_noti.data);
            
        }
    }
    #endregion

}
