using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.EarlygameArmors
{
    public class NinjaArmor : ModPlayer
    {
        public bool Active = false;
        int NinjaCooldown = 0;
        int dashDirection = 0;
        public override void ResetEffects()
        {
            Active = false;
            dashDirection = 0;
            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[2] < 15 && Player.doubleTapCardinalTimer[3] == 0)
            {
                dashDirection = 1;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[3] < 15 && Player.doubleTapCardinalTimer[2] == 0)
            {
                dashDirection = -1;
            }
            if (NinjaCooldown > -40)
            {
                NinjaCooldown--;
                Player.eocDash = NinjaCooldown;
                Player.armorEffectDrawShadowEOCShield = true;
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.head == ArmorIDs.Head.NinjaHood && Player.body == ArmorIDs.Body.NinjaShirt && Player.legs == ArmorIDs.Legs.NinjaPants)
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Ninja");
                Active = true;
            }
        }
        public override void PreUpdateMovement()
        {
            if (!Player.CanUseDash() || !Active)
                return;

            float dashSpeed = 10;

            if (dashDirection == 0 || NinjaCooldown > -40)
                return;

            if (dashDirection == 1)
            {
                Player.velocity.X = Math.Max(Player.velocity.X, dashSpeed);
            }
            else
            {
                Player.velocity.X = Math.Min(Player.velocity.X, -dashSpeed);
            }

            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.head, DustID.Smoke, Player.velocity.X * 0.3f, 0, 128);
                d.scale *= 1.3f;
            }

            NinjaCooldown = 20;
            Player.immune = true;
            Player.AddImmuneTime(ImmunityCooldownID.General, 15);
        }
    }
}
