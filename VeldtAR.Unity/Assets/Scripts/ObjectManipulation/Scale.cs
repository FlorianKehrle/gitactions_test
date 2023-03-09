using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectManipulation
{
    public class Scale : MonoBehaviour
    {
        private static float _scaleFactor = 0.0f;


        private void Update()
        {
            if (MyGameObject.my3DModel != null)
            {
                MyGameObject.my3DModel.transform.localScale = new Vector3(1 + _scaleFactor, 1 + _scaleFactor, 1 + _scaleFactor);
            }
        }


        public static float ScaleFactor
        {
            get
            {
                return _scaleFactor;
            }
            set
            {
                _scaleFactor = value;
            }
        }


        public void Smaller()
        {
           if (_scaleFactor > -0.8f)
           {
                _scaleFactor -= 0.2f;
           }
           Debug.Log("Trigger smaller event");
        }


        public void Bigger()
        {
            _scaleFactor += 0.2f;
            Debug.Log("Trigger bigger event");
        }
    }
}