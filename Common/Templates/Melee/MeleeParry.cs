using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class MeleeParry : ModProjectile
    {
        public Player player { get => Main.player[Projectile.owner]; }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(player.width * 0.8f, player.height + 8);
            Projectile.hide = true;
            Projectile.Opacity = 0;
            Projectile.timeLeft = 15;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            player.SetDummyItemTime(2);
            player.PlayerStats().ParryProjectile = Projectile.whoAmI;
            Projectile.Center = player.Center + new Vector2(player.direction * Projectile.width / 2, 2);
        }
        public virtual void OnParryNPC(NPC n, Player player)
        {
            Projectile.Kill();
            player.immune = true;
            player.AddImmuneTime(ImmunityCooldownID.General, (int)(60 * player.PlayerStats().ParryImmunityDurationMultiplier));
            SoundEngine.PlaySound(SoundID.Item37,player.position);
            for(int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(n.Hitbox.ClosestPointInRect(player.Center),DustID.Smoke);
            }
            player.GiveTension(20);
        }
        public virtual void OnParryProjectile(Projectile p, Player player)
        {
            Projectile.Kill();
            player.immune = true;
            player.AddImmuneTime(ImmunityCooldownID.General, (int)(60 * player.PlayerStats().ParryImmunityDurationMultiplier));
            SoundEngine.PlaySound(SoundID.Item37, player.position);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(p.Hitbox.ClosestPointInRect(player.Center), DustID.Smoke);
            }
            player.GiveTension(20);
        }
    }
}
