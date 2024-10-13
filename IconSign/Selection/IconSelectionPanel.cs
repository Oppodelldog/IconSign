using System;
using System.Collections.Generic;
using IconSign.Selection.Interaction;
using IconSign.Selection.Scrollpane;
using IconSign.Selection.TabBar;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;
using Logger = Jotunn.Logger;

namespace IconSign.Selection
{
    public class IconSelectionPanel
    {
        private static IconSelectionPanel _instance;
        public static IconSelectionPanel Instance => _instance ?? (_instance = new IconSelectionPanel());

        private static Dictionary<string, GameObject> _tabButtons = new Dictionary<string, GameObject>();
        private static readonly Dictionary<string, GameObject> TabContainers = new Dictionary<string, GameObject>();

        private GameObject _iconSelectionPanel;

        public event Action<string> OnIconSelected;

        internal void RequestSelection()
        {
            EnsurePanel();
            _iconSelectionPanel.SetActive(true);
            GUIManager.BlockInput(true);
        }

        internal void ClosePanel()
        {
            foreach(var tabButton in _tabButtons)
            {
                tabButton.Value.GetComponent<TabButton>().UpdateTextColor();
            }
            
            _iconSelectionPanel.SetActive(false);
            GUIManager.BlockInput(false);
        }

        private void EnsurePanel()
        {
            if (_iconSelectionPanel) return;

            if (GUIManager.Instance == null)
            {
                Logger.LogError("GUIManager instance is null");
                return;
            }

            if (!GUIManager.CustomGUIFront)
            {
                Logger.LogError("GUIManager CustomGUI is null");
                return;
            }

            CreateWoodPanel();

            CreateHeadline();

            _tabButtons = CreateTabButtons.Create(_iconSelectionPanel.transform);
            CreateTabButtons.OnCategoryButtonClicked += SwitchTab;

            TabContainers.Add(Sign.IconSign.TabNameCategories, CreateCategoriesScrollPane.Create(_iconSelectionPanel.transform));
            CreateCategoriesScrollPane.OnIconClicked += TriggerSelectionEvent;

            TabContainers.Add(Sign.IconSign.TabNameInventory, CreateInventoryScrollPane.Create(_iconSelectionPanel.transform));
            CreateInventoryScrollPane.OnIconClicked += TriggerSelectionEvent;
            
            TabContainers.Add(Sign.IconSign.TabNameRecent, CreateRecentScrollPane.Create(_iconSelectionPanel.transform));
            CreateRecentScrollPane.OnIconClicked += TriggerSelectionEvent;

            SwitchTab(Sign.IconSign.TabNameCategories);
        }

        private void CreateWoodPanel()
        {
            _iconSelectionPanel = GUIManager.Instance.CreateWoodpanel(
                parent: GUIManager.CustomGUIFront.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                position: new Vector2(0, 0),
                width: 1200,
                height: 800,
                draggable: false);
            _iconSelectionPanel.SetActive(false);
            _iconSelectionPanel.AddComponent<EscClosePanelListener>();
        }

        private void CreateHeadline()
        {
            GUIManager.Instance.CreateText(
                text: LocalizationManager.Instance.TryTranslate(Sign.IconSign.TranslationKeyName),
                parent: _iconSelectionPanel.transform,
                anchorMin: new Vector2(0.5f, 1f),
                anchorMax: new Vector2(0.5f, 1f),
                position: new Vector2(0f, -45f),
                font: GUIManager.Instance.AveriaSerifBold,
                fontSize: 38,
                color: GUIManager.Instance.ValheimOrange,
                outline: true,
                outlineColor: Color.black,
                width: 650f,
                height: 48f,
                addContentSizeFitter: false).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        }

        private void TriggerSelectionEvent(string spriteName)
        {
            OnIconSelected?.Invoke(spriteName);
            ClosePanel();
        }

        private static void SwitchTab(string tabName)
        {
            foreach (var tabContainer in TabContainers)
            {
                tabContainer.Value.SetActive(tabContainer.Key == tabName);
            }

            foreach (var tabButton in _tabButtons)
            {
                tabButton.Value.GetComponent<TabButton>().IsSelected = tabButton.Key == tabName;
            }
        }
    }
}