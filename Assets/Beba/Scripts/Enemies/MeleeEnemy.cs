using UnityEngine;

namespace bebaSpace
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] protected float chaseRadius = 4f;
        [SerializeField] protected float attackRadius = 2f;

        protected Rigidbody2D rb;
        protected Transform target;


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


        protected virtual void CheckDistance(){ }

        protected void ChangeAnim(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    SetAnimFloat(Vector2.right);

                }
                else if (direction.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }


            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
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
