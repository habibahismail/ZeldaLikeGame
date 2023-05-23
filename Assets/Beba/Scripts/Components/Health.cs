using UnityEngine;

namespace bebaSpace
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected FloatValue health;

        public virtual void Heal(float amountToHeal)
        {
            float currentHealth = health.RunTimeValue + amountToHeal;
            
             if(currentHealth > health.InitialValue)
            {
                health.RunTimeValue = health.InitialValue;
            }
        }

        public virtual void FullHeal()
        {
            health.RunTimeValue = health.InitialValue;
        }

        public virtual float Damage(float damageAmount)
        {
            float currentHealth = health.RunTimeValue - damageAmount;

            if (currentHealth < 0)
                health.RunTimeValue = 0;
            else
                health.RunTimeValue = currentHealth;

            return currentHealth;
        }

        public virtual void InstantDeath()
        {
            health.RunTimeValue = 0; 
        }

    }
}
