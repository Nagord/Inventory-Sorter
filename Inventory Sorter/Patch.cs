using HarmonyLib;
using PulsarPluginLoader.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace Inventory_Sorter
{
    class PawnInventorySorter : IComparer<PLPawnItem>
    {
        public int Compare(PLPawnItem x, PLPawnItem y)
        {
            if(x == null || y == null)
            {
                return 0;
            }
            return x.getHash().CompareTo(y.getHash());
        }
    }
    [HarmonyPatch(typeof(PLPawnInventoryBase), "UpdateItem")]
    class UpdateItemPatch
    {
        private static void Postfix(PLPawnInventoryBase __instance)
        {
            string text = string.Empty;
            foreach (PLPawnItem item in __instance.AllItems)
            {
                text += $"\n {item.GetTypeString()}";
            }
            Logger.Info($"Sorting inv UI. The folowing is the current item list {text}");
            PawnInventorySorter PIS = new PawnInventorySorter();
            __instance.AllItems.Sort(PIS);
            string text2 = string.Empty;
            foreach (PLPawnItem item in __instance.AllItems)
            {
                text2 += $"\n {item.GetTypeString()}";
            }
            Logger.Info($"Sorted inv UI. The folowing is the new item list {text}");
        }
    }
    [HarmonyPatch(typeof(PLPawnInventoryBase), "ServerItemSwap")]
    class ServerItemSwapPatch
    {
        private static void Postfix(PLPawnInventoryBase __instance, ref int targetInvID)
        {
            Logger.Info($"Trying to sort inv SIS Target id: {targetInvID} Local id: {__instance.InventoryID}");
            PawnInventorySorter PIS = new PawnInventorySorter();
            __instance.AllItems.Sort(PIS);
            PLNetworkManager.Instance.GetInvAtID(targetInvID).AllItems.Sort(PIS);
        }
    }
}
