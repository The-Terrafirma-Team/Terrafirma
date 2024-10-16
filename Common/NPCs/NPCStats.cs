using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using ReLogic.Content;

namespace Terrafirma.Common.NPCs
{
    public class NPCStats : GlobalNPC
    {
        private static Asset<Texture2D> SilenceTex;
        public override void Load()
        {
            SilenceTex = Mod.Assets.Request<Texture2D>("Assets/Silenced");
        }

        public static Color EnemyManaGainColor = new Color(200, 64, 255);
        public static Color EnemyManaDamageColor = new Color(128,0,255);
        public static Color FriendlyManaDamageColor = new Color(255, 0, 255);
        public override bool InstancePerEntity => true;

        public int Mana;
        public int MaxMana;
        public int ManaRegenTimer;
        public bool Silenced;

        public bool PhantasmalBurn;
        public bool ElectricCharge;
        public bool Stunned;
        public bool Inked;
        public bool Chilled;
        public float ThrowerDOT;
        public override void ResetEffects(NPC npc)
        {
            if (ManaRegenTimer > 30 && ManaRegenTimer % 10 == 0)
                Mana++;
            
            Mana = Math.Clamp(Mana, 0, MaxMana);
            ManaRegenTimer++;

            ThrowerDOT = 0;
            Silenced = false;
            PhantasmalBurn = false;
            ElectricCharge = false;
            Stunned = false;
            Inked = false;
            Chilled = false;
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if (Mana != 0)
            //    DrawManaBar(npc,spriteBatch);
            if (Silenced)
            {
                float alpha = Lighting.Brightness((int)(npc.Center.X / 16), (int)((npc.Center.Y + npc.gfxOffY) / 16));
                spriteBatch.Draw(SilenceTex.Value, npc.Top - screenPos + new Vector2(0,-10), null, Color.White * alpha, 0f, SilenceTex.Size() / 2, 1f + Main.masterColor * 0.1f, SpriteEffects.None, 0);
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PhantasmalBurn)
            {
                damage += 15;
                npc.lifeRegen -= 60;
            }
            if (ElectricCharge)
            {
                damage += 5;
                npc.lifeRegen -= (int)(npc.velocity.Length() * 4f) - 1;
            }
            npc.lifeRegen -= (int)ThrowerDOT;
            damage += (int)(ThrowerDOT / 5f);
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Inked)
            {
                drawColor.R = (byte)(drawColor.R * (128 / 255f));
                drawColor.G = (byte)(drawColor.G * (130 / 255f));
                drawColor.B = (byte)(drawColor.B * (237 / 255f));
                //drawColor = new Color(179, 130, 237);
            }
            if (Chilled)
            {
                drawColor.R = (byte)(drawColor.R * (128 / 255f));
                drawColor.G = (byte)(drawColor.G * (200 / 255f));
                //drawColor = new Color(128, 200, 255);
                if (Main.rand.NextBool(3))
                {
                    Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Snow);
                    d.noGravity = true;
                    d.velocity *= 0.2f;
                    d.scale = 0.6f;
                }
            }
        }
    }
}
