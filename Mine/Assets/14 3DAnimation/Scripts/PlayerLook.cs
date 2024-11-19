using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class PlayerLook : MonoBehaviour
    {
        public Transform cameraRig;

        public float mouseSensivity;

        private float rigAngle = 0f;

        private void Update()
        {

            if (false == SimpleMouseControl.isFocusing) return;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(0, mouseX * mouseSensivity * Time.deltaTime, 0);

            rigAngle -= mouseY * mouseSensivity * Time.deltaTime;
            rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);

            cameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);

            //cameraRig.Rotate(-mouseY * mouseSensivity * Time.deltaTime, 0, 0);

            //Vector3 camRigRotEuler = cameraRig.eulerAngles;

            //camRigRotEuler.y = Mathf.Clamp(camRigRotEuler.x, 0, 180);

            //cameraRig.eulerAngles = camRigRotEuler;
        }
    }
}
