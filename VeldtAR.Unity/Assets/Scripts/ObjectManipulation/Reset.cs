using UnityEngine;


namespace ObjectManipulation
{
    public class Reset : MonoBehaviour
    {
        public void Trigger()
        {
            if (MyGameObject.my3DModel != null)
            {
                Vector3 devicePosition = Camera.main.transform.position;
                Vector3 gazeDirection = Camera.main.transform.forward;


                // Reset model in front of camera, respectively to the HoloLens gaze direction
                MyGameObject.my3DModel.transform.position = new Vector3(devicePosition.x + (2*gazeDirection.x), 
                                                                        devicePosition.y + (2*gazeDirection.y), 
                                                                        devicePosition.z + (2*gazeDirection.z));
                MyGameObject.my3DModel.transform.rotation = Quaternion.LookRotation(gazeDirection);
                MyGameObject.my3DModel.transform.localScale = new Vector3(1, 1, 1);

                Rotation.Turning = null;
                Scale.ScaleFactor = 0.0f;

                Debug.Log("Reset object to user gaze direction (location & rotation), and initial scale");
            }
        }
    }
}