using Microsoft.Xna.Framework;
using Terrafirma.Items.Equipment;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Buffs
{
    public class Confidence : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(player.BottomLeft - new Vector2(10,0), player.width + 20, 0, DustID.GemTopaz, 0, -5);
                d.noGravity = true;
                d.velocity.X = 0;
            }
            player.endurance += 0.2f;
            player.lifeRegen += 40;
            player.GetAttackSpeed(DamageClass.Generic) += 0.5f;
        }
    }
}
