using UnityEngine;

namespace bebaSpace
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private float thrust = 5f;
        [SerializeField] private float knockTime = 0.4f;
        [SerializeField] private float damage = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
            {
                Rigidbody2D hit = collision.attachedRigidbody;

                if (hit != null)
                {
                    if (hit.CompareTag("Enemy")) {

                        Enemy enemy = hit.gameObject.GetComponent<Enemy>();

                        if (enemy.isActiveAndEnabled)
                        {
                            InitiateKnockback(hit);
                            enemy.CurrentState = EnemyState.Stagger;
                            enemy.Knockback(hit, knockTime);
                        }
                        
                    }

                    if (hit.CompareTag("Player") && damage != 0)
                    {
                        PlayerMovement player = hit.gameObject.GetComponent<PlayerMovement>();

                        if(player.Playerstate != PlayerState.Stagger)
                        {
                            InitiateKnockback(hit);
                            player.Playerstate = PlayerState.Stagger;
                            player.KnockbackPlayer(knockTime, damage);
                            
                        }

                    }
                    
                }
            }

        }

        private void InitiateKnockback(Rigidbody2D _rb)
        {
            Vector2 difference = _rb.transform.position - transform.position;
            difference = difference.normalized * thrust;

            _rb.AddForce(difference, ForceMode2D.Impulse);
        }

    }
}
