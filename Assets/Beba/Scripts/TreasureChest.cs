using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace { 
public class TreasureChest : Interactables
{
        [SerializeField] private Item contents;
        [SerializeField] private Inventory playerInventory;  
        [SerializeField] private Signal raiseItem;

        [SerializeField] private bool isOpen;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (Input.anyKey && dialogBoxIsShowing)
            {
                StartCoroutine(ShowDialogBox(false));
                dialogLinePlaceholder.text = "";
                isInteracting = false;
                dialogBoxIsShowing = false;

                isOpen = true;
                raiseItem.Raise();

            }

            if (playerInRange && Input.GetButtonDown("Action") && !isInteracting)
            {
                isInteracting = true;
                DoAction();
            }
        }

        protected override void DoAction()
        {
            if (!isOpen)
            {
                if (needDialog)
                {
                    StartCoroutine(ShowDialogBox(true));
                    dialogLinePlaceholder.text = contents.itemDescription;
                }

                anim.SetTrigger("openChest");

                playerInventory.AddItem(contents);
                playerInventory.currentItem = contents;

                contextSignal.Raise();
                playerInRange = false;
                raiseItem.Raise();
            }

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
