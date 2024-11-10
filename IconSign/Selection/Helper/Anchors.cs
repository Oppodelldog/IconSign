using UnityEngine;

namespace IconSign.Selection.Helper
{
    public abstract class Anchors
    {

        public static void SetTopLeft(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0, 1);
            uitransform.anchorMax = new Vector2(0, 1);
            uitransform.pivot = new Vector2(0, 1);
        }

        public static void SetTopMiddle(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0.5f, 1);
            uitransform.anchorMax = new Vector2(0.5f, 1);
            uitransform.pivot = new Vector2(0.5f, 1);
        }


        public static void SetTopRight(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(1, 1);
            uitransform.anchorMax = new Vector2(1, 1);
            uitransform.pivot = new Vector2(1, 1);
        }

        public static void SetMiddleLeft(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0, 0.5f);
            uitransform.anchorMax = new Vector2(0, 0.5f);
            uitransform.pivot = new Vector2(0, 0.5f);
        }

        public static void SetMiddle(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0.5f, 0.5f);
            uitransform.anchorMax = new Vector2(0.5f, 0.5f);
            uitransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public static void SetMiddleRight(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(1, 0.5f);
            uitransform.anchorMax = new Vector2(1, 0.5f);
            uitransform.pivot = new Vector2(1, 0.5f);
        }

        public static void SetBottomLeft(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0, 0);
            uitransform.anchorMax = new Vector2(0, 0);
            uitransform.pivot = new Vector2(0, 0);
        }

        public static void SetBottomMiddle(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(0.5f, 0);
            uitransform.anchorMax = new Vector2(0.5f, 0);
            uitransform.pivot = new Vector2(0.5f, 0);
        }

        public static void SetBottomRight(RectTransform uitransform)
        {
            uitransform.anchorMin = new Vector2(1, 0);
            uitransform.anchorMax = new Vector2(1, 0);
            uitransform.pivot = new Vector2(1, 0);
        }
        
        
        public static void SetTopLeft(GameObject uitransform)
        {
            SetTopLeft(uitransform.GetComponent<RectTransform>());
        }

    }
}