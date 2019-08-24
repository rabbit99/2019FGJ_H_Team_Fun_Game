using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyInt : ModifyValueBase
{
    public ModifyValueEnum key;
    public int value;

    public override ModifyValueEnum ModifyKey
    {
        get
        {
            return key;
        }
    }
    public override object ModifyVlue
    {
        get
        {
            return value;
        }
    }
}