using System.Collections;
using UnityEngine;

namespace bebaSpace
{

    public enum DoorType { KEY, ENEMY, BUTTON }

    public class Door : Interactables
    {
        [Header("Door Settings")]
        [SerializeField] private bool isOpen = false;
        [SerializeField] private DoorType doorType;
        [SerializeField] private float closeYpos, openYpos;
        [SerializeField] private float doorAnimationDuration = 1f;

        [Space]
        [SerializeField] private Inventory playerInventory;

        //private bool isOpening;
        private GameObject theDoor;
        private BoxCollider2D theDoorCollider;

        private void Start()
        {
            theDoor = transform.parent.gameObject;
            theDoorCollider = theDoor.GetComponent<BoxCollider2D>();

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
                case DoorType.ENEMY:

                    //do nothing here.

                    break;
                case DoorType.BUTTON:

                    if (!isOpen)
                    {
                        dialogText = "The door won't open.";
                        ShowDialog();
                    }
                    else
                    {
                        OpenDoor();
                    }

                    break;
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
            isOpen = true;

            float xPos = theDoor.transform.localPosition.x;
            Vector2 positionToMoveTo = new Vector2(xPos, openYpos);
            StartCoroutine(LerpPosition(positionToMoveTo, doorAnimationDuration));

            theDoorCollider.enabled = false;
        }

        private void CloseDoor()
        {
            isOpen = false;

            float xPos = theDoor.transform.localPosition.x;
            Vector2 positionToMoveTo = new Vector2(xPos, closeYpos);
            StartCoroutine(LerpPosition(positionToMoveTo, doorAnimationDuration));

            theDoorCollider.enabled = true;

            //disable the door trigger collider.
            if (doorType == DoorType.ENEMY)
                    gameObject.GetComponent<Collider2D>().enabled = false; 
           
        }

        private IEnumerator LerpPosition(Vector2 targetPosition, float duration)
        {
            float time = 0;
            Vector2 startPosition = theDoor.transform.localPosition;
            while (time < duration)
            {
                theDoor.transform.localPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
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

        public void OpenTheDoor()
        {
            if (doorType == DoorType.ENEMY)
            {
                OpenDoor();
            }
            else
            {
                isOpen = true;
                DoAction();

            }

        }

        public void CloseTheDoor()
        {
            CloseDoor();
        }


    }
}
