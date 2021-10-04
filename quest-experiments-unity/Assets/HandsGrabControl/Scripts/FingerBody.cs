using UnityEngine;

namespace OvrAltGrab
{
    public class FingerBody : MonoBehaviour
    {
        OVRSkeleton.BoneId _id;
        IFingerTouchDetector _detector;
        public void SetTouchDetector(OVRSkeleton.BoneId id, IFingerTouchDetector detector)
        {
            _id = id;
            _detector = detector;
        }
        void OnCollisionEnter(Collision collision)
        {
            _detector.OnFingerCollisionEnter(_id, collision);
        }
        void OnCollisionExit(Collision collision)
        {
            _detector.OnFingerCollisionExit(_id, collision);
        }

    }
}
