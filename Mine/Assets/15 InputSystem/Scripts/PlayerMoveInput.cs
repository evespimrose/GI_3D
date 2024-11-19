using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    [RequireComponent (typeof(CharacterController), typeof(Animator))]
    public class PlayerMoveInput : MonoBehaviour
    {
        #region Private Components

        private CharacterController charCtrl;
        private Animator animator;

        #endregion

        #region Public Fields

        public float walkSpeed;
        public float runSpeed;

        #endregion

        #region Private Fields
        private float currentSpeed;
        #endregion

        private void Awake()
        {
            charCtrl = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 inputValue = Vector3.zero;
            inputValue.x = Input.GetAxis("Horizontal");
            inputValue.z = Input.GetAxis("Vertical");

            inputValue = Vector3.ClampMagnitude(inputValue, 1);

            float runValue = Input.GetAxis("Fire3");

            currentSpeed = (inputValue.magnitude * walkSpeed) + (runValue * (runSpeed - walkSpeed));

            Vector3 inputmoveDir = inputValue * currentSpeed;

            Vector3 actualMoveDir = transform.TransformDirection(inputmoveDir);

            charCtrl.Move(actualMoveDir * Time.deltaTime);

            animator.SetFloat("Xdir",inputValue.x);
            animator.SetFloat("Ydir", inputValue.z);

            animator.SetFloat("Speed",inputValue.magnitude + runValue);
        }

    }
}
