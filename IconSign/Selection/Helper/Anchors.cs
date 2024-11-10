using UnityEngine;

namespace IconSign.Selection.Helper
{
    public abstract class Anchors
    {
        private static void SetTopLeft(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
        }

        public static void SetTopMiddle(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.pivot = new Vector2(0.5f, 1);
        }


        public static void SetTopRight(RectTransform rt)
        {
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(1, 1);
        }

        public static void SetMiddleLeft(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0, 0.5f);
            rt.anchorMax = new Vector2(0, 0.5f);
            rt.pivot = new Vector2(0, 0.5f);
        }

        public static void SetMiddle(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
        }

        public static void SetMiddleRight(RectTransform rt)
        {
            rt.anchorMin = new Vector2(1, 0.5f);
            rt.anchorMax = new Vector2(1, 0.5f);
            rt.pivot = new Vector2(1, 0.5f);
        }

        public static void SetBottomLeft(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
        }

        public static void SetBottomMiddle(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 0);
            rt.anchorMax = new Vector2(0.5f, 0);
            rt.pivot = new Vector2(0.5f, 0);
        }

        public static void SetBottomRight(RectTransform rt)
        {
            rt.anchorMin = new Vector2(1, 0);
            rt.anchorMax = new Vector2(1, 0);
            rt.pivot = new Vector2(1, 0);
        }

        public static void SetPosition(RectTransform rt, Vector2 vector2)
        {
            rt.anchoredPosition = vector2;
        }


        public static void SetSize(RectTransform rt, Vector2 vector2)
        {
            rt.sizeDelta = vector2;
        }

        public static void SetTopLeft(GameObject rt)
        {
            SetTopLeft(rt.GetComponent<RectTransform>());
        }

        public static void SetPosition(GameObject searchIcon, Vector2 vector2)
        {
            SetPosition(searchIcon.GetComponent<RectTransform>(), vector2);
        }

        public static void SetSize(GameObject searchIcon, Vector2 vector2)
        {
            SetSize(searchIcon.GetComponent<RectTransform>(), vector2);
        }
    }
}