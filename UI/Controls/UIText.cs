using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls
{
    public class UIText : UIControl
    {
        private readonly string text;
        private readonly int fontSize;

        public UIText(float x, float y, float width, float height, string text, int fontSize = 20) 
            : base(x, y, width, height)
        {
            this.text = text;
            this.fontSize = fontSize;
        }

        public override string PrefabName { get { return "Text"; } }

        public Text TextComponent { get; set; }

        protected override void PerformOnSpawnActions()
        {
            var recTransform = Instance.GetComponent<RectTransform>();
            TextComponent = Instance.GetComponent<Text>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);

            TextComponent.text = text;
            TextComponent.fontSize = fontSize;
        }

        public void SetText(string newText)
        {
            TextComponent.text = newText;
        }
    }
}