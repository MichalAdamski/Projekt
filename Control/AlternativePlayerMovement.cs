using Assets;
using Assets.OVRInputWrapper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Control
{
    public class AlternativePlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject cam;
        [SerializeField]
        private GameObject player;

        void Awake()
        {
            RegisterInputs();
        }

        private void RegisterInputs()
        {
            var inputRegistrator = OVRInputBinder.Instance.GetInputRegistrator(Consts.InputGroups.Movement);

            // przypisanie przyciskowi funckji: poruszanie
            inputRegistrator.Register2DAxisBinding(OVRInput.Axis2D.SecondaryThumbstick, MoveVertical);

            inputRegistrator.Register2DAxisBinding(OVRInput.Axis2D.PrimaryThumbstick, MoveForwardAndBackward);

            //przypisanie przyciskowi funkcji: zmiana prędkości (później potencjalnie przeniesiona do watch menu)
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.One, ControlParameters.PlayerSpeedDecrease);
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.Two, ControlParameters.PlayerSpeedIncrease);

            inputRegistrator.ActivateInputSet();
        }

        private void MoveVertical(Vector2 input)
        {
            Vector2 inertia = Inertia.PerformInertiaRightHand(input);

            player.transform.position += ControlParameters.Inertia ?
                cam.transform.up * input.y * ControlParameters.MovementSpeed * Time.deltaTime
                + cam.transform.up * inertia.y * ControlParameters.MovementSpeed * Time.deltaTime
                : cam.transform.up * input.y * ControlParameters.MovementSpeed * Time.deltaTime;

            player.transform.position += ControlParameters.Inertia ?
                cam.transform.right * input.x * ControlParameters.MovementSpeed * Time.deltaTime
                + cam.transform.right * inertia.x * ControlParameters.MovementSpeed * Time.deltaTime
                : cam.transform.right * input.x * ControlParameters.MovementSpeed * Time.deltaTime;
        }

        private void MoveForwardAndBackward(Vector2 input)
        {
            input = CalculateInputWithThresholdY(input, 0.2f);
            Vector2 inertia = Inertia.PerformInertiaLeftHand(input);

            player.transform.position += ControlParameters.Inertia ?
                cam.transform.forward * input.y * ControlParameters.MovementSpeed * Time.deltaTime
                + cam.transform.forward * inertia.y * ControlParameters.MovementSpeed * Time.deltaTime
                : cam.transform.forward * input.y * ControlParameters.MovementSpeed * Time.deltaTime;
        }

        private Vector2 CalculateInputWithThresholdY(Vector2 input, float threshold)
        {
            if (input.y > threshold || input.y < -threshold)
            {
                input.y += input.y > 0 ? -threshold : threshold;
                return input;
            }

            return Vector2.zero;
        }
    }
}