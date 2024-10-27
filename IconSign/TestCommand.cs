using IconSign.Data;
using IconSign.Selection.IconScrollContent;
using Jotunn.Entities;

namespace IconSign
{
    public class TestCommand : ConsoleCommand
    {
        public override void Run(string[] args)
        {
            Jotunn.Logger.LogInfo($"Search.... {args[0]}");
            var result = SearchIndex.Search(args[0]);
            foreach (var icons in result)
            {
                Jotunn.Logger.LogInfo(icons);
            }
            
            CreateCategorizedIcons.ApplyFilter(result);
            Jotunn.Logger.LogInfo($"Search finished");
        }

        public override string Name => "search";
        public override string Help => "for debug purpose";
    }
}