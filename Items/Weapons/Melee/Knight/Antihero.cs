using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Items.Materials;
using Terrafirma.Projectiles.Melee.Knight;
using Terrafirma.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class Antihero : ModItem
    {
        Projectile swordproj = null;
        public float auraPresence = 0f;
        public float auraRadius = -1;
        public int auraFadeTimer = 0;
        private int damageTick = 4;
        public override void SetDefaults()
        {
            Item.DefaultToSword(75, 10, 3);
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 56;
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/SwordSound2") { PitchVariance = 0.3f, Pitch = -0.45f, MaxInstances = 10 };
            Item.shoot = ModContent.ProjectileType<AntiheroProjectile>();
            Item.shootSpeed = 8;
            Item.rare = ModContent.RarityType<FinalQuestRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.shootSpeed = 16f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Asset<Texture2D> glowtex = ModContent.Request<Texture2D>("Terrafirma/Items/Weapons/Melee/Knight/AntiheroGlow");

            spriteBatch.Draw(glowtex.Value,
                Item.position + Item.Size / 2 - Main.screenPosition,
                glowtex.Frame(),
                new Color(1f,0f,0f,0f) * (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1.5f) / 5f),
                rotation,
                Item.Size / 2 + new Vector2(-6,6),
                scale + (float)(Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 10f,
                SpriteEffects.None,
                1f);
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> glowtex = ModContent.Request<Texture2D>("Terrafirma/Items/Weapons/Melee/Knight/AntiheroGlow");

            spriteBatch.Draw(glowtex.Value,
                position,
                glowtex.Frame(),
                new Color(1f, 0f, 0f, 0f) * (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1.5f) / 5f),
                0f,
                Item.Size / 2 + new Vector2(-6, 6),
                scale + (float)(Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 20f,
                SpriteEffects.None,
                1f);
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool AltFunctionUse(Player player)
        {
            return (player.CheckTension(20,false) || player.ownedProjectileCounts[ModContent.ProjectileType<AntiheroProjectile>()] > 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<AntiheroProjectile>()] > 0)
                {
                    Item.noUseGraphic = false;
                    if (swordproj != null) swordproj.Kill();
                }
                else
                {
                    Item.noUseGraphic = true;
                    swordproj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                    swordproj.scale = Item.scale;
                    swordproj.Size *= Item.scale;
                    player.CheckTension(20, true);
                }
            }
            else
            {
                Item.noUseGraphic = false;
                if (swordproj != null) swordproj.Kill();
                
            }
            return false;
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AntiheroProjectile>()] > 0) auraRadius = -1;
            else auraRadius = MathHelper.Lerp(auraRadius, 200, 0.15f);

            if (auraFadeTimer == 0) auraPresence = MathHelper.Clamp(auraPresence - 0.05f, 0f, 1f);
            auraFadeTimer = (int)MathHelper.Clamp(auraFadeTimer - 1, 0, 10000);

            base.HoldItem(player);
        }
        public override void UseAnimation(Player player)
        {
            base.UseAnimation(player);
        }

        public override bool? UseItem(Player player)
        {
            damageTick--;
            if (player.altFunctionUse != 2)
            {
                auraPresence = 1f;
                auraFadeTimer = 5;
            }
            NPC[] npcArray = new NPC[] { };
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].Center.Distance(player.Center) <= auraRadius && Main.npc[i].active)
                {
                    npcArray = npcArray.Append(Main.npc[i]).ToArray();
                }
            }
            for (int i = 0; i < npcArray.Length; i++)
            {
                if (i % 4 == damageTick)
                {
                    player.ApplyDamageToNPC(npcArray[i], (int)(Item.damage * 0.2f), 0f, npcArray[i].Center.X > player.Center.X ? 1 : -1, damageType: DamageClass.Melee);

                    for (int k = 0; k < 5; k++)
                    {
                        Vector2 vel = Main.rand.NextVector2Circular(4f, 4f);
                        Dust.NewDust(npcArray[i].Center, 4, 4, DustID.Blood, vel.X, vel.Y, Scale: 1.5f);
                    }

                    player.GiveTension(3);
                }
            }
            if (damageTick <= 0) damageTick = 4;
            return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(4f, 4f);
                Dust.NewDust(target.Center, 4, 4, DustID.Blood, vel.X, vel.Y, Scale: 1.5f);
            }
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AntiheroProjectile>()] > 0 && swordproj != null)
            {
                swordproj.ai[1] = 1;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<AntiheroProjectile>()] <= 0;
        }

    }
}
