using UnityEngine;

namespace bebaSpace
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Float Value", menuName = "RPG/Float Value")]
    public class FloatValue : ScriptableObject
    {
        public float InitialValue;
        public float RunTimeValue;

    }
}
