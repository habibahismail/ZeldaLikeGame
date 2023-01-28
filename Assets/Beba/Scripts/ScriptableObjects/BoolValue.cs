using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace bebaSpace
{
    [CreateAssetMenu(fileName = "New BoolValue", menuName = "RPG/Bool Value")]
    public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public bool InitialValue;
        
        [HideInInspector]
        public bool RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize()
        {
            
        }
    }

}