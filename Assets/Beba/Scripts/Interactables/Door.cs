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
        [SerializeField] private float openingSpeed = 1f;

        [Space]
        [SerializeField] private Inventory playerInventory;

        private bool isOpening;
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

        protected override void Update()
        {
            base.Update();

            Vector3 currentPos = theDoor.transform.localPosition;

            if (isOpen && isOpening)
            {
                if(currentPos.y < openYpos)
                {
                    theDoor.transform.localPosition = Vector3.Lerp(currentPos,
                        new Vector3(currentPos.x, openYpos, currentPos.z), openingSpeed * Time.deltaTime);
    
                }

                if (Mathf.Ceil(theDoor.transform.localPosition.y) > openYpos )
                    isOpening = false;

            }
            else if(!isOpen && isOpening)
            {
                if (currentPos.y > closeYpos)
                {
                    Debug.Log("close sesame.");
                    theDoor.transform.position = Vector3.Lerp(currentPos,
                        new Vector3(currentPos.x, closeYpos, currentPos.z), openingSpeed * Time.deltaTime);
                }

                if (closeYpos <= Mathf.Ceil(theDoor.transform.localPosition.y)) //macam salah nanti check balik
                    isOpening = false;
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
            isOpening = true;
            
            theDoorCollider.enabled = false;
        }

        private void CloseDoor()
        {
            isOpen = false;
            isOpening = true;
            theDoorCollider.enabled = true;
            
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
            isOpen = true;
            DoAction();
        }


    }
}
