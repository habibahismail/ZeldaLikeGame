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


        private void Start()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            playerstate = PlayerState.Walk;
            updateMove = false;

            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", -1);
        }

        private void Update()
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && playerstate != PlayerState.Attack)
            {
                StartCoroutine(Attack());

            }else if (playerstate == PlayerState.Walk)
            {
                updateMove = true;
            }

        }

        private void FixedUpdate()
        {
            if (updateMove && playerstate != PlayerState.Attack)
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
            }
            else
            {
                animator.SetBool("isMoving", false);
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
            playerstate = PlayerState.Attack;
            yield return new WaitForSeconds(0.3f);
            playerstate = PlayerState.Walk;
        }
    }

    public enum PlayerState { Walk, Attack, Interact }
}
