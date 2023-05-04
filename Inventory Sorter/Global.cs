using System.Collections.Generic;

namespace Inventory_Sorter
{
    public static class Global
    {
        public static int sortmode = 2; //0 = type, 1 = name, 2 = modifiedtypeorder (default)
        public static List<EPawnItemType> ModifiedTypeOrder = new List<EPawnItemType> { EPawnItemType.E_HANDS, EPawnItemType.E_REPAIRGUN, EPawnItemType.E_FB_SELL_TOOL, EPawnItemType.E_FIREGUN,
            EPawnItemType.E_SCANNER, EPawnItemType.E_PHASEPISTOL, EPawnItemType.E_SMUGGLERS_PISTOL, EPawnItemType.E_HEAVY_PISTOL, EPawnItemType.E_BURST_PISTOL, EPawnItemType.E_HAND_SHOTGUN,
            EPawnItemType.E_PIERCING_BEAM_PISTOL, EPawnItemType.E_RANGER, EPawnItemType.E_HELD_BEAM_PISTOL, EPawnItemType.E_HELD_BEAM_PISTOL_W_HEALING, EPawnItemType.E_WD_HEAVY, EPawnItemType.E_ICE_SPIKES,
            EPawnItemType.E_VORTEX_GRENADE, EPawnItemType.E_PULSE_GRENADE, EPawnItemType.E_MINI_GRENADE, EPawnItemType.E_STUN_GRENADE, EPawnItemType.E_HEAL_GRENADE, EPawnItemType.E_REPAIR_GRENADE,
            EPawnItemType.E_ANTIFIRE_GRENADE, EPawnItemType.E_FOOD, EPawnItemType.E_SYRINGE, EPawnItemType.E_BATTERY, EPawnItemType.E_AMMO_CLIP, EPawnItemType.E_RESEARCH_MAT, EPawnItemType.E_QUEST_ITEM,
            EPawnItemType.E_KEYCARD, EPawnItemType.E_ARTIFACT, EPawnItemType.E_ARMOR, EPawnItemType.E_LASERPISTOL, EPawnItemType.E_JETPACK };




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
