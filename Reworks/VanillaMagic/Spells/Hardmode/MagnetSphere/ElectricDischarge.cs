using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Data;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.MagnetSphere
{
    internal class ElectricDischarge : Spell
    {
        public override int UseAnimation => 21;
        public override int UseTime => 7;
        public override int ReuseDelay => 10;
        public override int ManaCost => 8;
        public override int[] SpellItem => new int[] { ItemID.MagnetSphere };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.4) * 10, ModContent.ProjectileType<ElectricDischargeBomb>(), (int)(damage * 0.6), knockback,player.whoAmI);
            return false;
        }
    }
    public class ElectricDischargeBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.alpha = 255;
            Projectile.QuickDefaults(false, 16);
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.96f;
            Projectile.rotation += Projectile.velocity.X * 0.03f;
            Projectile.frameCounter++;
            if(Projectile.frameCounter > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 6)
                    Projectile.frame = 0;
            }
            Projectile.alpha = (int)(Projectile.alpha * 0.9f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * tex.Height() / 7, tex.Width(), tex.Width()),new Color(1f,1f,1f,0f) * Projectile.Opacity, Projectile.rotation,new Vector2(tex.Width() / 2),Projectile.scale,SpriteEffects.None);

            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition - Projectile.velocity, new Rectangle(0, Projectile.frame * tex.Height() / 7, tex.Width(), tex.Width()), new Color(1f, 1f, 1f, 0f) * 0.5f * Projectile.Opacity, Projectile.rotation, new Vector2(tex.Width() / 2), Projectile.scale * 1.1f, SpriteEffects.None);

            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition - Projectile.velocity * 2, new Rectangle(0, Projectile.frame * tex.Height() / 7, tex.Width(), tex.Width()), new Color(1f, 1f, 1f, 0f) * 0.25f * Projectile.Opacity, Projectile.rotation, new Vector2(tex.Width() / 2), Projectile.scale * 1.2f, SpriteEffects.None);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundStyle style = SoundID.NPCHit53;
            style.MaxInstances = 10;
            SoundEngine.PlaySound(style, Projectile.Center);
            Projectile.Explode(128);

            for(int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, Main.rand.NextVector2CircularEdge(7,5) * Main.rand.NextFloat(0.7f,1.1f));
                d.noGravity = !Main.rand.NextBool(4);
            }
        }
    }
}
