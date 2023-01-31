using UnityEngine;
using UnityEngine.UI;

namespace bebaSpace
{
    public class SpManager : MonoBehaviour
    {
        [SerializeField] private Slider spSlider;
        [SerializeField] private FloatValue playerSP;

        private void Start()
        {
            spSlider.maxValue = playerSP.InitialValue;
            spSlider.value = playerSP.InitialValue;
        }

        public void IncreaseSP(int spValue)
        {
            spSlider.value += spValue;

            if (spSlider.value > spSlider.maxValue)
                spSlider.value = spSlider.maxValue;
        }

        public void DecreaseSP(int spValue)
        {
            spSlider.value -= spValue;

            if (spSlider.value < 0)
                spSlider.value = 0;
        }


    }
}
