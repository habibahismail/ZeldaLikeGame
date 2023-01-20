using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class Coin : PowerUp
    {
        [SerializeField] private Inventory playerInventory;

        private void Start()
        {
            powerUpSignal.Raise();   
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerInventory.coins += 1;
                powerUpSignal.Raise();
                Destroy(gameObject);
            }
        }
    }
}
