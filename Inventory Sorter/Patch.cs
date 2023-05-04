using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace Inventory_Sorter
{
    class PawnItemSorter
    {
        public static int Compare(PLPawnItem x, PLPawnItem y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    if (Global.sortmode == 0)
                    {
                        if ((int)x.PawnItemType == (int)y.PawnItemType)
                        {
                            if (x.SubType == y.SubType)
                            {
                                if (x.Level == y.Level) { return 0; }
                                else if (x.Level > y.Level) { return 1; }
                                else { return -1; }
                            }
                            else if (x.SubType > y.SubType) { return 1; }
                            else { return -1; }
                        }
                        else if ((int)x.PawnItemType > (int)y.PawnItemType) { return 1; }
                        else { return -1; }
                    }
                    else if (Global.sortmode == 1)
                    {
                        return x.GetItemName().CompareTo(y.GetItemName());
                    }
                    else
                    {
                        if (x.PawnItemType == y.PawnItemType)
                        {
                            if (x.SubType == y.SubType)
                            {
                                if (x.Level == y.Level) { return 0; }
                                else if (x.Level > y.Level) { return 1; }
                                else { return -1; }
                            }
                            else if (x.SubType > y.SubType) { return 1; }
                            else { return -1; }
                        }
                        else if (Global.ModifiedTypeOrder.IndexOf(x.PawnItemType) > Global.ModifiedTypeOrder.IndexOf(y.PawnItemType)) { return 1; }
                        else { return -1; }
                    }
                }
            }
        }
    }
    class PawnItemDisplaySorter
    {
        public static int Compare(PLTabMenu.PawnItemDisplay x, PLTabMenu.PawnItemDisplay y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    return PawnItemSorter.Compare(x.Item, y.Item);
                }
            }
        }
    }
    [HarmonyPatch(typeof(PLPawnInventoryBase), "UpdateItem")]
    class UpdateItemPatch
    {
        private static void Postfix(PLPawnInventoryBase __instance)
        {
            __instance.AllItems.Sort(PawnItemSorter.Compare);
            PLTabMenu.Instance.DisplayedPIDS_MyInventory.Sort(PawnItemDisplaySorter.Compare);
            PLTabMenu.Instance.DisplayedPIDS_Container.Sort(PawnItemDisplaySorter.Compare);
        }
    }
    [HarmonyPatch(typeof(PLPawnInventoryBase), "ServerItemSwap")]
    class ServerItemSwapPatch
    {
        private static void Postfix(PLPawnInventoryBase __instance, ref int targetInvID)
        {
            __instance.AllItems.Sort(PawnItemSorter.Compare);
            PLNetworkManager.Instance.GetInvAtID(targetInvID).AllItems.Sort(PawnItemSorter.Compare);
            PLTabMenu.Instance.DisplayedPIDS_MyInventory.Sort(PawnItemDisplaySorter.Compare);
            PLTabMenu.Instance.DisplayedPIDS_Container.Sort(PawnItemDisplaySorter.Compare);
        }
    }
    [HarmonyPatch(typeof(PLTabMenu), "UpdatePIDs")]
    class TabMenuUpdatePIDsPatch
    {
        static void PatchMethod(List<PLTabMenu.PawnItemDisplay> list)
        {
            list.Sort(PawnItemDisplaySorter.Compare);
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            //Every time something is added to the PID list
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "allPIDs")),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<PLTabMenu.PawnItemDisplay>), "Add")),
            };

            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "allPIDs")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TabMenuUpdatePIDsPatch), "PatchMethod")),
            };

            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);


            //Every time something is removed from the PID list
            /*targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "allPIDs")),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<PLTabMenu.PawnItemDisplay>), "Remove")),
            };

            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);*/
        }
    }
}
