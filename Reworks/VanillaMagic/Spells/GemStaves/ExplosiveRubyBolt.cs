using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class ExplosiveRubyBolt : Spell
    {
        public override int UseAnimation => 60;
        public override int UseTime => 60;
        public override int ManaCost => 9;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/RubyExplosiveShot";
        public override int[] SpellItem => new int[] { ItemID.RubyStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity *= 0.7f;
            damage = (int)(damage * 1.2f);
            type = ModContent.ProjectileType<ExplodingRuby>();


            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class ExplodingRuby : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RubyBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RubyBolt);
            AIType = ProjectileID.RubyBolt;
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.velocity *= 0.985f;

            if (Projectile.velocity.Length() < 1)
            {
                Projectile.Kill();
            }

        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.position, DustID.GemRuby, new Vector2(Main.rand.NextFloat(-5.8f, 5.8f), Main.rand.NextFloat(-5.8f, 5.8f)), 0, Color.White, Main.rand.NextFloat(1.7f, 2f));
                newdust.noGravity = true;
            }
            Projectile.Explode(100);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}
