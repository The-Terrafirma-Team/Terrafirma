using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Attributes;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Movement.SpringBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    class SpringBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(26, 36);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 75, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            player.noFallDmg = true;

            player.jumpSpeedBoost = player.GetModPlayer<SpringBootsPlayer>().JumpMultiplier * player.GetModPlayer<SpringBootsPlayer>().JumpMultiplier / 2;
            player.maxRunSpeed = 3 * player.GetModPlayer<SpringBootsPlayer>().JumpMultiplier;

            player.GetModPlayer<SpringBootsPlayer>().SpringBoots = true;
        }

        public override void AddRecipes()
        {

            CreateRecipe().AddIngredient(ItemID.HermesBoots, 1).AddIngredient(ItemID.LuckyHorseshoe, 1).AddTile(TileID.TinkerersWorkbench).Register();
            CreateRecipe().AddIngredient(ItemID.SailfishBoots, 1).AddIngredient(ItemID.LuckyHorseshoe, 1).AddTile(TileID.TinkerersWorkbench).Register();
            CreateRecipe().AddIngredient(ItemID.FlurryBoots, 1).AddIngredient(ItemID.LuckyHorseshoe, 1).AddTile(TileID.TinkerersWorkbench).Register();
            CreateRecipe().AddIngredient(ItemID.SandBoots, 1).AddIngredient(ItemID.LuckyHorseshoe, 1).AddTile(TileID.TinkerersWorkbench).Register();
        }

    }
    public class SpringBootsPlayer : TFModPlayer
    {
        [ResetDefaults(false)]
        public bool SpringBoots = true;
        public Vector2 Momentum = Vector2.Zero;
        public int FloorTime = 0;
        public float JumpMultiplier = 1f;
        public override void PostUpdateRunSpeeds()
        {
            if (SpringBoots)
            {
                Player.runSlowdown = 0.2f;

                if (Player.velocity.Y != 0) FloorTime = 0;
                else FloorTime++;

                if (FloorTime > 10) JumpMultiplier = 1f;

            }
        }

        public override void PreUpdateMovement()
        {

            if (SpringBoots)
            {
                Player.frogLegJumpBoost = true;

                if (Player.justJumped)
                {

                    if (JumpMultiplier > 1)
                    {
                        SoundStyle boing = new SoundStyle("Terrafirma/Assets/Sounds/Boing", SoundType.Sound);
                        boing.Volume = 0.8f;
                        boing.PitchRange = (-0.1f, 0.1f);
                        boing.Pitch -= JumpMultiplier / 10;

                        SoundEngine.PlaySound(boing, Player.position);
                    }

                    JumpMultiplier = MathHelper.Clamp(JumpMultiplier * 1.25f, 1f, 3f);
                }
            }
        }
    }
}
