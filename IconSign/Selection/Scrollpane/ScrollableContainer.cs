using IconSign.Config;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace IconSign.Selection.Scrollpane
{
    public class ScrollableContainer : MonoBehaviour
    {
        private const float TopOffset = -60;
        private const float PanelWidth = 1200;
        private const float PanelHeight = 660;
        private const float ViewportWidth = 1120;
        private const float ViewportHeight = 660;
        private const float ScrollBarWidth = 20;
        private const float ScrollBarRightInset = 24;
        private const float SmoothScrollSpeed = 8f;

        private GameObject _panel;
        private Image _panelImage;
        private RectTransform _viewportRect;
        private Image _viewportImage;
        private SmoothScrollRect _scrollRect;
        private Image _contentImage;
        private Scrollbar _verticalScrollbar;
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
            CreatePanel();
            CreateViewport();
            CreateScrollRect();
            AttachContent();
            CreateVerticalScrollbar();
            AttachScrollbar();
            
            ApplyDebugView();
        }

        private void CreatePanel()
        {
            _panel = new GameObject("ScrollablePanel");
            _panel.transform.SetParent(transform, false);

            var panelRect = _panel.AddComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(PanelWidth, PanelHeight);
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.anchoredPosition = new Vector2(0, TopOffset);

            _panelImage = _panel.AddComponent<Image>();
            _panelImage.color = new Color(0, 0, 0, 0f);
        }

        private void CreateViewport()
        {
            var viewport = new GameObject("Viewport");
            viewport.transform.SetParent(_panel.transform, false);

            _viewportRect = viewport.AddComponent<RectTransform>();
            _viewportRect.sizeDelta = new Vector2(ViewportWidth, ViewportHeight);
            _viewportRect.anchorMin = new Vector2(0.5f, 0.5f);
            _viewportRect.anchorMax = new Vector2(0.5f, 0.5f);
            _viewportRect.pivot = new Vector2(0.5f, 0.5f);
            _viewportRect.anchoredPosition = Vector2.zero;

            _viewportImage = viewport.AddComponent<Image>();
            _viewportImage.color = new Color(0f, 0f, 0f, 0.1f);

            var mask = viewport.AddComponent<Mask>();
            mask.showMaskGraphic = false;
        }

        private void CreateScrollRect()
        {
            _scrollRect = _panel.AddComponent<SmoothScrollRect>();
            _scrollRect.viewport = _viewportRect;
            _scrollRect.horizontal = false;
            _scrollRect.vertical = true;
            _scrollRect.scrollSensitivity = Mathf.Max(1f, ModConfig.SelectionPanel.ScrollSensitivity.Value);
            _scrollRect.SmoothScrollSpeed = SmoothScrollSpeed;
        }

        private void AttachContent()
        {
            Content.SetParent(_viewportRect.transform, false);

            _contentImage = _content.AddComponent<Image>();
            _contentImage.color = new Color(0f, 0f, 0f, 0f);

            _scrollRect.content = Content.GetComponent<RectTransform>();
        }

        private void CreateVerticalScrollbar()
        {
            var scrollbar = new GameObject("Scrollbar");
            scrollbar.transform.SetParent(_panel.transform, false);

            _verticalScrollbar = scrollbar.AddComponent<Scrollbar>();
            var scrollbarImage = scrollbar.AddComponent<Image>();
            scrollbarImage.color = new Color(0f, 0f, 0f, 0.35f);

            var scrollbarRect = scrollbar.GetComponent<RectTransform>();
            scrollbarRect.anchorMin = new Vector2(1, 0);
            scrollbarRect.anchorMax = new Vector2(1, 1);
            scrollbarRect.pivot = new Vector2(1f, 0.5f);
            scrollbarRect.offsetMin = new Vector2(-ScrollBarRightInset - ScrollBarWidth, 0f);
            scrollbarRect.offsetMax = new Vector2(-ScrollBarRightInset, 0f);

            var handleRect = CreateScrollbarHandle(CreateSlidingArea(scrollbar.transform));
            var handleImage = handleRect.GetComponent<Image>();

            _verticalScrollbar.targetGraphic = handleImage;
            _verticalScrollbar.handleRect = handleRect;
            _verticalScrollbar.direction = Scrollbar.Direction.BottomToTop;
            _verticalScrollbar.value = 1;
        }

        private static RectTransform CreateSlidingArea(Transform parent)
        {
            var slidingArea = new GameObject("Sliding Area");
            slidingArea.transform.SetParent(parent, false);

            var slidingAreaRect = slidingArea.AddComponent<RectTransform>();
            slidingAreaRect.anchorMin = Vector2.zero;
            slidingAreaRect.anchorMax = Vector2.one;
            slidingAreaRect.offsetMin = new Vector2(3f, 3f);
            slidingAreaRect.offsetMax = new Vector2(-3f, -3f);

            return slidingAreaRect;
        }

        private static RectTransform CreateScrollbarHandle(RectTransform slidingArea)
        {
            var handle = new GameObject("Handle");
            handle.transform.SetParent(slidingArea.transform, false);

            var handleRect = handle.AddComponent<RectTransform>();
            handleRect.anchorMin = Vector2.zero;
            handleRect.anchorMax = Vector2.one;
            handleRect.offsetMin = Vector2.zero;
            handleRect.offsetMax = Vector2.zero;

            var handleImage = handle.AddComponent<Image>();
            handleImage.color = GUIManager.Instance.ValheimOrange;

            return handleRect;
        }

        private void AttachScrollbar()
        {
            _scrollRect.verticalScrollbar = _verticalScrollbar;
            _scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        }

        private void ApplyDebugView()
        {
            if (!DevConfig.SelectionPanel.DebugView.Value)
            {
                return;
            }

            _panelImage.color = new Color(0, 1, 0, 0.4f);
            _viewportImage.color = new Color(0f, 1f, 1f, 0.4f);
            _contentImage.color = new Color(1f, 0f, 0.4f, 0.4f);
        }
    }
}
