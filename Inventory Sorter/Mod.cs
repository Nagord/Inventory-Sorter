using PulsarModLoader;

namespace Inventory_Sorter
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.0.1";

        public override string Author => "Dragon";

        public override string ShortDescription => "Sorts inventories";

        public override string Name => "InventorySorter";

        public override string HarmonyIdentifier()
        {
            return "Dragon.InventorySorter";
        }
    }
}