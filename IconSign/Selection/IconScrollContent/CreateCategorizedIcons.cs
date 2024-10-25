using System;
using System.Collections;
using IconSign.Config;
using IconSign.Data;
using IconSign.Extensions;
using IconSign.Selection.Helper;
using IconSign.Selection.Interaction;
using IconSign.Selection.Scrollpane;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;
using Logger = Jotunn.Logger;

namespace IconSign.Selection.IconScrollContent
{
    public static class CreateCategorizedIcons
    {
        public delegate void IconClickedAction(string iconName);

        public static IconClickedAction OnIconClicked;

        private const float IconLineWidth = 1120;
        private const float IconSize = 44;
        private const float Spacing = 10;
        private const int BatchSize = 100;

        public static void StartFillingContent(Transform content, ScrollableContainer scrollableContainer)
        {
            scrollableContainer.StartCoroutine(FillContentCoroutine(content, scrollableContainer));
        }

        private static IEnumerator FillContentCoroutine(Transform content, ScrollableContainer scrollableContainer)
        {
            const float left = 0;
            const float right = left + IconLineWidth;
            const float top = 0;
            const float stepSize = IconSize + Spacing;
            

            var categories = new[]
            {
                Constants.CategoryConsumables,
                Constants.CategoryFarming,
                Constants.CategoryArmor,
                Constants.CategoryWeapons,
                Constants.CategoryBuilding,
                Constants.CategoryFurniture,
                Constants.CategoryMiscellaneous,
                Constants.CategoryPlunder,
                Constants.CategoryAbstract
            };

            var catSpriteDict = CategorizedIcons.PrepareData(categories);

            yield return null; // Wait for the next frame

            var createdCount = 0;

            var startTime = DateTime.Now;
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            scrollableContainer.SetSize(new Vector2(0, 0));
            var isNewLine = true;
            var y = -top;
            foreach (var category in categories)
            {
                var categorySprites = catSpriteDict[category];

                // Add category label
                var categoryLabelObject = new GameObject("CategoryLabel");
                categoryLabelObject.transform.SetParent(content, false);
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                var categoryLabel = categoryLabelObject.AddComponent<Text>();
                categoryLabel.text = LocalizationManager.Instance.TryTranslate(category);
                categoryLabel.font = GUIManager.Instance.AveriaSerifBold;
                categoryLabel.fontSize = 20;
                categoryLabel.color = GUIManager.Instance.ValheimBeige;
                categoryLabel.alignment = TextAnchor.MiddleLeft;

                // position the category label
                var x = left;
                y -= stepSize;
                if (!isNewLine)
                {
                    y -= stepSize;
                }

                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                var categoryLabelRect = categoryLabelObject.GetComponent<RectTransform>();
                categoryLabelRect.sizeDelta = new Vector2(IconLineWidth, 30);
                categoryLabelRect.anchorMin = new Vector2(0, 1); // Anchor to top-left
                categoryLabelRect.anchorMax = new Vector2(0, 1); // Anchor to top-left
                categoryLabelRect.pivot = new Vector2(0, 1); // Pivot at the top-left
                categoryLabelRect.anchoredPosition = new Vector2(x, y); // Position in the content

                // reset x,y
                x = left;
                y -= stepSize;
                isNewLine = true;

                yield return null; // Wait for the next frame

                foreach (var sprite in categorySprites)
                {
                    // ReSharper disable block Unity.PerformanceCriticalCodeInvocation
                    GUIManager.Instance.CreateImage(
                            IconName.GetName(sprite),
                            content,
                            new Vector2(0, 1),
                            new Vector2(0, 1),
                            new Vector2(0, 1),
                            new Vector2(x, y),
                            new Vector2(IconSize, IconSize))
                        .AddComponent<HoverEffect>().OnClicked += () => TriggerClickEvent(sprite);

                    x += stepSize;
                    isNewLine = false;
                    if (x > right)
                    {
                        x = left;
                        y -= stepSize;
                        isNewLine = true;
                    }

                    createdCount++;
                    if (createdCount % BatchSize == 0)
                    {
                        yield return null; // Wait for the next frame
                    }
                }
            }

            Logger.LogInfo($"Created {createdCount} icons in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            var size = new Vector2(1, Mathf.Abs(y) + stepSize * 2);

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            scrollableContainer.SetSize(size);
        }

        private static void TriggerClickEvent(Sprite sprite)
        {
            OnIconClicked?.Invoke(IconName.GetName(sprite));
        }
    }
}