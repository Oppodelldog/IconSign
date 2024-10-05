using System;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace IconSign
{
    public class IconSelectionPanel
    {
        private static IconSelectionPanel _instance;
        public static IconSelectionPanel Instance => _instance ?? (_instance = new IconSelectionPanel());

        private GameObject _iconSelectionPanel;

        public event Action<string> OnIconSelected;

        internal void RequestSelection()
        {
            EnsurePanel();
            _iconSelectionPanel.SetActive(true);
            GUIManager.BlockInput(true);
        }

        private void ClosePanel()
        {
            _iconSelectionPanel.SetActive(false);
            GUIManager.BlockInput(false);
        }

        internal void ToggleIconSelectionPanel()
        {
            EnsurePanel();

            var state = !_iconSelectionPanel.activeSelf;

            _iconSelectionPanel.SetActive(state);

            GUIManager.BlockInput(state);
        }

        private void EnsurePanel()
        {
            if (_iconSelectionPanel) return;

            if (GUIManager.Instance == null)
            {
                Jotunn.Logger.LogError("GUIManager instance is null");
                return;
            }

            if (!GUIManager.CustomGUIFront)
            {
                Jotunn.Logger.LogError("GUIManager CustomGUI is null");
                return;
            }

            _iconSelectionPanel = GUIManager.Instance.CreateWoodpanel(
                parent: GUIManager.CustomGUIFront.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                position: new Vector2(0, 0),
                width: 850,
                height: 600,
                draggable: false);
            _iconSelectionPanel.SetActive(false);

            GUIManager.Instance.CreateText(
                text: Localization.instance.Localize("iconsign_name"),
                parent: _iconSelectionPanel.transform,
                anchorMin: new Vector2(0.5f, 1f),
                anchorMax: new Vector2(0.5f, 1f),
                position: new Vector2(0f, -30f),
                font: GUIManager.Instance.AveriaSerifBold,
                fontSize: 30,
                color: GUIManager.Instance.ValheimOrange,
                outline: true,
                outlineColor: Color.black,
                width: 350f,
                height: 40f,
                addContentSizeFitter: false).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            var atlas = PrefabManager.Cache.GetPrefab<SpriteAtlas>("IconAtlas");
            var sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);

            var x = -400;
            var y = -70;
            foreach (var sprite in sprites)
            {
                if (x > 400)
                {
                    x = -400;
                    y -= 20;
                }

                GUIManager.Instance.CreateImage(
                        sprite.name,
                        _iconSelectionPanel.transform,
                        new Vector2(0.5f, 1f),
                        new Vector2(0.5f, 1f),
                        new Vector2(x, y),
                        new Vector2(16, 16))
                    .AddComponent<HoverEffect>()
                    .OnClicked += () => TriggerSelectionEvent(sprite);
                x += 20;
            }
        }

        private void TriggerSelectionEvent(Sprite sprite)
        {
            OnIconSelected?.Invoke(GetOriginalSpriteName(sprite));
            ClosePanel();
        }

        private static string GetOriginalSpriteName(Sprite sprite)
        {
            var spriteName = sprite.name;

            if (spriteName.EndsWith("(Clone)"))
            {
                spriteName = spriteName.Substring(0, spriteName.Length - "(Clone)".Length);
            }

            return spriteName;
        }
    }
}