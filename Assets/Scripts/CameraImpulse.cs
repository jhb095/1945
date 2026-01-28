using Unity.Cinemachine;
using UnityEngine;

public class CameraImpulse : Singleton<CameraImpulse>
{
    [SerializeField] CinemachineImpulseSource impulse;

    // 카메라 흔들기
    public void ShowCameraShake()
    {
        impulse.GenerateImpulse();
    }
}
