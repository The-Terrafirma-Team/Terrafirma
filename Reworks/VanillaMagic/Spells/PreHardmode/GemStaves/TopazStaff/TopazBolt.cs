﻿using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.TopazStaff
{
    internal class TopazBolt : Spell
    {
        public override int UseAnimation => 36;
        public override int UseTime => 36;
        public override int ManaCost => 5;
        public override int[] SpellItem => new int[] { ItemID.TopazStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.TopazBolt;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
