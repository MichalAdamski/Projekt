using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls.Complex
{
    public class UITitle : UIText
    {
        private const float BarSize = 4f;
        private readonly Color barColor = new Color(0.9f, 0.95f, 0.95f, 0.6f);

        public UITitle(float x, float y, float width, string text) : base(x, y, width, 50f, text, 45)
        {
        }

        protected override void PerformOnSpawnActions()
        {
            base.PerformOnSpawnActions();

            var bar = new UIColor(X, Y, BarSize, Height, barColor);
            bar.Spawn(Instance.transform);

            var text = Instance.GetComponent<Text>();
            text.fontStyle = FontStyle.Bold;
        }
    }
}