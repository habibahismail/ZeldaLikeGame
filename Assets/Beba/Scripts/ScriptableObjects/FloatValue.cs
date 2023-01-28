using UnityEngine;

namespace bebaSpace
{
    [CreateAssetMenu(fileName = "New Float Value", menuName = "RPG/Float Value")]
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
