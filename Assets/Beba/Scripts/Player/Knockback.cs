using UnityEngine;

namespace bebaSpace
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private float thrust = 5f;
        [SerializeField] private float knockTime = 0.4f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
            {
                Rigidbody2D hit = collision.attachedRigidbody;

                if(hit != null)
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;

                    hit.AddForce(difference, ForceMode2D.Impulse);

                    if (hit.CompareTag("Enemy")) {

                        Enemy enemy = hit.gameObject.GetComponent<Enemy>();

                        if (enemy.isActiveAndEnabled)
                        {
                            enemy.CurrentState = EnemyState.Stagger;
                            enemy.Knockback(hit, knockTime);
                        }
                        
                    }

                    if (hit.CompareTag("Player"))
                    {
                        PlayerMovement player = hit.gameObject.GetComponent<PlayerMovement>();

                        player.Playerstate = PlayerState.Stagger;
                        player.KnockbackPlayer(knockTime);
                        //TakeDamage(collision);
                    }
                    
                }
            }

        }

        private void TakeDamage(Collider2D collider)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                if (collider.CompareTag("Player") && collider.isTrigger)
                {
                    damageable.TakeDamage(1);
                }

            }
        }

       
    }
}
