using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyBool : ModifyValueBase
{
    public ModifyValueEnum key;
    public bool value;

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
