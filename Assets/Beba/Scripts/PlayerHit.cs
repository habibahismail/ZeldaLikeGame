using UnityEngine;

namespace bebaSpace
{
    public class PlayerHit : MonoBehaviour
    {
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.TakeDamage(1);
            }
        }
    }
    
}
