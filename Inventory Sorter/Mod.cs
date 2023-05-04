using PulsarModLoader;

namespace Inventory_Sorter
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.1.0";

        public override string Author => "Dragon";

        public override string LongDescription => "Sorts inventories";

        public override string Name => "InventorySorter";

        public override string HarmonyIdentifier()
        {
            return "Dragon.InventorySorter";
        }
    }
}