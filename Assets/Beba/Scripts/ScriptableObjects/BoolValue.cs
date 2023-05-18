using UnityEngine;


namespace bebaSpace
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New BoolValue", menuName = "RPG/Bool Value")]
    public class BoolValue : ScriptableObject
    {
        public bool InitialValue;
        public bool RuntimeValue;

       // private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    }

}