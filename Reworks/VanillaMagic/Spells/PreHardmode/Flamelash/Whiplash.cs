using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terrafirma.Systems.Primitives;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.Flamelash
{
    internal class Whiplash : Spell
    {
        Projectile whipproj = null;
        bool canshoot = false;
        public override int UseAnimation => 1;
        public override int UseTime => 1;
        public override int ManaCost => 1;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.Flamelash };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<WhiplashProj>()] <= 0)
            {
                type = ModContent.ProjectileType<WhiplashProj>();
                velocity = Vector2.Zero;
                position = player.Center;

                whipproj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            }
            return false;
        }
        public override bool Channeled => true;
        public override void SetDefaults(Item item)
        {
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.Shoot;
        }

    }

    internal class WhiplashProj : ModProjectile
    {
        static FlameTrail trail = new FlameTrail();
        static FlameTrail trail2 = new FlameTrail();
        static FlameTrail trail3 = new FlameTrail();
        Vector2[] trailPoints = new Vector2[] { };
        Projectile[] projArray = new Projectile[] { };
        Texture2D trailTexture;

        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.BallofFire}";

        public override void Load()
        {
            trailTexture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/SeamlessFireTrail").Value;
        }
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;

            Projectile.timeLeft = 60 * 300;
            Projectile.Opacity = 0f;
            Projectile.Size = new Vector2(40);

            trailTexture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/SeamlessFireTrail").Value;

            trail.textureOffsetSlowdown = 15f;
            trail.widthWaveScaling = 2f;
            trail.widthWaveBase = 1.5f;
            trail.widthWaveSlowdown = 2f;
            trail.width = 20f;
            trail.singleColor = false;
            trail.colorStart = new Color(255, 255, 255, 0);
            trail.colorEnd = new Color(255, 60, 0, 0);
            trail.texture = trailTexture;
            trail.pixellate = true;
            trail.flipTextureX = true;
            trail.points = trailPoints;

            trail2.textureOffsetSlowdown = 15f;
            trail2.widthWaveScaling = 2f;
            trail2.widthWaveBase = 2f;
            trail2.widthWaveSlowdown = 4f;
            trail2.width = 20f;
            trail2.singleColor = false;
            trail2.colorStart = new Color(255, 255, 255, 0);
            trail2.colorEnd = new Color(255, 60, 0, 0);
            trail2.texture = trailTexture;
            trail2.pixellate = true;
            trail2.flipTextureX = true;
            trail2.points = trailPoints;

            trail3.textureOffsetSlowdown = 15f;
            trail3.widthWaveScaling = 2f;
            trail3.widthWaveBase = 2f;
            trail3.widthWaveSlowdown = -2f;
            trail3.width = 30f;
            trail3.singleColor = false;
            trail3.colorStart = new Color(200, 50, 100, 0) * 0.5f;
            trail3.colorEnd = new Color(255, 170, 0, 0) * 0.5f;
            trail3.texture = trailTexture;
            trail3.pixellate = true;
            trail3.flipTextureX = true;
            trail3.points = trailPoints;

        }


        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] == 0)
            {
                trail3.QueueDraw(trailPoints);
                trail2.QueueDraw(trailPoints);
                trail.QueueDraw(trailPoints);
            }
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            float curAngle = player.Center.AngleTo(Projectile.Center);

            //Projectile.Center = player.Center + player.Center.DirectionTo(player.PlayerStats().MouseWorld) * (Projectile.ai[2] + 30f);
            float dist = (500f - Projectile.ai[2]) / 500f;
            Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center + player.Center.DirectionTo(player.PlayerStats().MouseWorld) * (Projectile.ai[2] + 30f), dist); 

            if (Projectile.ai[0] == 0)
            {
                if (Projectile.ai[1] == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Projectile proj = Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            player.Center + player.Center.DirectionTo(player.PlayerStats().MouseWorld) * (10f),
                            Vector2.Zero,
                            Type,
                            Projectile.damage,
                            Projectile.knockBack,
                            Projectile.owner,
                            1,
                            (40f * (i + 1)),
                            (10f));
                        projArray = projArray.Append(proj).ToArray();
                    }
                    Projectile.ai[1] = 1;


                }

                if (Projectile.timeLeft % 4 == 0)
                {
                    ColorDot dot = new ColorDot();
                    dot.Size = Main.rand.NextFloat(0.15f,0.3f);
                    dot.TimeInWorld = 40;
                    dot.color = Color.Red;
                    dot.Waviness = 0.1f;
                    ParticleSystem.AddParticle(dot, Projectile.Center, new Vector2(Main.rand.NextFloat(5f, 10f), 0f).RotatedBy(player.Center.AngleTo(player.PlayerStats().MouseWorld)).RotatedByRandom(0.2f), Color.Orange);
                }

                if (Projectile.timeLeft % 1 == 0)
                {
                    trailPoints = new Vector2[] { };
                    trailPoints = trailPoints.Append(Projectile.Center).ToArray();

                    foreach (Projectile proj in projArray)
                    {
                        trailPoints = trailPoints.Append(proj.Center).ToArray();
                    }
                }

                trail.points = trailPoints;
                trail2.points = trailPoints;
                trail3.points = trailPoints;

                foreach (Vector2 point in trailPoints)
                {
                    Lighting.AddLight(point, new Vector3(255, 100, 0) / 200f);
                }
            }
            else
            {
                if (Projectile.timeLeft % 24 == 0)
                {
                    ColorDot dot = new ColorDot();
                    dot.Size = 0.15f;
                    dot.TimeInWorld = 40;
                    dot.color = Color.White * 0.5f;
                    dot.Waviness = 0.1f;
                    ParticleSystem.AddParticle(dot, Projectile.Center, new Vector2(Main.rand.NextFloat(2f, 3f), 0f).RotatedBy(player.Center.AngleTo(player.PlayerStats().MouseWorld)).RotatedByRandom(0.2f));
                }
            }

            Projectile.ai[2] = Projectile.ai[2] < Projectile.ai[1]? Projectile.ai[2] + (Projectile.ai[1] - Projectile.ai[2]) / 20f : Projectile.ai[2];

            if (!player.channel) Projectile.Kill();
            //Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
    }

    public class FlameTrail : TFTrail
    {
        public float segmentPart = 0f;

        public float textureOffsetSlowdown = 10f;

        public float width = 20f;
        public float widthWaveScaling = 2f;
        public float widthWaveBase = 1.5f;
        public float widthWaveSlowdown = 2f;

        public bool singleColor = true;
        public Color color = Color.White;
        public Color colorStart = Color.White;
        public Color colorEnd = Color.White;
        public override float Width(float segment)
        {
            segmentPart = segment;         
            return ((float)(Math.Sin(segment * 10f + Main.timeForVisualEffects / widthWaveSlowdown) + (widthWaveBase - segment) * widthWaveScaling) * (float)Math.Sin(segment * MathHelper.Pi)) * width;
        }

        public override Vector2 TrailOffset(float segment, float segmentRotation)
        {
            return new Vector2(0f, (float)(Math.Sin(segment * 10f + Main.timeForVisualEffects / 10f)) * 10f * (float)Math.Sin(segment * MathHelper.TwoPi)).RotatedBy(segmentRotation);
        }

        public override Color TrailColor(float segment)
        {
            if (singleColor) return color;
            return new Color(
                (int)MathHelper.Lerp(colorStart.R, colorEnd.R, segment),
                (int)MathHelper.Lerp(colorStart.G, colorEnd.G, segment),
                (int)MathHelper.Lerp(colorStart.B, colorEnd.B, segment),
                (int)MathHelper.Lerp(colorStart.A, colorEnd.A, segment)
                );

            //new Color(
            //    MathHelper.Lerp(colorStart.R, colorEnd.G, segment),
            //    150 + (int)(120f * segment),
            //    (int)(255f * (1f - segment)), 1) * (0.5f + (1f - segment) / 2f
            //    );
        }

        public override Vector2 TextureOffset(float segment)
        {
            return new Vector2((float)Main.timeForVisualEffects / textureOffsetSlowdown, 0f);
        }
    }
}
