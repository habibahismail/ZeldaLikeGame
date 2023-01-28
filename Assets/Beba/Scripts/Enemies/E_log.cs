using UnityEngine;

namespace bebaSpace
{
    public class E_log : MeleeEnemy
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            animator.SetBool("wakeUp", true);
        }

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

                    animator.SetBool("wakeUp", true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                animator.SetBool("wakeUp", false);
            }

        }

    }
}
