using UnityEngine;

namespace bebaSpace
{
    public class Heart : PowerUp
    {
        [SerializeField] private FloatValue currentHealth;
        [SerializeField] private float amountToHeal;

        private PlayerHealth playerHealth;

        private void Start()
        {
            playerHealth = GameObject.Find("PlayerStats").GetComponent<PlayerHealth>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerHealth.Heal(amountToHeal);

                powerUpSignal.Raise();
                Destroy(gameObject);
            }
        }
    }
}
