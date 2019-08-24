using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerBase))]
[RequireComponent(typeof(ModifyValueBase))]
public class ItemBase : TriggerBase
{
    public System.Action<ItemBase> OnDeath = null;
    public ModifyValueBase modifyData;
    public float LifeTime = 5;
    private float overLifeTime = 0;

    private void Awake()
    {
        modifyData = gameObject.GetComponent<ModifyValueBase>();
        SendData = modifyData.ModifyVlue;
        KeyName = modifyData.ModifyKeyString;

        if(ItemManager.Instance != null)
        {
            if (ItemManager.Instance != null && !ItemManager.Instance.SampleItems.Contains(this))
            {
                ItemManager.Instance.aliveItems.Add(this);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
       
    }

    void Update()
    {
        CheckLife();
        overLifeTime += Time.deltaTime;
    }

    void CheckLife()
    {
        if (ItemManager.Instance != null && ItemManager.Instance.SampleItems.Contains(this))
        {
            return;
        }
        if (overLifeTime > LifeTime)
        {
            if (ItemManager.Instance != null)
            {
                ItemManager.Instance.aliveItems.Remove(this);
            }
            Destroy(gameObject);

            if(OnDeath != null)
            {
                OnDeath.Invoke(this);
            }
        }
    }
}
