using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace {

    [CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item")]
    public class Item : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemDescription;
        public bool isKey;
    }

}
