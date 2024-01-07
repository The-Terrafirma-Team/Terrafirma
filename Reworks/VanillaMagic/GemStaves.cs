using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class GemStaves : GlobalItemInstanced
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is >= 739 and <= 744 || entity.type == ItemID.AmberStaff;
        }

        //Use Speed Multiplier
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 0: return 1.2f;
                case 2: return 1.2f;
                case 3: return 1.1f;
                case 5: return 0.85f;
                case 7: return 0.6f;
                case 9: return 0.2f;
                case 11: return 0.75f;
            }
            return base.UseSpeedMultiplier(item, player);
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: mult = 1.2f; break;
                case 3: mult = 1.2f; break;
                case 5: mult = 1f + 2/6f; break;
                case 7: mult = 1f + 2/7f; break;
                case 9: mult = 2f + 4/9f; break;
                case 11: mult = 1f + 1/6f; break;
                case 12: mult = 2f; break;
            }
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }

        //Modify Shoot Stats
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1:
                    type = ModContent.ProjectileType<HomingAmethyst>();
                    damage = (int)(damage * 0.8f);
                    break;
                case 3:
                    type = ModContent.ProjectileType<SplittingTopaz>();
                    break;
                case 5:
                    type = ModContent.ProjectileType<PiercingEmerald>();
                    velocity *= 0.9f;
                    break;
                case 7:
                    type = ModContent.ProjectileType<ExplodingRuby>();
                    velocity *= 0.7f;
                    damage = (int)(damage * 1.2f);
                    break;
                case 9:
                    type = ModContent.ProjectileType<DiamondTurret>();
                    break;
                case 11:
                    velocity *= 2f;
                    damage = (int)(damage * 1.2f);
                    break;
                case 13:
                    type = ModContent.ProjectileType<AmberWallCrystal>();
                    break;

            }

        }

        //Shoot
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 9: return player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurret>()] < 1;
                case 13: if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberWall>()] < 4 && player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallCrystal>()] < 1) { return true; } return false; 

            }
            return base.Shoot(item,player,source,position,velocity,type,damage,knockback);
        }

        //Can Use Item
        public override bool CanUseItem(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 9: return player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurret>()] < 1;
                case 13: if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberWall>()] < 4 && player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallCrystal>()] < 1) { return true; } return false;
            }
            return base.CanUseItem(item, player);
        }

        //Spell Projectiles
        #region Homing Amethyst
        public class HomingAmethyst : ModProjectile
        {
            public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.AmethystBolt);
                AIType = ProjectileID.AmethystBolt;
                Projectile.Size = new Vector2(16);
            }
            public override void AI()
            {
                NPC target = Utils.FindClosestNPC(200, Projectile.Center);
                if (target != null && target.active)
                {
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.1f, Projectile.Center.DirectionTo(target.Center) * Projectile.velocity.Length(), 0.1f);
                }
                Projectile.velocity = Projectile.velocity.LengthClamp(5);
            }
        }
        #endregion

        #region Splitting Topaz
        public class SplittingTopaz : ModProjectile
        {
            public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.TopazBolt);
                AIType = ProjectileID.TopazBolt;
                Projectile.ai[2] = 0;
                Projectile.Size = new Vector2(16);
            }

            public override void AI()
            {
                Projectile.ai[0]++;

                if (Projectile.ai[0] > 20 && Projectile.ai[2] == 0)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(20 * (Math.PI / 180) * i), Projectile.type, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, 0, 2);

                    }
                    Projectile.Kill();
                }
            }
        }
        #endregion

        #region Piercing Emerald
        public class PiercingEmerald : ModProjectile
        {
            public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.EmeraldBolt}";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
                AIType = ProjectileID.EmeraldBolt;
                Projectile.penetrate = 6;
                Projectile.tileCollide = false;
                Projectile.Size = new Vector2(16);
            }
        }
        #endregion

        #region Exploding Ruby
        public class ExplodingRuby : ModProjectile
        {
            public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RubyBolt}";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.RubyBolt);
                AIType = ProjectileID.RubyBolt;
                Projectile.penetrate = 1;
                Projectile.Size = new Vector2(16);
            }

            public override void AI()
            {
                Projectile.velocity *= 0.985f;

                if (Projectile.velocity.Length() < 1)
                {
                    Projectile.Kill();
                }

            }

            public override void Kill(int timeLeft)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.position, DustID.GemRuby, new Vector2(Main.rand.NextFloat(-5.8f, 5.8f), Main.rand.NextFloat(-5.8f, 5.8f)), 0, Color.White, Main.rand.NextFloat(1.7f, 2f));
                    newdust.noGravity = true;
                }
                Projectile.Explode(100);
            }
        }
        #endregion

        #region Diamond Turret
        public class DiamondTurret : ModProjectile
        {
            public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/DiamondTurret";
            public override void SetDefaults()
            {
                Projectile.penetrate = -1;
                Projectile.timeLeft = 600;
                Projectile.Size = new Vector2(16);
            }

            public override bool? CanHitNPC(NPC target)
            {
                return false;
            }

            public override void AI()
            {
                Projectile.ai[0]++;
                Projectile.velocity *= 0.98f;
                if (Projectile.ai[0] % 90 == 0 && Utils.FindClosestNPC(600f, Projectile.position) != null)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(Utils.FindClosestNPC(600f, Projectile.position).Center) * 10f, ProjectileID.DiamondBolt, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
                }
            }

            public override void OnKill(int timeLeft)
            {
                SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                for (int i = 0; i < 15; i++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                    newdust.noGravity = true;
                }
            }
        }
        #endregion

        #region Amber Wall Crystal
        public class AmberWallCrystal : ModProjectile
        {
            public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/AmberCrystal";
            public override void SetDefaults()
            {
                Projectile.tileCollide = false;
                Projectile.Size = new Vector2(16);
                Projectile.timeLeft = 90;
                Projectile.penetrate = 1;
                Projectile.friendly = true;
            }

            public override void AI()
            {
                Projectile.ai[0]++;
                Projectile.velocity *= 0.98f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                if (Projectile.ai[0]  % 2 == 0)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, default, Main.rand.NextFloat(0.9f, 1.4f));
                    newdust.noGravity = true;
                }
                

            }

            public override void OnKill(int timeLeft)
            {
                Vector2 length = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
                for (int i = -3; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter - length.RotatedBy( 0.05f * i), Vector2.Zero, ModContent.ProjectileType<AmberWall>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
                }
                SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            }
        }
        #endregion

        #region Amber Wall
        public class AmberWall : ModProjectile
        {
            float randfall = 0f;
            public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/AmberWall";
            public override void SetDefaults()
            {
                Projectile.penetrate = 12;
                Projectile.tileCollide = false;
                DrawOffsetX = -5;
                DrawOriginOffsetY = -5;
                Projectile.Size = new Vector2(16);
                Projectile.timeLeft = 400;
                Projectile.friendly = true;

                Projectile.usesLocalNPCImmunity = true;
                Projectile.localNPCHitCooldown = 40;
            }

            public override void OnSpawn(IEntitySource source)
            {
                randfall = Main.rand.NextFloat(0.05f, 0.15f);
                for (int j = 0; j < 5; j++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                    newdust.noGravity = true;
                }

                Projectile.scale = Main.rand.NextFloat(0.7f, 1.3f);
            }

            public override void AI()
            {
                if (Projectile.timeLeft < 30)
                {
                    Projectile.velocity.Y += randfall;
                }
            }

            public override void OnKill(int timeLeft)
            {
                SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                for (int j = 0; j < 5; j++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                    newdust.noGravity = true;
                }
            }
        } 
        #endregion
    }
}
