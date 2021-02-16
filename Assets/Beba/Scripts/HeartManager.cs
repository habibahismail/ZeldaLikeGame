using UnityEngine;
using UnityEngine.UI;

namespace bebaSpace
{
    public class HeartManager : MonoBehaviour
    {
        [SerializeField] private Image[] hearts;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;
        [SerializeField] private FloatValue heartContainers;

        private void Start()
        {
            InitHearts();
        }

        public void InitHearts()
        {
            for (int i = 0; i < heartContainers.InitialValue; i++)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].gameObject.SetActive(true);
            }
        }

    }

}