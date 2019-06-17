using UnityEngine;

namespace Assets.UI.Controls
{
    public class Tab : Panel
    {
        public string Name { get; private set; }

        public override string PrefabName { get { return "Panel"; } }

        private readonly Color borderColor = new Color(0.9f, 0.95f, 0.95f, 0.6f);
        private const float BorderSize = 0.075f;

        public Tab(float width, float height, string name)
            :base(0, 0, width, height)
        {
            Name = name;
        }

        protected override void PerformOnSpawnActions()
        {
            base.PerformOnSpawnActions();

            Instance.name = string.Format("{0}_Tab", Name);
            var boxCollider = Instance.GetComponent<BoxCollider>();
            UIHelpers.SetColliderSize(boxCollider, Width, Height, 0.001f);

            var leftBorder = new UIColor(0, 0, BorderSize, Height, borderColor);
            var topBorder = new UIColor(0, 0, Width, BorderSize, borderColor);
            var rightBorder = new UIColor(Width - BorderSize, 0, BorderSize, Height, borderColor);
            var bottomBorder = new UIColor(0, Height - BorderSize, Width, BorderSize, borderColor);

            var root = Instance.transform;

            leftBorder.Spawn(root);
            topBorder.Spawn(root);
            rightBorder.Spawn(root);
            bottomBorder.Spawn(root);
        }
    }
}