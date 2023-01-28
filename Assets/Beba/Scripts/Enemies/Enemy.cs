using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [Header("Enemy Settings")]
        [SerializeField] protected string enemyName;
        [SerializeField] protected FloatValue maxHealth;
        [SerializeField] protected Vector2 homePosition;
        [SerializeField] protected float moveSpeed = 1.5f;
        [SerializeField] protected int baseAttack;

        [Space]
        [SerializeField] protected GameObject deathEffect;
        [SerializeField] protected Signal enemyDieSignal;
        [SerializeField] protected LootTable lootTable;

        protected Animator animator;
        protected float health;

        public EnemyState CurrentState { get; set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void OnEnable()
        {
            ResetEnemy();

            health = maxHealth.InitialValue;
            CurrentState = EnemyState.Idle;
        }

        protected virtual void Start() { }

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
                lootTable.GenerateLoot(lootTable, gameObject.transform);

                if(enemyDieSignal != null)
                    enemyDieSignal.Raise();
                
                gameObject.SetActive(false);
                
            }
        }

        public void ResetEnemy()
        {
            if (homePosition != null)
                transform.position = homePosition;
        }
    }

    public enum EnemyState { Idle, Walk, Attack, Stagger }

}