using System;
using System.Collections.Generic;
using System.Linq;
using IconSign.Config;
using IconSign.Selection.Helper;
using IconSign.Selection.IconScrollContent.CategorizedIcons;
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
            foreach (var tabButton in _tabButtons)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
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

            CreateSearchInput();

            _tabButtons = CreateTabButtons.Create(_iconSelectionPanel.transform);
            CreateTabButtons.OnCategoryButtonClicked += SwitchTab;

            TabContainers.Add(Constants.TabNameCategories, CreateCategoriesScrollPane.Create(_iconSelectionPanel.transform));
            CreateCategoriesScrollPane.OnIconClicked += TriggerSelectionEvent;

            TabContainers.Add(Constants.TabNameInventory, CreateInventoryScrollPane.Create(_iconSelectionPanel.transform));
            CreateInventoryScrollPane.OnIconClicked += TriggerSelectionEvent;

            TabContainers.Add(Constants.TabNameRecent, CreateRecentScrollPane.Create(_iconSelectionPanel.transform));
            CreateRecentScrollPane.OnIconClicked += TriggerSelectionEvent;

            SwitchTab(ModConfig.SelectionPanel.SelectedTab.Value);
        }

        private void CreateSearchInput()
        {
            var input = GUIManager.Instance.CreateInputField(
                parent: _iconSelectionPanel.transform,
                anchorMin: Anchors.TopLeft,
                anchorMax: Anchors.TopLeft,
                position: new Vector2(0, 0),
                contentType: InputField.ContentType.Standard,
                placeholderText: "input...",
                fontSize: 16,
                width: 160f,
                height: 30f);

            input.GetComponent<InputField>().onValueChanged.AddListener(CreateCategorizedIcons.SearchInputChanged);
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
                text: LocalizationManager.Instance.TryTranslate(Constants.TranslationKeyName),
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
            if (!ModConfig.SelectionPanel.Tabs.Contains(tabName)) tabName = Constants.TabNameCategories;

            ModConfig.SelectionPanel.SelectedTab.Value = tabName;

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