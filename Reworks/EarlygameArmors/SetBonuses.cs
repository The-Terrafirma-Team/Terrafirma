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
    public class SetBonuses : ModPlayer
    {
        public bool GoldArmor = false;
        public bool LeadArmor = false;
        public bool Ninja = false;
        int NinjaCooldown = 0;
        int dashDirection = 0;
        public override void ResetEffects()
        {
            GoldArmor = false;
            LeadArmor = false;
            Ninja = false;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (GoldArmor) target.AddBuff(BuffID.Midas,60 * 2);
            else if (LeadArmor && Main.rand.NextBool(6)) target.AddBuff(BuffID.Poisoned, 60 * 5);
        }
        public override void PostUpdateEquips()
        {
            if (Player.head == 48 && Player.body == 29 && Player.legs == 28) // Lead
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Lead");
                Player.PlayerStats().AmmoSaveChance += 0.33f;
                LeadArmor = true;
            }
            if (Player.head == 3 && Player.body == 3 && Player.legs == 3) // Silver
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Silver");
                Player.PlayerStats().DebuffTimeMultiplier -= 0.5f;
            }
            else if (Player.head == 49 && Player.body == 30 && Player.legs == 29) // Tungsten
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Tungsten");
                Player.GetKnockback(DamageClass.Generic) += 0.1f;
                Player.PlayerStats().MeleeWeaponScale += 0.2f;
            }
            else if ((Player.head is 4 or 73) && Player.body == 4 && Player.legs == 4) // Gold
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Gold");
                GoldArmor = true;
            }
            else if (Player.head == ArmorIDs.Head.NinjaHood && Player.body == ArmorIDs.Body.NinjaShirt && Player.legs == ArmorIDs.Legs.NinjaPants) // Gold
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Ninja");
                Ninja = true;
            }
        }
        public override void PreUpdateMovement()
        {
            if (!Player.CanUseDash() || !Ninja)
                return;

            float dashSpeed = 10;

            if (dashDirection == 0 || NinjaCooldown > -40)
                return;

            if(dashDirection == 1)
            {
                Player.velocity.X = Math.Max(Player.velocity.X, dashSpeed);
            }
            else
            {
                Player.velocity.X = Math.Min(Player.velocity.X, -dashSpeed);
            }

            for(int i = 0; i < 20; i++)
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
