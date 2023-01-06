using UnityEngine;

namespace bebaSpace
{
    [CreateAssetMenu]
    public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public float InitialValue;

        [HideInInspector]
        public float RunTimeValue;

        public void OnAfterDeserialize()
        {
            RunTimeValue = InitialValue;
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
