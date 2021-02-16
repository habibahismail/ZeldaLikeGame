using UnityEngine;
using UnityEngine.Events;

namespace bebaSpace {
    public class SignalListener : MonoBehaviour
    {
        [SerializeField] private Signal signal;
        [SerializeField] private UnityEvent signalEvent;

        private void OnEnable()
        {
            signal.RegisterListener(this);
        }

        private void OnDisable()
        {
            signal.DeregisterListener(this);
        }

        public void OnSignalRaised()
        {
            signalEvent.Invoke();
        }

    }
}
