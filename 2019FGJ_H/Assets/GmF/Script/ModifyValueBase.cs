using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemBase))]
public class ModifyValueBase : MonoBehaviour
{
    object modifyValue;
    public virtual object ModifyVlue
    {
        get
        {
            return modifyValue;
        }
        set
        {
            modifyValue = value;
        }
    }
    ModifyValueEnum modifyKye;
    public virtual ModifyValueEnum ModifyKey
    {
        get
        {
            return modifyKye;
        }
        set
        {
            modifyKye = value;
        }
    }
    public virtual string ModifyKeyString
    {
        get
        {
            return System.Enum.GetName(typeof(ModifyValueEnum), ModifyKey);
        }
    }
}

[System.Flags]
public enum ModifyValueEnum
{
    MoveSpeed   = 1,
    RopeSpeed      = 2,
    RopeLenght     = 4,
}
