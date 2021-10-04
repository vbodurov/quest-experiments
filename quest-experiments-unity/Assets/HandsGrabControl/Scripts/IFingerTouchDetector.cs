using UnityEngine;

namespace OvrAltGrab
{
    public interface IFingerTouchDetector
    {
        void Update();
        void OnAuraEnter(OVRSkeleton.BoneId id, Collider other);
        void OnAuraWithin(OVRSkeleton.BoneId id, Collider other);
        void OnAuraExit(OVRSkeleton.BoneId id, Collider other);
    }
}
