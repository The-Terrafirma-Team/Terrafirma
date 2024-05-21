using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Humanizer;
using Terrafirma.Systems.MageClass;

namespace Terrafirma.Systems.AccessorySynergy
{
    public abstract class AccessorySynergy : ModType
    {
        public virtual List<int> SynergyAccessories => new List<int> { };
        public override void Load()
        {
            if (!SynergyID.SynergyIDs.Contains(this)) SynergyID.SynergyIDs.Add(this);

            Language.GetOrRegister("Mods.Terrafirma.AccessorySynergy." + $"{this.GetType().Name}" + ".Name", CreateSynergyName);
            Language.GetOrRegister("Mods.Terrafirma.AccessorySynergy." + $"{this.GetType().Name}" + ".Description", CreateSynergyDescription);
        }
        

        string CreateSynergyName() => this.GetType().Name.Titleize();

        static string CreateSynergyDescription() => "";


        public string GetSynergyName()
        {
            return Language.GetTextValue("Mods.Terrafirma.AccessorySynergy." + $"{this.GetType().Name}" + ".Name");
        }
        public string GetSynergyDesc()
        {
            return Language.GetTextValue("Mods.Terrafirma.AccessorySynergy." + $"{this.GetType().Name}" + ".Description");
        }

        protected override void Register()
        {
            ModTypeLookup<AccessorySynergy>.Register(this);
        }

    }

    public static class AccessorySynergyMethods
    {
        /// <summary>
        /// Use this to check for Synergies, contains() doesn't work :(
        /// </summary>
        public static bool ContainsSynergy(this List<AccessorySynergy> SynergyList, AccessorySynergy Synergy)
        {
            for (int i = 0; i < SynergyList.Count; i++)
            {
                if (SynergyList[i].GetSynergyName() == Synergy.GetSynergyName()) return true;
            }
            return false;

        }

        /// <summary>
        /// Use this to check for Synergies, contains() doesn't work :(
        /// </summary>
        public static bool ContainsSynergy(this AccessorySynergy[] SynergyList, AccessorySynergy Synergy)
        {
            for (int i = 0; i < SynergyList.Length; i++)
            {
                if (SynergyList[i].GetSynergyName() == Synergy.GetSynergyName()) return true;
            }
            return false;

        }
    }

    public class SynergyID : ModSystem
    {
        public static List<AccessorySynergy> SynergyIDs = new List<AccessorySynergy> { };
    }
}
