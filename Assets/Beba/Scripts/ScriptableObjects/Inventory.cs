﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace bebaSpace {

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "RPG/Inventory")]
    public class Inventory : ScriptableObject
    {
        public Item currentItem;
        public List<Item> items = new List<Item>();
        public int numberOfKeys;
        public int coins;

        public void AddItem(Item itemToAdd)
        {
            if (itemToAdd.isKey)
            {
                numberOfKeys++;
            }
            else
            {
                if (!items.Contains(itemToAdd))
                {
                    items.Add(itemToAdd);
                }
            }
        }

        public bool CheckInventoryIfItemExist(Item item)
        {
            if (items.Contains(item))
            {
                return true;
            }

            return false;
        }
    }

    
}
