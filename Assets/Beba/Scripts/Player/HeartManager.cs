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
        [SerializeField] private FloatValue playerCurrentHealth;

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

        public void UpdateHearts()
        {
            float tempHealth = playerCurrentHealth.RunTimeValue / 2;
            for (int i = 0; i < heartContainers.InitialValue; i++)
            {
                if(i <= tempHealth-1)
                {
                    //Full heart
                    hearts[i].sprite = fullHeart;

                }else if (i>= tempHealth)
                {
                    //empty heart
                    hearts[i].sprite = emptyHeart;
                }
                else
                {
                    //half full heart
                    hearts[i].sprite = halfHeart;
                }
            }
        }

    }

}