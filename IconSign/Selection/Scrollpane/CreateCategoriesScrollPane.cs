using System.Collections;
using IconSign.Extensions;
using IconSign.Selection.IconScrollContent.CategorizedIcons;
using UnityEngine;

namespace IconSign.Selection.Scrollpane
{
    public static class CreateCategoriesScrollPane
    {
        internal static event IconClickedDelegate OnIconClicked;

        internal static GameObject Create(Transform parent)
        {
            var scroll = new GameObject("scroll-container-categories");
            var rectTransform = scroll.AddComponent<RectTransform>();
            scroll.AddComponent<ScrollableContainer>();
            scroll.AddComponent<IconLoadingInitializer>();

            scroll.transform.SetParent(parent);
            rectTransform.Expand();
            CreateCategorizedIcons.OnIconClicked -= TriggerClickEvent;
            CreateCategorizedIcons.OnIconClicked += TriggerClickEvent;

            return scroll;
        }

        private static void TriggerClickEvent(string iconName)
        {
            OnIconClicked?.Invoke(iconName);
        }

        internal delegate void IconClickedDelegate(string iconName);
    }

    internal class IconLoadingInitializer : MonoBehaviour
    {
        private IEnumerator _loading;

        private void Start()
        {
            var scrollContainer = GetComponent<ScrollableContainer>();
            _loading = CreateCategorizedIcons.FillContent(scrollContainer.Content, scrollContainer);
        }

        private void Update()
        {
            // Advance one batch per frame. Unlike a coroutine, the iterator survives
            // hiding the tab or closing the dialog and resumes when visible again.
            if (_loading != null && !_loading.MoveNext())
                DisposeLoading();
        }

        private void OnDestroy()
        {
            DisposeLoading();
        }

        private void DisposeLoading()
        {
            (_loading as System.IDisposable)?.Dispose();
            _loading = null;
        }
    }
}