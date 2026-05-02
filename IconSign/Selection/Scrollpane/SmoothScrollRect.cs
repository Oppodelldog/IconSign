using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IconSign.Selection.Scrollpane
{
    internal class SmoothScrollRect : ScrollRect
    {
        private const float MinScrollableHeight = 1f;
        private const float StopThreshold = 0.001f;

        private float _targetVerticalNormalizedPosition = 1f;
        private bool _isSmoothing;

        public float SmoothScrollSpeed { get; set; } = 18f;

        public override void OnScroll(PointerEventData data)
        {
            if (!IsActive() || content == null || viewport == null)
            {
                base.OnScroll(data);
                return;
            }

            var scrollableHeight = content.rect.height - viewport.rect.height;
            if (!vertical || scrollableHeight <= MinScrollableHeight)
            {
                base.OnScroll(data);
                return;
            }

            if (!_isSmoothing)
            {
                _targetVerticalNormalizedPosition = verticalNormalizedPosition;
            }

            var scrollDelta = data.scrollDelta.y * scrollSensitivity / scrollableHeight;
            _targetVerticalNormalizedPosition = Mathf.Clamp01(_targetVerticalNormalizedPosition + scrollDelta);
            _isSmoothing = true;

            data.Use();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (!_isSmoothing)
            {
                return;
            }

            var currentPosition = verticalNormalizedPosition;
            var easedStep = 1f - Mathf.Exp(-SmoothScrollSpeed * Time.unscaledDeltaTime);
            verticalNormalizedPosition = Mathf.Lerp(currentPosition, _targetVerticalNormalizedPosition, easedStep);

            if (Mathf.Abs(verticalNormalizedPosition - _targetVerticalNormalizedPosition) > StopThreshold)
            {
                return;
            }

            verticalNormalizedPosition = _targetVerticalNormalizedPosition;
            _isSmoothing = false;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            StopSmoothing();
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            StopSmoothing();
            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            StopSmoothing();
            base.OnEndDrag(eventData);
        }

        private void StopSmoothing()
        {
            _targetVerticalNormalizedPosition = verticalNormalizedPosition;
            _isSmoothing = false;
        }
    }
}
