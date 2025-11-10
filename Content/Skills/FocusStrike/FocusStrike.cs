using Microsoft.Xna.Framework;
using Terrafirma.Common.Mechanics;
using Terrafirma.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Skills.FocusStrike
{
    public class FocusStrike : Skill
    {
        public override int TensionCost => 20;
        public override int CastTimeMax => 120;
        public override int CooldownMax => 60 * 30;
        public override Color RechargeFlashColor => Color.Goldenrod;
        public override void Casting(Player player)
        {
            player.runSlowdown += 0.5f;
            player.PlayerStats().AirResistenceMultiplier *= 2;
            Vector2 vect = Main.rand.NextVector2CircularEdge(1, 1);
            Dust d = Dust.NewDustPerfect(player.Center + vect * 40, DustID.HallowedWeapons, -vect * 2);
            d.noGravity = true;
            d.velocity += player.velocity;
        }
        public override void CastingEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            drawInfo.drawPlayer.bodyFrame.Y = 56 * 6;
        }
        public override void Use(Player player)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.Excalibur, new ParticleOrchestraSettings() with { PositionInWorld = player.Center + new Vector2(0,player.gravDir * -8)});
            player.AddBuff(ModContent.BuffType<FocusStrikeBuff>(), 60 * 5);
        }
    }
    public class FocusStrikeBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FocusStrikePlayer>().Active = true;
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.HallowedWeapons);
                d.velocity += player.velocity;
                d.velocity *= 0.1f;
                d.noGravity = true;
            }
        }
    }
    internal class FocusStrikePlayer : ModPlayer
    {
        public bool Active = false;
        public override void ResetEffects()
        {
            Active = false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Active)
            {
                modifiers.SetCrit();
                modifiers.ScalingBonusDamage += 2f;
                modifiers.ScalingArmorPenetration += 1f;
                Player.ClearBuff(ModContent.BuffType<FocusStrikeBuff>());
                Active = false;
            }
        }
    }
}
