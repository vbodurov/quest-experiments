using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OvrAltGrab
{
    [DefaultExecutionOrder(-100)]
    public class OvrAltGrabControl : MonoBehaviour
    {
        GameObject _test1, _test2;
        IFingerTouchDetector _touchDetector;
        // Start is called before the first frame update
        void Start()
        {
            // _test1 = GameObject.Find("Obj1");
            // _test2 = GameObject.Find("Cube1");
            var controller = new GrabController();
            var skeleton = GetComponent<OVRSkeleton>();
            skeleton.SetBoneSetter(controller);
            _touchDetector = controller;
        }

        // Update is called once per frame
        void Update()
        {
            _touchDetector.Update();
            // var q = _test2.transform.rotation;
            //
            // _test1.transform.rotation = new Quaternion {x = q.x, y = -q.y, z = -q.z, w = q.w};
        }
    }

}

