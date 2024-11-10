using IconSign.Selection.IconScrollContent.CategorizedIcons;
using Jotunn.Entities;

namespace IconSign
{
    public class TestCommand : ConsoleCommand
    {
        public override string Name => "search";
        public override string Help => "for debug purpose";

        public override void Run(string[] args)
        {
            CreateCategorizedIcons.SearchInputChanged(args[0]);
        }
    }
}