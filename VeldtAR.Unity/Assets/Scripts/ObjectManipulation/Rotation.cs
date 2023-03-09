using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectManipulation
{
    public class Rotation : MonoBehaviour
    {
        private const float _degreesPerSecond = 7.5f;
        private static string _turning = "null";


        private void Update()
        {
            if(MyGameObject.my3DModel != null)
            {
                ConstantRotation();
            }
        }

        private void ConstantRotation()
        {
            switch (_turning)
            {
                case "left":
                    MyGameObject.my3DModel.transform.Rotate(0, -_degreesPerSecond * Time.deltaTime, 0, Space.World);
                    break;
                case "right":
                    MyGameObject.my3DModel.transform.Rotate(0, _degreesPerSecond * Time.deltaTime, 0, Space.World);
                    break;
                case "up":
                    MyGameObject.my3DModel.transform.Rotate(-_degreesPerSecond * Time.deltaTime, 0, 0, Space.World);
                    break;
                case "down":
                    MyGameObject.my3DModel.transform.Rotate(_degreesPerSecond * Time.deltaTime, 0, 0, Space.World);
                    break;
                default:
                    MyGameObject.my3DModel.transform.Rotate(0, 0, 0);
                    break;
            }
        }


        public static string Turning
        {
            get
            {
                return _turning;
            }
            set
            {
                _turning = value;
            }
        }


        public void TriggerTurnLeft()
        {
            _turning = "left";
            Debug.Log("Trigger Left Rotation Event");
        }

        public void TriggerTurnRight()
        {
            _turning = "right";
            Debug.Log("Trigger Right Rotation Event");
        }

        public void TriggerTurnUp()
        {
            _turning = "up";
            Debug.Log("Trigger Up Rotation Event");
        }

        public void TriggerTurnDown()
        {
            _turning = "down";
            Debug.Log("Trigger Down Rotation Event");
        }

        public void TriggerStopRotation()
        {
            _turning = null;
            Debug.Log("Trigger Stop Rotation Event");
        }
    }
}