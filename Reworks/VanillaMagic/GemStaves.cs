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

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class GemStaves : GlobalItemInstanced
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is >= 739 and <= 744 || entity.type == ItemID.AmberStaff;
        }
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            if (item.type == ItemID.AmethystStaff && item.GetGlobalItem<GlobalItemInstanced>().Spell == 0)
                return 1.2f;
            return base.UseSpeedMultiplier(item, player);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell == 1)
            {
                switch (item.type)
                {
                    case ItemID.AmethystStaff:
                        type = ModContent.ProjectileType<HomingAmethyst>();
                        damage = (int)(damage * 0.8f);
                        break;
                }
            }
        }
    }

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
            NPC target = Utils.FindClosestNPC(200,Projectile.Center);
            if(target != null && target.active)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.1f, Projectile.Center.DirectionTo(target.Center) * Projectile.velocity.Length(),0.1f);
            }
            Projectile.velocity = Projectile.velocity.LengthClamp(5);
        }
    }
}
