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
            return "Changes sortmode for inventories. sortmodes: type, name, default.";
        }

        public override void Execute(string arguments)
        {
            switch (arguments.ToLower())
            {
                case "name":
                case "n":
                    Global.sortmode = 1;
                    Messaging.Notification("Now sorting by name.");
                    break;
                case "type":
                case "t":
                    Global.sortmode = 0;
                    Messaging.Notification("Now sorting by type.");
                    break;
                case "default":
                case "d":
                    Global.sortmode = 2;
                    Messaging.Notification("Now sorting by default sort mode.");
                    break;
                case "fix":
                    foreach (PLPawnInventoryBase inventory in PLNetworkManager.Instance.AllPawnInventories)
                    {
                        Global.Fix0SubtypeList(inventory.AllItems);
                    }
                    Messaging.Notification("Fixing known type-subtype pairs");
                    break;
                default:
                    Messaging.Notification("Syntax: /sortby [sortmode]. sortmodes: type, name, default.");
                    break;
            }
        }

        public override string[][] Arguments()
        {
            return new string[][] { new string[] { "name", "n", "type", "t", "default", "d" } };
        }
    }
}
