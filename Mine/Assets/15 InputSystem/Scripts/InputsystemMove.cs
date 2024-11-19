using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Myproject
{
    [RequireComponent(typeof(CharacterController), typeof(Animator), typeof(PlayerInput))]
    public class InputsystemMove : MonoBehaviour
    {
        private CharacterController charCtrl;
        private Animator animator;

        public float walkSpeed;
        public float runSpeed;

        private Vector2 inputValue;
        private Vector2 smoothValue;
        private Vector2 smoothVelocity;

        public float smoothTime = 0.1f;

        public InputActionAsset controlDefine;
        InputAction moveAction;

        private void Awake()
        {
            charCtrl = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            controlDefine = GetComponent<PlayerInput>().actions;
            moveAction = controlDefine.FindAction("Move");
        }

        private void OnEnable()
        {
            moveAction.performed += OnMoveEvent;
            moveAction.canceled += OnMoveEvent;
        }

        private void OnDisable()
        {
            moveAction.performed -= OnMoveEvent;
            moveAction.canceled -= OnMoveEvent;
        }

        public void OnMoveEvent(InputAction.CallbackContext context)
        {
            inputValue = context.ReadValue<Vector2>();

            print($"OnMoveEvent 호출됨. 파라미터 : {inputValue}");
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            smoothValue = Vector2.SmoothDamp(smoothValue, inputValue, ref smoothVelocity, smoothTime);

            Vector3 inputMoveDir = new Vector3(smoothValue.x, 0, smoothValue.y) * walkSpeed;
            Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);

            charCtrl.Move(actualMoveDir * Time.deltaTime);

            animator.SetFloat("Xdir", smoothValue.x);
            animator.SetFloat("Ydir", smoothValue.y);
            animator.SetFloat("Speed", smoothValue.magnitude);
        }
    }
}
