using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyFloat : ModifyValueBase
{
    public ModifyValueEnum key;
    public float value;

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
