using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    [System.Serializable]
    public class Loot
    {
        public PowerUp thisLoot;
        public int lootChance;
    }

    [CreateAssetMenu(fileName = "New LootTable", menuName = "RPG/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private Loot[] loots;

        private PowerUp LootPowerUp()
        {
            int cumProb = 0;
            int currentProb = Random.Range(0, 100);

            for (int i = 0; i < loots.Length; i++)
            {
                cumProb += loots[i].lootChance;
                if(currentProb <= cumProb)
                {
                    return loots[i].thisLoot;
                }
            }

            return null;
        }

        public void GenerateLoot(LootTable _lootTable, Transform _lootPosition)
        {
            if (_lootTable != null)
            {
                PowerUp currentPowerUp = _lootTable.LootPowerUp();
                if (currentPowerUp != null)
                {
                    Instantiate(currentPowerUp.gameObject, _lootPosition.position, Quaternion.identity);
                }
            }
        }
    }
}
