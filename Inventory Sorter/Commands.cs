using PulsarPluginLoader.Chat.Commands;
using PulsarPluginLoader.Utilities;

namespace Inventory_Sorter
{
    class Commands : IChatCommand
    {
        public string[] CommandAliases()
        {
            return new string[] { "sortby" };
        }

        public string Description()
        {
            return "Changes sortmode for inventories. sortmodes: type, name, default.";
        }

        public bool Execute(string arguments, int SenderID)
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
                default:
                    Messaging.Notification("Syntax: /sortby [sortmode]. sortmodes: type, name, default.");
                    break;
            }
            
            return false;
        }

        public bool PublicCommand()
        {
            return false;
        }

        public string UsageExample()
        {
            return "/sortby [sortmode]";
        }
    }
}
