using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Dungeon
{
    internal class SkeletonHand : Spell
    {
        public override int UseAnimation => 60;
        public override int UseTime => 60;
        public override int ManaCost => 40;
        public override int[] SpellItem => new int[] { ItemID.BookofSkulls };

        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override bool CanUseItem(Item item, Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<SkeletonHandProj>()] < 1 ? base.CanUseItem(item, player) : false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SkeletonHandProj>();
            velocity = Vector2.Zero;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class SkeletonHandProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }

        Vector2 OriginalPos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(0, 0);
            Projectile.timeLeft = 400;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            DrawOffsetX = -18;
            DrawOriginOffsetY = -28;
        }
        public override void AI()
        {


            if (Projectile.ai[0] == 0)
            {
                Projectile.frame = 0;
                Projectile.velocity = new Vector2(0, -5f);
                Projectile.position = Main.MouseWorld;
                OriginalPos = Projectile.Center + new Vector2(14, 0);
            }
            else if (Projectile.ai[0] < 60)
            {
                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y + 0.1f, -5f, 0f);
            }
            else
            {
                Projectile.frame = 1;
                NPC[] AreaNPCs = new NPC[] { };
                if (Projectile.ai[0] % 40 == 0 && AreaNPCs != null)
                {
                    AreaNPCs = TFUtils.GetAllNPCsInArea(400f, Projectile.Center + new Vector2(18, 28));
                    for (int i = 0; i < AreaNPCs.Length; i++)
                    {
                        NPC.HitInfo hitinfo = new NPC.HitInfo();

                        hitinfo.Damage = (int)(Projectile.damage / 3f);
                        hitinfo.Knockback = 0f;
                        hitinfo.DamageType = DamageClass.Magic;

                        AreaNPCs[i].StrikeNPC(hitinfo);
                        NetMessage.SendStrikeNPC(AreaNPCs[i], hitinfo, 1);

                        for (int j = 0; j < Projectile.Center.Distance(AreaNPCs[i].Center) / 2; j++)
                        {
                            Dust newflame = Dust.NewDustDirect(
                                Projectile.Center + (Projectile.Center.DirectionTo(AreaNPCs[i].Center) * (j * 2f)) +
                                new Vector2(0, (float)Math.Sin(j / 4f) * 10f).RotatedBy(Projectile.Center.DirectionTo(AreaNPCs[i].Center).ToRotation()),
                                1,
                                1,
                                DustID.Torch,
                                0,
                                0,
                                0,
                                new Color(255, 0, 255, 0),
                                2f);
                            newflame.noGravity = true;
                        }
                    }
                }
            }

            Dust newddust = Dust.NewDustDirect(
                                OriginalPos + new Vector2(-16, 28) + new Vector2(0, (Projectile.Center.Y + 28 - OriginalPos.Y) % 32),
                                1,
                                1,
                                DustID.Torch,
                                Main.rand.NextFloat(-4f, 4f),
                                Main.rand.NextFloat(-1f, 1f),
                                0,
                                new Color(255, 0, 255, 0),
                                2.5f);
            newddust.noGravity = true;

            Projectile.ai[0]++;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D SkeletonHandBone = ModContent.Request<Texture2D>("Terrafirma/Reworks/VanillaMagic/Spells/Dungeon/SkeletonHandBone").Value;
            for (int i = 0; i < Math.Abs((Projectile.Center.Y - OriginalPos.Y) / 32); i++)
            {
                Main.EntitySpriteDraw(
                SkeletonHandBone,
                (Projectile.Center + new Vector2(-6, 28 + (i * 32))) - Main.screenPosition,
                SkeletonHandBone.Frame(),
                lightColor,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None
                );
            }

            base.PostDraw(lightColor);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust newddust = Dust.NewDustDirect(
                                Projectile.Center + new Vector2(18, 28),
                                1,
                                1,
                                DustID.Torch,
                                Main.rand.NextFloat(-3f, 3f),
                                Main.rand.NextFloat(-3f, 3f),
                                0,
                                new Color(255, 0, 255, 0),
                                2f);
                newddust.noGravity = true;
            }
        }

    }

}
