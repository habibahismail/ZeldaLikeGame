using UnityEngine;

namespace bebaSpace {

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item")]
    public class Item : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemDescription;
        public bool isKey;
    }

}
