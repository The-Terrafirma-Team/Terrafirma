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
    internal class GranitePlatform : Spell
    {
        public override int UseAnimation => 30;
        public override int UseTime => 30;
        public override int ManaCost => 8;
        public override int[] SpellItem => [ModContent.ItemType<GraniteStaff>()];
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            foreach(Projectile p in Main.ActiveProjectiles)
            {
                if(p.owner == player.whoAmI && p.type == ModContent.ProjectileType<Projectiles.Magic.PreHardmode.GranitePlatform>())
                {
                    p.Kill();
                }
            }
            Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Projectiles.Magic.PreHardmode.GranitePlatform>(), 0, knockback,player.whoAmI);
            return false;
        }
    }
}
