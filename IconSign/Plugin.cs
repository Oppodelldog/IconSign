using System;
using System.Collections.Generic;
using BepInEx;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IconSign
{
    [BepInDependency(Main.ModGuid)]
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    internal class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "oppodelldog.mod";
        public const string PluginName = "IconSign";
        public const string PluginVersion = "0.1.0";

        private void Awake()
        {
            Jotunn.Logger.LogInfo("awake");


            PrefabManager.OnVanillaPrefabsAvailable += CreateIconSign;
        }

        private void CreateIconSign()
        {
            Jotunn.Logger.LogInfo("creating icon sign");
            var iconSignPiece = new PieceConfig
            {
                Name = IconSign.TranslationKeyName,
                PieceTable = "Hammer",
                Category = "Misc"
            };

            iconSignPiece.AddRequirement(new RequirementConfig("Wood", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Coal", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Raspberry", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Blueberries", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Guck", 1));

            LocalizationManager.Instance.GetLocalization().AddTranslation("English", new Dictionary<string, string>
            {
                { IconSign.TranslationKeyName, "Icon Sign" },
                { IconSign.TranslationKeyUse, "Paint" },
            });

            var customPiece = new CustomPiece("iconsign_name", "sign", iconSignPiece);
            PieceManager.Instance.AddPiece(customPiece);
            customPiece.PiecePrefab.gameObject.AddComponent<IconSign>();

            PrefabManager.OnVanillaPrefabsAvailable -= CreateIconSign;
        }
    }

    class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Image _image;
        public Color normalColor = Color.gray;
        public Color hoverColor = Color.white;

        public delegate void ClickAction();

        public event ClickAction OnClicked;

        void Awake()
        {
            _image = GetComponent<Image>();
            _image.color = normalColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _image.color = normalColor;
        }

        public void OnPointerClick(Action eventData)
        {
            OnClicked?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }


    public static class ImageCreationExtensions
    {
        public static GameObject CreateImage(
            this GUIManager guiManager,
            string text,
            Transform parent,
            Vector2 anchorMin,
            Vector2 anchorMax,
            Vector2 position,
            Vector2 size)
        {
            var obj = new GameObject("Image", typeof(RectTransform), typeof(Image));
            var img = obj.GetComponent<Image>();
            var rectTransform = obj.GetComponent<RectTransform>();

            obj.transform.SetParent(parent, false);

            img.sprite = guiManager.GetSprite(text);

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;

            return obj;
        }
    }
}