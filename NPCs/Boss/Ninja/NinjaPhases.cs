using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using static Terraria.GameContent.PlayerEyeHelper;

namespace Terrafirma.NPCs.Boss.Ninja
{
    public partial class Ninja : ModNPC
    {
        //Select Phase based on condition

        int shurikentimer = 300;
        int dashesexecuted = 0;
        int distancetimer = 0;
        private void ChoosePhase()
        {

            NPC.TargetClosest();

            byte statenumber;

            //Smoke bomb if too many dashes have been executed without the player being hit
            if (NPC.Center.Distance(target.Center) > 150f && (dashesexecuted > 12 || distancetimer > 200))
            {
                state = NinjaPhase.smokebomb;
                distancetimer = 30;
                dashesexecuted = 3;
                return;
            }

            //Jump is player is very high up
            if (NPC.Center.Y - target.Center.Y > 100f && NPC.Center.Y - target.Center.Y > Math.Abs(NPC.Center.X - target.Center.X))
            {
                state = NinjaPhase.jump;
                return;
            }

            //swing sword if player is in its range
            if (NPC.Center.Distance(target.Center) <= 60f)
            {
                state = NinjaPhase.swingsword;
                return;
            }

            //throw shurikens from time to time when the player gets too far away
            if (NPC.Center.Distance(target.Center) <= 300f && NPC.Center.Distance(target.Center) >= 200f && NPC.life < NPC.lifeMax * 0.7f)
            {
                if (shurikentimer <= 0) { 
                    state = NinjaPhase.throwshuriken;
                    shurikentimer = Main.rand.Next(700, 800);
                    return;
                }
            }

            //Otherwise check if player is far away -> Fast chase
            if (Math.Abs(NPC.Center.X - target.Center.X) > 400f)
            {
                state = NinjaPhase.fastchase;
                return;
            }
            //Otherwise if player is close -> Jump
            else if (Math.Abs(NPC.Center.X - target.Center.X) < 200f && Math.Abs(NPC.Center.X - target.Center.X) > 70f)
            {
                if (Main.rand.NextBool(4)) state = NinjaPhase.jump;
                else state = NinjaPhase.chase;
                return;
            }
            //Otherwise if player is very close -> chase
            else if (Math.Abs(NPC.Center.X - target.Center.X) <= 70f)
            {
                state = NinjaPhase.chase;
                return;
            }
            //Else just Idle or chase
            else statenumber = (byte)Main.rand.Next(0, 2);
            switch (statenumber)
            {
                case 0: state = Main.rand.NextBool(3) && NPC.life < NPC.lifeMax / 2? NinjaPhase.slashdash : NinjaPhase.idle; break;
                case 1: state = NinjaPhase.chase; break;
            }
        }
        private void Idle()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            NPC.velocity.X *= 0.9f;
            NPC.velocity.Y += 0.5f;
            NPC.frameCounter = 0;

            if (NPC.ai[0] > 30)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.none;
            }
            NPC.ai[0]++;
        }

        //Chase ________________________________________________________________________________________________________________
        private void Follow()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            NPC.velocity.Y += 0.5f;
            float maxspeed = NPC.life < NPC.lifeMax / 2 ? 7.5f : 5f;
            NPC.velocity.X = Math.Clamp(NPC.velocity.X + targetDirection * 0.5f, -maxspeed, maxspeed);
            NPC.spriteDirection = -targetDirection;

            if (NPC.ai[0] == 1) 
            { 
                dashesexecuted++; 
                if (NPC.Center.Distance(target.Center) > 500f)
                {
                    dashesexecuted += 2;
                }
            }

            if (NPC.ai[0] % 10 == 0) SoundEngine.PlaySound(SoundID.Run, NPC.Center);

            //Go back to base state after 60 frames
            if (NPC.ai[0] > 60)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.none;
            }

            //Speed up if player is far away
            if (Math.Abs(NPC.Center.X - target.Center.X) > 300f && NPC.life < NPC.lifeMax * 0.7f)
            {
                NPC.ai[0] = 0;
                if (Main.rand.NextBool(4)) state = NinjaPhase.throwshuriken;
                else state = NinjaPhase.fastchase;
                return;
            }

            //Jump is player is high up
            if (NPC.Center.Y - target.Center.Y > 100f && NPC.Center.Y - target.Center.Y > Math.Abs(NPC.Center.X - target.Center.X))
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.jump;
                return;
            }

            NPC.ai[0]++;
        }

        //Fast Chase ________________________________________________________________________________________________________________
        private void FastFollow()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0.2f * -NPC.spriteDirection, 0.2f);
            NPC.height = 47;
            NPC.velocity.Y += 0.5f;
            float maxspeed = NPC.life < NPC.lifeMax / 2 ? 13f : 10f;
            NPC.velocity.X = Math.Clamp(NPC.velocity.X + targetDirection * 0.5f, -maxspeed, maxspeed);
            NPC.spriteDirection = -targetDirection;

            if (NPC.ai[0] == 1) 
            { 
                dashesexecuted += 2;
                if (NPC.Center.Distance(target.Center) > 500f)
                {
                    dashesexecuted += 2;
                }
            }

            if (NPC.ai[0] % 10 == 0) SoundEngine.PlaySound(SoundID.Run, NPC.Center);

            //Go into slash dash if the player is far
            if (NPC.Center.Distance(target.Center) > 600f && NPC.life < NPC.lifeMax/2) state = NinjaPhase.slashdash;

            //Go back to base state after 90 frames
            if (NPC.ai[0] > 90)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.none;
                return;
            }

            //Slow down if player is close
            float slowdownrange = NPC.life < NPC.lifeMax / 2 ? 100f : 150f;
            if (Math.Abs(NPC.Center.X - target.Center.X) < slowdownrange)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.chase;
                return;
            }

            //Jump is player is high up
            if (NPC.Center.Y - target.Center.Y > 80f)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.jump;
                return;
            }

            afterimagefloat = Math.Clamp(afterimagefloat += 0.05f, 0f, 1f);
            NPC.ai[0]++;
        }

        bool coolspin = false;
        float afterimagefloat = 0f;

        int maxjumps = 2;

        //Jump ________________________________________________________________________________________________________________
        private void Jump()
        {
            NPC.height = NPC.width;

            //Jump movement
            if (NPC.ai[0] == 1)
            {
                //Check ground collision for double jump particles
                if (NPC.velocity.Y > 0.5f)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Dust d = Dust.NewDustPerfect(NPC.Bottom + Main.rand.NextVector2Circular(5f, 1f), DustID.Cloud, new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(2f, 3.5f)), Scale: 1.2f);
                    }
                }

                NPC.rotation = 0f;
                NPC.velocity.Y = Math.Clamp((target.Bottom.Y - NPC.Bottom.Y) / 8f, -25f, -10f);

                NPC.velocity.X = targetDirection * 2f * Math.Clamp(Math.Abs(NPC.velocity.X), 0.2f, 3.5f);
                NPC.spriteDirection = -targetDirection;
                SoundEngine.PlaySound(SoundID.DoubleJump, NPC.Center);

                coolspin = NPC.velocity.Length() > 16f;
            }

            NPC.velocity.Y += 0.5f;

            //Jump again if player is just out of reach
            if (Math.Abs(NPC.Center.X - target.Center.X) < 100f && NPC.Center.Y - target.Center.Y > 30f && NPC.ai[0] > 10 && maxjumps > 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust d = Dust.NewDustPerfect(NPC.Bottom + Main.rand.NextVector2Circular(5f, 1f), DustID.Cloud, new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(2f, 3.5f)), Scale: 1.2f);
                }
                NPC.ai[0] = 0;
                maxjumps--;
            }

             //Do cool spin and after image
            if (coolspin)
            {
                NPC.rotation += 0.2f * (NPC.velocity.X / Math.Abs(NPC.velocity.X));
                afterimagefloat = Math.Clamp(afterimagefloat += 0.05f, 0f, 1f);
            }

            //Slow down if the x velocity is not towards the player
            if (NPC.velocity.X / Math.Abs(NPC.velocity.X) != targetDirection) NPC.velocity.X *= 0.93f;

            //Increase air time for animation
            if (!NPC.collideY) airtime = Math.Clamp(airtime + 1, 0, 40);
            else airtime = 0;

            //stop when touching the ground
            if (NPC.ai[0] > 10 && NPC.collideY)
            {
                coolspin = false;
                NPC.height = 47;
                NPC.position.Y -= 24;
                NPC.ai[0] = 0;
                state = NinjaPhase.none;
                maxjumps = 2;
            }
            NPC.ai[0]++;
        }

        //Swing Sword ________________________________________________________________________________________________________________
        private void SwingSword()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            NPC.velocity.Y += 0.5f;
            NPC.velocity.X *= 0.9f;
            NPC.spriteDirection = -targetDirection;

            if (NPC.ai[0] == 1) 
            { 
                SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                dashesexecuted--;
            }

            if (katanaprojectile == null)
            {
                if(Main.netMode != NetmodeID.MultiplayerClient ) katanaprojectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<NinjaKatanaProj>(), NPC.damage, 1f, -1);
            }

            katanaprojectile.BottomLeft = frontarmholdposition;
            katanaprojectile.rotation = frontarmrot + MathHelper.Pi / 3;
            katanaprojectile.spriteDirection = -NPC.spriteDirection;

            if (NPC.ai[0] > 20)
            {
                NPC.ai[0] = 0;
                state = NinjaPhase.none;

                if (katanaprojectile != null) katanaprojectile.Kill();
                katanaprojectile = null;
            }
            NPC.ai[0]++;
        }

        int shurikenamount = 3;

        //Throw Shuriken ________________________________________________________________________________________________________________
        public void ThrowShuriken()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            float maxspeed = NPC.life < NPC.lifeMax / 2 ? 4f : 2f;
            NPC.velocity.X = Math.Clamp(NPC.velocity.X + targetDirection * 0.05f, -maxspeed, maxspeed); ;
            NPC.velocity.Y += 0.5f;
            NPC.spriteDirection = -targetDirection;

            float distancefloat = Math.Clamp((300f - NPC.Center.Distance(target.Center)) / 50f, 1f, 2f);

            if (NPC.ai[0] == 1 && NPC.ai[1] == 0) shurikenamount = Main.rand.Next(3, 6);

            if (NPC.ai[0] > 40 / distancefloat)
            {
                NPC.ai[0] = 0;
                NPC.ai[1]++;

                SoundEngine.PlaySound(SoundID.Item1);

                if (NPC.ai[1] % 2 == 0)
                {
                    for (int i = -2; i <= 1; i++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedBy(i * 0.4f + 0.2f) * 10f, ModContent.ProjectileType<NinjaShurikenHostile>(), NPC.damage / 4, NPC.knockBackResist, -1);
                    }
                }
                else
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedBy(i * 0.6f) * 10f, ModContent.ProjectileType<NinjaShurikenHostile>(), NPC.damage / 4, NPC.knockBackResist, -1);
                    }
                }
            }

            if (NPC.ai[1] >= shurikenamount)
            {
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;

                if (NPC.Center.Distance(target.Center) > 300f && NPC.life < NPC.lifeMax / 2 && Main.rand.NextBool()) state = NinjaPhase.slashdash;
                else if (NPC.life < (int)(NPC.lifeMax * 0.7f) && Main.rand.NextBool()) state = NinjaPhase.smokebomb;
                else state = NinjaPhase.none;
            }
            NPC.ai[0]++;
        }

        //Slash Dash ________________________________________________________________________________________________________________

        float dashtrailfloat = 0f;
        Vector2 setvelocity = Vector2.Zero;
        public void SlashDash()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            NPC.velocity.X *= 0.9f;
            NPC.spriteDirection = -targetDirection;

            if (katanaprojectile == null)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) katanaprojectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<NinjaKatanaProj>(), NPC.damage, 1f, -1);
            }
            katanaprojectile.timeLeft = 100;
            katanaprojectile.BottomLeft = frontarmholdposition;
            katanaprojectile.rotation = frontarmrot + MathHelper.Pi / 3;
            katanaprojectile.spriteDirection = -NPC.spriteDirection;

            if (NPC.ai[0] < 40) NPC.velocity.Y += 0.2f;
            if (NPC.ai[0] == 40) setvelocity = NPC.Center.DirectionTo(target.Center) * 128f;
            if (NPC.ai[0] == 50) 
            { 
                NPC.velocity = setvelocity;
                dashtrailfloat = 1f;
            }
            if (NPC.ai[0] >= 55)
            {
                NPC.ai[0] = 0;
                NPC.ai[1]++;

                SoundStyle slashsound = new SoundStyle("Terrafirma/Sounds/BigSlash");
                slashsound.PitchVariance = 0.2f;
                SoundEngine.PlaySound(slashsound, NPC.Center);

                NPC.velocity.X *= 0.3f;
                NPC.velocity.Y *= 0.05f;
            }

            if (NPC.ai[1] >= 5)
            {
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;

                if (katanaprojectile != null) katanaprojectile.Kill();
                katanaprojectile = null;

                state = NinjaPhase.jump;

            }
            NPC.ai[0]++;
        }

        //Smoke Bomb ________________________________________________________________________________________________________________

        float smokebombanimation = 0f;
        private void SmokeBomb()
        {
            NPC.rotation = Utils.AngleLerp(NPC.rotation % (float)(Math.PI * 2), 0f, 0.2f);
            NPC.height = 47;
            NPC.velocity.X *= 0.9f;
            NPC.velocity.Y += 0.5f;
            NPC.spriteDirection = -targetDirection;

            if (NPC.ai[0] < 45)
            {
                smokebombanimation = MathHelper.Lerp(smokebombanimation, 5f, 0.15f);
            }
            if (NPC.ai[0] >= 45 && NPC.ai[0] < 55)
            {
                smokebombanimation = Utils.AngleLerp(smokebombanimation, 0f, 0.6f);
            }
            if (NPC.ai[0] == 55)
            {
                SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
                for (int i =  0; i < 30; i++) Dust.NewDustPerfect(NPC.Center, DustID.Smoke, Main.rand.NextVector2Circular(1.3f, 1.3f), newColor: Color.DarkGray, Scale: 3f);
                NPC.position = target.Center + new Vector2(200f, -45f) * (target.velocity.X / Math.Abs(target.velocity.X));
                for (int i = 0; i < 30; i++) Dust.NewDustPerfect(NPC.Center, DustID.Smoke, Main.rand.NextVector2Circular(1.3f, 1.3f), newColor: Color.DarkGray, Scale: 3f);

                NPC.ai[0] = 0;
                if(NPC.life < NPC.lifeMax / 2 && Main.rand.NextBool()) state = NinjaPhase.slashdash;
                else state = NinjaPhase.none;
            }

            NPC.ai[0]++;

        }
    }
}
