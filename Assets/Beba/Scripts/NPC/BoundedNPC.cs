using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{

    public class BoundedNPC : Interactables
    {
        [SerializeField] private Collider2D bound;
        [SerializeField] private float speed = 5;
        [Space]
        [SerializeField] private float minMoveTime, maxMoveTime;
        [Space]
        [SerializeField] private float minWaitTime, maxWaitTime;
       
        private Vector3 direction;
        private Transform npcTransform;
        private Rigidbody2D rb;
        private Animator animator;

        private bool isMoving;
        private bool canMove = true;
        private float moveTimeSeconds;
        private float waitTimeSeconds;

        private void Start()
        {
            animator = GetComponent<Animator>();
            npcTransform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody2D>();

            moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);

            ChangeDirection();
        }

        protected override void Update()
        {
            base.Update();

            if (isMoving)
            {
                moveTimeSeconds -= Time.deltaTime;

                if (playerInRange)
                {
                    canMove = false;
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    if (moveTimeSeconds <= 0)
                    {
                        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime); ;
                        isMoving = false;

                        UpdateAnimation();
                        animator.SetBool("isWalking", false);
                    }

                    canMove = true;
                }
                
                if(canMove)
                    Move();
           
            }
            else
            {
                waitTimeSeconds -= Time.deltaTime;

                if(waitTimeSeconds <= 0)
                {
                    ChooseDifferentDirection();
                    isMoving = true;

                    UpdateAnimation();
                    animator.SetBool("isWalking", true);

                    waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime); ;
                }
            }

        }

        private void UpdateAnimation()
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }

        private void Move()
        {
                direction.Normalize();
                Vector3 pos = npcTransform.position + speed * Time.deltaTime * direction;

                if (bound.bounds.Contains(pos))
                {
                    rb.MovePosition(pos);
                }
                else
                {
                    ChangeDirection();
                }
        }

        private void ChangeDirection()
        {
            int _direction = Random.Range(0, 4);

            switch (_direction)
            {
                case 0:
                    //walk right
                    direction = Vector3.right;
                    break;

                case 1:
                    //walk up
                    direction = Vector3.up;
                    break;

                case 2:
                    //walk left
                    direction = Vector3.left;
                    break;
                case 3:
                    //walk down
                    direction = Vector3.down;
                    break;
                default: break;

            }

            UpdateAnimation();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ChooseDifferentDirection();
        }

        private void ChooseDifferentDirection()
        {
            Vector3 _direction = direction;
            ChangeDirection();
            int loops = 0;

            while (_direction == direction && loops < 100)
            {
                loops++;
                ChangeDirection();
            }
        }
    }
}

