using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class SpPotion : PowerUp
    {


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                powerUpSignal.Raise();
                Destroy(gameObject);
            }
                


        }

    }

}