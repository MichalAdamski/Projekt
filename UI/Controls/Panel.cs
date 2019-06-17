using System;
using System.Collections.Generic;
using Assets.UI.Controls.Complex;
using UnityEngine;

namespace Assets.UI.Controls
{
    public class Panel : UIControl
    {
        public List<UIControl> UIControls { get; private set; }

        public override string PrefabName { get { return "Color"; } }

        protected override void PerformOnSpawnActions()
        {
            Instance.name = "Panel";

            var recTransform = Instance.GetComponent<RectTransform>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);

            SpawnAllUIControls();
        }

        private void SpawnAllUIControls()
        {
            var root = Instance.transform;

            foreach (var uiControl in UIControls)
            {
                uiControl.Spawn(root);
            }
        }

        public Panel(float x, float y, float width, float height)
            :base (x, y, width, height)
        {
            UIControls = new List<UIControl>();
        }

        public Panel AddPanel(float x, float y, float width, float height)
        {
            var panel = new Panel(x, y, width, height);
            UIControls.Add(panel);
            return panel;
        }

        public UIButton AddButton(float x, float y, float width, float height, string text)
        {
            var button = new UIButton(x, y, width, height, text);
            UIControls.Add(button);
            return button;
        }

        public UIPlusMinus AddPlusMinus(float x, float y, float width, float height, float buttonSize = 10f, int minValue = 0, int maxValue = 100, int currentValue = 50, int valueStep = 1, int fontSize = 20)
        {
            var control = new UIPlusMinus(x, y, width, height, buttonSize, minValue, maxValue, currentValue, valueStep, fontSize);
            UIControls.Add(control);
            return control;
        }

        public UISlider AddSlider(float x, float y, float width, float height, int minValue = 0, int maxValue = 100, int currentValue = 0)
        {
            var control = new UISlider(x, y, width, height, minValue, maxValue, currentValue);
            UIControls.Add(control);
            return control;
        }

        public UIToggle AddToggle(float x, float y, float width, float height, Sprite activeImage, Sprite inactiveImage)
        {
            var control = new UIToggle(x, y, width, height, activeImage, inactiveImage);
            UIControls.Add(control);
            return control;
        }

        public void AddTitle(float x, float y, float width, string text)
        {
            var control = new UITitle(x, y, width, text);
            UIControls.Add(control);
        }

        public void AddVisibilityManager(float x, float y, string name, float textWidth, Sprite activeImage, Sprite inactiveImage, GameObject gameObject, Material material)
        {
            var control = new UIVisibilityManager(x, y, name, textWidth, activeImage, inactiveImage, gameObject, material);
            UIControls.Add(control);
        }
    }
}