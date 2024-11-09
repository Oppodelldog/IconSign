using System;
using System.Collections;
using System.Collections.Generic;
using IconSign.Config;
using IconSign.Extensions;
using IconSign.Selection.Helper;
using IconSign.Selection.Interaction;
using IconSign.Selection.Scrollpane;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;
using Logger = Jotunn.Logger;

namespace IconSign.Selection.IconScrollContent.CategorizedIcons
{
    public static class CreateCategorizedIcons
    {
        public delegate void IconClickedAction(string iconName);

        private const int BatchSize = 100;

        public static IconClickedAction OnIconClicked;
        private static readonly List<Category> IconCategories = new List<Category>();
        private static ScrollableContainer _scrollableContainer;
        private static GameObject NoResultsLabel { get; set; }

        public static void StartFillingContent(Transform content, ScrollableContainer container)
        {
            _scrollableContainer = container;
            _scrollableContainer.StartCoroutine(FillContentCoroutine(content, _scrollableContainer));
        }

        public static void ApplyFilter(string[] iconNames)
        {
            foreach (var cat in IconCategories) cat.HideAll();

            foreach (var cat in IconCategories) cat.ShowIcons(iconNames);

            foreach (var cat in IconCategories)
                if (cat.IsHidden())
                    cat.Label.SetActive(false);
                else
                    cat.Label.SetActive(true);

            if (iconNames.Length == 0)
                NoResultsLabel.SetActive(true);
            else
                NoResultsLabel.SetActive(false);

            Layout.Apply(IconCategories, _scrollableContainer);
        }

        private static IEnumerator FillContentCoroutine(Transform content, ScrollableContainer scrollableContainer)
        {
            IconCategories.Clear();

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

            var catSpriteDict = Data.CategorizedIcons.PrepareData(categories);

            yield return null; // Wait for the next frame

            var createdCount = 0;

            var startTime = DateTime.Now;
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            scrollableContainer.SetSize(new Vector2(0, 0));

            NoResultsLabel = CreateLabel(content, LocalizationManager.Instance.TryTranslate(Constants.SearchNoResults));
            NoResultsLabel.SetActive(false);
            var noResultsRect = NoResultsLabel.GetComponent<RectTransform>();
            noResultsRect.sizeDelta = new Vector2(200, 30);
            noResultsRect.anchorMin = new Vector2(0.5f, 0.5f);
            noResultsRect.anchorMax = new Vector2(0.5f, 0.5f);
            noResultsRect.pivot = new Vector2(0.5f, 0.5f);
            noResultsRect.anchoredPosition = new Vector2(0, 0);


            foreach (var category in categories)
            {
                var categorySprites = catSpriteDict[category];
                var iconCategory = new Category(category);
                IconCategories.Add(iconCategory);

                // Add category label
                var categoryLabelObject = CreateLabel(content, category);

                iconCategory.Label = categoryLabelObject;

                yield return null; // Wait for the next frame

                foreach (var sprite in categorySprites)
                {
                    // ReSharper disable block Unity.PerformanceCriticalCodeInvocation
                    var iconName = IconName.GetName(sprite);
                    var image = GUIManager.Instance.CreateImage(
                        iconName,
                        content,
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 0),
                        new Vector2(1, 1));
                    image.AddComponent<HoverEffect>().OnClicked += () => TriggerClickEvent(sprite);

                    iconCategory.Icons.Add(iconName, image);

                    createdCount++;
                    if (createdCount % BatchSize == 0) yield return null; // Wait for the next frame
                }
            }

            Logger.LogInfo($"Created {createdCount} icons in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            Layout.Apply(IconCategories, scrollableContainer);
        }

        private static GameObject CreateLabel(Transform content, string category)
        {
            var categoryLabelObject = new GameObject("CategoryLabel");
            categoryLabelObject.transform.SetParent(content, false);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            var categoryLabel = categoryLabelObject.AddComponent<Text>();
            categoryLabel.text = LocalizationManager.Instance.TryTranslate(category);
            categoryLabel.font = GUIManager.Instance.AveriaSerifBold;
            categoryLabel.fontSize = 20;
            categoryLabel.color = GUIManager.Instance.ValheimBeige;
            categoryLabel.alignment = TextAnchor.MiddleLeft;
            return categoryLabelObject;
        }

        private static void TriggerClickEvent(Sprite sprite)
        {
            OnIconClicked?.Invoke(IconName.GetName(sprite));
        }
    }
}