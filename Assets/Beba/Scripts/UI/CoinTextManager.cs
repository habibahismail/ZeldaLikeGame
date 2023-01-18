using UnityEngine;
using TMPro;

namespace bebaSpace
{
    public class CoinTextManager : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private TextMeshProUGUI coinDisplay;

      
        public void UpdateCoinCount()
        {
            coinDisplay.text = playerInventory.coins.ToString("0000000");
        }
    }

}
