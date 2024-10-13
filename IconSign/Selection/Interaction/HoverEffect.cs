using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IconSign.Selection.Interaction
{
    internal class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Image _image;
        public Color normalColor = Color.gray;
        public Color hoverColor = Color.white;

        public delegate void ClickAction();

        public event ClickAction OnClicked;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.color = normalColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _image.color = normalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}