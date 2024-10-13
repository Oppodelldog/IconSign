using IconSign.Extensions;
using IconSign.Selection.IconScrollContent;
using UnityEngine;

namespace IconSign.Selection.Scrollpane
{
    internal static class CreateRecentScrollPane
    {
        internal delegate void IconClicked(string iconName);

        internal static event IconClicked OnIconClicked;

        public static GameObject Create(Transform transform)
        {
            var scroll = new GameObject("scroll-container-recent");
            var rectTransform = scroll.AddComponent<RectTransform>();
            scroll.AddComponent<ScrollableContainer>();
            scroll.AddComponent<RecentScrollPaneRefresher>();

            scroll.transform.SetParent(transform);
            rectTransform.Expand();
            CreateRecentIcons.OnIconClicked += TriggerClickEvent;

            return scroll;
        }


        private static void TriggerClickEvent(string iconName)
        {
            OnIconClicked?.Invoke(iconName);
        }
    }

    internal class RecentScrollPaneRefresher : MonoBehaviour
    {
        private void OnEnable()
        {
            var scrollContainer = GetComponent<ScrollableContainer>();
            var content = GetComponent<ScrollableContainer>().Content;
            for (var i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            var contentSize = CreateRecentIcons.FillContent(content);
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