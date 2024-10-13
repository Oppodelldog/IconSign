using Jotunn.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IconSign.Selection.TabBar
{
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Text textComponent;

        private bool _isSelected;

        public bool IsSelected
        {
            set
            {
                _isSelected = value;
                UpdateTextColor();
            }
        }

        private readonly Color _normalTextColor = Color.white;
        private readonly Color _highlightedTextColor = GUIManager.Instance.ValheimOrange;

        private void Start()
        {
            UpdateTextColor();
        }

        public void UpdateTextColor()
        {
            textComponent.color = _isSelected ? _highlightedTextColor : _normalTextColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            textComponent.color = _highlightedTextColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UpdateTextColor();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            textComponent.color = _highlightedTextColor;
        }
    }
}