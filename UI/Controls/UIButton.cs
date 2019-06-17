using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Controls
{
    public class UIButton : UIControl
    {
        private readonly string text;

        private event Action OnClick;

        public UIButton(float x, float y, float width, float height, string text) 
            : base(x, y, width, height)
        {
            this.text = text;
        }

        public override string PrefabName { get { return "Button"; } }

        protected override void PerformOnSpawnActions()
        {
            var recTransform = Instance.GetComponent<RectTransform>();
            var boxCollider = Instance.GetComponent<BoxCollider>();
            var textComponent = Instance.GetComponentInChildren<Text>();

            UIHelpers.SetItemPosition(recTransform, X, Y);
            UIHelpers.SetItemSize(recTransform, Width, Height);
            UIHelpers.SetColliderSize(boxCollider, Width, Height);

            textComponent.text = text;
        }

        public override void AssignCallbacks()
        {
            Interactive.RegisterOnClickAction(InvokeOnClickEvent);
        }

        public void RegisterOnClickAction(Action action)
        {
            OnClick += action;
        }

        private void InvokeOnClickEvent()
        {
            if (OnClick != null)
                OnClick();
        }
    }
}