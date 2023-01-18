using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bebaSpace { 
    public class SceneTransfer : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private VectorValue playerStorage;
        [SerializeField] private Vector2 playerPosition;
        [SerializeField] private GameObject fadeInPanel;
        [SerializeField] private GameObject fadeOutPanel;
        [SerializeField] private float fadeWait = 0.3f;

        //[Header("Camera Confiner2D Bounds")]
        //[SerializeField] private Vector2 bounds;

        //private Transform cameraContainer;

        private void Awake()
        {
            if(fadeInPanel != null)
            {
                Transform panelParent = GameObject.Find("FadeCanvas").transform;
                GameObject panel = Instantiate(fadeInPanel, panelParent) as GameObject;
                Destroy(panel, 1f);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                playerStorage.InitialValue = playerPosition;
                StartCoroutine(nameof(FadeCo));
            }
        }

        private IEnumerator FadeCo()
        {
            if(fadeOutPanel != null) { 

                Transform panelParent = GameObject.Find("FadeCanvas").transform;

                Instantiate(fadeOutPanel, panelParent);
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

                //get the transform of CameraContainer
                //cameraContainer = GameObject.Find("CameraContainer").transform;
                //GameObject vcam = GameObject.Find("TopDownCamera");

                //Vector3 newBound = new Vector3(bounds.x, bounds.y, 10);
                //cameraContainer.transform.position = newBound;

                //vcam.GetComponent<CinemachineConfiner>().InvalidatePathCache();
                //Debug.Log("camera bounds: " + newBound);
                //Debug.Log("new camera bound: " + cameraContainer.transform.position);


                while (!asyncOperation.isDone)
                {
                yield return null;
                }


            }

        }
    }
}
