using System;
using System.Collections;
using System.Collections.Generic;
using IconSign.Config;
using IconSign.Data;
using IconSign.Extensions;
using IconSign.Helper;
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
        private static bool _isReady;
        private static string _searchInput = string.Empty;

        internal static void Reset()
        {
            _isReady = false;
            _searchInput = string.Empty;
            IconCategories.Clear();
            NoResultsLabel = null;
            _scrollableContainer = null;
        }

        public static IEnumerator FillContent(Transform content, ScrollableContainer container)
        {
            _scrollableContainer = container;
            _isReady = false;
            return FillContentInBatches(content, container);
        }

        public static void SearchInputChanged(string searchInput)
        {
            _searchInput = searchInput;
            if (!_isReady) return;

            var matchingIcons = string.IsNullOrWhiteSpace(searchInput)
                ? null
                : new HashSet<string>(SearchIndex.Search(searchInput), StringComparer.Ordinal);
            ApplyFilter(matchingIcons);
        }

        private static void ApplyFilter(ISet<string> matchingIcons)
        {
            var visibleCount = 0;
            foreach (var category in IconCategories)
                visibleCount += category.ApplyFilter(matchingIcons);

            var showNoResults = visibleCount == 0;
            if (NoResultsLabel.activeSelf != showNoResults)
                NoResultsLabel.SetActive(showNoResults);

            Layout.Apply(IconCategories, _scrollableContainer);
        }

        private static IEnumerator FillContentInBatches(Transform content, ScrollableContainer scrollableContainer)
        {
            IconCategories.Clear();
            
            var loadingLabel = CreateLabel(scrollableContainer.Viewport, Constants.LoadingIcons);
            loadingLabel.name = "LoadingIconsLabel";
            var loadingRect = loadingLabel.GetComponent<RectTransform>();
            loadingRect.anchorMin = Vector2.zero;
            loadingRect.anchorMax = Vector2.one;
            loadingRect.pivot = new Vector2(0.5f, 0.5f);
            loadingRect.offsetMin = new Vector2(20, 20);
            loadingRect.offsetMax = new Vector2(-20, -20);
            var loadingText = loadingLabel.GetComponent<Text>();
            loadingText.alignment = TextAnchor.MiddleCenter;
            loadingText.raycastTarget = false;
            scrollableContainer.SetSize(new Vector2(1, 108));

            // Render the hint before loading the atlas and building the icon groups.
            yield return null;

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
            NoResultsLabel = CreateLabel(scrollableContainer.Viewport, Constants.SearchNoResults);
            NoResultsLabel.SetActive(false);
            var noResultsRect = NoResultsLabel.GetComponent<RectTransform>();
            noResultsRect.anchorMin = Vector2.zero;
            noResultsRect.anchorMax = Vector2.one;
            noResultsRect.pivot = new Vector2(0.5f, 0.5f);
            noResultsRect.offsetMin = new Vector2(20, 20);
            noResultsRect.offsetMax = new Vector2(-20, -20);
            var noResultsText = NoResultsLabel.GetComponent<Text>();
            noResultsText.alignment = TextAnchor.MiddleCenter;
            noResultsText.raycastTarget = false;


            foreach (var category in categories)
            {
                var categorySprites = catSpriteDict[category];
                var iconCategory = new Category();
                IconCategories.Add(iconCategory);

                // Add category label
                var categoryLabelObject = CreateLabel(content, category);

                categoryLabelObject.SetActive(false);
                iconCategory.Label = categoryLabelObject;

                yield return null; // Wait for the next frame

                foreach (var sprite in categorySprites)
                {
                    // ReSharper disable block Unity.PerformanceCriticalCodeInvocation
                    var iconName = IconName.GetName(sprite);
                    var image = GUIManager.Instance.CreateImage(
                        sprite,
                        content,
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 0),
                        new Vector2(1, 1));
                    image.AddComponent<HoverEffect>().OnClicked += () => TriggerClickEvent(sprite);

                    image.SetActive(false);
                    iconCategory.Icons.Add(iconName, image);

                    createdCount++;
                    if (createdCount % BatchSize == 0) yield return null; // Wait for the next frame
                }
            }

            Logger.LogInfo($"Created {createdCount} icons in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            _isReady = true;
            SearchInputChanged(_searchInput);
            UnityEngine.Object.Destroy(loadingLabel);
        }

        private static GameObject CreateLabel(Transform content, string category)
        {
            var categoryLabelObject = new GameObject("CategoryLabel");
            categoryLabelObject.transform.SetParent(content, false);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            var categoryLabel = categoryLabelObject.AddComponent<Text>();
            categoryLabel.text = Translations.Translate(category);
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