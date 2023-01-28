using UnityEngine;

namespace bebaSpace
{
    public class DungeonLockedEnemies : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private Door[] doors;

        private int enemyCount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                CloseDoors();
                enemyCount = enemies.Length;

                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i],true);
                }
            }
        }

        private void ChangeActivation(Component component, bool activation)
        {
            component.gameObject.SetActive(activation);
        }

        public void CheckEnemies()
        {
            enemyCount--;

            if (enemyCount == 0)
                OpenDoors();
        }

        private void OpenDoors()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].OpenTheDoor();
            }
        }

        private void CloseDoors()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].CloseTheDoor();
            }
        }
    }
}
