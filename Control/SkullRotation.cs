using Assets.OVRInputWrapper;
using UnityEngine;

namespace Assets.Control
{
    public class SkullRotation : MonoBehaviour
    {
        private Vector3 pivot;
        [SerializeField]
        private GameObject skullPartsHolder;
        [SerializeField]
        private GameObject fullHead;

        private void Awake()
        {
            RegisterInputs();
        }

        void Start()
        {
            pivot = GeometricCenterPointCalculator.CalculateGeometricCenter(skullPartsHolder);
        }

        private void RegisterInputs()
        {
            var inputRegistrator = OVRInputBinder.Instance.GetInputRegistrator(Consts.InputGroups.Rotating, Consts.InputSets.Skull);

            // przypisanie przyciskowi funckji: rotacja wzgledem osi horyzontalnej, prostopadłej do gracza
            inputRegistrator.Register2DAxisBinding(OVRInput.Axis2D.SecondaryThumbstick, RotateSkullHorizontalFacedAxis);

            // przypisanie przyciskowi funkcji: rotacja
            inputRegistrator.Register2DAxisBinding(OVRInput.Axis2D.PrimaryThumbstick, RotateSkull);

            //przypisanie przyciskowi funkcji: zmiana prędkości (później potencjalnie przeniesiona do watch menu)
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.One, ControlParameters.SkullSpeedDecrease);
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.Two, ControlParameters.SkullSpeedIncrease);
        }

        private void RotateSkull(Vector2 input)
        {
            fullHead.transform.RotateAround(pivot, Vector3.up, -input.x * ControlParameters.SkullRotationSpeed * Time.deltaTime);
            fullHead.transform.RotateAround(pivot, Vector3.right, -input.y * ControlParameters.SkullRotationSpeed * Time.deltaTime);
        }

        private void RotateSkullHorizontalFacedAxis(Vector2 input)
        {
            fullHead.transform.RotateAround(pivot, Vector3.forward, input.x * ControlParameters.SkullRotationSpeed * Time.deltaTime);
        }
    }
}