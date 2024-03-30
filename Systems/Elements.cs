using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems
{
    public class Elements: ModSystem
    {
        public static HashSet<int> fireNPC = new HashSet<int>();
        public static HashSet<int> fireItem = new HashSet<int>();
        public static HashSet<int> waterNPC = new HashSet<int>();
        public static HashSet<int> waterItem = new HashSet<int>();
        public static HashSet<int> earthNPC = new HashSet<int>();
        public static HashSet<int> earthItem = new HashSet<int>();
        public static HashSet<int> airNPC = new HashSet<int>();
        public static HashSet<int> airItem = new HashSet<int>();
        public static HashSet<int> iceNPC = new HashSet<int>();
        public static HashSet<int> iceItem = new HashSet<int>();
        public static HashSet<int> poisonNPC = new HashSet<int>();
        public static HashSet<int> poisonItem = new HashSet<int>();
        public static HashSet<int> lightNPC = new HashSet<int>();
        public static HashSet<int> lightItem = new HashSet<int>();
        public static HashSet<int> darkNPC = new HashSet<int>();
        public static HashSet<int> darkItem = new HashSet<int>();
        public static HashSet<int> electricNPC = new HashSet<int>();
        public static HashSet<int> electricItem = new HashSet<int>();
        public static HashSet<int> magicNPC = new HashSet<int>();
        public static HashSet<int> magicItem = new HashSet<int>();

        public override void Load()
        {
            //FireNPC
            fireNPC.Add(NPCID.Lavabat);
            //WaterItems
            waterItem.Add(ItemID.Muramasa);
            waterItem.Add(ItemID.WaterBolt);
        }
        public override void Unload()
        {
            fireNPC.Clear();
            fireItem.Clear();
            waterNPC.Clear();
            waterItem.Clear();
            earthNPC.Clear();
            earthItem.Clear();
            airNPC.Clear();
            airItem.Clear();
            iceNPC.Clear();
            iceItem.Clear();
            poisonNPC.Clear();
            poisonItem.Clear();
            lightNPC.Clear();
            lightItem.Clear();
            darkNPC.Clear();
            darkItem.Clear();
            electricNPC.Clear();
            electricItem.Clear();
            magicNPC.Clear();
            magicItem.Clear();
        }
    }
    public class ElementPlayer : ModPlayer
    {
        const float SuperStrongDamageBonus = 0.5f;
        const float StrongDamageBonus = 0.2f;
        const float WeakDamageBonus = -0.2f;
        public static float getItemToNPCModifer(bool Fire, bool Water, bool Earth, bool Air, bool Ice, bool Poison, bool Light, bool Dark, bool Electric, bool Magic, NPC target)
        {
            float mod = 1f;
            if (Elements.fireNPC.Contains(target.type))
            {
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += StrongDamageBonus;
            }
            if (Elements.waterNPC.Contains(target.type))
            {
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.earthNPC.Contains(target.type))
            {
                if (Magic) mod += WeakDamageBonus;
                if (Earth) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Light) mod += StrongDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.airNPC.Contains(target.type))
            {
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            return mod;
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            ElementProjectile eProj = proj.GetGlobalProjectile<ElementProjectile>();
            getItemToNPCModifer(eProj.Fire,eProj.Water,eProj.Earth,eProj.Air,eProj.Ice,eProj.Poison,eProj.Light,eProj.Dark,eProj.Electric,eProj.Magic, target);
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithItem(item, target, ref modifiers);
        }
    }
    public class ElementProjectile : GlobalProjectile
    {
        public bool Fire = false;
        public bool Water = false;
        public bool Earth = false;
        public bool Air = false;
        public bool Ice = false;
        public bool Poison = false;
        public bool Light = false;
        public bool Dark = false;
        public bool Electric = false;
        public bool Magic = false;
        public bool Elementless
        {
            get { return !Fire && !Water && !Earth && !Air && !Ice && !Poison && !Light && !Dark && !Electric && !Magic; }
            set { Fire = false; Water = false; Earth = false; Air = false; Ice = false; Poison = false; Light = false; Dark = false; Electric = false; Magic = false; }
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            // sets the element based on the item
            if (!ProjectileSets.DontInheritElementFromWeapon[projectile.type])
            {
                if(source is EntitySource_ItemUse eSource)
                {
                    int item = eSource.Item.type;
                    if (Elements.fireItem.Contains(item))
                        Fire = true;
                    if (Elements.waterItem.Contains(item))
                        Water = true;
                    if (Elements.earthItem.Contains(item))
                        Earth = true;
                    if (Elements.airItem.Contains(item))
                        Air = true;
                    if (Elements.iceItem.Contains(item))
                        Ice = true;
                    if (Elements.poisonItem.Contains(item))
                        Poison = true;
                    if (Elements.lightItem.Contains(item))
                        Light = true;
                    if (Elements.darkItem.Contains(item))
                        Dark = true;
                    if (Elements.electricItem.Contains(item))
                        Electric = true;
                    if (Elements.magicItem.Contains(item))
                        Magic = true;
                }
            }
        }
    }
}
