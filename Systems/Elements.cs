using Microsoft.Xna.Framework;
using rail;
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
            AddNPCSToSets();
            AddItemsToSets();
        }
        private void AddNPCSToSets()
        {
            #region fire
            AddIDRange(fireNPC, 23, 25);
            AddIDRange(fireNPC, 29, 30);
            AddIDRange(fireNPC, 59, 60);
            fireNPC.Add(72);
            fireNPC.Add(151);
            AddIDRange(earthNPC, 245, 249);
            AddIDRange(fireNPC, 277, 280);
            fireNPC.Add(325);
            fireNPC.Add(378);
            AddIDRange(fireNPC, 412, 418);
            AddIDRange(fireNPC, 471, 472);
            AddIDRange(fireNPC, 516, 519);
            fireNPC.Add(527);
            fireNPC.Add(533);
            fireNPC.Add(551);
            AddIDRange(fireNPC, 572, 575);
            fireNPC.Add(614);
            fireNPC.Add(665);
            #endregion fire
            #region water
            //Should take another look through this at some point
            AddIDRange(waterNPC, -55, -54);
            AddIDRange(waterNPC, 32, 33);
            AddIDRange(waterNPC, 63, 64);
            waterNPC.Add(166);
            AddIDRange(waterNPC, 220, 221);
            AddIDRange(waterNPC, 223, 225);
            waterNPC.Add(250);
            waterNPC.Add(268);
            AddIDRange(waterNPC, 370, 373);
            waterNPC.Add(465);
            waterNPC.Add(526);
            waterNPC.Add(586);
            waterNPC.Add(587);
            AddIDRange(waterNPC, 618, 623);
            #endregion water
            #region earth
            AddIDRange(earthNPC, -53, -46);
            AddIDRange(earthNPC, 7, 15);
            earthNPC.Add(21);
            earthNPC.Add(23);
            AddIDRange(earthNPC, 43, 45);
            earthNPC.Add(56);
            earthNPC.Add(69);
            earthNPC.Add(77);
            AddIDRange(earthNPC, 95, 101);
            earthNPC.Add(153);
            AddIDRange(earthNPC, 163, 165);
            AddIDRange(earthNPC, 201, 203);
            AddIDRange(earthNPC, 218, 220);
            AddIDRange(earthNPC, 236, 238);
            AddIDRange(earthNPC, 245, 249);
            AddIDRange(earthNPC, 254, 261);
            AddIDRange(earthNPC, 262, 265);
            AddIDRange(earthNPC, 449, 452);
            AddIDRange(earthNPC, 494, 506);
            earthNPC.Add(8);
            AddIDRange(earthNPC, 510, 515);
            AddIDRange(earthNPC, 524, 532);
            earthNPC.Add(537);
            AddIDRange(earthNPC, 542, 545);
            AddIDRange(earthNPC, 568, 569);
            AddIDRange(earthNPC, 580, 582);
            earthNPC.Add(635);
            earthNPC.Add(631);
            #endregion earth
            #region air
            AddIDRange(airNPC, -65, -56);
            AddIDRange(airNPC, -43, -38);
            AddIDRange(airNPC, -23, -16);
            AddIDRange(airNPC, -12, -11);
            airNPC.Add(2);
            AddIDRange(airNPC, 4, 6);
            airNPC.Add(34);
            airNPC.Add(42);
            AddIDRange(airNPC, 48, 49);
            airNPC.Add(51);
            AddIDRange(airNPC, 60, 62);
            airNPC.Add(66);
            AddIDRange(airNPC, 87, 94);
            AddIDRange(airNPC, 121, 122);
            airNPC.Add(133);
            airNPC.Add(137);
            AddIDRange(airNPC, 150, 152);
            airNPC.Add(156);
            AddIDRange(airNPC, 170, 171);
            airNPC.Add(173);
            airNPC.Add(176);
            airNPC.Add(180);
            airNPC.Add(182);
            AddIDRange(airNPC, 190, 194);
            airNPC.Add(205);
            airNPC.Add(224);
            airNPC.Add(226);
            AddIDRange(airNPC, 231, 235);
            airNPC.Add(252);
            airNPC.Add(258);
            AddIDRange(airNPC, 317, 318);
            AddIDRange(airNPC, 327, 328);
            airNPC.Add(347);
            airNPC.Add(370);
            airNPC.Add(388);
            AddIDRange(airNPC, 407, 408);
            AddIDRange(airNPC, 412, 414);
            AddIDRange(airNPC, 418, 421);
            AddIDRange(airNPC, 426, 428);
            AddIDRange(airNPC, 454, 459);
            airNPC.Add(509);
            airNPC.Add(541);
            airNPC.Add(551);
            AddIDRange(airNPC, 558, 560);
            AddIDRange(airNPC, 574, 575);
            airNPC.Add(581);
            airNPC.Add(587);
            #endregion air
            #region ice
            iceNPC.Add(147);
            iceNPC.Add(150);
            AddIDRange(iceNPC, 154, 155);
            iceNPC.Add(161);
            AddIDRange(iceNPC, 168, 171);
            iceNPC.Add(180);
            AddIDRange(iceNPC, 184, 185);
            iceNPC.Add(206);
            iceNPC.Add(218);
            iceNPC.Add(243);
            iceNPC.Add(343);
            iceNPC.Add(345);
            iceNPC.Add(352);
            iceNPC.Add(431);
            iceNPC.Add(628);
            #endregion ice
            #region poison
            AddIDRange(poisonNPC, -65, -56);
            AddIDRange(poisonNPC, -21, -16);
            poisonNPC.Add(41);
            poisonNPC.Add(141);
            AddIDRange(poisonNPC, 163, 165);
            poisonNPC.Add(176);
            poisonNPC.Add(204);
            AddIDRange(poisonNPC, 231, 238);
            AddIDRange(poisonNPC, 262, 265);
            AddIDRange(poisonNPC, 530, 531);
            #endregion poison
            #region light
            #endregion light
            #region dark
            // add mimics and dungeon skeletons to this list plz
            #endregion dark
            #region electric
            // basically just martians and jellyfish
            #endregion electric
            #region magic
            #endregion magic
        }
        private void AddItemsToSets()
        {
            #region fire
            #endregion fire
            #region water
            #endregion water
            #region earth
            #endregion earth
            #region air
            #endregion air
            #region ice
            #endregion ice
            #region poison
            #endregion poison
            #region light
            #endregion light
            #region dark
            #endregion dark
            #region electric
            #endregion electric
            #region magic
            #endregion magic
        }
        private void AddIDRange(HashSet<int> set, int start, int end)
        {
            for(int i = start; i <= end; i++)
            {
                set.Add(i);
            }
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

            if (Elements.fireNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += StrongDamageBonus;
            }
            if (Elements.waterNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.earthNPC.Contains(target.netID))
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
            if (Elements.airNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.iceNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.poisonNPC.Contains(target.netID))
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
            if (Elements.lightNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Earth) mod += WeakDamageBonus;
                if (Ice) mod += StrongDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += SuperStrongDamageBonus;
            }
            if (Elements.darkNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Earth) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += WeakDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.electricNPC.Contains(target.netID))
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
            if (Elements.magicNPC.Contains(target.netID))
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
