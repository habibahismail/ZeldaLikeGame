using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    [CreateAssetMenu]
    public class Signal : ScriptableObject
    {
        public List<SignalListener> signalListeners = new List<SignalListener>();

        public void Raise()
        {
            for (int i = signalListeners.Count -1; i >= 0; i--)
            {
                signalListeners[i].OnSignalRaised();
            }
        }

        public void RegisterListener(SignalListener signalListener)
        {
            signalListeners.Add(signalListener);
        }
        public void DeregisterListener(SignalListener signalListener)
        {
            signalListeners.Remove(signalListener);
        }
    }
}
