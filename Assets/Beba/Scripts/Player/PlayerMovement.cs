using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private FloatValue currentHealth;
        [SerializeField] private FloatValue currentSP;

        [Space]
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private VectorValue startingPosition;
        [SerializeField] private SpriteRenderer receivedItemSprite;

        [Header("Event Signals")]
        [SerializeField] private Signal cameraShake;
        [SerializeField] private Signal playerHealthSignal;
        [SerializeField] private Signal playerUseSpSignal;

        [Header("I-Frame")]
        [SerializeField] Color flashColor;
        [SerializeField] Color regularColor;
        [SerializeField] private Collider2D triggerCollider;
        [SerializeField] float flashDuration;
        [SerializeField] int numberOfFlashes;

        [Space]
        [SerializeField] private GameObject swordProjectile;


        private Rigidbody2D playerRigidBody;
        private Animator animator;
        private Vector3 change;

        private PlayerState playerstate;
        private bool updateMove;

        private SpriteRenderer spriteRenderer;

        public PlayerState Playerstate { get => playerstate; set => playerstate = value; }

        private void Start()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            

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
                else if (Input.GetButtonDown("SpecialAttack") && Playerstate != PlayerState.Attack && Playerstate != PlayerState.Stagger)
                {
                    StartCoroutine(SpecialAttack());
                }
                else if (Playerstate == PlayerState.Walk || Playerstate == PlayerState.Idle)
                {
                    updateMove = true;
                }

            }
        }

        private void FixedUpdate()
        {
            if (updateMove && Playerstate != PlayerState.Attack && playerstate != PlayerState.Interact)
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

                change.x = Mathf.Round(change.x);
                change.y = Mathf.Round(change.y);

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
            playerRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change);
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

        private IEnumerator SpecialAttack()
        {
            if(currentSP.RunTimeValue >= 3)
            {
                currentSP.RunTimeValue -= 3;
                playerUseSpSignal.Raise();

                animator.SetTrigger("attack");
                Playerstate = PlayerState.Attack;
                yield return null;

                InstantiateProjectile();

                if (playerstate != PlayerState.Interact)
                {
                    Playerstate = PlayerState.Walk;
                }

            }
        }

        private void InstantiateProjectile()
        {
            float _moveX = animator.GetFloat("moveX");
            float _moveY = animator.GetFloat("moveY");
            
            Vector2 temp = new Vector2(_moveX, _moveY );

            Projectile projectile = Instantiate(swordProjectile,
                transform.position, Quaternion.identity).GetComponent<Projectile>();

            projectile.Setup(temp, projectile.ProjectileDirection(_moveX, _moveY));
        }

        private IEnumerator Knockback(float knockTime)
        {
            if (playerRigidBody != null)
            {
                cameraShake.Raise();
                StartCoroutine(FlashCO());
                yield return new WaitForSeconds(knockTime);

                playerRigidBody.velocity = Vector2.zero;
                Playerstate = PlayerState.Idle;
                playerRigidBody.velocity = Vector2.zero;

            }
        }

        private IEnumerator FlashCO()
        {
            int temp = 0;
            triggerCollider.enabled = false;

            while(temp < numberOfFlashes)
            {
                spriteRenderer.color = flashColor;
                yield return new WaitForSeconds(flashDuration);
                spriteRenderer.color = regularColor;
                yield return new WaitForSeconds(flashDuration);

                temp++;
            }

            triggerCollider.enabled = true;
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
            if (playerInventory.currentItem != null)
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

        public void AddSP()
        {
            currentSP.RunTimeValue += 2;
        }

        //temp hack function to revive the player in testing.
        public void RevivePlayer()
        {
            currentHealth.RunTimeValue = currentHealth.InitialValue;
            Playerstate = PlayerState.Idle;
            updateMove = false;

            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", -1);

            playerHealthSignal.Raise();
        }
    }

    public enum PlayerState { Idle, Walk, Attack, Interact, Stagger }
}
