using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HUD
{
    public class HandHUDAnimationManager : MonoBehaviour
    {
        private Animator anim;
        public static bool IsAnimationNeeded { get; set; }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            StartHUDAnimation();
        }

        private void StartHUDAnimation()
        {
            if (IsAnimationNeeded)
            {
                anim.SetTrigger("HUDVisible");
                IsAnimationNeeded = false;
            }
        }
    }
}
