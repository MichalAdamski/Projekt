using UnityEngine;

namespace Assets.UI
{
    public class FingerInteraction : MonoBehaviour
    {
        private UIInteractive interactive;

        void OnTriggerEnter(Collider other)
        {
            var newInteractive = other.GetComponent<UIInteractive>();

            if (newInteractive == null || other.gameObject.name.Contains("Panel") || other.gameObject.name.Contains("Tab"))
                return;

            interactive = newInteractive;
            interactive.Click();
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name.Contains("Panel") || other.gameObject.name.Contains("Tab"))
                return;

            interactive = null;
        }

        void Update()
        {
            if (interactive == null)
                return;

            interactive.ClickHold();
            interactive.ClickHoldDetailed(transform.position);
        }
    }
}
