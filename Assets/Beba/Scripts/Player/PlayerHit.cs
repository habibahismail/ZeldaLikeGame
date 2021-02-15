using UnityEngine;

namespace bebaSpace
{
    public class PlayerHit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {

            IDamageable damageable = collision.GetComponent<IDamageable>();

            if (damageable != null)
            {

                if(collision.CompareTag("Breakable") || (collision.CompareTag("Enemy") && collision.isTrigger))
                    damageable.TakeDamage(1);

            }


        }
    }
}

    

