using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class RoomEnemyReactivator : MonoBehaviour
    {
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

        public void ReactivateEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].gameObject.activeInHierarchy == false)
                {
                    enemies[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
