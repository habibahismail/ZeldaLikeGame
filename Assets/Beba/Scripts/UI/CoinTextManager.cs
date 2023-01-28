using UnityEngine;
using TMPro;

namespace bebaSpace
{
    public class CoinTextManager : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private TextMeshProUGUI coinDisplay;

        private void Awake()
        {
            UpdateCoinCount();
        }

        public void UpdateCoinCount()
        {
            coinDisplay.text = playerInventory.coins.ToString("0000000");
        }
    }

}
