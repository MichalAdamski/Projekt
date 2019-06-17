using Assets.Control;
using Assets.HUD;
using Assets.OVRInputWrapper;
using UnityEngine;

namespace Assets.UI.UICreationsScripts
{
    public class VisibilityMenu : MonoBehaviour
    {
        [SerializeField] private GameObject UI;
        [SerializeField] private Transform head;

        [SerializeField] private Sprite activeImage;
        [SerializeField] private Sprite inactiveImage;

        [SerializeField] private GameObject skull;
        [SerializeField] private Material skullMaterial;

        [SerializeField] private GameObject brain;
        [SerializeField] private Material brainMaterial;

        [SerializeField] private GameObject veins;
        [SerializeField] private Material veinsMaterial;

        [SerializeField] private GameObject nerves;
        [SerializeField] private Material nervesMaterial;

        [SerializeField] private GameObject eyes;
        [SerializeField] private Material eyesMaterial;

        private UICreator uiCreator;

        void Start ()
        {
            uiCreator = GetComponent<UICreator>();
            CreateBasicMenu();
            uiCreator.Spawn();

            ToggleUI();
        }

        void Update()
        {
            ShowUI();
        }

        private void ToggleUI()
        {
            if (UI == null)
                return;

            UI.SetActive(!UI.activeSelf);
        }

        private void CreateBasicMenu()
        {
            var tab = uiCreator.AddTab(15, 21.2f, "main");

            tab.AddTitle(2f, 1f, 350f, "Widoczność");
            tab.AddVisibilityManager(2.05f, 3f, "Czaszka", 240f, activeImage, inactiveImage, skull, skullMaterial);
            tab.AddVisibilityManager(2.05f, 6.5f, "Mózg", 180f, activeImage, inactiveImage, brain, brainMaterial);
            tab.AddVisibilityManager(2.05f, 10f, "Naczynia", 250f, activeImage, inactiveImage, veins, veinsMaterial);
            tab.AddVisibilityManager(2.05f, 13.5f, "Nerwy", 190f, activeImage, inactiveImage, nerves, nervesMaterial);
            tab.AddVisibilityManager(2.05f, 17f, "Oczy", 180f, activeImage, inactiveImage, eyes, eyesMaterial);
        }

        private void ShowUI()
        {
            if (CheckForProperArmRotation() && !UI.activeSelf)
            {
                UI.SetActive(true);
            }
            else if (!CheckForProperArmRotation() && UI.activeSelf)
            {
                UI.SetActive(false);
            }
        }

        private bool CheckForProperArmRotation()
        {
            var angle =  Vector3.Angle(UI.transform.forward, head.forward);
            return angle < 50;
        }
    }
}
