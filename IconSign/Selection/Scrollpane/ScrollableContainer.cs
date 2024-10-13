using IconSign.Data;
using UnityEngine;
using UnityEngine.UI;

namespace IconSign.Selection.Scrollpane
{
    public class ScrollableContainer : MonoBehaviour
    {
        private GameObject _content;

        public Transform Content
        {
            get
            {
                if (_content == null) InitContent();

                return _content.transform;
            }
        }

        public void SetSize(Vector2 size)
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            _content.GetComponent<RectTransform>().sizeDelta = size;
        }

        private void InitContent()
        {
            if (_content == null)
            {
                _content = new GameObject("Content");
            }

            var contentRect = _content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0f, 1f); // Anchor to top-left
            contentRect.anchorMax = new Vector2(1f, 1f); // Anchor to top-right
            contentRect.pivot = new Vector2(0.5f, 1f); // Pivot at the top-center            
        }

        private void Start()
        {
            const float topOffset = -60;
            const float panelWidth = 1200;
            const float panelHeight = 660;
            const float viewportWidth = 1120;
            const float viewportHeight = 660;

            const float scrollBarWidth = 20;
            const float scrollSensitivity = 10;

            // Create a new GameObject for the panel
            var panel = new GameObject("ScrollablePanel");
            panel.transform.SetParent(transform, false); // Attach to the Canvas or parent

            // Add RectTransform and set its size
            var panelRect = panel.AddComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(panelWidth, panelHeight); // Size of the panel
            panelRect.anchorMin = new Vector2(0.5f, 0.5f); // Center anchor
            panelRect.anchorMax = new Vector2(0.5f, 0.5f); // Center anchor
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.anchoredPosition = new Vector2(0, topOffset);

            // Add an Image component to make the panel visible (optional)
            var panelImage = panel.AddComponent<Image>();

            // ReSharper disable once HeuristicUnreachableCode
            panelImage.color = new Color(0, 0, 0, 0f); // Transparent background

            // Create a ScrollRect component for scrolling functionality
            var scrollRect = panel.AddComponent<ScrollRect>();

            // Create a viewport for the ScrollRect
            var viewport = new GameObject("Viewport");
            viewport.transform.SetParent(panel.transform, false);
            var viewportRect = viewport.AddComponent<RectTransform>();
            viewportRect.sizeDelta = new Vector2(viewportWidth, viewportHeight); // Slightly smaller than the panel
            viewportRect.anchorMin = new Vector2(0.5f, 0.5f);
            viewportRect.anchorMax = new Vector2(0.5f, 0.5f);
            viewportRect.pivot = new Vector2(0.5f, 0.5f);
            viewportRect.anchoredPosition = new Vector2(0, 0);

            // Add an Image to the viewport
            var viewportImage = viewport.AddComponent<Image>();
            // ReSharper disable once HeuristicUnreachableCode
            viewportImage.color = new Color(0f, 0f, 0f, 0.1f); // if alpha is 0, the mask will not work
            var mask = viewport.AddComponent<Mask>();
            mask.showMaskGraphic = false;
            // Assign the viewport to the ScrollRect
            scrollRect.viewport = viewportRect;

            // Create the content holder inside the viewport
            Content.SetParent(viewport.transform, false);

            // add image to get a visual representation of the content
            var contentImage = _content.AddComponent<Image>();
            // ReSharper disable once HeuristicUnreachableCode
            contentImage.color = new Color(0f, 0f, 0f, 0f);

            // Assign the content to the ScrollRect
            scrollRect.content = Content.GetComponent<RectTransform>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.scrollSensitivity = scrollSensitivity;

            // Optionally, add a vertical scrollbar
            var scrollbar = new GameObject("Scrollbar");
            scrollbar.transform.SetParent(panel.transform, false);
            var verticalScrollbar = scrollbar.AddComponent<Scrollbar>();
            var scrollbarRect = scrollbar.GetComponent<RectTransform>();
            scrollbarRect.sizeDelta = new Vector2(scrollBarWidth, panelHeight); // Width of the scrollbar
            scrollbarRect.anchorMin = new Vector2(1, 0); // Right anchor
            scrollbarRect.anchorMax = new Vector2(1, 1); // Right anchor
            verticalScrollbar.direction = Scrollbar.Direction.TopToBottom;
            verticalScrollbar.value = 1; // Start scrolled to the top

            // Attach the scrollbar to the ScrollRect
            scrollRect.verticalScrollbar = verticalScrollbar;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
            
            if (!DevConfig.SelectionPanel.DebugView.Value) return;
            panelImage.color = new Color(0, 1, 0, 0.4f);
            panelImage.color = new Color(0, 1, 0, 0.4f);
            viewportImage.color = new Color(0f, 1f, 1f, 0.4f);
            contentImage.color = new Color(1f, 0f, 0.4f, 0.4f);
        }
    }
}