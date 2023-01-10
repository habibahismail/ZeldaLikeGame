using UnityEngine;

namespace bebaSpace
{
    public class E_patrolLog : E_log
    {
        [SerializeField] private Transform[] path;
        [SerializeField] private int currentPoint;

        [SerializeField]  private float roundingDistance = 0.2f;


        private Transform currentGoal;

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
                    //ChangeState(EnemyState.Walk);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
                {

                    Vector3 movePosition = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);

                    ChangeAnim(movePosition - transform.position);
                    rb.MovePosition(movePosition);
                }
                else
                {
                    ChangeGoal();
                }
            }

        }

        private void ChangeGoal()
        {
            if(currentPoint == path.Length - 1)
            {
                currentPoint = 0;
                currentGoal = path[0];
            }
            else
            {
                currentPoint++;
                currentGoal = path[currentPoint];
            }
        }
    }
}
