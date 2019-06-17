using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HUD
{
    public class PopUpInfoController : MonoBehaviour
    {
        private static string popUpInfo;
        private static bool isInfoNeeded;
        private Animator anim;
        [SerializeField]
        private Text text;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            isInfoNeeded = false;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInfo();
            StartAnim();
        }

        private void UpdateInfo()
        {
            if (isInfoNeeded)
            {
                text.text = popUpInfo;
            }
        }

        private void StartAnim()
        {
            if (isInfoNeeded)
            {
                isInfoNeeded = false;
                anim.SetBool("IsPopUpNeeded",true);
            }
        }

        private void EndAnimation()
        {
            anim.SetBool("IsPopUpNeeded", false);
        }

        public static void ShowPopUpInfo(string info)
        {
                popUpInfo = info;
                isInfoNeeded = true;
        }
    }
}
