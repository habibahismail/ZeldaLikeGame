using UnityEngine;

namespace bebaSpace
{
    public class PlayerHealth : Health, IDamageable
    {
        [Header("Event Signals")]
        [SerializeField] private Signal playerHealthSignal;

        public void TakeDamage(float damageAmount)
        {
            Damage(damageAmount);
            playerHealthSignal.Raise();  
        }

        //temp hack function to revive the player in testing.
        public void RevivePlayer()
        {
            health.RunTimeValue = health.InitialValue;
            playerHealthSignal.Raise();
        }

        public float GetCurrentHealth()
        {
            return health.RunTimeValue;
        }
    }
}
