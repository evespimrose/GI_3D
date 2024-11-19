using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;


namespace Myproject
{
    [RequireComponent (typeof(Animator), typeof(RigBuilder))]
    public class InputSystemAction : MonoBehaviour
    {
        Animator animator;
        Rig rig;
        WaitUntil untilReload;
        WaitUntil untilFire;
        WaitUntil untilGrenade;


        private bool isReloading;
        private bool isFiring;
        private bool isThrowing;


        public AnimationClip reloadClip;
        public AnimationClip fireClip;
        public AnimationClip grenadeClip;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            rig = GetComponent<RigBuilder>().layers[0].rig;
        }

        private void Start()
        {
            StartCoroutine(ReloadCoroutine());
            StartCoroutine(FireCoroutine());
            StartCoroutine(ThrowCoroutine());

        }

        private IEnumerator ReloadCoroutine()
        {
            untilReload = new WaitUntil(() => isReloading);
            while (true)
            {
                yield return untilReload;
                yield return new WaitForSeconds(reloadClip.length);

                isReloading = false;
                rig.weight = 1.0f;
            }
        }

        private IEnumerator FireCoroutine()
        {
            untilFire = new WaitUntil(() => isFiring);
            while (true)
            {
                yield return untilFire;
                yield return new WaitForSeconds(fireClip.length);

                isFiring = false;
                rig.weight = 1.0f;
            }
        }

        private IEnumerator ThrowCoroutine()
        {
            untilGrenade = new WaitUntil(() => isThrowing);
            while (true)
            {
                yield return untilGrenade;
                yield return new WaitForSeconds(grenadeClip.length);

                isThrowing = false;
                rig.weight = 1.0f;
            }
        }

        public void OnReloadEnd()
        {
            print("OnReloadEnd called by Animation Event!");
        }

        public void OnFireEnd()
        {
            print("OnFireEnd called by Animation Event!");
        }

        public void OnThrowEnd()
        {
            print("OnThrowEnd called by Animation Event!");
        }

        private void OnReload(InputValue value)
        {
            print($"OnReload 호출. 값 : {value.isPressed}{value.Get<Single>()}");
            if (isReloading) return;

            rig.weight = 0f;
            isReloading = true;
            animator.SetTrigger("Reload");
        }

        private void OnReload(Context context)
        {
            if (isReloading) return;

            isReloading = context.ReadValue<bool>();
            rig.weight = 0f;
            animator.SetTrigger("Reload");
        }

        private void OnFire(InputValue value)
        {
            print($"OnFire 호출. 값 : {value.isPressed}{value.Get<Single>()}");
            if (isFiring) return;

            rig.weight = 0f;
            isFiring = true;
            animator.SetTrigger("Fire");
        }
        private void OnFire(Context context)
        {
            if (isFiring) return;

            rig.weight = 0f;
            isFiring = context.ReadValue<bool>();
            animator.SetTrigger("Fire");
        }

        private void OnThrow(InputValue value)
        {
            print($"OnFire 호출. 값 : {value.isPressed}{value.Get<Single>()}");
            if (isThrowing) return;

            rig.weight = 0f;
            isThrowing = true;
            animator.SetTrigger("Grenade");
        }

        private void OnThrow(Context context)
        {
            if (isThrowing) return;

            rig.weight = 0f;
            isThrowing = true;
            animator.SetTrigger("Grenade");
        }
    }
}
