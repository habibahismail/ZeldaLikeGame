using UnityEngine;
using Cinemachine;

namespace bebaSpace
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float intensity = 5f;
        [SerializeField] private float time = 0.5f;

        private void Update()
        {
            if(shakeTimer > 0) { 
                shakeTimer -= Time.deltaTime;

                if(shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }

        }

        private CinemachineVirtualCamera cinemachineVirtualCamera;
        float shakeTimer;

            private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void ShakeCamera()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }

    }
}
