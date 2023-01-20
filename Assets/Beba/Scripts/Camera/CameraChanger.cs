using UnityEngine;

namespace bebaSpace
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private GameObject virtualCamera;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                virtualCamera.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                virtualCamera.SetActive(false);
            }
        }
    }
}
