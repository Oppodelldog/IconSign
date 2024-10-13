using IconSign.Data;
using IconSign.Extensions;
using IconSign.Selection.Interaction;
using Jotunn.Managers;
using UnityEngine;

namespace IconSign.Selection.IconScrollContent
{
    public static class CreateRecentIcons
    {
        public delegate void IconClickedAction(string iconName);

        public static event IconClickedAction OnIconClicked;

        private const float IconLineWidth = 1120;
        private const float IconSize = 44;
        private const float Spacing = 10;

        public static Vector2 FillContent(Transform content)
        {
            const float left = 0;
            const float right = left + IconLineWidth;
            const float top = 20;
            const float stepSize = IconSize + Spacing;
            var x = left;
            var y = -top;

            var iconNames = RecentIcons.Get();

            foreach (var icon in iconNames)
            {
                if (string.IsNullOrEmpty(icon))
                {
                    continue;
                }

                GUIManager.Instance.CreateImage(
                        icon,
                        content,
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(x, y),
                        new Vector2(IconSize, IconSize))
                    .AddComponent<HoverEffect>().OnClicked += () => TriggerClickEvent(icon);

                x += stepSize;
                if (x <= right) continue;
                x = left;
                y -= stepSize;
            }

            var size = new Vector2(1, Mathf.Abs(y) + stepSize * 2);

            return size;
        }

        private static void TriggerClickEvent(string iconName)
        {
            OnIconClicked?.Invoke(iconName);
        }
    }
}