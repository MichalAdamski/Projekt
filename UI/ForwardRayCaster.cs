using Assets.OVRInputWrapper;
using UnityEngine;

namespace Assets.UI
{
    public class ForwardRayCaster : MonoBehaviour
    {
        [SerializeField] private Transform sourceTransform;
        [SerializeField] private Reticle reticle;
        [SerializeField] private float rayLength = 200f;

        private UIInteractive lastInteraciveItem;
        private UIInteractive currentInteractiveItem;

        private RaycastHit hit;

        void Start()
        {
            var raycastInput = OVRInputBinder.Instance.GetInputRegistrator("raycast");

            raycastInput.RegisterButtonDownAction(OVRInput.Button.SecondaryIndexTrigger, PerformClickActionOnCurrecInteractiveItem);
            raycastInput.RegisterButtonAction(OVRInput.Button.SecondaryIndexTrigger, PerformClockHoldActionOnCurrentInteractiveItem);

            raycastInput.ActivateInputSet();
        }

        void Update()
        {
            PerformRaycast();
        }

        private void PerformRaycast()
        {
            Debug.DrawRay(sourceTransform.position, sourceTransform.forward, Color.blue, 0.01f);
            if (!RaycastForCurrentInteractiveItem())
            {
                DeselectLastInteractiveItem();
                UpdateReticle();
                return;
            }

            UpdateInteractiveItemsInformation();
            UpdateReticle();
        }

        private void UpdateReticle()
        {
            if (currentInteractiveItem == null)
            {
                reticle.Hide();
                return;
            }

            reticle.Show();
            reticle.SetPosition(hit.point, hit.normal);
        }

        private bool RaycastForCurrentInteractiveItem()
        {
            var ray = new Ray(sourceTransform.position, sourceTransform.forward);

            if (!Physics.Raycast(ray, out hit, rayLength))
            {
                currentInteractiveItem = null;
                return false;
            }

            currentInteractiveItem = hit.collider.GetComponent<UIInteractive>();
            return currentInteractiveItem != null;
        }

        private void UpdateInteractiveItemsInformation()
        {
            if (lastInteraciveItem == currentInteractiveItem)
                return;

            currentInteractiveItem.CursorIn();
            DeselectLastInteractiveItem();

            lastInteraciveItem = currentInteractiveItem;
        }

        private void DeselectLastInteractiveItem()
        {
            if (lastInteraciveItem == null)
                return;

            lastInteraciveItem.CursorOut();
            lastInteraciveItem = null;
        }

        public void PerformClickActionOnCurrecInteractiveItem()
        {
            if (currentInteractiveItem != null)
                currentInteractiveItem.Click();
        }

        public void PerformClockHoldActionOnCurrentInteractiveItem()
        {
            if (currentInteractiveItem == null)
                return;

            currentInteractiveItem.ClickHoldDetailed(hit.point);
        }
    }
}
