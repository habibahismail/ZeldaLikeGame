using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float health;
        [SerializeField] protected float moveSpeed = 1.5f;
        [SerializeField] protected string enemyName;
        [SerializeField] protected int baseAttack;
        [SerializeField] protected GameObject deathEffect;

        [SerializeField] protected FloatValue maxHealth;

        protected Animator animator;

        public EnemyState CurrentState { get; set; }

        protected virtual void Start()
        {
            CurrentState = EnemyState.Idle;
            animator = GetComponent<Animator>();
            health = maxHealth.InitialValue;
        }

        protected void ChangeState(EnemyState newState)
        {
            if(CurrentState != newState)
            {
                CurrentState = newState;
            }
        }
        
        private void DeathFX()
        {
            if(deathEffect != null)
            {
                GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.5f);
            }
        }

        private IEnumerator KnockbackEnemy(Rigidbody2D rb, float knockTime)
        {
            if (rb != null)
            {
                yield return new WaitForSeconds(knockTime);

                rb.velocity = Vector2.zero;
                rb.GetComponent<Enemy>().ChangeState(EnemyState.Idle);
                rb.velocity = Vector2.zero;

            }
        }

        public void Knockback(Rigidbody2D rb, float knockTime)
        {
            StartCoroutine(KnockbackEnemy(rb, knockTime));
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                DeathFX();
                gameObject.SetActive(false);
            }
        }
    }

    public enum EnemyState { Idle, Walk, Attack, Stagger }

}