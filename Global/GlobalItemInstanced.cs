using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class GlobalItemInstanced : GlobalItem
    {
        public byte Spell = 255;
        public override bool InstancePerEntity => true;
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(Spell);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            Spell = reader.ReadByte();
        }
        public override void SetDefaults(Item entity)
        {
            Spell = 0;

            if (entity.DamageType == DamageClass.Summon && entity.type <= 5455 && !entity.sentry)
            {
                entity.useTime = entity.useAnimation = 16;
                entity.mana = 0;
            }

            if (
                entity.type == ItemID.EnchantedBoomerang ||
                entity.type == ItemID.FruitcakeChakram ||
                entity.type == ItemID.IceBoomerang ||
                entity.type == ItemID.Shroomerang ||
                entity.type == ItemID.WoodenBoomerang ||
                entity.type == ItemID.BloodyMachete ||
                entity.type == ItemID.CombatWrench ||
                entity.type == ItemID.Flamarang ||
                entity.type == ItemID.ThornChakram ||
                entity.type == ItemID.Trimarang ||
                entity.type == ItemID.Bananarang ||
                entity.type == ItemID.LightDisc ||
                entity.type == ItemID.FlyingKnife ||
                entity.type == ItemID.PossessedHatchet ||
                entity.type == ItemID.PaladinsHammer 
            )
            {
                entity.DamageType = DamageClass.Ranged;
            }
        }

        //CanConsumeAmmo

        int[] BulletSlots = new int[] { };
        int canrand = 0;
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {

            #region Ammo Can
            //Ammo Can
            if (weapon.useAmmo == AmmoID.Bullet && player.GetModPlayer<TerrafirmaGlobalPlayer>().AmmoCan)
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
            if (item.useAmmo == AmmoID.Bullet && player.GetModPlayer<TerrafirmaGlobalPlayer>().AmmoCan && BulletSlots.Length != 0)
            {


                canrand = BulletSlots[Main.rand.Next(BulletSlots.Length)];

                type = player.inventory[canrand].shoot;
                player.inventory[canrand].stack--;

                BulletSlots = new int[] { };
            }
            #endregion

            #region Silly Ammo Belt Synergy
            //Silly Ammo Belt Synergy
            if (player.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergyNames() != null)
            {
                if (player.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergyNames().Contains("Silly Ammo Belt") && item.useAmmo == AmmoID.Bullet)
                {
                    if (Main.rand.Next(100) < 33)
                    {
                        Item newitem = new Item(TerrafirmaRedux.Mod.BulletArray[Main.rand.Next(TerrafirmaRedux.Mod.BulletArray.Length)]);
                        type = newitem.shoot;
                    }
                }
            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
            #endregion
        }

    }
}
