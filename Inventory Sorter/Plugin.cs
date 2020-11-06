using PulsarPluginLoader;

namespace Inventory_Sorter
{
    public class Plugin : PulsarPlugin
    {
        public override string Version => "0.3.0";

        public override string Author => "Dragon";

        public override string ShortDescription => "Sorts inventories";

        public override string Name => "InventorySorter";

        public override string HarmonyIdentifier()
        {
            return "Dragon.InventorySorter";
        }
    }
}