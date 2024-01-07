using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
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
            }
            return base.UseSpeedMultiplier(item, player);
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: mult = 1.2f; break;
                case 3: mult = 1.2f; break;
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
            }

        }

        //Spell Projectiles
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
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy( 20 * (Math.PI/180) * i), Projectile.type, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, 0, 2);
                    
                    }
                    Projectile.Kill();
                }
            }
        }
    }
}
