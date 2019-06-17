using System;
using UnityEngine;

namespace Assets.UI.Controls
{
    public class UIPlusMinus : UIControl
    {
        private readonly float buttonSize;

        private readonly int maxValue;
        private readonly int minValue;
        private readonly int step;

        private int value;

        private readonly int fontSize;

        private event Action<int> OnValueChanged;

        private UIButton minusButton;
        private UIButton plusButton;
        private UIText valueText;

        public UIPlusMinus(float x, float y, float width, float height, float buttonSize = 10f, int minValue = 0, int maxValue = 100, int currentValue = 50, int valueStep = 1, int fontSize = 20)
            : base(x, y, width, height)
        {
            this.buttonSize = buttonSize;
            this.value = currentValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = valueStep;
            this.fontSize = fontSize;
        }

        public override string PrefabName { get { return "Empty"; } }

        private float GetYButtonPosition()
        {
            return (Height - buttonSize) / 2;
        }

        protected override void PerformOnSpawnActions()
        {
            var root = Instance.transform;
            var recTransform = Instance.GetComponent<RectTransform>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);

            minusButton = new UIButton(0, GetYButtonPosition(), buttonSize, buttonSize, "-");
            plusButton = new UIButton((Width - buttonSize) * 0.1f, GetYButtonPosition(), buttonSize, buttonSize, "+");
            valueText = new UIText(0, 0, Width * 10f / 2f, Height * 10f / 2f, value.ToString(), fontSize);

            minusButton.Spawn(root);
            plusButton.Spawn(root);
            valueText.Spawn(root);
        }

        public override void AssignCallbacks()
        {
            minusButton.RegisterOnClickAction(DecreaseValue);
            plusButton.RegisterOnClickAction(IncreaseValue);
            RegisterOnValueChangedAction(newValue => valueText.SetText(newValue.ToString()));
        }

        public void RegisterOnValueChangedAction(Action<int> action)
        {
            OnValueChanged += action;
        }

        private void InvokeOnValueChangedEvent()
        {
            if (OnValueChanged != null)
                OnValueChanged(value);
        }

        public void IncreaseValue()
        {
            var newValue = value + step;

            if (newValue > maxValue)
                newValue = maxValue;

            if (newValue == value)
                return;

            value = newValue;
            InvokeOnValueChangedEvent();
        }

        public void DecreaseValue()
        {
            var newValue = value - step;

            if (newValue < minValue)
                newValue = minValue;

            if (newValue == value)
                return;

            value = newValue;
            InvokeOnValueChangedEvent();
        }
    }
}