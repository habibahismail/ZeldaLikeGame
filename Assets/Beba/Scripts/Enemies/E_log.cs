using UnityEngine;

namespace bebaSpace
{
    public class E_log : Enemy
    {

        [SerializeField] private Transform homePosition;
        [SerializeField] private float chaseRadius = 4f;
        [SerializeField] private float attackRadius = 2f;

        private Rigidbody2D rb;

        private Transform target;

        protected override void Start()
        {
            base.Start();

            target = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckDistance();
        }

        private void CheckDistance()
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

        private void ChangeAnim(Vector2 direction)
        {
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0)
                {
                    SetAnimFloat(Vector2.right);

                }else if(direction.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }


            }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    SetAnimFloat(Vector2.up);
                }
                else if (direction.y < 0)
                {
                    SetAnimFloat(Vector2.down);
                }
            }
        }

        private void SetAnimFloat(Vector2 value)
        {
            value = value.normalized;
            animator.SetFloat("moveX", value.x);
            animator.SetFloat("moveY", value.y);
        }

    }
}
