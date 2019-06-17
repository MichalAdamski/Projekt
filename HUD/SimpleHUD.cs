using Assets.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HUD
{
    public class SimpleHUD : MonoBehaviour
    {
        private Text text;
        private string skullInfo = string.Empty;
        [SerializeField]
        private GameObject skull;
        [SerializeField]
        private GameObject HUD;
        [SerializeField]
        private GameObject textHolder;
        [SerializeField]
        private GameObject VectorHolder;
        [SerializeField]
        private GameObject PlayerEyes;

        // Use this for initialization
        void Start()
        {
            text = textHolder.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            ShowHud();
            ShowInfo();
        }

        private void ShowInfo()
        {
            if (HUD.activeSelf)
            {
                SkullLockCheck();
                text.text = skullInfo +
                    "\nPrędkość obrotu czaszki: " + ControlParameters.SkullRotationSpeed +
                    "\nPrędkość obrotu: " + ControlParameters.PlayerRotationSpeed +
                    "\nPrędkość ruchu: " + ControlParameters.MovementSpeed +
                    "\nRotacja czaszki w kątach Eulera:\n" +
                    "x: " + skull.transform.rotation.eulerAngles.x +
                    "\ny: " + skull.transform.rotation.eulerAngles.y +
                    "\nz: " + skull.transform.rotation.eulerAngles.z;
            }
        }

        private void SkullLockCheck()
        {
            skullInfo = SkullManipulator.IsPlayerRotationActive ? "Czaszka zablokowana" : "Czaszka odblokowana";
        }

        private void ShowHud()
        {
            bool isArmRotatedProperly = CheckForProperArmRotation();

            if (isArmRotatedProperly && !HUD.activeSelf)
            {
                HUD.SetActive(true);
                HandHUDAnimationManager.IsAnimationNeeded = true;
            }
            else if (!isArmRotatedProperly && HUD.activeSelf)
            {
                HUD.SetActive(false);
            }
        }

        private Vector3 GetTuchEulerRotation()
        {
            return new Vector3(OVRInput.GetLocalControllerRotation(
                OVRInput.Controller.RTouch).eulerAngles.x,
                OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).eulerAngles.y,
                OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).eulerAngles.z);
        }

        private bool CheckForProperArmRotation()
        {
            var angle = CheckForAngleBetwenVectors();

            return angle < 40;
        }

        private float CheckForAngleBetwenVectors()
        {
            var angleFrom = -PlayerEyes.transform.forward;
            var angleTo = VectorHolder.transform.right;

            var angle = Vector3.Angle(angleFrom, angleTo);

            return angle;
        }
    }
}