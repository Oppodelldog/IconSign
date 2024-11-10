using System;
using System.Collections.Generic;
using IconSign.Helper;
using IconSign.Selection.Scrollpane;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace IconSign.Selection.IconScrollContent.CategorizedIcons
{
    public abstract class Layout
    {
        private const float IconLineWidth = 1120;
        private const float IconSize = 44;
        private const float Spacing = 10;
        
        private static StatsLogger _layoutStats;

        public static void Apply(List<Category> iconCategories, ScrollableContainer scrollableContainer)
        {
            if(_layoutStats == null)  _layoutStats = new StatsLogger("Layout", Config.DevConfig.Layout.LogLayoutStatsEvery.Value);
            _layoutStats.Start();
            
            const float left = 0;
            const float right = left + IconLineWidth;
            const float top = 0;
            const float stepSize = IconSize + Spacing;

            var y = -top;

            foreach (var category in iconCategories)
            {
                if (!category.Label.activeSelf) continue;

                var x = left;
                y -= stepSize;

                var categoryLabelRect = category.Label.GetComponent<RectTransform>();
                categoryLabelRect.sizeDelta = new Vector2(IconLineWidth, 30);
                categoryLabelRect.anchoredPosition = new Vector2(x, y);
                Anchors.SetTopLeft(category.Label);

                // New Line
                x = left;
                y -= stepSize;

                foreach (var icon in category.GetActiveIcons())
                {
                    if (IsAtEndOfLine(x, right))
                    {
                        x = left;
                        y -= stepSize;
                    }

                    var iconRect = icon.GetComponent<RectTransform>();
                    iconRect.sizeDelta = new Vector2(IconSize, IconSize);
                    iconRect.anchoredPosition = new Vector2(x, y);
                    Anchors.SetTopLeft(icon);

                    x += stepSize;
                }
            }

            var size = new Vector2(1, Mathf.Abs(y) + stepSize * 2);

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            scrollableContainer.SetSize(size);
            
            _layoutStats.Done();
        }

        private static bool IsAtEndOfLine(float x, float right)
        {
            return x + IconSize > right;
        }
    }
}