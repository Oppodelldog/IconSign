using BepInEx;
using IconSign.Config;
using IconSign.Data;
using IconSign.Sign;
using Jotunn;

namespace IconSign
{
    [BepInDependency(Main.ModGuid)]
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    internal class Plugin : BaseUnityPlugin
    {
        // ReSharper disable block MemberCanBePrivate.Global
        public const string PluginGuid = "oppodelldog.mod.iconsign";
        public const string PluginName = "IconSign";
        public const string PluginVersion = "0.4.0";


        private void Awake()
        {
            RecentIcons.ConfigEntry = Config.Bind("config", "recent_icons", "", "your recently used icons");
            ModConfig.SelectionPanel.SelectedTab = Config.Bind("config", "selection_panel_selected_tab", "", "selected tab in selection panel");
            DevConfig.SelectionPanel.DebugView = Config.Bind("dev", "selection_panel_debug_view", false, "show debug info in selection panel");
            DevConfig.IconSign.ShowInternalName = Config.Bind("dev", "icon_sign_show_internal_name", false, "show internal name of IconSign when hovering the sign");

            IconSignFactory.Register();
        }
    }
}