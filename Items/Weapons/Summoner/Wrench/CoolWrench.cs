using Microsoft.Xna.Framework;
using Terrafirma.Common.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class CoolWrench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(12, 28);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2, 0);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            player.WrenchHitSentry(hitbox, SentryBuffID.CoolWrench, 60 * 6);
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 10)
            .AddRecipeGroup("Terrafirma:GoldBar", 6)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.MinionAttackTargetNPC = target.whoAmI;
            base.OnHitNPC(player, target, hit, damageDone);
        }
    }
}
