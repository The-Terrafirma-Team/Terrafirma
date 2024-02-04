using Terrafirma.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    internal class AmmoTypes : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            switch (entity.type)
            {
                case ItemID.PoisonedKnife:
                    entity.ammo = ItemID.ThrowingKnife;
                    entity.shoot = ProjectileID.PoisonedKnife;
                    break;
                case ItemID.ThrowingKnife:
                    entity.ammo = ItemID.ThrowingKnife;
                    entity.shoot = ProjectileID.ThrowingKnife;
                    break;
                case ItemID.IceBlock:
                    entity.ammo = ItemID.IceBlock;
                    entity.shoot = ModContent.ProjectileType<IcicleProjectile>();
                    break;
            }
        }
    }
}
