using UnityEngine;

namespace IconSign.Helper
{
    public static class IconName
    {
        internal static string GetName(Sprite sprite)
        {
            var spriteName = sprite.name;

            if (spriteName.EndsWith("(Clone)"))
            {
                spriteName = spriteName.Substring(0, spriteName.Length - "(Clone)".Length);
            }

            return spriteName;
        }
    }
}