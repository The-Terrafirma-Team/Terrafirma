using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class CommanderSummonTemplate : ModProjectile
    {
        public byte mode = 0;
        public virtual int AssociatedBuff => -1;
        public virtual int AssociatedWeapon => -1;
        public Player owner { get => Main.player[Projectile.owner]; }

        public override void AI()
        {
            if (!owner.HasBuff(AssociatedBuff))
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 2;
                switch (mode)
                {
                    case 0:
                        PrimaryAI(owner.HeldItem.type == AssociatedWeapon);
                        break;
                    case 1:
                        SecondaryAI(owner.HeldItem.type == AssociatedWeapon);
                        break;
                    case 2:
                        TertriaryAI(owner.HeldItem.type == AssociatedWeapon);
                        break;
                }
            }
        }
        public virtual void SwitchMode(byte oldMode)
        {

        }
        public virtual void PrimaryAI(bool Focused)
        {
        }
        public virtual void SecondaryAI(bool Focused)
        {
        }
        public virtual void TertriaryAI(bool Focused)
        {
        }
    }
}
