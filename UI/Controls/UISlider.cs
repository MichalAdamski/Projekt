using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls
{
    public class UISlider : UIControl
    {
        private readonly int minValue;
        private readonly int maxValue;
        private int value;

        private const float IgnoreCap = 3f;
        private readonly float ignoreValue;

        private Slider slider;
        private int valueScope;

        private event Action<int> OnValueChanged;

        private readonly Sprite activeBg;
        private readonly Sprite activeHandle;
        private readonly Sprite inactiveBg;
        private readonly Sprite inactiveHandle;

        private Image handle;
        private Image bg;
        private Image fillBg;

        public UISlider(float x, float y, float width, float height, int minValue = 0, int maxValue = 100, int currentValue = 0) : base(x, y, width, height)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = currentValue;

            activeBg = Resources.Load<Sprite>("UI/Images/slider_bg_on");
            activeHandle = Resources.Load<Sprite>("UI/Images/slider_handle_on");
            inactiveBg = Resources.Load<Sprite>("UI/Images/slider_bg_off");
            inactiveHandle = Resources.Load<Sprite>("UI/Images/slider_handle_off");

            ignoreValue = (maxValue - minValue) * IgnoreCap / 100;
        }

        public override string PrefabName { get { return "Slider"; } }

        protected override void PerformOnSpawnActions()
        {
            var recTransform = Instance.GetComponent<RectTransform>();
            var boxCollider = Instance.GetComponent<BoxCollider>();
            slider = Instance.GetComponent<Slider>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);
            UIHelpers.SetColliderSize(boxCollider, Width, Height);

            slider.minValue = minValue;
            slider.maxValue = maxValue;

            valueScope = maxValue - minValue;

            var minText = new UIText(0.08f, Height, 50f, 10f, "0%", 250);
            var maxText = new UIText(Width, Height, 50f, 10f, "100%", 250);

            minText.Spawn(Instance.transform);
            maxText.Spawn(Instance.transform);

            bg = Instance.transform.GetChild(2).GetComponent<Image>();
            fillBg = Instance.transform.GetChild(3).GetComponentInChildren<Image>();
            handle = Instance.transform.GetChild(4).GetComponentInChildren<Image>();

            InvokeOnValueChangedEvent();
        }

        public override void AssignCallbacks()
        {
            Interactive.RegisterOnClickHoldDetailed(UpdateValue);
        }

        public void UpdateValue(int newValue)
        {
            if (newValue < minValue || newValue > maxValue)
                return;

            value = newValue;
            slider.value = newValue;
            InvokeOnValueChangedEvent();
        }

        private void UpdateValue(Vector3 hitPosition)
        {
            var localPosition = Instance.GetComponent<RectTransform>().InverseTransformPoint(hitPosition);
            if (localPosition.x < ignoreValue || localPosition.x > Width - ignoreValue)
                return;

            value = minValue + Mathf.RoundToInt((localPosition.x - 2f * ignoreValue) / (Width -2f * ignoreValue) * valueScope);

            InvokeOnValueChangedEvent();
        }

        public void RegisterOnValueChangedAction(Action<int> action)
        {
            OnValueChanged += action;
        }

        private void InvokeOnValueChangedEvent()
        {
            if (OnValueChanged != null)
                OnValueChanged(value);

            slider.value = value;
        }

        public void SetActive(bool isActive)
        {
            bg.sprite = isActive ? activeBg : inactiveBg;
            fillBg.sprite = isActive ? activeBg : inactiveBg;
            handle.sprite = isActive ? activeHandle : inactiveHandle;

            bg.type = Image.Type.Sliced;
            fillBg.type = Image.Type.Sliced;
            handle.type = Image.Type.Sliced;
        }
    }
}