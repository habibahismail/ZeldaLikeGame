using System.Collections;
using UnityEngine;
using TMPro;


namespace bebaSpace
{
    public class Interactables : MonoBehaviour
    {
        [SerializeField] protected Signal contextSignal;

        [Header("Dialog settings")]
        [SerializeField] protected bool needDialog = false;
        [SerializeField] protected TMP_Text dialogLinePlaceholder;
        [SerializeField] private CanvasGroup dialogCanvas;
        [SerializeField] protected string dialogText;

        [SerializeField] protected bool playerInRange = false;
        [SerializeField] protected bool isInteracting = false;
        protected bool dialogBoxIsShowing = false;

        readonly float duration = 0.3f; // This will be your time in seconds.
        readonly float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother.


        protected virtual void Update()
        {
            if (Input.anyKey && dialogBoxIsShowing)
            {
                StartCoroutine(ShowDialogBox(false));
                dialogLinePlaceholder.text = "";
                isInteracting = false;
                dialogBoxIsShowing = false;

            }

            if (playerInRange && Input.GetButtonDown("Action") && !isInteracting)
            {
                isInteracting = true;
                DoAction();
            }
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerInRange = true;
                contextSignal.Raise();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerInRange = false;
                contextSignal.Raise();
            }
        }

        protected virtual void DoAction() 
        {
            if (needDialog)
            {
                StartCoroutine(ShowDialogBox(true));
                dialogLinePlaceholder.text = dialogText;
            }
        }

        protected IEnumerator ShowDialogBox(bool wantToShow)
        {
            float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
            float increment = smoothness / duration; //The amount of change to apply.

            float startvalue = 0;
            float endvalue = 1;

            if (!wantToShow)
            {
                startvalue = 1;
                endvalue = 0;
            }

            while (progress < 1)
            {
                dialogCanvas.alpha = Mathf.Lerp(startvalue, endvalue, progress);
                progress += increment;

                if(progress > 0.9 && wantToShow)
                {
                    dialogBoxIsShowing = true;
                }

                yield return new WaitForSeconds(smoothness);
            }
        }

    }
}
