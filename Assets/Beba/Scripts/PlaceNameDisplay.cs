using System.Collections;
using UnityEngine;
using TMPro;

namespace bebaSpace
{
    public class PlaceNameDisplay : MonoBehaviour
    {
        [SerializeField] private bool needPlaceName;

        [SerializeField] private TMP_Text placeNamePlaceholder;
        [SerializeField] private CanvasGroup placeNameCanvas;
        [SerializeField] private string placeNameText;

        readonly float duration = 0.3f; // This will be your time in seconds.
        readonly float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother.

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vector3 newCameraBoundPos = Vector3.zero;

            if (collision.CompareTag("Player"))
            {
                if (needPlaceName)
                {
                    placeNamePlaceholder.text = placeNameText;
                    StartCoroutine(FadeInOutText());
                }

            }
        }

        IEnumerator FadeInOutText()
        {
            float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
            float increment = smoothness / duration; //The amount of change to apply.

            float startvalue = 0;
            float endvalue = 1;

            while (progress < 1)
            {
                placeNameCanvas.alpha = Mathf.Lerp(startvalue, endvalue, progress);
                progress += increment;

                yield return new WaitForSeconds(smoothness);
            }

            yield return new WaitForSeconds(1.5f);

            startvalue = 1;
            endvalue = 0;
            progress = 0;

            while (progress < 1)
            {
                placeNameCanvas.alpha = Mathf.Lerp(startvalue, endvalue, progress);
                progress += increment;

                yield return new WaitForSeconds(smoothness);
            }
        }

    }

}
