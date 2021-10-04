using UnityEngine;

namespace OvrAltGrab
{
    public interface IFingerTouchDetector
    {
        void Update();
        void OnAuraEnter(OVRSkeleton.BoneId id, Collider other);
        void OnAuraExit(OVRSkeleton.BoneId id, Collider other);
        void OnFingerCollisionEnter(OVRSkeleton.BoneId id, Collision collision);
        void OnFingerCollisionExit(OVRSkeleton.BoneId id, Collision collision);
    }
}
