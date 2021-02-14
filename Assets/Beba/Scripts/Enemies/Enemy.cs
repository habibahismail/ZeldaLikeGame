using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected string enemyName;
        [SerializeField] protected int baseAttack;
        [SerializeField] protected float moveSpeed = 1.5f;

        public EnemyState CurrentState { get; set; }

        protected virtual void Start()
        {
            CurrentState = EnemyState.Idle;
        }

        protected void ChangeState(EnemyState newState)
        {
            if(CurrentState != newState)
            {
                CurrentState = newState;
            }
        }

        private IEnumerator KnockbackEnemy(Rigidbody2D rb, float knockTime)
        {
            if (rb != null)
            {
                yield return new WaitForSeconds(knockTime);

                rb.GetComponent<Enemy>().CurrentState = EnemyState.Idle;
                rb.velocity = Vector2.zero;
            }
        }

        public void Knockback(Rigidbody2D rb, float knockTime)
        {
            StartCoroutine(KnockbackEnemy(rb, knockTime));
        }
    }

    public enum EnemyState { Idle, Walk, Attack, Stagger }

}