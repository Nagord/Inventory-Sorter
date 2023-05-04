using static UnityEngine.GUILayout;
using PulsarModLoader.CustomGUI;
using System.Collections.Generic;

namespace Inventory_Sorter
{
    internal class GUI : ModSettingsMenu
    {
        string GetNameFromItemType(int type)
        {
            switch (type)
            {
                default:
                    return ((EPawnItemType)type).ToString();
                case 2:
                    return "Phase Pistol";
                case 3:
                    return "Repair Gun";
                case 4:
                    return "Fire Extinguisher";
                case 5:
                    return "Food";
                case 6:
                    return "Generic Items";
                case 7:
                    return "Beam Pistol";
                case 8:
                    return "Smuggler's Pistol";
                case 9:
                    return "Burst Rifle";
                case 10:
                    return "Heavy Beam Pistol";
                case 11:
                    return "Heavy Pistol";
                case 12:
                    return "Splitshot";
                case 15:
                    return "Mission Items";
                case 16:
                    return "Scanner";
                case 17:
                    return "Research Materials";
                case 18:
                    return "Keycards";
                case 19:
                    return "Pulse Grenade Launcher";
                case 20:
                    return "Healing Grenade Launcher";
                case 21:
                    return "Mini Grenade Launcher";
                case 22:
                    return "Fire-Killer Grenade Launcher";
                case 23:
                    return "Repair Grenade Launcher";
                case 24:
                    return "F.B. MultiTool";
                case 25:
                    return "Beam Rifle";
                case 26:
                    return "Healing Beam Rifle";
                case 27:
                    return "Stune Grenade Launcher";
                case 28:
                    return "I.M.P.A.C.T. Prototype";
                case 29:
                    return "S.P.I.K.E.R. Prototype";
                case 30:
                    return "W.D. Heavy";
                case 31:
                    return "Ammo Clip";
                case 33:
                    return "Revitalyzing Syringe";
            }
        }
        string GetSortmodeName(int ID)
        {
            switch (ID)
            {
                case 0:
                    return "Type";
                case 1:
                    return "Name";
                case 2:
                    return "Default";
                default:
                    return "Custom";
            }
        }
        public override void Draw()
        {
            if (Button($"Sortmode = {GetSortmodeName(Global.sortmode.Value)}"))
            {
                if (Global.sortmode.Value < 3)
                {
                    Global.sortmode.Value += 1;
                }
                else
                {
                    Global.sortmode.Value = 0;
                }
            }


            int indexOfChange = 0;
            List<int> list = Global.CustomSorting.Value;
            Label("Custom Sorting, Click an item to move it up.");
            for (int i = 0; i < list.Count; i++)
            {
                if (Button($"{GetNameFromItemType(list[i])}"))
                {
                    indexOfChange = i;
                }
            }
            if (indexOfChange != 0)
            {
                int ChangedValue = list[indexOfChange];
                list.RemoveAt(indexOfChange);
                list.Insert(indexOfChange - 1, ChangedValue);
                Global.CustomSorting.Value = list;
            }
        }

        public override string Name()
        {
            return "Inventory Sorter";
        }
    }
}
