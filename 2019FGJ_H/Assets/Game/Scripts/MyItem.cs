using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem : MonoBehaviour, INotification
{
    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.Default.AddObserver(this,"OH");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnNotify(Notification eventdata){

    }
}
