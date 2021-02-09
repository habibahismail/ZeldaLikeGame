using System.Collections;
using UnityEngine;
using TMPro;


namespace bebaSpace
{
    public class Interactables : MonoBehaviour
    {
        [SerializeField] private TMP_Text dialogLinePlaceholder;
        [SerializeField] private CanvasGroup dialogCanvas;
        [SerializeField] private string dialogText;

        private bool playerInRange = false;
        private bool isInteracting = false;
        private bool dialogBoxIsShowing = false;

        readonly float duration = 0.3f; // This will be your time in seconds.
        readonly float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother.


        private void Update()
        {
            if (Input.anyKeyDown && dialogBoxIsShowing)
            {
                StartCoroutine(ShowDialogBox(false));
                dialogLinePlaceholder.text = "";
                isInteracting = false;
                dialogBoxIsShowing = false;

            }

            if (playerInRange && Input.GetButtonDown("Action") && !isInteracting)
            {
                isInteracting = true;

                StartCoroutine(ShowDialogBox(true));

                dialogLinePlaceholder.text = dialogText;
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        IEnumerator ShowDialogBox(bool wantToShow)
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
