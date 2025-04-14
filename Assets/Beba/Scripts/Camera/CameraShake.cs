using UnityEngine;
using Unity.Cinemachine;

namespace bebaSpace
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource cameraShake;

        private void Start()
        {
            cameraShake = GetComponent<CinemachineImpulseSource>();
        }

        public void ShakeCamera()
        {
            cameraShake.GenerateImpulse();
        }
    }
}
