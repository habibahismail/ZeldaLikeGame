using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class E_Brotherhood : MeleeEnemy
    {
        [SerializeField] private float attackDelay = 0.5f;

        protected override void CheckDistance()
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
                Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (CurrentState == EnemyState.Walk || CurrentState == EnemyState.Idle && CurrentState != EnemyState.Stagger)
                {
                    Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                    ChangeAnim(movePosition - transform.position);
                    rb.MovePosition(movePosition);

                    ChangeState(EnemyState.Walk);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
                Vector3.Distance(target.position, transform.position) <= attackRadius)
            {
                if (CurrentState == EnemyState.Walk || CurrentState == EnemyState.Idle
                    && CurrentState != EnemyState.Stagger)
                {
                    StartCoroutine(AttackCo());
                }
                    
            }

        }

        private IEnumerator AttackCo()
        {
            CurrentState = EnemyState.Attack;
            animator.SetBool("attack", true);

            yield return new WaitForSeconds(attackDelay);
            CurrentState = EnemyState.Walk;
            animator.SetBool("attack", false);

        }
    }
}
