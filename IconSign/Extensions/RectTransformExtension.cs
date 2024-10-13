using UnityEngine;

namespace IconSign.Extensions
{
    public static class RectTransformExtension
    {
        public static void Expand(this RectTransform rectTransform)
        {
            // Set anchors to stretch the UI element across its parent
            rectTransform.anchorMin = new Vector2(0, 0); // Bottom-left corner
            rectTransform.anchorMax = new Vector2(1, 1); // Top-right corner

            // Set pivot to the center of the UI element
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Set offsets to zero to remove any padding/margins
            rectTransform.offsetMin = Vector2.zero; // Left and bottom
            rectTransform.offsetMax = Vector2.zero; // Right and top

            // Set the scale to 1 to ensure the UI element is not scaled
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}