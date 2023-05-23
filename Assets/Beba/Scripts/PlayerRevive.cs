using UnityEngine;

namespace bebaSpace
{
    public class PlayerRevive : MonoBehaviour
    {
        [SerializeField] private PlayerHealth player;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.transform.parent.GetComponent<SpriteRenderer>().enabled = true;
                player.RevivePlayer();
            }
        }
    }
}
