﻿using UnityEngine;

namespace bebaSpace {

    [CreateAssetMenu(fileName = "New Vector Value", menuName = "RPG/Vector Value")]
    public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 InitialValue;
    public Vector2 DefaultValue;

    public void OnAfterDeserialize()
    {
        InitialValue = DefaultValue;
    }

    public void OnBeforeSerialize()
    {
       
    }
}
}
