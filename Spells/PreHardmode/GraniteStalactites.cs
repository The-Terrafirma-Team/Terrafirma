using Microsoft.Xna.Framework;
using System;
using Terrafirma.Items.Weapons.Magic.PreHardmode;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Spells.PreHardmode
{
    internal class GraniteStalactites : Spell
    {
        public override int UseAnimation => 30;
        public override int UseTime => 30;
        public override int ManaCost => 8;
        public override int[] SpellItem => [ModContent.ItemType<GraniteStaff>()];
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 3; i++)
                Projectile.NewProjectile(source, Main.MouseWorld + Main.rand.NextVector2Circular(32,8), Main.rand.NextVector2Circular(0.3f,0.1f), ModContent.ProjectileType<Projectiles.Magic.PreHardmode.GraniteStalactites>(), damage, knockback, player.whoAmI, Main.rand.Next(3),Main.rand.Next(0,15), player.position.Y);
            return false;
        }
    }
}
