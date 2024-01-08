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

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class DungeonWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is >= 1444 and <= 1446 || entity.type == ItemID.WaterBolt;
        }
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.InfernoFork)
                entity.UseSound = null;
        }
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            base.ModifyManaCost(item, player, ref reduce, ref mult);
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
            }
            return base.UseAnimationMultiplier(item, player);
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 1:
                    return 0.17f;
                case 2:
                    return 0.3f;
                case 4:
                    return 1.8f;
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
                    if(player.ItemAnimationJustStarted) 
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
                    type = ProjectileID.Flames;
                    damage = (int)(damage * 0.6f);
                    velocity *= 0.7f;
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
            }
        }
    }

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
            Projectile.Size = new Vector2(28,20);
            Projectile.timeLeft = 400;
            Projectile.Opacity = 0;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
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
            if ( Projectile.timeLeft > 390)
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
                    if (Collision.SolidCollision(Projectile.Center + new Vector2(0, i * 2), Projectile.width, Projectile.height))
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

                Dust newdust = Dust.NewDustDirect(Projectile.Center , Projectile.width / 2, Projectile.height / 2, DustID.DungeonWater, 0, -2f, Projectile.alpha, Color.White, Projectile.Opacity);
                newdust.velocity.X *= 0.3f;
                newdust.noGravity = Main.rand.NextBool();
            }


            if (Projectile.ai[0] == 4)
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

            for(int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].Hitbox.Intersects(Projectile.Hitbox) && Main.player[i].velocity.Y > -45f)
                {
                    if (Math.Abs(Main.player[i].velocity.X) > 0.2f )
                    {
                        Main.player[i].velocity.Y -= Math.Abs(Main.player[i].velocity.X) / 10;
                    }
                    else
                    {
                        Main.player[i].velocity.Y -= 0.2f;
                    }
                }
            }

            Projectile.ai[0]++;

        }
    }
}
