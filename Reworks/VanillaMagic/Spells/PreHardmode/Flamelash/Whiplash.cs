using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Common.Players;
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
        public override int UseAnimation => 8;
        public override int UseTime => 8;
        public override int ManaCost => 6;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.Flamelash };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (whipproj == null)
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
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.Shoot;
        }

    }

    internal class WhiplashProj : ModProjectile
    {

        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.BallofFire}";
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;

            Projectile.timeLeft = 60 * 300;
            Projectile.Opacity = 0f;
            Projectile.Size = new Vector2(40);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }

        public override void AI()
        {
            base.AI();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
    }

    //internal class WhiplashProj : ModProjectile
    //{
    //    Projectile ownerproj = null;
    //    Projectile[] trailprojectiles = new Projectile[] { };
    //    Vector2[] trailpoints = new Vector2[] { };
    //    Vector2 MousePos = Vector2.Zero;
    //    public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.BallofFire}";
    //    public override void SetDefaults()
    //    {
    //        Projectile.tileCollide = false;
    //        Projectile.friendly = true;
    //        Projectile.penetrate = -1;

    //        Projectile.timeLeft = 60 * 300;
    //        Projectile.Opacity = 0f;
    //        Projectile.Size = new Vector2(40);
    //    }
    //    public override bool PreDraw(ref Color lightColor)
    //    {

    //        if (Projectile.ai[0] == 0 && trailpoints.Length > 0)
    //        {

    //            Trail trail = new Trail(trailpoints, TrailWidthMethod, 10);
    //            trail.offset = TrailOffsetMethod;
    //            trail.trailtexture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/FireTrail").Value;
    //            trail.color = f => new Color(1f, 1f, 0.8f, 0f);
    //            trail.Draw(Projectile.Center);

    //            trail.widthmodifier = TrailWidthMethodMed;
    //            trail.color = f => new Color(1f, 0.6f, 0.2f, 0f) * 0.5f;
    //            trail.Draw(Projectile.Center);

    //            trail.widthmodifier = TrailWidthMethodBig;
    //            trail.color = f => new Color(1f, 0.4f, 0.0f, 0f) * 0.3f;
    //            trail.Draw(Projectile.Center);

    //        }
    //        return true;
    //    }

    //    private float TrailWidthMethod(float x)
    //    {
    //        return (float)Math.Sin((Main.timeForVisualEffects + 30 * x) % 30 / 2f) + 1.5f * Math.Clamp((1f - x) * 5f, 1f, 2f);
    //    }

    //    private float TrailWidthMethodMed(float x)
    //    {
    //        return (float)Math.Sin((Main.timeForVisualEffects + 30 * x) % 30 / 3f) * 4f + 2.5f * Math.Clamp((1f - x) * 5f, 1f, 2f);
    //    }

    //    private float TrailWidthMethodBig(float x)
    //    {
    //        return (float)Math.Sin((Main.timeForVisualEffects + 30 * x) % 30 / 5f) * 7f + 5f * Math.Clamp((1f - x) * 5f, 1f, 2f);
    //    }

    //    private Vector2 TrailOffsetMethod(float x)
    //    {
    //        float pointdirection = Main.player[Projectile.owner].Center.DirectionTo(MousePos).ToRotation();
    //        float sinefloat = (float)Math.Sin((Main.timeForVisualEffects + 20 * x) % 20 / 1.5f) / 10f;
    //        return new Vector2(40, 100 * sinefloat * ((1f - x) * 2.5f)).RotatedBy(pointdirection);
    //    }
    //    public override void OnSpawn(IEntitySource source)
    //    {
    //        if (Projectile.ai[0] == 0)
    //        {
    //            trailprojectiles = trailprojectiles.Append(Projectile).ToArray();
    //            //for (int i = 0; i < 3; i++)
    //            //{
    //            //    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -30 * i), Vector2.Zero, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, i + 1, Projectile.identity);
    //            //    trailprojectiles = trailprojectiles.Append(proj).ToArray();
    //            //}
    //        }
    //    }
    //    public override void AI()
    //    {
    //        if (Main.LocalPlayer == Main.player[Projectile.owner]) Main.player[Projectile.owner].SendMouseWorld();
    //        MousePos = Main.player[Projectile.owner].PlayerStats().MouseWorld;

    //        if (ownerproj == null && Projectile.ai[0] > 0) ownerproj = Main.projectile[(int)Projectile.ai[1]];

    //        //Whip Projectiles
    //        if (Projectile.ai[0] > 0)
    //        {
    //            Projectile.Center = Vector2.Lerp(Projectile.Center, ownerproj.Center + Main.player[Projectile.owner].Center.DirectionTo(MousePos) * 60f * Projectile.ai[0], 0.5f / (Projectile.ai[0] / 3f));
    //            if (!ownerproj.active) Projectile.Kill();

    //            if (Main.rand.NextBool(32))
    //            {
    //                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(25, 0).RotatedBy(Main.player[Projectile.owner].DirectionTo(MousePos).ToRotation()), DustID.OrangeTorch, Main.rand.NextVector2Circular(2f, 0.2f), 0, Color.MintCream, Main.rand.NextFloat(0.8f, 1.5f));
    //            }
    //        }

    //        //Base Projectile
    //        if (Projectile.ai[0] == 0)
    //        {
    //            Projectile.Center = Main.player[Projectile.owner].Center;
    //            for (int i = 0; trailprojectiles.Length > i; i++)
    //            {
    //                if (trailpoints.Length - 1 > i)
    //                {
    //                    trailpoints[i] = trailprojectiles[i].Center;
    //                }
    //                else trailpoints = trailpoints.Append(trailprojectiles[i].Center).ToArray();
    //            }

    //            if (trailprojectiles.Length < 5 && Projectile.ai[2] % 4 == 0 && Projectile.ai[2] != 0)
    //            {
    //                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -30 * trailprojectiles.Length), Vector2.Zero, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, trailprojectiles.Length, Projectile.identity);
    //                proj.Center = Projectile.Center + Main.player[proj.owner].Center.DirectionTo(MousePos) * 50f * (proj.ai[0] - 1);
    //                trailprojectiles = trailprojectiles.Append(proj).ToArray();
    //            }
    //            if (trailprojectiles.Length < 8 && Projectile.ai[2] % 10 == 0 && Projectile.ai[2] != 0)
    //            {
    //                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -30 * trailprojectiles.Length), Vector2.Zero, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, trailprojectiles.Length, Projectile.identity);
    //                proj.Center = Projectile.Center + Main.player[proj.owner].Center.DirectionTo(MousePos) * 50f * (proj.ai[0] - 1);
    //                trailprojectiles = trailprojectiles.Append(proj).ToArray();
    //            }
    //            else if (trailprojectiles.Length < 12 && Projectile.ai[2] % 80 == 0 && Projectile.ai[2] != 0)
    //            {
    //                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -30 * trailprojectiles.Length), Vector2.Zero, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, trailprojectiles.Length, Projectile.identity);
    //                proj.Center = Projectile.Center + Main.player[proj.owner].Center.DirectionTo(MousePos) * 50f * (proj.ai[0] - 1);
    //                trailprojectiles = trailprojectiles.Append(proj).ToArray();
    //            }

    //        }

    //        Projectile.ai[2]++;
    //    }

    //    public override void OnKill(int timeLeft)
    //    {

    //    }
    //}
}
