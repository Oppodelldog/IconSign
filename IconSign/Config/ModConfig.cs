using BepInEx.Configuration;

namespace IconSign.Config
{
    internal static class ModConfig
    {
        internal static class SelectionPanel
        {
            internal static ConfigEntry<string> SelectedTab;
            internal static ConfigEntry<float> ScrollSensitivity;

            internal static readonly string[] Tabs =
            {
                Constants.TabNameCategories,
                Constants.TabNameRecent,
                Constants.TabNameInventory
            };
        }
    }
}
