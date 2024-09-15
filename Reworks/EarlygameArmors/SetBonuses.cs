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
        public override void ResetEffects()
        {
            GoldArmor = false;
            LeadArmor = false;
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
        }
    }
}
