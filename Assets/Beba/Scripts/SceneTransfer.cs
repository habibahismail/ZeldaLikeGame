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
        
                while (!asyncOperation.isDone)
                {
                yield return null;
                }

            }

        }
    }
}
