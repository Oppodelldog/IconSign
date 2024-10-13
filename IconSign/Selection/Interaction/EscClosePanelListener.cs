using UnityEngine;

namespace IconSign.Selection.Interaction
{
    internal class EscClosePanelListener : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                IconSelectionPanel.Instance.ClosePanel();
            }
        }
    }
}