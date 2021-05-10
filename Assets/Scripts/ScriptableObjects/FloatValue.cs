using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]//lets you create this as an object with right click
public class FloatValue : ScriptableObject,ISerializationCallbackReceiver
{
    // Start is called before the first frame update
    public float initialValue;
    [NonSerialized] public float RuntimeValue;

    

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

}
