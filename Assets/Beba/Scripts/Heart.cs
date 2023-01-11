using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class Heart : PowerUp
    {
        [SerializeField] private FloatValue playerHealth;
        [SerializeField] private FloatValue heartContainers;
        [SerializeField] private float amountToIncrease;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerHealth.RunTimeValue += amountToIncrease;
                if(playerHealth.InitialValue > heartContainers.RunTimeValue * 2f)
                {
                    playerHealth.InitialValue = heartContainers.RunTimeValue * 2f;
                }
                powerUpSignal.Raise();
                Destroy(gameObject);
            }
        }
    }
}
