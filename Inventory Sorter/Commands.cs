using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace Inventory_Sorter
{
    class Commands : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "sortby" };
        }

        public override string Description()
        {
            return "Changes sortmode for inventories. sortmodes: type, name, default, custom.";
        }

        public override void Execute(string arguments)
        {
            switch (arguments.ToLower())
            {
                case "name":
                case "n":
                    Global.sortmode.Value = 1;
                    Messaging.Notification("Now sorting by name.");
                    break;
                case "type":
                case "t":
                    Global.sortmode.Value = 0;
                    Messaging.Notification("Now sorting by type.");
                    break;
                case "default":
                case "d":
                    Global.sortmode.Value = 2;
                    Messaging.Notification("Now sorting by default sort mode.");
                    break;
                case "custom":
                case "c":
                    Global.sortmode.Value = 3;
                    Messaging.Notification("Now sorting by custom sort mode.");
                    break;
                case "fix":
                    foreach (PLPawnInventoryBase inventory in PLNetworkManager.Instance.AllPawnInventories)
                    {
                        Global.Fix0SubtypeList(inventory.AllItems);
                    }
                    PLTabMenu.Instance.DisplayedPIDS_MyInventory.Clear();
                    PLTabMenu.Instance.DisplayedPIDS_Container.Clear();
                    Messaging.Notification("Fixing known type-subtype pairs");
                    break;
                default:
                    Messaging.Notification("Syntax: /sortby [sortmode]. sortmodes: type, name, default.");
                    break;
            }
        }

        public override string[][] Arguments()
        {
            return new string[][] { new string[] { "name", "type", "default", "custom", "fix" } };
        }
    }
}
