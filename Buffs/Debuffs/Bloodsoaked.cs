using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    public class Bloodsoaked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.aggro += 2500;
            player.PlayerStats().MaxEnemySpawnMultiplier += 3f;
            player.PlayerStats().EnemySpawnRateMultiplier -= 0.99f;
            player.GetModPlayer<BloodsoakedPlayer>().Active = true;
        }
    }
    internal class BloodsoakedPlayer : ModPlayer
    {
        public bool Active;
        public override void ResetEffects()
        {
            Active = false;
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Active)
            {
                g *= 0.1f;
                b *= 0.1f;
                if (drawInfo.shadow == 0 && Main.rand.NextBool(2))
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Blood, 0, 0);
                    d.velocity *= 0.2f;
                    d.velocity += Player.velocity;
                    d.alpha = Main.rand.Next(255);
                }
            }
        }
    }
}
