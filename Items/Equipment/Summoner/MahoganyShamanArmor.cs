using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    [AutoloadEquip(EquipType.Head)]
    public class MahoganyShamanMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.MahoganyShamanMask.SetBonus");
        }
        public override void UpdateVanitySet(Player player)
        {
            //if (Main.timeForVisualEffects % 2 == 0 && (MathF.Abs(player.velocity.X) > player.maxRunSpeed || player.velocity.Y != 0))
            //{
            //    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Terra);
            //    d.velocity = player.velocity;
            //}
            for (int i = 0; i < 2; i++)
            {
                Dust d = Dust.NewDustPerfect(player.Center + new Vector2((2 + i*6) * player.direction, -10 + player.gfxOffY + (player.LegFrameIsOneThatRaisesTheBody() ? -2 : 0) * player.gravDir), DustID.GemEmerald);
                d.scale = 0.6f;
                d.velocity = Vector2.Zero;
                d.alpha = 128;
                d.noGravity = true;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<MahoganyShamanBody>() && legs.type == ModContent.ItemType<MahoganyShamanLegs>();
        }
    }
    [AutoloadEquip(EquipType.Body)]
    public class MahoganyShamanBody : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class MahoganyShamanLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }
    }
}