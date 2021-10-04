using System.Collections.Generic;
using UnityEngine;

namespace OvrAltGrab
{
    public sealed class GrabController : IBoneSetter, IFingerTouchDetector
    {
        readonly IDictionary<OVRSkeleton.BoneId, BoneInfo> _boneById = new Dictionary<OVRSkeleton.BoneId, BoneInfo>();
        void IBoneSetter.Initialize(IList<OVRBone> bones)
        {
            IGrabControllerInitializer initializer = new GrabControllerInitializer(this);
            initializer.Initialize(bones, _boneById);
        }
        void IBoneSetter.SetBone(OVRSkeleton.BoneId boneId, ref Quaternion rot)
        {
            if (_boneById.TryGetValue(boneId, out var bone))
            {
                //rot = bone.IniRot;
            }
        }
        void IFingerTouchDetector.OnAuraEnter(OVRSkeleton.BoneId id, Collider other)
        {
            

            Debug.Log($"OnAuraEnter {id} {other.name}");
        }
        void IFingerTouchDetector.OnAuraExit(OVRSkeleton.BoneId id, Collider other)
        {
            Debug.Log($"OnAuraExit {id} {other.name}");
        }
        void IFingerTouchDetector.OnFingerCollisionEnter(OVRSkeleton.BoneId id, Collision collision)
        {
            Debug.Log($"OnFingerCollisionEnter {id} {collision.transform.name}");
        }
        void IFingerTouchDetector.OnFingerCollisionExit(OVRSkeleton.BoneId id, Collision collision)
        {
            Debug.Log($"OnFingerCollisionExit {id} {collision.transform.name}");
        }

        void IFingerTouchDetector.Update()
        {
            
        }
    }
}
