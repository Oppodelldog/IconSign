using IconSign.Extensions;
using IconSign.Selection.Helper;
using IconSign.Selection.Interaction;
using Jotunn.Managers;
using UnityEngine;

namespace IconSign.Selection.IconScrollContent
{
    internal static class CreateInventoryIcons
    {
        internal delegate void IconClicked(string iconName);

        internal static event IconClicked OnIconClicked;

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

            foreach (var item in Player.m_localPlayer.GetInventory().GetAllItems())
            {
                GUIManager.Instance.CreateImage(
                        IconName.GetName(item.GetIcon()),
                        content,
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 1),
                        new Vector2(x, y),
                        new Vector2(IconSize, IconSize)
                    )
                    .AddComponent<HoverEffect>().OnClicked += () => TriggerClickEvent(IconName.GetName(item.GetIcon()));

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