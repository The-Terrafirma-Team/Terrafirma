using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace TerrafirmaRedux.Items.Equipment.Ranged
{
    internal class AmmoCan : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(32, 28);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaGlobalPlayer>().AmmoCan = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

    }

    public class AmmoCanBullet : GlobalItem
    {
        int canrand;
        int ammotype = -1;
        public override bool InstancePerEntity => true;
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            ammotype = -1;

            if (weapon.useAmmo == AmmoID.Bullet && player.GetModPlayer<TerrafirmaGlobalPlayer>().AmmoCan)
            {

                for (int i = 0; i < 10; i++)
                {
                    canrand = Main.rand.Next(54, 58);

                    if (player.inventory[canrand].ammo == AmmoID.Bullet)
                    {
                        player.GetModPlayer<TerrafirmaGlobalPlayer>().CanUseAmmo = false;
                        player.inventory[canrand].stack--;
                        ammotype = player.inventory[canrand].shoot;
                        return false;
                    }

                }

            }

            return base.CanConsumeAmmo(weapon,ammo,player);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (ammotype != -1)
            {
                type = ammotype;
            }
        }
    }
}
