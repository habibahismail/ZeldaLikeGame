using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public enum DoorType { KEY, ENEMY, BUTTON }

    public class Door : Interactables
    {
        [Header("Door Settings")]
        [SerializeField] private bool isOpen = false;
        [SerializeField] private DoorType doorType;
        [SerializeField] private Inventory playerInventory;

        private Animator animator;
        private BoxCollider2D theDoorCollider;

        private void Start()
        {
            animator = GetComponentInParent<Animator>();
            theDoorCollider = transform.parent.GetComponent<BoxCollider2D>();

            if (isOpen)
            {
                OpenDoor();
            }
        }

        protected override void DoAction()
        {
            switch (doorType)
            {
                case DoorType.KEY:

                    if (!isOpen && playerInventory.numberOfKeys == 0)
                    {
                        dialogText = "The door is locked. There must be a key somewhere around here.";
                        ShowDialog();
                    }
                    else
                    {
                        OpenDoor();
                        playerInventory.numberOfKeys--;
                        contextSignal.Raise();
                    }
                    
                    
                    break;
                case DoorType.ENEMY: break;
                case DoorType.BUTTON: break;
                default: break;
            }

        }

        private void ShowDialog()
        {
            if (needDialog)
            {
                StartCoroutine(ShowDialogBox(true));
                dialogLinePlaceholder.text = dialogText;
            }
        }

        private void OpenDoor()
        {
            animator.SetBool("doorOpen", true);
            theDoorCollider.enabled = false;
            isOpen = true;
        }

        private void CloseDoor()
        {
            animator.SetBool("doorOpen", false);
            theDoorCollider.enabled = true;
            isOpen = false;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
            {
                playerInRange = true;
                contextSignal.Raise();
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
            {
                playerInRange = false;
                contextSignal.Raise();
            }
        }


    }
}
