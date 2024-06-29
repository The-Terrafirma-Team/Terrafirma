using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Audio;
using Terraria.Map;
using Terrafirma.Systems.Primitives;

namespace Terrafirma.NPCs.Boss.Ninja
{
    public partial class Ninja : ModNPC
    {
        private enum NinjaPhase
        {
            none = 0,
            idle = 1,
            chase = 2,
            fastchase = 3,
            jump = 4,
            swingsword = 5,
            throwshuriken = 6,
            slashdash = 7,
        }

        private NinjaPhase state = NinjaPhase.none;
        private Player target { get => Main.player[NPC.target]; }
        private int targetDirection { get => (int)((target.Center.X - NPC.Center.X) / Math.Abs(target.Center.X - NPC.Center.X)); }

        private int animframe = 0;

        public float frontarmrot = 0f;
        public int frontarmdirection = 1;
        public Vector2 frontarmoffset = Vector2.Zero;
        public Vector2 frontarmholdposition { get => GetArmHoldPosition(); }
        public float backarmrot = 0f;
        public int backarmdirection = 1;
        public Vector2 backarmoffset = Vector2.Zero;

        Projectile katanaprojectile = null;

        private float velocitytimer = 0f;
        private int airtime = 0;

        private Trail slashtrail;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 20;
            NPCID.Sets.TrailingMode[NPC.type] = 3;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }
        public override bool? CanFallThroughPlatforms()
        {
            return target.Bottom.Y > NPC.Bottom.Y + 48;
        }
        public override void SetDefaults()
        {
            NinjaPhase state = NinjaPhase.none;
            NPC.lifeMax = 2000;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit48;
            NPC.DeathSound = SoundID.NPCDeath50;
            NPC.boss = true;
            NPC.npcSlots = ContentSamples.NpcsByNetId[NPCID.EyeofCthulhu].npcSlots;
            NPC.noTileCollide = false;
            NPC.width = 24;
            NPC.height = 24;
            NPC.noGravity = true;
            NPC.knockBackResist = 0;
            NPC.damage = 15;

            Music = MusicID.Boss1;
            slashtrail = new Trail(NPC.oldPos, TrailWidth.SpikeWidth, 30);
        }

        public override void FindFrame(int frameHeight)
        {
            if (state == NinjaPhase.jump)
            {
                if (NPC.velocity.Y == 0f) animframe = 5;

                frontarmdirection = -1;

                frontarmoffset = Vector2.Zero;
                backarmoffset = Vector2.Zero;

                frontarmrot = MathHelper.Lerp(frontarmrot, -Math.Clamp(airtime / 4f, 0f, 2.8f) * NPC.spriteDirection, 0.6f);
                backarmrot = MathHelper.Lerp(backarmrot, Math.Clamp(airtime / 4f, 0f, 2.8f) * NPC.spriteDirection, 0.6f);
            }
            else if (state == NinjaPhase.fastchase)
            {
                frontarmdirection = 1;
                backarmdirection = 1;

                frontarmoffset = Vector2.Zero;
                backarmoffset = new Vector2(NPC.spriteDirection == 1 ? -4 : 0, 0);

                //Walk
                NPC.frameCounter += Math.Abs(NPC.velocity.X);
                if (NPC.frameCounter > 2)
                {
                    animframe = animframe > 18 ? 6 : animframe += 1;
                    NPC.frameCounter = 0f;
                }
                if (Math.Abs(NPC.velocity.X) < 0.4f) animframe = 0;
                //Arm swing
                frontarmrot = MathHelper.Lerp(frontarmrot, -1f * NPC.spriteDirection, 0.2f);
                backarmrot = MathHelper.Lerp(frontarmrot, -1f * NPC.spriteDirection, 0.2f);

            }
            else if (state == NinjaPhase.chase || state == NinjaPhase.none || state == NinjaPhase.idle)
            {
                frontarmdirection = 1;
                backarmdirection = 1;

                frontarmoffset = Vector2.Zero;
                backarmoffset = new Vector2(NPC.spriteDirection == 1? -4 : 0, 0);

                //Walk
                NPC.frameCounter += Math.Abs(NPC.velocity.X);
                if (NPC.frameCounter > 2)
                {
                    animframe = animframe > 18 ? 6 : animframe += 1;
                    NPC.frameCounter = 0f;
                }
                if (Math.Abs(NPC.velocity.X) < 0.4f) animframe = 0;
                //Arm swing
                if (Math.Abs(NPC.velocity.X) > 0.4f)
                {
                    velocitytimer += Math.Abs(NPC.velocity.X);
                    frontarmrot = MathHelper.Lerp(frontarmrot, (float)Math.Sin((velocitytimer) / 20f), 0.2f);
                    backarmrot = MathHelper.Lerp(backarmrot, (float)Math.Cos((velocitytimer) / 20f), 0.2f);
                }
                else
                {
                    frontarmrot = MathHelper.Lerp(frontarmrot, 0f, 0.2f);
                    backarmrot = MathHelper.Lerp(backarmrot, 0f, 0.2f);
                }
            }
            else if (state == NinjaPhase.swingsword)
            {
                frontarmdirection = 1;
                backarmdirection = 1;

                frontarmrot = (NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2) - ((NPC.ai[0] - 20f) / 20f * 4f) * NPC.spriteDirection;
                backarmrot = MathHelper.Lerp(backarmrot, 0f, 0.2f);

                frontarmoffset = Vector2.Zero;
                backarmoffset = new Vector2(NPC.spriteDirection == 1 ? -4 : 0, 0);
            }
            else if (state == NinjaPhase.throwshuriken)
            {
                frontarmdirection = 1;
                backarmdirection = 1;

                int direcctionswitch = NPC.ai[1] % 2 == 0? 1 : -1;
                float armshurikenrot = (-MathHelper.PiOver2 + 1f * direcctionswitch) * targetDirection;
                frontarmrot = MathHelper.Lerp(frontarmrot, armshurikenrot, 0.2f);
                backarmrot = MathHelper.Lerp(backarmrot, 0f, 0.2f);

                //Walk
                NPC.frameCounter += Math.Abs(NPC.velocity.X);
                if (NPC.frameCounter > 2)
                {
                    animframe = animframe > 18 ? 6 : animframe += 1;
                    NPC.frameCounter = 0f;
                }
            }
            else if (state == NinjaPhase.slashdash)
            {
                if(NPC.ai[0] < 60) frontarmrot = Utils.AngleLerp(frontarmrot, NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2, 0.2f);
                backarmrot = MathHelper.Lerp(backarmrot, 0f, 0.2f);
                animframe = 5;
            }

        }


        public override void AI()
        {

            switch (state)
            {
                case NinjaPhase.none: ChoosePhase(); break;
                case NinjaPhase.idle: Idle(); break;
                case NinjaPhase.chase: Follow(); break;
                case NinjaPhase.fastchase: FastFollow(); break;
                case NinjaPhase.jump: Jump(); break;
                case NinjaPhase.swingsword: SwingSword(); break;
                case NinjaPhase.throwshuriken: ThrowShuriken(); break;
                case NinjaPhase.slashdash: SlashDash(); break;
            }

            afterimagefloat *= 0.9f;
            dashtrailfloat *= 0.9f;

            if (state != NinjaPhase.slashdash) NPC.velocity.Y = Math.Clamp(NPC.velocity.Y, -999f, 16f);
            shurikentimer--;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[Type].Value;

            //For cool afterimage
            if (afterimagefloat > 0f)
            {
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    //Back Arm
                    Main.EntitySpriteDraw(tex,
                        NPC.oldPos[i] + new Vector2(14, 20) - Main.screenPosition + (new Vector2(NPC.spriteDirection == 1 ? 4 : 6, 0) * -NPC.spriteDirection + backarmoffset).RotatedBy(NPC.oldRot[i]),
                        new Rectangle(58, 2, 10, 18),
                        drawColor * ((20 - i) / 20f) * 0.6f * afterimagefloat,
                        NPC.oldRot[i] + backarmrot,
                        new Vector2(4, 2),
                        NPC.scale,
                        (NPC.spriteDirection * backarmdirection) == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
                    //Body
                    Main.EntitySpriteDraw(tex,
                        NPC.oldPos[i] + new Vector2(14, 20) - Main.screenPosition,
                        new Rectangle(0, 56 * animframe, 40, 56),
                        drawColor * ((20 - i) / 20f) * 0.6f * afterimagefloat,
                        NPC.oldRot[i],
                        new Vector2(20, 28),
                        NPC.scale,
                        NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
                    //Front Arm
                    Main.EntitySpriteDraw(tex,
                        NPC.oldPos[i] + new Vector2(14, 20) - Main.screenPosition - (new Vector2(NPC.spriteDirection == 1 ? 6 : 8, 0) * -NPC.spriteDirection + frontarmoffset).RotatedBy(NPC.oldRot[i]),
                        new Rectangle(44, 2, 10, 18),
                        drawColor * ((20 - i) / 20f) * 0.6f * afterimagefloat,
                        NPC.oldRot[i] + frontarmrot,
                        new Vector2(4, 2),
                        NPC.scale,
                        (NPC.spriteDirection * frontarmdirection) == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
                }
            }

            //Back Arm
            Main.EntitySpriteDraw(tex,
                NPC.Center - Main.screenPosition + (new Vector2(NPC.spriteDirection == 1 ? 4 : 6, 0) * -NPC.spriteDirection + backarmoffset).RotatedBy(NPC.rotation),
                new Rectangle(58, 2, 10, 18),
                drawColor,
                NPC.rotation + backarmrot,
                new Vector2(4, 2),
                NPC.scale,
                (NPC.spriteDirection * backarmdirection) == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            //Body
            Main.EntitySpriteDraw(tex,
                NPC.Center - Main.screenPosition,
                new Rectangle(0, 56 * animframe, 40, 56),
                drawColor,
                NPC.rotation,
                new Vector2(20, 28),
                NPC.scale,
                NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            //Front Arm
            Main.EntitySpriteDraw(tex,
                NPC.Center - Main.screenPosition - (new Vector2(NPC.spriteDirection == 1 ? 6 : 8, 0) * -NPC.spriteDirection + frontarmoffset).RotatedBy(NPC.rotation),
                new Rectangle(44, 2, 10, 18),
                drawColor,
                NPC.rotation + frontarmrot,
                new Vector2(4, 2),
                NPC.scale,
                (NPC.spriteDirection * frontarmdirection) == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            slashtrail.color = FadeOutTrailColor;
            slashtrail.offset = f => new Vector2(0, 30);
            slashtrail.opacity = dashtrailfloat;
            slashtrail.trailtexture = TextureAssets.MagicPixel.Value;
            slashtrail.Draw(NPC.Center);

            return false;
        }

        public static Color FadeOutTrailColor(float trailpart)
        {
            return new Color(1f, 1f, 1f, 0f) * trailpart;
        }

        private Vector2 GetArmHoldPosition()
        {
            Vector2 armlengthrotated = new Vector2(2, 14).RotatedBy(frontarmrot);
            return NPC.Center - (new Vector2(NPC.spriteDirection == 1 ? 6 : 8, 0) * -NPC.spriteDirection + frontarmoffset).RotatedBy(NPC.rotation) + armlengthrotated;
        }

        public override void OnKill()
        {
            if (katanaprojectile != null) katanaprojectile.Kill();
            katanaprojectile = null;
            base.OnKill();
        }
    }
}
