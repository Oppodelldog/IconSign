using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace IconSign.Extensions
{
    public static class ImageCreationExtensions
    {
        public static GameObject CreateImage(
            this GUIManager guiManager,
            string spriteName,
            Transform parent,
            Vector2 anchorMin,
            Vector2 anchorMax,
            Vector2 pivot,
            Vector2 position,
            Vector2 size)
        {
            // ReSharper disable block Unity.PerformanceCriticalCodeInvocation
            var obj = new GameObject("Image", typeof(RectTransform), typeof(Image));
            var img = obj.GetComponent<Image>();
            var rectTransform = obj.GetComponent<RectTransform>();

            obj.transform.SetParent(parent, false);

            img.sprite = guiManager.GetSprite(spriteName);

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = pivot;
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;

            return obj;
        }
    }
}