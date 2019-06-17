using System;
using UnityEngine;

namespace Assets.UI
{
    public class UIInteractive : MonoBehaviour
    {
        private event Action OnClick;
        private event Action OnCursorIn;
        private event Action OnCursorOut;
        private event Action OnClickHold;
        private event Action<Vector3> OnClickHoldDetailed;

        public void RegisterOnClickAction(Action action)
        {
            OnClick += action;
        }

        public void RegisterOnCursorInAction(Action action)
        {
            OnCursorIn += action;
        }

        public void RegisterOnCursorOutAction(Action action)
        {
            OnCursorOut += action;
        }

        public void RegisterOnClickHoldAction(Action action)
        {
            OnClickHold += action;
        }

        public void RegisterOnClickHoldDetailed(Action<Vector3> action)
        {
            OnClickHoldDetailed += action;
        }

        public void Click()
        {
            if (OnClick != null)
                OnClick();
        }

        public void CursorIn()
        {
            if (OnCursorIn != null)
                OnCursorIn();
        }

        public void CursorOut()
        {
            if (OnCursorOut != null)
                OnCursorOut();
        }

        public void ClickHold()
        {
            if (OnClickHold != null)
                OnClickHold();
        }

        public void ClickHoldDetailed(Vector3 hitPosition)
        {
            if (OnClickHoldDetailed != null)
                OnClickHoldDetailed(hitPosition);
        }
    }
}