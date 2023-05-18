using UnityEngine;
using UnityEngine.UI;

namespace bebaSpace
{

    public class SpManager : MonoBehaviour
    {
        [SerializeField] private Slider spSlider;
        [SerializeField] private FloatValue playerSP;
        [SerializeField] private float spIncreaseValue;
        [SerializeField] private float spDecreaseValue;

        private void Start()
        {
            spSlider.maxValue = playerSP.InitialValue;
            spSlider.value = playerSP.RunTimeValue;
        }

        public void IncreaseSP()
        {
            spSlider.value += spIncreaseValue;

            if (spSlider.value > spSlider.maxValue)
                spSlider.value = spSlider.maxValue;
        }

        public void DecreaseSP()
        {
            spSlider.value -= spDecreaseValue;

            if (spSlider.value < 0)
                spSlider.value = 0;
        }

    }
}
