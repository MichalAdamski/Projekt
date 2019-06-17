using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls
{
    public class UIColor : UIControl
    {
        private Color Color { get; set; }

        public UIColor(float x, float y, float width, float height, Color color) : base(x, y, width, height)
        {
            Color = color;
        }

        public override string PrefabName { get { return "Color"; } }

        protected override void PerformOnSpawnActions()
        {
            var image = Instance.GetComponent<Image>();
            image.color = Color;

            var recTransform = Instance.GetComponent<RectTransform>();
            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);
        }
    }
}