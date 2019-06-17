using System;
using System.Collections.Generic;
using System.Linq;
using Assets.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UICreator : MonoBehaviour
    {
        [SerializeField] private Transform anchor;
        [SerializeField] private GameObject cleanCanvasPrefab;

        private readonly List<Tab> tabs = new List<Tab>();
        private readonly Dictionary<string, GameObject> instantiatedTabs = new Dictionary<string, GameObject>();
        private GameObject activeTab;

        private Transform rootCanvasTransform;

        public void SwitchToTab(string tabName)
        {
            if (!instantiatedTabs.ContainsKey(tabName))
            {
                Debug.LogError(string.Format("There is not tab with name: {0}. Switching tab aborted.", tabName));
                return;
            }

            if (activeTab == null)
                Debug.LogError("Active tab is null, probably there is no tab added to the UI Creator");
            else
                activeTab.SetActive(false);

            activeTab = instantiatedTabs[tabName];
            activeTab.SetActive(true);
        }

        public void Spawn ()
        {
            SpawnCanvas();
            SpawnAllTabs();
            ActivateFirstTab();
        }

        private void SpawnCanvas()
        {
            if (tabs.Count == 0)
            {
                Debug.LogWarning("There are no tabs to spawn");
                return;
            }

            var maxHeight = tabs.Max(x => x.Height);
            var maxWidth = tabs.Max(x => x.Width);

            var canvas = Instantiate(cleanCanvasPrefab, anchor);
            var rectTransform = canvas.GetComponent<RectTransform>();
            rootCanvasTransform = canvas.transform;

            UIHelpers.SetItemSize(rectTransform, maxWidth, maxHeight);
        }

        private void SpawnAllTabs()
        {
            tabs.ForEach(SpawnTab);
        }

        private void SpawnTab(Tab tab)
        {
            tab.Spawn(rootCanvasTransform);
            instantiatedTabs.Add(tab.Name, tab.Instance);
        }

        private void ActivateFirstTab()
        {
            if (instantiatedTabs.Count == 0)
            {
                Debug.LogError("There are no spawned tabs. Activating first tab aborted.");
                return;
            }

            foreach (var tab in instantiatedTabs.Values.Skip(1))
            {
                tab.SetActive(false);
            }

            activeTab = instantiatedTabs.FirstOrDefault().Value;
        }    

        public Tab AddTab(float width, float height, string tabName)
        {
            var tab = new Tab(width, height, tabName);
            tabs.Add(tab);
            return tab;
        }
    }
}
