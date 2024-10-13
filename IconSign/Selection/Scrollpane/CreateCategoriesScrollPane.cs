using IconSign.Extensions;
using IconSign.Selection.IconScrollContent;
using UnityEngine;

namespace IconSign.Selection.Scrollpane
{
    public static class CreateCategoriesScrollPane
    {
        internal delegate void IconClickedDelegate(string iconName);

        internal static event IconClickedDelegate OnIconClicked;

        internal static GameObject Create(Transform parent)
        {
            var scroll = new GameObject("scroll-container-categories");
            var rectTransform = scroll.AddComponent<RectTransform>();
            scroll.AddComponent<ScrollableContainer>();
            scroll.AddComponent<CategoryIconLoadingInitializer>();

            scroll.transform.SetParent(parent);
            rectTransform.Expand();
            CreateCategorizedIcons.OnIconClicked += TriggerClickEvent;

            return scroll;
        }

        private static void TriggerClickEvent(string iconName)
        {
            OnIconClicked?.Invoke(iconName);
        }
    }

    internal class CategoryIconLoadingInitializer : MonoBehaviour
    {
        private void Start()
        {
            var scrollContainer = GetComponent<ScrollableContainer>();
            CreateCategorizedIcons.StartFillingContent(scrollContainer.Content, scrollContainer);
        }
    }
}