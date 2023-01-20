using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace bebaSpace
{
    [CreateAssetMenu]
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