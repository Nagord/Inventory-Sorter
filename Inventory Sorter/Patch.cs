using HarmonyLib;
using PulsarModLoader.Utilities;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace Inventory_Sorter
{
    class PawnItemSorter
    {
        public static int Compare(PLPawnItem x, PLPawnItem y)
        {
            //Run through null sorting
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y == null)
                    return 1;


                //All Sorting
                else
                {


                    //Sortby type
                    if (Global.sortmode == 0)
                    {
                        if ((int)x.PawnItemType == (int)y.PawnItemType)
                        {
                            if (x.SubType == y.SubType)
                            {
                                if (x.Level == y.Level) return 0;
                                else if (x.Level > y.Level) return 1;
                                else return -1;
                            }
                            else if (x.SubType > y.SubType) return 1;
                            else return -1;
                        }
                        else if ((int)x.PawnItemType > (int)y.PawnItemType) return 1;
                        else return -1;
                    }


                    //sortby name
                    else if (Global.sortmode == 1)
                    {
                        return x.GetItemName().CompareTo(y.GetItemName());
                    }


                    //Sortby Refined
                    else
                    {
                        if (x.PawnItemType == y.PawnItemType)
                        {
                            if (x.SubType == y.SubType)
                            {
                                if (x.Level == y.Level) return 0;
                                else if (x.Level > y.Level) return 1;
                                else return -1;
                            }
                            else if (x.SubType > y.SubType) return 1;
                            else return -1;
                        }
                        else if (Global.ModifiedTypeOrder.IndexOf(x.PawnItemType) > Global.ModifiedTypeOrder.IndexOf(y.PawnItemType)) return 1;
                        else return -1;
                    }
                }
            }
        }
    }
    class PawnItemDisplaySorter
    {
        public static int Compare(PLTabMenu.PawnItemDisplay x, PLTabMenu.PawnItemDisplay y)
        {
            //Null sorting first
            if (x == null)
            {
                if (y == null) return 0;
                else return -1;
            }
            else
            {
                if (y == null) return 1;

                //Actual sorting
                else return PawnItemSorter.Compare(x.Item, y.Item);
            }
        }
    }

    //Locker inventories
    [HarmonyPatch(typeof(PLPawnInventoryBase), "UpdateItem")]
    class UpdateItemPatch
    {
        static void Postfix(PLPawnInventoryBase __instance)
        {
            __instance.AllItems.Sort(PawnItemSorter.Compare);
        }
    }
    [HarmonyPatch(typeof(PLPawnInventoryBase), "ServerItemSwap")]
    class ServerItemSwapPatch
    {
        static void Postfix(PLPawnInventoryBase __instance, int targetInvID)
        {
            __instance.AllItems.Sort(PawnItemSorter.Compare);
            PLNetworkManager.Instance.GetInvAtID(targetInvID).AllItems.Sort(PawnItemSorter.Compare);
        }
    }


    class TabMenuDisplaysPatches
    {
        static void PatchMethod(List<PLTabMenu.PawnItemDisplay> list)
        {
            list.Sort(PawnItemDisplaySorter.Compare);
        }

        //Tab menu Inventory
        [HarmonyPatch(typeof(PLTabMenu), "UpdatePIDs")]
        class UpdatePIDsPatch
        {
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
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TabMenuDisplaysPatches), "PatchMethod")),
                };

                return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);
            }
        }

        //Container Menus (Lockers)
        [HarmonyPatch(typeof(PLTabMenu), "UpdateContainerMenu")]
        class UpdateContainerMenuPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                //Locker player inventory PID list
                List<CodeInstruction> targetSequence = new List<CodeInstruction>()
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "DisplayedPIDS_MyInventory")),
                    new CodeInstruction(OpCodes.Ldloc_S),
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<PLTabMenu.PawnItemDisplay>), "Add")),
                };

                List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "DisplayedPIDS_MyInventory")),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TabMenuDisplaysPatches), "PatchMethod")),
                };

                instructions = PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);


                //Current locker PID list
                targetSequence = new List<CodeInstruction>()
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "DisplayedPIDS_Container")),
                    new CodeInstruction(OpCodes.Ldloc_S),
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<PLTabMenu.PawnItemDisplay>), "Add")),
                };

                InjectedSequence = new List<CodeInstruction>()
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLTabMenu), "DisplayedPIDS_Container")),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TabMenuDisplaysPatches), "PatchMethod")),
                };

                return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);
            }
        }
    }
}
