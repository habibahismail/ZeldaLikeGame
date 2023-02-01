using UnityEngine;

namespace bebaSpace
{
    public class PlayerRevive : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.gameObject.SetActive(true);
                player.RevivePlayer();
            }
        }
    }
}
