using System.Reflection;
using Terrafirma.Common.Attributes;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Defense.SapphireWard
{
    public class SapphireWard : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<SapphireWardPlayer>().Active = true;
        }
    }
    public class SapphireWardPlayer : TFModPlayer
    {
        [ResetDefaults(false)]
        public bool Active = false;
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (Active && proj.reflected && proj.owner == Player.whoAmI)
            {
                modifiers.ScalingArmorPenetration += -0.5f;
            }
        }
    }
}
