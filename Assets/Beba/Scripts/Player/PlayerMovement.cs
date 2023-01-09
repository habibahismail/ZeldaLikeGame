using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private FloatValue currentHealth;
        [SerializeField] private Signal playerHealthSignal;
        [SerializeField] private VectorValue startingPosition;
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private SpriteRenderer receivedItemSprite;

        private Rigidbody2D playerRigidBody;
        private Animator animator;
        private Vector3 change;

        [SerializeField] private PlayerState playerstate;
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

            transform.position = startingPosition.InitialValue;
        }

        private void Update()
        {
            if(playerstate == PlayerState.Interact) { return; } else
            {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");
                change.y = Input.GetAxisRaw("Vertical");

                if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Space) && Playerstate != PlayerState.Attack && Playerstate != PlayerState.Stagger)
                {
                    StartCoroutine(Attack());

                }
                else if (Playerstate == PlayerState.Walk || Playerstate == PlayerState.Idle)
                {
                    updateMove = true;
                }
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

            if(playerstate != PlayerState.Interact) 
            { 
            Playerstate = PlayerState.Walk;
            }
        }

        private IEnumerator Knockback(float knockTime)
        {
            if (playerRigidBody != null)
            {
                yield return new WaitForSeconds(knockTime);

                playerRigidBody.velocity = Vector2.zero;
                Playerstate = PlayerState.Idle;
                playerRigidBody.velocity = Vector2.zero;
            }
        }

        public void KnockbackPlayer(float knockTime, float damage)
        {

            currentHealth.RunTimeValue -= damage;
            playerHealthSignal.Raise();

            if (currentHealth.RunTimeValue > 0)
            {
                
                StartCoroutine(Knockback(knockTime));
            }
            else
            {
                gameObject.SetActive(false);
            }
        } 

        public void RaiseItem()
        {
            if(playerInventory.currentItem != null)
            {
                if (playerstate != PlayerState.Interact)
                {
                    playerstate = PlayerState.Interact;
                    animator.SetBool("receiveItem", true);
                    receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
                }
                else
                {
                    animator.SetBool("receiveItem", false);
                    receivedItemSprite.sprite = null;
                    playerInventory.currentItem = null;

                    playerstate = PlayerState.Idle;
                  
                }
            }
            
        }
    }

    public enum PlayerState { Idle, Walk, Attack, Interact, Stagger }
}
