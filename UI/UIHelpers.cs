using UnityEngine;

namespace Assets.UI
{
    public static class UIHelpers
    {
        public static void SetItemPosition(RectTransform recTransform, float x, float y)
        {
            recTransform.anchorMax = new Vector2(0, 1);
            recTransform.anchorMin = new Vector2(0, 1);
            recTransform.pivot = new Vector2(0, 1);
            recTransform.anchoredPosition = new Vector2(x, -y);
        }

        public static void SetItemSize(RectTransform recTransform, float width, float height)
        {
            recTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            recTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public static void SetColliderSize(BoxCollider boxCollider, float width, float height, float depth = 0.002f)
        {
            boxCollider.center = new Vector3(width / 2, -height / 2);
            boxCollider.size = new Vector3(width, height, depth);
        }
    }
}