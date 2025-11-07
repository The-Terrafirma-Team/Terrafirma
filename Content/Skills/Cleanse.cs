using Microsoft.Xna.Framework;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.PlayerLayers;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Particles;
using Terrafirma.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Skills
{
    public class Cleanse : Skill
    {
        public override object[] TooltipFormatting()
        {
            return new object[] { 5 };
        }
        public override int ManaCost => 100;
        public override int CastTimeMax => 120;
        public override int CooldownMax => 60 * 30;
        public override Color RechargeFlashColor => Color.LimeGreen;
        public override void Casting(Player player)
        {
            player.runSlowdown += 0.5f;
            player.PlayerStats().AirResistenceMultiplier *= 2;

            if(Main.timeForVisualEffects % 20 == 0)
                ParticleSystem.NewParticle(new ScalingCircle(100, 20, 30, new Color(0.3f, 1f, Main.rand.NextFloat(0.5f), 0f), 0, player), player.Center);
        }
        public override void CastingEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            drawInfo.drawPlayer.bodyFrame.Y = 56 * 6;
            drawInfo.drawPlayer.GetModPlayer<PlayerMouthLayer>().MouthFrame = MouthFrame.Talking;
        }
        public override void Use(Player player)
        {
            int Radius = 400;
            foreach(Player p in Main.ActivePlayers)
            {
                if (p.Center.Distance(player.Center) < Radius)
                {
                    p.AddBuff(ModContent.BuffType<CleanseBuff>(), 60 * 5, true);
                    for (int i = 0; i < 10; i++)
                    {
                        ParticleSystem.NewParticle(new StarSparkle(Main.rand.Next(14, 42), Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(0.4f, 1f), new Color(0.3f, 1f, Main.rand.NextFloat(0.5f), 0f), Main.rand.NextVector2Circular(5, 5) + new Vector2(0, -3)), p.Bottom);
                    }
                }
            }
            ParticleSystem.NewParticle(new ScalingCircle(0,Radius,40, new Color(0.3f, 1f, Main.rand.NextFloat(0.5f), 0f)), player.Center);
            ParticleSystem.NewParticle(new ScalingCircle(0, Radius, 60, new Color(0.3f, 1f, Main.rand.NextFloat(0.5f), 0f) * 0.2f), player.Center);
        }
    }
    public class CleanseBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.timeForVisualEffects % 6 == 0)
            ParticleSystem.NewParticle(new StarSparkle(Main.rand.Next(14,24),Main.rand.NextFloat(-0.3f,0.3f),Main.rand.NextFloat(0.2f,0.5f),new Color(0.3f,1f,Main.rand.NextFloat(0.5f),0f) * 0.3f, Main.rand.NextVector2Circular(2,2) + new Vector2(0,-3)), Main.rand.NextVector2FromRectangle(player.Hitbox));

            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (Main.debuff[player.buffType[i]] && !BuffID.Sets.NurseCannotRemoveDebuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                    i--;
                }
                else if (player.buffType[i] == 0)
                    break;
            }
        }
    }
}
