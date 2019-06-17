using UnityEngine;

namespace Assets.UI.Controls
{
    public abstract class UIControl
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public float Width { get; private set; }
        public float Height { get; private set; }

        protected UIControl(float x, float y, float width, float height)
        {
            LoadPrefab();

            X = x;
            Y = y;

            Width = width;
            Height = height;
        }

        private void LoadPrefab()
        {
            Prefab = Resources.Load(string.Format("{0}{1}", PrefabFolderPath, PrefabName), typeof(GameObject)) as GameObject;
        }

        private const string PrefabFolderPath = "UI/";

        public abstract string PrefabName { get; }
        public GameObject Prefab { get; set; }

        public GameObject Instance { get; private set; }
        public UIInteractive Interactive { get; private set; }

        public void Spawn(Transform root)
        {
            CreateInstance(root);
            PerformOnSpawnActions();
            AssignCallbacks();
        }

        private void CreateInstance(Transform root)
        {
            Instance = Object.Instantiate(Prefab, root);
            Interactive = Instance.GetComponent<UIInteractive>();
        }

        protected abstract void PerformOnSpawnActions();

        public virtual void AssignCallbacks() { }
    }
}
