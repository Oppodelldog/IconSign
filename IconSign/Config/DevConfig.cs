using BepInEx.Configuration;

namespace IconSign.Config
{
    internal static class DevConfig
    {
        internal static class SelectionPanel
        {
            internal static ConfigEntry<bool> DebugView;
        }
        
        internal static class IconSign
        {
            internal static ConfigEntry<bool> ShowInternalName;
        }
    }
}