using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GrayZapinator
{
    internal class DuckHunter : Spell
    {
        private int duckSpawnTimer = 0;
        private int duckHoldTimer = 160;
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => 9;
        public override int[] SpellItem => new int[] { ItemID.ZapinatorGray };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity * 1.5f, ModContent.ProjectileType<DuckHunterShot>(), (int)(damage * 0.3f), knockback);
            return false;
        }
        public override void Update(Item item, Player player)
        {
            if (player.HeldItem != item && duckHoldTimer <= 0) return;
            if (player.HeldItem == item) duckHoldTimer = 160;

            duckSpawnTimer++;
            duckHoldTimer--;

            if (duckSpawnTimer > 50)
            {
                int randomYPos = Main.rand.Next((int)player.Center.Y - Main.ScreenSize.Y / 2, (int)player.Center.Y + Main.ScreenSize.Y / 2);
                bool flipped = Main.rand.NextBool(1,2);
                
                Projectile.NewProjectileDirect(player.GetSource_ItemUse(item), new Vector2((int)player.Center.X - (flipped? Main.ScreenSize.X / 2 : -Main.ScreenSize.X / 2), randomYPos), new Vector2((flipped ? 0.6f : -0.6f), 0), ModContent.ProjectileType<DuckHunterDuck>(), (int)(item.damage * 2f), 0f, player.whoAmI);
                duckSpawnTimer = 0;
            }
        }
    }

    public class DuckHunterShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(14, 14);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
        }

        NPC targetDuck = null;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.scale = 0.8f + (float)Math.Sin(Main.timeForVisualEffects / 10f) / 10f;
            if (Projectile.ai[0] % 2 == 0)
            {
                Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.4f, 0.4f));

                PixelCircle p = new PixelCircle();
                p.outlineColor = new Color(160, 160, 160);
                p.scale = 2;
                p.tileCollide = true;
                ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(0, 0).RotatedBy(Projectile.rotation + MathHelper.PiOver2), Vector2.Zero, new Color(255, 255, 255));
            }

            Projectile.ai[0]++;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                PixelCircle p = new PixelCircle();
                p.outlineColor = new Color(160, 160, 160);
                p.scale = 4;
                p.tileCollide = true;
                p.gravity = 0f;
                p.affectedByLight = true;
                p.outlineAffectedByLight = true;
                ParticleSystem.AddParticle(p, Projectile.Center, new Vector2(2, 0).RotatedBy((MathHelper.TwoPi / 6f) * i), new Color(255, 255, 255));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.White);
            return false;
        }
    }
    public class DuckHunterDuck : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(56, 34);
            Projectile.timeLeft = 36000;
            Projectile.friendly = true;
            Projectile.extraUpdates = 10;
            Projectile.gfxOffY = -11;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = 0;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.ai[1] == 1? true : false;
        }

        public override bool CanHitPvp(Player target)
        {
            return Projectile.ai[1] == 1 ? true : false;
        }

        int frameIncrement = 1;
        NPC target = null;
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[1] == 0) Projectile.velocity.Y = (float)Math.Sin(Projectile.ai[0] / 200f) / 10f; 
            else
            {
                target = TFUtils.FindClosestNPC(1200f, Projectile.Center);
                if (target != null) Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(target.Center) * 0.7f, 0.01f);
                else Projectile.Kill();
            }

            if (Projectile.ai[0] % (6 * Projectile.extraUpdates) == 0)
            {

                PixelCircle p = new PixelCircle();
                p.outlineColor = new Color(160, 160, 160);
                p.scale = 2;
                p.tileCollide = true;
                p.affectedByLight = true;
                p.outlineAffectedByLight = true;
                ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(20,0).RotatedBy(Projectile.rotation + MathHelper.PiOver2), Vector2.Zero, new Color(255, 255, 255));

                if (Projectile.frame == 2) frameIncrement = -1;
                else if (Projectile.frame == 0) frameIncrement = 1;
                Projectile.frame += frameIncrement;
            }

            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.friendly && Main.player[proj.owner].team == Main.player[Projectile.owner].team && proj.type != Type && proj.damage > 0 && proj.Colliding(Projectile.Hitbox, proj.Hitbox))
                {
                    SoundStyle killSound = new SoundStyle("Terrafirma/Sounds/DuckHunterShot");
                    SoundEngine.PlaySound(killSound, Projectile.Center);
                    Projectile.penetrate = 1;
                    Projectile.ai[1] = 1;
                }
            }

            if(Main.LocalPlayer == Main.player[Projectile.owner] && 
                (Projectile.Center.X >= Main.player[Projectile.owner].Center.X + Main.ScreenSize.X / 2 + 100 || Projectile.Center.X <= Main.player[Projectile.owner].Center.X - Main.ScreenSize.X / 2 - 100))
            {
                Projectile.Kill();
            }

            Projectile.ai[0]++;
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[1] == 1)
            {
                SoundStyle killSound = new SoundStyle("Terrafirma/Sounds/DuckEliminated");
                SoundEngine.PlaySound(killSound, Projectile.Center);
            }

            for (int i = 0; i < 6; i++)
            {
                PixelCircle p = new PixelCircle();
                p.outlineColor = new Color(160, 160, 160);
                p.scale = 4;
                p.tileCollide = true;
                p.gravity = 0f;
                p.affectedByLight = true;
                p.outlineAffectedByLight = true;
                ParticleSystem.AddParticle(p, Projectile.Center, new Vector2(2,0).RotatedBy((MathHelper.TwoPi / 6f) * i), new Color(255, 255, 255));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Lerp(Color.White, lightColor, 0.8f));
            return false;
        }

    }
}
