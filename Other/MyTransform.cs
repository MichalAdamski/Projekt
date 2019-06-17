using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MyTransform
    {
        public Vector3 myPosition { get; set; }
        public Quaternion myRotation { get; set; }

        public MyTransform()
        {
            myPosition = Vector3.zero;
            myRotation = Quaternion.identity;
        }
    }
}

