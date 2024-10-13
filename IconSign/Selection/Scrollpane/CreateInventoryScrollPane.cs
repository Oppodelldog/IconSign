using IconSign.Extensions;
using IconSign.Selection.IconScrollContent;
using UnityEngine;

namespace IconSign.Selection.Scrollpane
{
    internal static class CreateInventoryScrollPane
    {
        internal delegate void IconClicked(string iconName);

        internal static event IconClicked OnIconClicked;

        public static GameObject Create(Transform transform)
        {
            var scroll = new GameObject("scroll-container-inventory");
            var rectTransform = scroll.AddComponent<RectTransform>();
            scroll.AddComponent<ScrollableContainer>();
            scroll.AddComponent<InventoryScrollPaneRefresher>();

            scroll.transform.SetParent(transform);
            rectTransform.Expand();
            CreateInventoryIcons.OnIconClicked += TriggerClickEvent;

            return scroll;
        }

        private static void TriggerClickEvent(string iconName)
        {
            OnIconClicked?.Invoke(iconName);
        }
    }

    internal class InventoryScrollPaneRefresher : MonoBehaviour
    {
        private void OnEnable()
        {
            var scrollContainer = GetComponent<ScrollableContainer>();
            var content = scrollContainer.Content;
            for (var i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            var contentSize = CreateInventoryIcons.FillContent(content);
            scrollContainer.SetSize(contentSize);
        }

        private void OnDisable()
        {
            var content = GetComponent<ScrollableContainer>().Content;
            for (var i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
    }
}