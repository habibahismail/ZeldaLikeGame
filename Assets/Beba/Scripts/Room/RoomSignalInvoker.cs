using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class RoomSignalInvoker : MonoBehaviour
    {
        [SerializeField] private List<Signal> onExitSignals = new List<Signal>();
        [SerializeField] private List<Signal> onEnterSignals = new List<Signal>();

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !collision.isTrigger)
            {
                for (int i = 0; i < onExitSignals.Count; i++)
                {
                    onExitSignals[i].Raise();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                for (int i = 0; i < onEnterSignals.Count; i++)
                {
                    onEnterSignals[i].Raise();
                }
            }
        }
    }
}
