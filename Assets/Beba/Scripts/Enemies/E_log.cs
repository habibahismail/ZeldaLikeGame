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

                    rb.MovePosition(movePosition);
                    ChangeState(EnemyState.Walk);
                }
            }
        }

    }
}
