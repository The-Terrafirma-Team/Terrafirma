using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.Flamelash
{
    internal class MarkOfTheDamned : Spell
    {
        public override int UseAnimation => 24;
        public override int UseTime => 24;
        public override int ManaCost => 0;
        public override int ReuseDelay => 40;
        public override int[] SpellItem => [ItemID.Flamelash, ItemID.InfernoFork];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<MarkOfTheDamnedProj>()] > 0)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].type == ModContent.ProjectileType<MarkOfTheDamnedProj>() && Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                    {
                        NPC closestNPC = TFUtils.FindClosestNPC(1000f, Main.MouseWorld);
                        Main.projectile[i].ai[0] = closestNPC == null ? -1 : closestNPC.whoAmI;
                    }
                }
            }
            else
            {
                type = ModContent.ProjectileType<MarkOfTheDamnedProj>();
                position = Main.MouseWorld;
                damage = (int)(damage * 1.1f);
                velocity = Vector2.Zero;

                NPC closestNPC = TFUtils.FindClosestNPC(1000f, Main.MouseWorld);

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, closestNPC == null ? -1 : closestNPC.whoAmI, 0, 0);
            }
            return false;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            return base.CanUseItem(item, player);
        }

        public override void OnLeftMousePressed(Item item, Player player)
        {
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.Shoot;
            item.channel = false;
            Item.staff[item.type] = true;
            base.OnLeftMousePressed(item, player);
        }
    }

    internal class MarkOfTheDamnedProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.timeLeft = 60 * 15;
            Projectile.Opacity = 1f;
            Projectile.Size = new Vector2(54);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Type].Value;
            //Glow
            Main.EntitySpriteDraw(Tex,
                Projectile.Center - Main.screenPosition,
                new Rectangle(58, 0, 60, 60),
                new Color(200, 50, 10, 0) * 0.7f * ((float)((Math.Sin(Main.timeForVisualEffects / 20f) + 1f) / 4f) + 0.6f) * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(30, 30),
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(Tex,
                Projectile.Center - Main.screenPosition,
                new Rectangle(58, 0, 60, 60),
                new Color(200, 50, 10, 0) * 0.1f * ((float)((Math.Sin(Main.timeForVisualEffects / 20f) + 1f) / 4f) + 0.6f) * Projectile.Opacity,
                Projectile.rotation * 0.3f,
                new Vector2(30, 30),
                Projectile.scale * 3f,
                SpriteEffects.None);
            //Main SPrite
            Main.EntitySpriteDraw(Tex,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 54, 54),
                new Color(1f, 1f, 1f, 0.5f) * 1f * ((float)((Math.Sin(Main.timeForVisualEffects / 20f) + 1f) / 4f) + 0.6f) * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(27, 27),
                Projectile.scale,
                SpriteEffects.None);
            return false;
        }
        public override void AI()
        {
            Projectile.rotation += 0.02f;
            Projectile.scale = 1f + (float)((Math.Sin((Main.timeForVisualEffects + 10f) / 20f) + 1f) / 10f);
            if (Main.rand.NextBool(12)) Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), DustID.Torch, Vector2.Zero, Scale: Main.rand.NextFloat(1f, 1.5f));

            if (Projectile.ai[0] != -1 && Main.npc[(int)Projectile.ai[0]].active) Projectile.Center = Main.npc[(int)Projectile.ai[0]].Center;
            else Projectile.Kill();

            if (Projectile.timeLeft < 60) Projectile.Opacity -= 0.02f;

            foreach(Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.friendly && proj.type != ProjectileID.FallingStar && Main.player[proj.owner].heldProj != proj.whoAmI && proj.Center.Distance(Projectile.Center) < 200f && proj != Projectile)
                {
                    proj.velocity = Vector2.Lerp(proj.velocity, proj.DirectionTo(Projectile.Center) * 16, 0.1f);
                    if (Main.myPlayer == Projectile.owner && Projectile.timeLeft % 6 == 0)
                    {

                        Main.player[Projectile.owner].CheckMana(1, true);
                        Main.player[Projectile.owner].manaRegenDelay = MathF.Max(Main.player[Projectile.owner].manaRegenDelay,6);
                        if(Main.player[Projectile.owner].statMana < 1)
                        {
                            Projectile.Kill();
                        }
                    }
                }
            }
            //for (int i = 0; i < Main.projectile.Length; i++)
            //{
            //    if (Main.projectile[i].Center.Distance(Projectile.Center) < 200f && Main.projectile[i] != Projectile && Main.projectile[i].active && !Main.projectile[i].IsTrueMeleeProjectile())
            //    {
            //        Main.projectile[i].velocity = Vector2.Lerp(Main.projectile[i].velocity, Main.projectile[i].DirectionTo(Projectile.Center) * 6f, 0.1f);
            //        if (Main.projectile[i].timeLeft == 0) Main.NewText("A");
            //    }
            //}
        }

        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPlayer(Player target) => false;

        public override void OnKill(int timeLeft)
        {
        }
    }
}
