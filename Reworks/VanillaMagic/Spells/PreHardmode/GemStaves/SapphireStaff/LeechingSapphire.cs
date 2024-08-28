using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.SapphireStaff
{
    internal class LeechingSapphire : Spell
    {
        public override int UseAnimation => 34;
        public override int UseTime => 34;
        public override int ManaCost => 3;
        public override int[] SpellItem => [ItemID.SapphireStaff];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 0.35f, ModContent.ProjectileType<LeechingSapphireBolt>(), (int)(damage * 0.4f), knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class LeechingSapphireBolt : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false,16);
            Projectile.hide = true;
            Projectile.timeLeft = 100;
            Projectile.extraUpdates = 10;
        }
        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center,DustID.GemSapphire,Projectile.velocity * Projectile.timeLeft / 80f);
            d.noGravity = true;
            d.scale = Projectile.timeLeft / 50f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            int manaRegen = 7;
            CombatText.NewText(player.Hitbox, CombatText.HealMana, manaRegen);
            player.statMana += manaRegen;
        }
    }
}
