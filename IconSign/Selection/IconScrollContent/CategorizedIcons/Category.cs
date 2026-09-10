using System.Collections.Generic;
using UnityEngine;

namespace IconSign.Selection.IconScrollContent.CategorizedIcons
{
    public class Category
    {
        public GameObject Label { get; set; }
        public Dictionary<string, GameObject> Icons { get; } = new Dictionary<string, GameObject>();

        public IEnumerable<GameObject> GetActiveIcons()
        {
            foreach (var icon in Icons.Values)
                if (icon.activeSelf)
                    yield return icon;
        }
        
        public int ApplyFilter(ISet<string> matchingIconNames)
        {
            var visibleCount = 0;
            foreach (var icon in Icons)
            {
                var visible = matchingIconNames == null || matchingIconNames.Contains(icon.Key);
                if (icon.Value.activeSelf != visible)
                    icon.Value.SetActive(visible);
                if (visible) visibleCount++;
            }

            var showLabel = visibleCount > 0;
            if (Label.activeSelf != showLabel)
                Label.SetActive(showLabel);

            return visibleCount;
        }
    }
}
