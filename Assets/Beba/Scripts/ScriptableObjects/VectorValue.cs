using UnityEngine;

namespace bebaSpace {

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Vector Value", menuName = "RPG/Vector Value")]
    public class VectorValue : ScriptableObject
{
    public Vector2 InitialValue;
    public Vector2 DefaultValue;

}
}
