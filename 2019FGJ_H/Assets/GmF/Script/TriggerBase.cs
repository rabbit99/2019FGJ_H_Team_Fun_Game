using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerBase : MonoBehaviour
{
    string keyName = null;
    object sendData = null;
    public bool TestOnTrigger;

    public virtual string KeyName
    {
        get
        {
            return keyName;
        }
        set
        {
            keyName = value;
        }
    }

    public virtual object SendData
    {
        get
        {
            return sendData;
        }
        set
        {
            sendData = value;
        }
    }
    public virtual void OnTrigger()
    {
        Debug.Log("TriggerBase.cs,  OnTrigger(), [key(" + keyName + ")], [data("+ SendData + ")]");
        NotificationCenter.Default.Post(gameObject, KeyName, SendData);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger();
    }

    public virtual void Update()
    {
        if (TestOnTrigger)
        {
            TestOnTrigger = false;
            OnTrigger();
        }
    }
}
