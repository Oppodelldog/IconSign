using System.Collections.Generic;
using IconSign.Config;
using IconSign.Extensions;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace IconSign.Selection.TabBar
{
    public static class CreateTabButtons
    {
        public delegate void OnCategoryButtonClickedDelegate(string name);

        public static event OnCategoryButtonClickedDelegate OnCategoryButtonClicked;

        private static readonly Dictionary<string, GameObject> TabButtons = new Dictionary<string, GameObject>();

        private static GameObject CreateTabButton(Transform parent, string name, string label, Vector2 pos, Vector2 size)
        {
            const string icon = "button_tab";
            const string iconDisabled = "button_tab_disabled";
            const string iconHover = "button_tab_hover";
            const string iconSelected = "button_tab_selected";

            var sprite = GUIManager.Instance.GetSprite(icon);
            var spriteDisabled = GUIManager.Instance.GetSprite(iconDisabled);
            var spriteHover = GUIManager.Instance.GetSprite(iconHover);
            var spriteSelected = GUIManager.Instance.GetSprite(iconSelected);

            var obj = new GameObject(name);
            obj.transform.SetParent(parent);

            var rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = pos;
            rectTransform.sizeDelta = size;
            rectTransform.localScale = new Vector3(1, 1, 1);
            obj.AddComponent<Image>().sprite = sprite;

            var button = obj.AddComponent<Button>();
            button.transition = Selectable.Transition.SpriteSwap;
            var spriteState = new SpriteState
            {
                disabledSprite = spriteDisabled,
                highlightedSprite = spriteHover,
                pressedSprite = spriteSelected
            };
            button.spriteState = spriteState;

            var text = GUIManager.Instance.CreateText(
                label,
                obj.transform,
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0, 0),
                GUIManager.Instance.AveriaSerifBold,
                20,
                Color.white,
                true,
                Color.black,
                size.x,
                size.y,
                true);
            text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            text.GetComponent<RectTransform>().Expand();

            var tabButton = obj.AddComponent<TabButton>();
            tabButton.textComponent = text.GetComponent<Text>();

            button.onClick.AddListener(() => TriggerButtonClicked(name));

            return obj;
        }

        internal static Dictionary<string, GameObject> Create(Transform parent)
        {
            const int buttonWidth = 140;
            const int buttonHeight = 38;
            var x = -buttonWidth;
            const int y = -92;
            foreach (var tab in ModConfig.SelectionPanel.Tabs)
            {
                var tabName = LocalizationManager.Instance.TryTranslate(tab);
                var tabButton = CreateTabButton(
                    parent,
                    tab,
                    tabName,
                    new Vector2(x, y),
                    new Vector2(buttonWidth, buttonHeight));
                TabButtons.Add(tab, tabButton);

                x += buttonWidth;
            }

            TabButtons[Constants.TabNameCategories].GetComponent<TabButton>().IsSelected = true;

            GUIManager.Instance.CreateImage(
                "panel_separator",
                parent,
                new Vector2(0.5f, 1f),
                new Vector2(0.5f, 1f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0, y - buttonHeight),
                new Vector2(440, 4)
            );

            return TabButtons;
        }


        private static void TriggerButtonClicked(string category)
        {
            foreach (var tabButton in TabButtons)
            {
                tabButton.Value.GetComponent<TabButton>().IsSelected = tabButton.Key == category;
            }

            OnCategoryButtonClicked?.Invoke(category);
        }
    }
}