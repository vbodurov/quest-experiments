using UnityEngine;

namespace OvrAltGrab
{
    public class FingerAura : MonoBehaviour
    {
        OVRSkeleton.BoneId _id;
        IFingerTouchDetector _detector;
        public void SetTouchDetector(OVRSkeleton.BoneId id, IFingerTouchDetector detector)
        {
            _id = id;
            _detector = detector;
        }
        void OnTriggerEnter(Collider other)
        {
            _detector.OnAuraEnter(_id, other);
        }
        void OnTriggerStay(Collider other)
        {
            _detector.OnAuraWithin(_id, other);
        }
        void OnTriggerExit(Collider other)
        {
            _detector.OnAuraExit(_id, other);
        }
    }
}
