using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Buffs.Debuffs;
using Terrafirma.Content.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Skills
{
    public class GroundSlam : Skill
    {
        public override SkillCategory Category => SkillCategory.Melee;
        public override int TensionCost => 20;
        public override int CooldownMax => 60 * 10;
        public override Color RechargeFlashColor => Color.LightCoral;
        public override void Use(Player player)
        {
            player.RemoveAllGrapplingHooks();
            player.GetModPlayer<GroundSlamPlayer>().GroundSlamming = true;

            for (int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustPerfect(player.Bottom, DustID.Cloud);
                d.noGravity = true;
                d.velocity = Main.rand.NextVector2CircularEdge(6, 3);
                d.velocity *= Main.rand.NextFloat(0.8f, 1f);
                d.scale *= 2;
                SoundEngine.PlaySound(SoundID.DoubleJump, player.position);
            }
        }
        public override object[] TooltipFormatting()
        {
            return new object[] 
            {
                (int)Main.LocalPlayer.GetTotalDamage(DamageClass.Melee).ApplyTo(1.5f) * 2,
                (int)Main.LocalPlayer.GetTotalDamage(DamageClass.Melee).ApplyTo(15) * 2
            };
        }
    }
    public class GroundSlamPlayer : ModPlayer
    {
        public bool GroundSlamming = false;
        public float Power = 0.5f;

        public override void PreUpdateBuffs()
        {
            if (GroundSlamming)
                Player.PlayerStats().ImmuneToContactDamage = true;

            bool flipped = Player.gravDir == -1;
            Vector2 bottom = flipped ? Player.Top : Player.Bottom;
            if (GroundSlamming && Player.velocity.Y == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14, Player.position);
                for (int i = 0; i < (5 * Power); i++)
                {
                    ParticleSystem.NewParticle(new Smoke(Main.rand.NextVector2Circular(2, 1f) - Vector2.UnitY * Player.gravDir, Color.Beige * 0.5f, Color.DarkGoldenrod * 0.3f, Main.rand.NextFloat(0.8f, 1.2f)), bottom);
                }
                DecalsSystem.NewDecal(new CrackDecal(0.5f + (Power / 3f)), bottom + Player.velocity);
                GroundSlamming = false;
                Player.fallStart = (int)Player.position.X / 16;
                Player.velocity.Y = -4f - Power * 0.5f * Player.gravDir;
                foreach (NPC n in Main.ActiveNPCs)
                {
                    if (!n.friendly && n.Center.Distance(Player.Center) < (16 * (4f + Power * 0.5f)))
                    {
                        Player.StrikeNPCDirect(n, n.CalculateHitInfo((int)(Power * 3), n.Center.X < Player.Center.X ? -1 : 1, true, MathHelper.Max(1f, Power), DamageClass.Melee, true));
                        n.AddBuff(ModContent.BuffType<Stunned>(), 60 * 3);
                        if (n.knockBackResist > 0)
                            n.velocity.Y = Player.velocity.Y;
                    }
                }
                Power = 0.5f;

                Player.AddImmuneTime(ImmunityCooldownID.General, 60);
                Player.immune = true;
            }
            if (GroundSlamming)
            {
                Player.wingTime = 0f;
                foreach (NPC n in Main.ActiveNPCs)
                {
                    if (!n.friendly && n.Hitbox.Intersects(Player.Hitbox))
                    {
                        n.AddBuff(ModContent.BuffType<Stunned>(), 60 * 3);
                        if (n.knockBackResist > 0)
                            n.velocity = Player.velocity;
                    }
                }
                Power *= 1.1f;
                if (Power > 5f)
                    Power = 5f;

                Player.PlayerStats().TurnOffDownwardsMovementRestrictions = true;
                Player.velocity.X *= 0.9f;
                Player.velocity.Y = flipped ? Math.Min(Player.velocity.Y - 0.3f, -2f) : Math.Max(Player.velocity.Y + 0.3f, 2f);

                Dust d = Dust.NewDustDirect((flipped ? Player.position : Player.BottomLeft) + Player.velocity, Player.width, 2, DustID.Cloud);
                d.noGravity = true;
                d.velocity = Main.rand.NextVector2Circular(2, 2);
                d.velocity.Y -= 2f * Player.gravDir;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Player.grappling[0] > -1 || (Player.controlJump && (Player.AnyExtraJumpUsable() || Player.wingTime > 0f)))
                GroundSlamming = false;
        }
    }
}
