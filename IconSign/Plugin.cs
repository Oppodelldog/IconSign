using BepInEx;
using IconSign.Config;
using IconSign.Data;
using IconSign.Sign;
using Jotunn;
using Jotunn.Managers;

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
            DevConfig.SeachIndex.DumpIndexToFile = Config.Bind("dev", "search_index_dump_index_to_file", false, "dump search index to file");
            DevConfig.SeachIndex.LogSearchStatsEvery = Config.Bind("dev", "search_index_log_search_stats", 10, "log search stats every nth search");

            IconSignFactory.Register();
            IconTranslation.Register();

            CommandManager.Instance.AddConsoleCommand(new TestCommand());
        }
    }
}