using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls
{
    public class UIToggle : UIControl
    {
        private bool isActive = true;
        private event Action<bool> OnValueChanged;

        public UIToggle(float x, float y, float width, float height, Sprite activeImage, Sprite inactiveImage)
            : base(x, y, width, height)
        {
            this.activeImage = activeImage;
            this.inactiveImage = inactiveImage;
        }

        public override string PrefabName { get { return "Panel"; } }

        private readonly Sprite activeImage;
        private readonly Sprite inactiveImage;

        private Image image;

        protected override void PerformOnSpawnActions()
        {
            var recTransform = Instance.GetComponent<RectTransform>();
            var boxCollider = Instance.GetComponent<BoxCollider>();
            image = Instance.GetComponent<Image>();

            Instance.name = "Toggle";

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);
            UIHelpers.SetColliderSize(boxCollider, Width, Height);

            image.color = new Color(1f, 1f, 1f, 1f);
            SetProperImage();
        }

        public override void AssignCallbacks()
        {
            Interactive.RegisterOnClickAction(Toggle);
        }

        private void Toggle()
        {
            isActive = !isActive;
            SetProperImage();
            InvokeOnValueChangedEvent();
        }

        private void SetProperImage()
        {
            image.sprite = isActive ? activeImage : inactiveImage;
        }

        public void RegisterOnValueChangedAction(Action<bool> action)
        {
            OnValueChanged += action;
        }

        private void InvokeOnValueChangedEvent()
        {
            if (OnValueChanged != null)
                OnValueChanged(isActive);
        }

        public void SetActive(bool value)
        {
            if (this.isActive == value)
                return;

            isActive = value;
            SetProperImage();
            InvokeOnValueChangedEvent();
        }
    }
}