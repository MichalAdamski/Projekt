using Assets.OVRInputWrapper;
using UnityEngine;

namespace Assets.Control
{
    public class AlternativePlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        void Awake()
        {
            RegisterInputs();
        }

        private void RegisterInputs()
        {
            var inputRegistrator = OVRInputBinder.Instance.GetInputRegistrator(Consts.InputGroups.Rotating, Consts.InputSets.Player);

            // przypisanie przyciskowi funkcji: rotacja
            inputRegistrator.Register2DAxisBinding(OVRInput.Axis2D.PrimaryThumbstick, RotateCamera);

        }

        private void RotateCamera(Vector2 input)
        {
            input = CalculateInputWithThresholdX(input, 0.2f);

            player.transform.Rotate(Vector3.up, input.x * ControlParameters.PlayerRotationSpeed * Time.deltaTime, Space.World);
        }

        private Vector2 CalculateInputWithThresholdX(Vector2 input, float threshold)
        {
            if (input.x > threshold || input.x < -threshold)
            {
                input.x += input.x > 0 ? -threshold : threshold;
                return input;
            }

            return Vector2.zero;
        }
    }
}