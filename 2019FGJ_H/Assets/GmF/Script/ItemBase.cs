using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerBase))]
[RequireComponent(typeof(ModifyValueBase))]
public class ItemBase : TriggerBase
{
    public ModifyValueBase modifyData;

    private void Awake()
    {
        modifyData = gameObject.GetComponent<ModifyValueBase>();
        SendData = modifyData.ModifyVlue;
        KeyName = modifyData.ModifyKeyString;
    }
}
