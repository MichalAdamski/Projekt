using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.Controls.Complex
{
    public class UIVisibilityManager : UIControl
    {
        private readonly string name;
        private readonly float textWidth;

        private const float BarSize = 0.08f;
        private readonly Color barColor = new Color(0.9f, 0.95f, 0.95f, 0.6f);

        private UIToggle visibilityToggle;
        private UISlider visibilitySlider;

        private readonly Sprite activeImage;
        private readonly Sprite inactiveImage;

        private readonly GameObject gameObject;
        private readonly Material material;

        private BlendMode currentMode = BlendMode.Cutout;

        public UIVisibilityManager(float x, float y, string name, float textWidth, Sprite activeImage, Sprite inactiveImage, GameObject gameObject, Material material) : base(x, y, 5f, 2.5f)
        {
            this.name = name;
            this.textWidth = textWidth;

            this.activeImage = activeImage;
            this.inactiveImage = inactiveImage;

            this.gameObject = gameObject;
            this.material = material;

            SetMeterialOpacity(100);
        }

        public override string PrefabName { get { return "Empty"; } }

        protected override void PerformOnSpawnActions()
        {
            SetRootObject();

            CreateBar();
            CreateText();
            CreateToggle();
            CreateOpacityText();
            CreateSlider();
        }

        public override void AssignCallbacks()
        {
            visibilityToggle.RegisterOnValueChangedAction(visibilitySlider.SetActive);
            visibilityToggle.RegisterOnValueChangedAction(gameObject.SetActive);

            visibilitySlider.RegisterOnValueChangedAction(SetMeterialOpacity);
        }

        private void SetRootObject()
        {
            var recTransform = Instance.GetComponent<RectTransform>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);
        }

        private void CreateBar()
        {
            var bar = new UIColor(0, 0, BarSize, Height, barColor);
            bar.Spawn(Instance.transform);
        }

        private void CreateText()
        {
            var text = new UIText(0, 0, textWidth, 20f, this.name, 40);
            text.Spawn(Instance.transform);
        }

        private void CreateToggle()
        {
            visibilityToggle = new UIToggle(0.85f, 0.98f, 1.5f, 1.5f, this.activeImage, this.inactiveImage);
            visibilityToggle.Spawn(Instance.transform);
        }

        private void CreateOpacityText()
        {
            var opacityText = new UIText(2.75f, 1.05f, 150f, 30f, "Nieprzezroczystość", 15);
            opacityText.Spawn(Instance.transform);
        }

        private void CreateSlider()
        {
            visibilitySlider = new UISlider(2.85f, 1.48f, 150f, 15f, currentValue:100, maxValue: 105);
            visibilitySlider.Spawn(Instance.transform);
        }

        private void SetMeterialOpacity(int value)
        {
            if (value > 95 && currentMode == BlendMode.Opaque)
                return;

            if (value > 95 && currentMode != BlendMode.Opaque)
            {
                ChangeRenderMode(material, BlendMode.Opaque);
                currentMode = BlendMode.Opaque;
                return;
            }

            if (currentMode != BlendMode.Fade)
            {
                ChangeRenderMode(material, BlendMode.Fade);
                currentMode = BlendMode.Fade;
            }

            material.color = new Color(material.color.r, material.color.g, material.color.b, value/100f);
        }

        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,
            Transparent
        }

        private static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 2450;
                    break;
                case BlendMode.Fade:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
                case BlendMode.Transparent:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
            }

        }
    }
}