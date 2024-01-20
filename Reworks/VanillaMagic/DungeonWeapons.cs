using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using TerrafirmaRedux.Projectiles.Magic;
using Terraria.DataStructures;
using Terraria.Audio;
using TerrafirmaRedux.Systems.MageClass;
using Microsoft.Xna.Framework.Graphics;

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class DungeonWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is >= 1444 and <= 1446 || entity.type == ItemID.WaterBolt || entity.type == ItemID.BookofSkulls || entity.type == ItemID.AquaScepter;
        }
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.InfernoFork)
                entity.UseSound = null;
        }
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: mult = 16 / 18f; break;
                case 2: mult = 1f + 6 / 18f; break;
                case 4: mult = 1.6f; break;
                case 5: mult = 1.2f; break;
                case 7: mult = 2 / 18f; break;
                case 9: mult = 12 / 7f; break;
                case 10: mult = 30 / 18f; break;
            }
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 10:
                    return player.ownedProjectileCounts[ModContent.ProjectileType<SkeletonHand>()] < 1? base.CanUseItem(item, player) : false;
            }
            return base.CanUseItem(item, player);
        }
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 2:
                    return 1.2f;
                case 4:
                    return 1.8f;
                case 7:
                    return 0.3f;
                case 9:
                    return 2f;
            }
            return base.UseAnimationMultiplier(item, player);
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 1:
                    return 0.14f;
                case 2:
                    return 0.3f;
                case 4:
                    return 1.8f;
                case 7:
                    return 0.3f;
                case 9:
                    return 5f;
            }

            return base.UseTimeMultiplier(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 0:
                    SoundEngine.PlaySound(SoundID.Item73, player.position);
                    break;
                case 1:
                    if (player.ItemAnimationJustStarted)
                        SoundEngine.PlaySound(SoundID.Item34, player.position);
                    break;
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 0:
                    type = ModContent.ProjectileType<InfernoFork>();
                    velocity *= 1.2f;
                    //velocity = new Vector2(6,-6);
                    break;
                case 1:
                    type = ModContent.ProjectileType<InfernoFlamethrower>();
                    damage = (int)(damage * 0.6f);
                    velocity *= 0.9f;
                    position += Vector2.Normalize(velocity) * 30;
                    break;
                case 2:
                    type = ModContent.ProjectileType<Firewall>();
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    damage = (int)(damage * 0.5f);
                    position = Main.MouseWorld;
                    break;
                case 4:
                    type = ModContent.ProjectileType<WaterGeyser>();
                    damage = (int)(damage * 0.5f);
                    position = Main.MouseWorld;
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    knockback *= 0.2f;
                    break;
                case 5:
                    type = ModContent.ProjectileType<AuraWave>();
                    position = Main.MouseWorld;
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    knockback *= 2f;
                    break;
                case 7:
                    type = ModContent.ProjectileType<BoneFragment>();
                    damage = (int)(damage * 0.5f);
                    velocity += velocity * 2f + new Vector2(0, Main.rand.NextFloat(-1f, 1f)).RotatedBy(velocity.ToRotation());
                    break;
                case 9:
                    type = ModContent.ProjectileType<HealingBubble>();
                    damage = (int)(damage * 0.1f);
                    break;
                case 10:
                    type = ModContent.ProjectileType<SkeletonHand>();
                    velocity = Vector2.Zero;
                    break;
            }
        }
    }

    #region Water Geyser
    public class WaterGeyser : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/WaterGeyser";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }

        Vector2 OriginalPos;
        bool findtile = false;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(28, 20);
            Projectile.timeLeft = 400;
            Projectile.Opacity = 0;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[2] % 2 == 0)
            {
                return base.CanHitNPC(target);
            }
            return false;

        }
        public override void AI()
        {

            Projectile.position.X += (float)Math.Sin(Projectile.ai[0] / 10) / 4;
            if (Projectile.timeLeft > 390)
            {
                Projectile.Opacity += 1 / 10f;
            }
            else if (Projectile.timeLeft < 60)
            {
                Projectile.Opacity -= 1 / 60f;
            }


            if (Projectile.ai[2] == 0 && !findtile)
            {
                float SetPos = 0;
                for (int i = 300; i > 0; i -= 4)
                {
                    if (Collision.SolidCollision(Projectile.Center + new Vector2(0, i * 2), Projectile.width, Projectile.height, true))
                    {
                        SetPos = new Vector2(0, Projectile.Bottom.Y + i * 2).ToTileCoordinates().Y * 16;
                    }
                }
                Projectile.position.Y = SetPos;
                findtile = true;
            }

            if (Projectile.ai[0] == 0)
            {
                OriginalPos = Projectile.Center;
            }

            if (Projectile.ai[0] % 3 == 0)
            {
                if (Projectile.frame == 3 + Projectile.ai[1] * 4)
                {
                    Projectile.frame = 0 + (int)(Projectile.ai[1] * 4);

                }
                else { Projectile.frame++; }
            }

            if (Projectile.ai[0] % 5 == 0)
            {

                Dust newdust = Dust.NewDustDirect(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.DungeonWater, 0, -2f, Projectile.alpha, Color.White, Projectile.Opacity);
                newdust.velocity.X *= 0.3f;
                newdust.noGravity = Main.rand.NextBool();
            }


            if (Projectile.ai[0] == 4 && Projectile.ai[2] <= 10)
            {
                if (Collision.SolidCollision(OriginalPos + new Vector2(0, -Projectile.height + 4), Projectile.width, Projectile.height))
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 6), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 11);
                    newproj.frame = Projectile.frame - 1;
                }
                else
                {
                    if (Projectile.ai[2] < 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 4), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                    else if (Projectile.ai[2] == 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 8), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                }



            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].Hitbox.Intersects(Projectile.Hitbox) && Main.player[i].velocity.Y > -45f)
                {
                    Main.player[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.player[i].velocity.X) > 2f)
                    {
                        Main.player[i].velocity.Y -= Math.Abs(Main.player[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.player[i].velocity.Y -= 0.2f;
                    }
                }
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].Hitbox.Intersects(Projectile.Hitbox) && Main.npc[i].velocity.Y > -45f && Main.npc[i].friendly)
                {
                    Main.npc[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.npc[i].velocity.X) > 2f)
                    {
                        Main.npc[i].velocity.Y -= Math.Abs(Main.npc[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.npc[i].velocity.Y -= 0.2f;
                    }
                }
            }

            Projectile.ai[0]++;

        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
    #endregion

    #region Aurawave
    public class AuraWave : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/AuraWave";

        Vector2 playerpos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(10, 10);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.ai[0]++;


            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter, new Vector2(3f, 0f).RotatedBy(Math.PI / 8f * i), Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
                    newproj.Opacity = 0.5f;
                    newproj.timeLeft = 60;
                }
                Projectile.Kill();

            }
            else
            {
                Projectile.Opacity = Projectile.timeLeft / 60f;
                Projectile.velocity = Projectile.velocity * 0.95f + ((Projectile.Center - playerpos) - (Projectile.Center - playerpos).RotatedBy(0.01f));
                Projectile.velocity *= 0.95f;
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

                //Projectile.velocity = Projectile.velocity.RotatedBy(0.08f);


                if (Projectile.ai[0] % 2 == 0)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(5, 0).RotatedBy(Projectile.velocity.ToRotation()), DustID.DungeonWater, Vector2.Zero, Projectile.alpha, Color.White, 1);
                    newdust.noGravity = !Main.rand.NextBool(8);
                }
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            playerpos = Main.player[Projectile.owner].MountedCenter;
        }
    }
    #endregion

    #region Bone Fragment
    public class BoneFragment : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/BoneFragments";

        Vector2 playerpos = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(14, 14);
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.3f;
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.TileCollision(Projectile.position, Projectile.velocity / 2, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnKill(int timeLeft)
        {
            SoundStyle bonesound = SoundID.NPCHit2;
            bonesound.Volume = 0.2f;
            bonesound.PitchRange = (-0.2f, 0.2f);
            SoundEngine.PlaySound(bonesound, Projectile.position);
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }
    }
    #endregion

    #region Healing Bubble
    public class HealingBubble : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/HealingBubble";
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(22, 22);
            Projectile.timeLeft = 200;
            Projectile.friendly = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity.Y *= 0.2f;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity.Y -= 0.05f;
            Projectile.velocity.X *= 0.98f;

            if (Projectile.ai[0] % 2 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.position + new Vector2(Main.rand.Next(23), Main.rand.Next(23)), DustID.DungeonWater, Vector2.Zero, 0, Color.White, 1f);
            }

        }
        public override bool CanHitPlayer(Player target)
        {
            if (target.team == Main.player[Projectile.owner].team)
            {
                return true;
            }
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (target.team == Main.player[Projectile.owner].team)
            {
                target.Heal(4);
                Projectile.Kill();
            }
            
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10f,10f), DustID.DungeonWater, Main.rand.NextVector2Circular(5f,5f), 0, Color.White, 1.25f);
            }
        }

    }
    #endregion

    #region SkeletonHand
    public class SkeletonHand : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/SkeletonHand";

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
                NPC[] AreaNPCs = new NPC[] {};
                if (Projectile.ai[0] % 40 == 0 && AreaNPCs != null)
                {
                    AreaNPCs = Utils.GetAllNPCsInArea(400f, Projectile.Center + new Vector2(18, 28));
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
                                new Color(255,0,255,0),
                                2f);
                            newflame.noGravity = true;
                        }
                    }
                }
            }

            Dust newddust = Dust.NewDustDirect(
                                OriginalPos + new Vector2(-16,28) + new Vector2(0, ( Projectile.Center.Y + 28 - OriginalPos.Y ) % 32),
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
            Texture2D SkeletonHandBone = ModContent.Request<Texture2D>("TerrafirmaRedux/Reworks/VanillaMagic/SkeletonHandBone").Value;
            for (int i = 0; i < Math.Abs((  Projectile.Center.Y - OriginalPos.Y  ) / 32); i++)
            {
                Main.EntitySpriteDraw(
                SkeletonHandBone,
                ( Projectile.Center + new Vector2(-6, 28 + (i * 32)) ) - Main.screenPosition,
                SkeletonHandBone.Frame(),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None
                );
            }
                
            base.PostDraw(lightColor);
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust newddust = Dust.NewDustDirect(
                                Projectile.Center + new Vector2(18, 28),
                                1,
                                1,
                                DustID.Torch,
                                Main.rand.NextFloat(-3f,3f),
                                Main.rand.NextFloat(-3f, 3f),
                                0,
                                new Color(255, 0, 255, 0),
                                2f);
                newddust.noGravity = true;
            }
        }

    } 
    #endregion
}
