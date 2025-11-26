using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Utilities
{
    public static class PlayerUtils
    {
        public static PlayerStats PlayerStats(this Player player)
        {
            return player.GetModPlayer<PlayerStats>();
        }
        public static void ParryStrike(this Player p, NPC n)
        {
            float power = p.PlayerStats().ParryPower;
            p.StrikeNPCDirect(n, n.CalculateHitInfo(p.PlayerStats().ParryDamage, n.Center.X < p.Center.X ? -1 : 1, false, 4, DamageClass.Melee));
            if (n.ModNPC is ICustomBlockBehavior behavior)
                behavior.OnBlocked(p, power);
            else
            {
                for (int i = 0; i < n.EntityGlobals.Length; i++)
                {
                    if (n.EntityGlobals[i] is ICustomBlockBehavior behaviorGlobal)
                    {
                        behaviorGlobal.OnBlocked(p, power, n);
                    }
                }
            }
        }
        public static bool CheckTension(this Player player, float Tension, bool Consume = true)
        {
            PlayerStats pStats = player.PlayerStats();
            if (pStats.Tension >= Tension)
            {
                if (Consume)
                {
                    pStats.Tension -= player.ApplyTensionBonusScaling(Tension, true);
                }
                return true;
            }
            return false;
        }
        public static int GiveTension(this Player player, int Tension, bool Numbers = true, bool smallNumbers = false)
        {
            PlayerStats pStats = player.PlayerStats();
            int gain = player.ApplyTensionBonusScaling(Tension);
            pStats.Tension += gain;
            if (pStats.Tension > pStats.TensionMax2)
            {
                Numbers = false;
                pStats.Tension = pStats.TensionMax2;
            }
            if (Numbers)
                CombatText.NewText(player.Hitbox, Terrafirma.TensionGainColor, gain, dot: smallNumbers);
            return gain;
        }
        public static int ApplyTensionBonusScaling(this Player player, float number, bool drain = false)
        {
            PlayerStats stats = player.PlayerStats();
            if (!drain)
            {
                return (int)(number * stats.TensionGainMultiplier) + stats.FlatTensionGain;
            }
            else
            {
                return (int)(number * stats.TensionCostMultiplier) + stats.FlatTensionCost;
            }
        }
    }
}
