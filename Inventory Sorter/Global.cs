using PulsarModLoader;
using System.Collections.Generic;

namespace Inventory_Sorter
{
    public static class Global
    {
        public static SaveValue<List<int>> CustomSorting = new SaveValue<List<int>>("CustomSorting", new List<int> { (int)EPawnItemType.E_HANDS, (int)EPawnItemType.E_REPAIRGUN, (int)EPawnItemType.E_FB_SELL_TOOL, (int)EPawnItemType.E_FIREGUN,
            (int)EPawnItemType.E_SCANNER, (int)EPawnItemType.E_PHASEPISTOL, (int)EPawnItemType.E_SMUGGLERS_PISTOL, (int)EPawnItemType.E_HEAVY_PISTOL, (int)EPawnItemType.E_BURST_PISTOL, (int)EPawnItemType.E_HAND_SHOTGUN,
            (int)EPawnItemType.E_PIERCING_BEAM_PISTOL, (int)EPawnItemType.E_RANGER, (int)EPawnItemType.E_HELD_BEAM_PISTOL, (int)EPawnItemType.E_HELD_BEAM_PISTOL_W_HEALING, (int)EPawnItemType.E_WD_HEAVY, (int)EPawnItemType.E_ICE_SPIKES,
            (int)EPawnItemType.E_VORTEX_GRENADE, (int)EPawnItemType.E_PULSE_GRENADE, (int)EPawnItemType.E_MINI_GRENADE, (int)EPawnItemType.E_STUN_GRENADE, (int)EPawnItemType.E_HEAL_GRENADE, (int)EPawnItemType.E_REPAIR_GRENADE,
            (int)EPawnItemType.E_ANTIFIRE_GRENADE, (int)EPawnItemType.E_FOOD, (int)EPawnItemType.E_SYRINGE, (int)EPawnItemType.E_RESEARCH_MAT, (int)EPawnItemType.E_QUEST_ITEM, (int)EPawnItemType.E_KEYCARD, (int)EPawnItemType.E_ARTIFACT, 
            (int)EPawnItemType.E_AMMO_CLIP, (int)EPawnItemType.E_BATTERY, (int)EPawnItemType.E_ARMOR, (int)EPawnItemType.E_LASERPISTOL, (int)EPawnItemType.E_JETPACK });

        public static SaveValue<int> sortmode = new SaveValue<int>("SortMode", 2); //0 = type, 1 = name, 2 = modifiedtypeorder (default), 3 = Custom


        public static List<EPawnItemType> ModifiedTypeOrder = new List<EPawnItemType> { EPawnItemType.E_HANDS, EPawnItemType.E_REPAIRGUN, EPawnItemType.E_FB_SELL_TOOL, EPawnItemType.E_FIREGUN,
            EPawnItemType.E_SCANNER, EPawnItemType.E_PHASEPISTOL, EPawnItemType.E_SMUGGLERS_PISTOL, EPawnItemType.E_HEAVY_PISTOL, EPawnItemType.E_BURST_PISTOL, EPawnItemType.E_HAND_SHOTGUN,
            EPawnItemType.E_PIERCING_BEAM_PISTOL, EPawnItemType.E_RANGER, EPawnItemType.E_HELD_BEAM_PISTOL, EPawnItemType.E_HELD_BEAM_PISTOL_W_HEALING, EPawnItemType.E_WD_HEAVY, EPawnItemType.E_ICE_SPIKES,
            EPawnItemType.E_VORTEX_GRENADE, EPawnItemType.E_PULSE_GRENADE, EPawnItemType.E_MINI_GRENADE, EPawnItemType.E_STUN_GRENADE, EPawnItemType.E_HEAL_GRENADE, EPawnItemType.E_REPAIR_GRENADE,
            EPawnItemType.E_ANTIFIRE_GRENADE, EPawnItemType.E_FOOD, EPawnItemType.E_SYRINGE, EPawnItemType.E_RESEARCH_MAT, EPawnItemType.E_QUEST_ITEM, EPawnItemType.E_KEYCARD, EPawnItemType.E_ARTIFACT, 
            EPawnItemType.E_AMMO_CLIP, EPawnItemType.E_BATTERY, EPawnItemType.E_ARMOR, EPawnItemType.E_LASERPISTOL, EPawnItemType.E_JETPACK };


        public static List<EPawnItemType> ZeroMainSubtypePairs = new List<EPawnItemType> { EPawnItemType.E_HANDS, EPawnItemType.E_REPAIRGUN, EPawnItemType.E_FB_SELL_TOOL, EPawnItemType.E_FIREGUN,
            EPawnItemType.E_SCANNER, EPawnItemType.E_PHASEPISTOL, EPawnItemType.E_SMUGGLERS_PISTOL, EPawnItemType.E_HEAVY_PISTOL, EPawnItemType.E_BURST_PISTOL, EPawnItemType.E_HAND_SHOTGUN,
            EPawnItemType.E_PIERCING_BEAM_PISTOL, EPawnItemType.E_RANGER, EPawnItemType.E_HELD_BEAM_PISTOL, EPawnItemType.E_HELD_BEAM_PISTOL_W_HEALING, EPawnItemType.E_WD_HEAVY, EPawnItemType.E_ICE_SPIKES,
            EPawnItemType.E_VORTEX_GRENADE, EPawnItemType.E_PULSE_GRENADE, EPawnItemType.E_MINI_GRENADE, EPawnItemType.E_STUN_GRENADE, EPawnItemType.E_HEAL_GRENADE, EPawnItemType.E_REPAIR_GRENADE,
            EPawnItemType.E_ANTIFIRE_GRENADE};

        public static void Fix0SubtypeList(List<PLPawnItem> list)
        {
            foreach(PLPawnItem item in list)
            {
                if(ZeroMainSubtypePairs.Contains(item.PawnItemType))
                {
                    item.SubType = 0;
                }
            }
        }
    }
}
