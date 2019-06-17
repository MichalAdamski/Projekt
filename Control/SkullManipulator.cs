using Assets;
using Assets.OVRInputWrapper;
using Assets.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Control
{
    [RequireComponent(typeof(AlternativePlayerMovement), typeof(AlternativePlayerRotation))]
    public class SkullManipulator : MonoBehaviour
    {
        // z racji fazy testów utrzymywane jest kilka wersji skryptów poruszania
        [SerializeField]
        private GameObject skull;

        public static bool IsPlayerRotationActive { get; private set; }

        void Awake()
        {
            RegisterInputs();
        }

        private void Start()
        {
            SetInitialControl();
        }

        private void RegisterInputs()
        {
            var inputRegistrator = OVRInputBinder.Instance.GetInputRegistrator(Consts.InputGroups.ControlManagement);

            // przypisanie przyciskowi funkcji: zmiana skryptu
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.Four, SwapControls);

            inputRegistrator.ActivateInputSet();
        }

        private void SwapControls()
        {
            OVRInputBinder.Instance.ActivateInputSet(
                Consts.InputGroups.Rotating,
                IsPlayerRotationActive ? Consts.InputSets.Skull : Consts.InputSets.Player
                );

            if (IsPlayerRotationActive)
            {
                OVRInputBinder.Instance.DeactivateInputSet(Consts.InputGroups.Movement);
            }
            else
            {
                OVRInputBinder.Instance.ActivateInputSet(Consts.InputGroups.Movement);
            }

            PopUpInfoController.ShowPopUpInfo(IsPlayerRotationActive ? "Skull unlocked" : "Skull locked");
            IsPlayerRotationActive = !IsPlayerRotationActive;
        }

        private void SetInitialControl()
        {
            OVRInputBinder.Instance.ActivateInputSet(Consts.InputGroups.Rotating, Consts.InputSets.Skull);
            OVRInputBinder.Instance.DeactivateInputSet(Consts.InputGroups.Movement);
            IsPlayerRotationActive = false;
        }
    }
}