using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    public class GlobalItemInstanced : GlobalItem
    {
        public Spell Spell = null;
        public bool droppedForRecovery = false;

        public bool spawnAmmoParticle = false;
        public bool defaultShootingAnimation = false;
        public override bool InstancePerEntity => true;

        //Net Send & Recieve
        //public override void NetSend(Item item, BinaryWriter writer)
        //{
        //    writer.Write(Spell);
        //}
        //public override void NetReceive(Item item, BinaryReader reader)
        //{
        //    Spell = reader.ReadInt32();
        //}

        //Modify Tooltips
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.ManaFlower || item.type == ItemID.ManaCloak || item.type == ItemID.MagnetFlower || item.type == ItemID.ArcaneFlower) tooltips.Remove(tooltips.Where(tooltip => tooltip.Name == "Tooltip1").FirstOrDefault());

            base.ModifyTooltips(item, tooltips);
        }
        public override bool GrabStyle(Item item, Player player)
        {
            if (droppedForRecovery)
            {
                item.velocity = Vector2.Lerp(item.Center.DirectionTo(player.Bottom) * 15,item.velocity,0.9f);
                return false;
            }
                return base.GrabStyle(item, player);
        }
        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (droppedForRecovery)
            {
                grabRange += 50;
                grabRange = (int)(grabRange * player.PlayerStats().ThrowerGrabRange);
            }
        }
        public override bool OnPickup(Item item, Player player)
        {
            droppedForRecovery = false;
            return base.OnPickup(item, player);
        }
        //Set Defaults
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.ManaFlower)
            {

            }

            if (entity.DamageType == DamageClass.Summon && entity.type <= 5455 && !entity.sentry)
            {
                entity.useTime = entity.useAnimation = 16;
                entity.mana = 0;
            }

            if (entity.DamageType == DamageClass.Ranged)
            {
                spawnAmmoParticle = true;
                defaultShootingAnimation = true;
            }
        }

        //CanConsumeAmmo

        int[] BulletSlots = new int[] { };
        int canrand = 0;
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {

            #region Ammo Can
            //Ammo Can
            if (weapon.useAmmo == AmmoID.Bullet && player.GetModPlayer<TerrafirmaModPlayer>().AmmoCan)
            {
                BulletSlots = new int[] { };
                for (int i = 54; i < 58; i++)
                {
                    if (player.inventory[i].ammo == AmmoID.Bullet)
                    {

                        BulletSlots = BulletSlots.Append(i).ToArray();
                    }
                }

                return false;
            }
            #endregion

            return base.CanConsumeAmmo(weapon, ammo, player);
        }

        //ModifyShootStats
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            #region Ammo Can
            //Ammo Can
            if (item.useAmmo == AmmoID.Bullet && player.GetModPlayer<TerrafirmaModPlayer>().AmmoCan && BulletSlots.Length != 0)
            {


                canrand = BulletSlots[Main.rand.Next(BulletSlots.Length)];

                type = player.inventory[canrand].shoot;
                player.inventory[canrand].stack--;

                BulletSlots = new int[] { };
            }
            #endregion

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool CanShoot(Item item, Player player)
        {
            if (item.type == ItemID.IceBlock) return false;

            return base.CanShoot(item, player);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (spawnAmmoParticle)
            {
                AmmoDropParticle particle = new AmmoDropParticle();
                particle.itemID = source.AmmoItemIdUsed;
                ParticleSystem.AddParticle(particle, player.Center + Vector2.Normalize(velocity) * (item.width/6), (-velocity + new Vector2(0,Main.rand.NextFloat(-5,-3))) * Main.rand.NextFloat(0.15f,0.4f));
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            //Gun shooting animation
            if (defaultShootingAnimation && item.ModItem == null && item.useAmmo == AmmoID.Bullet) PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.01f, 0.03f), 3, 0);
            switch (item.type)
            {
                case ItemID.Boomstick: PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.03f, 0.08f), 6, 1); break;
                case ItemID.Shotgun: PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.05f, 0.1f), 8, 3); break;
                case ItemID.TacticalShotgun: PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.03f, 0.08f), 6, 2); break;
                case ItemID.QuadBarrelShotgun: PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.04f, 0.09f), 7, 2); break;

                case ItemID.SniperRifle: PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.015f, 0.03f), 15, 0); break;
            }

            base.UseStyle(item, player, heldItemFrame);
        }

    }
}
