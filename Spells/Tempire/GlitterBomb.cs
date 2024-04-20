using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Magic;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Spells.Tempire
{
    internal class GlitterBomb : Spell
    {
        public override int UseAnimation => 80;
        public override int UseTime => 80;
        public override int ManaCost => 20;
        public override int[] SpellItem => new int[] { ModContent.ItemType<Majesty>() };


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<GlitterBombProj>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
