using System.Collections.Generic;
using UnityEngine;

namespace OvrAltGrab
{
    public interface IGrabControllerInitializer
    {
        void Initialize(IList<OVRBone> bones, IDictionary<OVRSkeleton.BoneId, BoneInfo> boneMap);
    }
    public sealed class GrabControllerInitializer : IGrabControllerInitializer
    {
        readonly IFingerTouchDetector _detector;
        public GrabControllerInitializer(IFingerTouchDetector detector)
        {
            _detector = detector;
        }

        void IGrabControllerInitializer.Initialize(IList<OVRBone> bones, IDictionary<OVRSkeleton.BoneId, BoneInfo> boneMap)
        {
            boneMap.Clear();
            var fingerTips = new HashSet<OVRSkeleton.BoneId>
            {
                OVRSkeleton.BoneId.Hand_Thumb3,
                OVRSkeleton.BoneId.Hand_Index3,
                OVRSkeleton.BoneId.Hand_Middle3,
                OVRSkeleton.BoneId.Hand_Ring3,
                OVRSkeleton.BoneId.Hand_Pinky3
            };
            foreach (var bone in bones)
            {
                boneMap[bone.Id] = new BoneInfo
                {
                    BoneId = bone.Id,
                    IniRot = bone.Transform.localRotation,
                    Progress = 0
                };
                if (fingerTips.Contains(bone.Id))
                {
                    AssignColliders(bone);
                }
            }
        }
        void AssignColliders(OVRBone bone)
        {
            var aura = new GameObject("aura_" + bone.Id);
            aura.transform.SetParent(bone.Transform);
            aura.transform.localPosition = new Vector3(0, 0, 0);
            aura.transform.localRotation = Quaternion.identity;

            var isPinky = bone.Id == OVRSkeleton.BoneId.Hand_Pinky3;
            var isThumb = bone.Id == OVRSkeleton.BoneId.Hand_Thumb3;

            // aura
            var cc = aura.AddComponent<CapsuleCollider>();
            cc.direction = 0;
            cc.center = isPinky ? new Vector3(-0.006f, 0, 0) : new Vector3(-0.008f, 0, 0);
            cc.height = 0.048f;
            cc.radius = isThumb ? 0.016f : 0.0112f;

            var rb = aura.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            var fa = aura.AddComponent<FingerAura>();
            fa.SetTouchDetector(bone.Id, _detector);
        }
    }
}