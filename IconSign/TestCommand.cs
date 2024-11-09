using IconSign.Data;
using IconSign.Selection.IconScrollContent.CategorizedIcons;
using Jotunn;
using Jotunn.Entities;

namespace IconSign
{
    public class TestCommand : ConsoleCommand
    {
        public override string Name => "search";
        public override string Help => "for debug purpose";

        public override void Run(string[] args)
        {
            Logger.LogInfo($"Search.... {args[0]}");
            var result = SearchIndex.Search(args[0]);
            foreach (var icons in result) Logger.LogInfo(icons);

            CreateCategorizedIcons.ApplyFilter(result);
            Logger.LogInfo("Search finished");
        }
    }
}