using Microsoft.Xna.Framework;
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
using static tModPorter.ProgressUpdate;

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
            fireNPC.Add(NPCID.LavaSlime);
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
            bool Elementless = !Fire && !Water && !Earth && !Air && !Ice && !Poison && !Light && !Dark && !Electric && !Magic;
            bool targetElementless = true;

            if (Elements.fireNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += StrongDamageBonus;
            }
            if (Elements.waterNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.earthNPC.Contains(target.type))
            {
                targetElementless = false;
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
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.iceNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.poisonNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += StrongDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += WeakDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.lightNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Earth) mod += WeakDamageBonus;
                if (Ice) mod += StrongDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += SuperStrongDamageBonus;
            }
            if (Elements.darkNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Earth) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += WeakDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.electricNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Magic) mod += StrongDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += SuperStrongDamageBonus;
                if (Earth) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Dark) mod += SuperStrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.magicNPC.Contains(target.type))
            {
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Fire) mod += StrongDamageBonus;
                if(Elementless) mod += WeakDamageBonus;
            }
            if (targetElementless)
            {
                if(Magic) mod += StrongDamageBonus;
            }
            return MathHelper.Clamp(mod,0.1f,3f);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            target.life = target.lifeMax;
            ElementProjectile eProj = proj.GetGlobalProjectile<ElementProjectile>();
            modifiers.FinalDamage *= getItemToNPCModifer(eProj.Fire,eProj.Water,eProj.Earth,eProj.Air,eProj.Ice,eProj.Poison,eProj.Light,eProj.Dark,eProj.Electric,eProj.Magic, target);
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            target.life = target.lifeMax;
            modifiers.FinalDamage *= getItemToNPCModifer(Elements.fireItem.Contains(item.type), Elements.waterItem.Contains(item.type), Elements.earthItem.Contains(item.type), Elements.airItem.Contains(item.type), Elements.iceItem.Contains(item.type), Elements.poisonItem.Contains(item.type), Elements.lightItem.Contains(item.type), Elements.darkItem.Contains(item.type), Elements.electricItem.Contains(item.type), Elements.magicItem.Contains(item.type), target);
        }
    }
    public class ElementProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

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
                int item = Main.player[projectile.owner].HeldItem.type;
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
