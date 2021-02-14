using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D playerRigidBody;
        private Animator animator;
        private Vector3 change;

        private PlayerState playerstate;
        private bool updateMove;

        public PlayerState Playerstate { get => playerstate; set => playerstate = value; }

        private void Start()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            Playerstate = PlayerState.Idle;
            updateMove = false;

            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", -1);
        }

        private void Update()
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && Playerstate != PlayerState.Attack && Playerstate != PlayerState.Stagger)
            {
                StartCoroutine(Attack());

            }else if (Playerstate == PlayerState.Walk || Playerstate == PlayerState.Idle)
            {
                updateMove = true;
            }

        }

        private void FixedUpdate()
        {
            if (updateMove && Playerstate != PlayerState.Attack)
            {
                UpdateAnimationAndMove();
                updateMove = false;
            }
        }

        private void UpdateAnimationAndMove()
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("isMoving", true);
                playerstate = PlayerState.Walk;
            }
            else
            {
                animator.SetBool("isMoving", false);
                playerstate = PlayerState.Idle;
            }
        }

        private void MoveCharacter()
        {
            change.Normalize();
            playerRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
        }

        private IEnumerator Attack()
        {
            animator.SetTrigger("attack");
            Playerstate = PlayerState.Attack;
            yield return new WaitForSeconds(0.3f);
            Playerstate = PlayerState.Walk;
        }

        private IEnumerator Knockback(float knockTime)
        {
            if (playerRigidBody != null)
            {
                yield return new WaitForSeconds(knockTime);

                Playerstate = PlayerState.Idle;
                playerRigidBody.velocity = Vector2.zero;
            }
        }



        public void KnockbackPlayer(float knockTime)
        {
            StartCoroutine(Knockback(knockTime));
        }
    }

    public enum PlayerState { Idle, Walk, Attack, Interact, Stagger }
}
