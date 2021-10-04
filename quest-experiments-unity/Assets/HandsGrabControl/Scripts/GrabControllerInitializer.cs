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

            var body = new GameObject("body_" + bone.Id);
            body.transform.SetParent(bone.Transform);
            body.transform.localPosition = new Vector3(0, 0, 0);
            body.transform.localRotation = Quaternion.identity;

            var isPinky = bone.Id == OVRSkeleton.BoneId.Hand_Pinky3;
            var isThumb = bone.Id == OVRSkeleton.BoneId.Hand_Thumb3;

            // body
            var cc1 = body.AddComponent<CapsuleCollider>();
            cc1.direction = 0;
            cc1.center = isPinky ? new Vector3(-0.006f, 0, 0) : new Vector3(-0.008f, 0, 0);
            cc1.height = 0.03f;
            cc1.radius = isThumb ? 0.01f : 0.007f;
            cc1.isTrigger = true;
            var rb = body.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            var fb = bone.Transform.gameObject.AddComponent<FingerBody>();
            fb.SetTouchDetector(bone.Id, _detector);

            // aura
            var cc2 = aura.AddComponent<CapsuleCollider>();
            cc2.direction = cc1.direction;
            cc2.center = cc1.center;
            cc2.height = cc1.height * 1.6f;
            cc2.radius = cc1.radius * 1.6f;

            var fa = aura.AddComponent<FingerAura>();
            fa.SetTouchDetector(bone.Id, _detector);
        }
    }
}