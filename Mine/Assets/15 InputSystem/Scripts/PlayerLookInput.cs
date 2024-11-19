using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Myproject
{
    public class PlayerLookInput : MonoBehaviour
    {
        public Transform cameraRig;
        public float mouseSensivity;

        private float rigAngle = 0f;

        public InputActionAsset controlDefine;
        private InputAction lookAction;

        private void Awake()
        {
            if (controlDefine != null)
            {
                lookAction = controlDefine.FindAction("Look");
            }
            else
            {
                Debug.LogError("InputActionAsset 'controlDefine' is not assigned in the Inspector.");
            }
        }

        private void OnEnable()
        {
            if (lookAction != null)
            {
                lookAction.performed += OnLookEvent;
                lookAction.canceled += OnLookEvent;
            }
        }

        private void OnDisable()
        {
            if (lookAction != null)
            {
                lookAction.performed -= OnLookEvent;
                lookAction.canceled -= OnLookEvent;
            }
        }

        public void OnLookEvent(Context context)
        {
            if (!SimpleMouseControl.isFocusing) return;
            Look(context.ReadValue<Vector2>());
        }

        private void OnLook(InputValue value)
        {
            if (!SimpleMouseControl.isFocusing) return;
            Vector2 mouseDelta = value.Get<Vector2>();
            Look(mouseDelta);
        }

        private void Look(Vector2 mouseDelta)
        {
            transform.Rotate(0, mouseDelta.x * mouseSensivity * Time.deltaTime, 0);
            rigAngle -= mouseDelta.y * mouseSensivity * Time.deltaTime;
            rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);
            cameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);
        }

private void Update()
        {

            //if (false == SimpleMouseControl.isFocusing) return;

            //float mouseX = Input.GetAxis("Mouse X");
            //float mouseY = Input.GetAxis("Mouse Y");

            //transform.Rotate(0, mouseX * mouseSensivity * Time.deltaTime, 0);

            //rigAngle -= mouseY * mouseSensivity * Time.deltaTime;
            //rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);

            //cameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);

            //cameraRig.Rotate(-mouseY * mouseSensivity * Time.deltaTime, 0, 0);

            //Vector3 camRigRotEuler = cameraRig.eulerAngles;

            //camRigRotEuler.y = Mathf.Clamp(camRigRotEuler.x, 0, 180);

            //cameraRig.eulerAngles = camRigRotEuler;
        }
    }
}
