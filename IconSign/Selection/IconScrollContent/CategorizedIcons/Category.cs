using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IconSign.Selection.IconScrollContent.CategorizedIcons
{
    public class Category
    {
        public GameObject Label { get; set; }
        public Dictionary<string, GameObject> Icons { get; } = new Dictionary<string, GameObject>();

        public List<GameObject> GetActiveIcons()
        {
            return Icons.Where(icon => icon.Value.activeSelf).Select(icon => icon.Value).ToList();
        }

        public bool IsHidden()
        {
            return Icons.All(icon => !icon.Value.activeSelf);
        }

        public void HideAll()
        {
            Label.SetActive(false);
            foreach (var icon in Icons) icon.Value.SetActive(false);
        }

        public void ShowIcons(string[] iconNames)
        {
            Label.SetActive(true);
            foreach (var iconName in iconNames)
                if (Icons.ContainsKey(iconName))
                    Icons[iconName].SetActive(true);
        }

        public void ShowAll()
        {
            Label.SetActive(true);
            foreach (var icon in Icons) icon.Value.SetActive(true);
        }
    }
}