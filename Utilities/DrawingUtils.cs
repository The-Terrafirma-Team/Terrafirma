using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Utilities
{
    public static class DrawingUtils
    {
        public static void DrawConfusedQuestionMark(this NPC rCurrentNPC, SpriteBatch spritebatch, Vector2 screenPos, float heightOffset = 0f)
        {
            float num36 = Main.NPCAddHeight(rCurrentNPC);
            if (rCurrentNPC.confused)
            {
                Vector2 halfSize = rCurrentNPC.frame.Size() / 2;
                spritebatch.Draw(TextureAssets.Confuse.Value, new Vector2(rCurrentNPC.position.X - screenPos.X + rCurrentNPC.width / 2 - TextureAssets.Npc[rCurrentNPC.type].Width() * rCurrentNPC.scale / 2f + halfSize.X * rCurrentNPC.scale, rCurrentNPC.position.Y - screenPos.Y + rCurrentNPC.height - TextureAssets.Npc[rCurrentNPC.type].Height() * rCurrentNPC.scale / Main.npcFrameCount[rCurrentNPC.type] + 4f + halfSize.Y * rCurrentNPC.scale + num36 + heightOffset - TextureAssets.Confuse.Height() - 20f), new Rectangle(0, 0, TextureAssets.Confuse.Width(), TextureAssets.Confuse.Height()), rCurrentNPC.GetShimmerColor(new Color(250, 250, 250, 70)), rCurrentNPC.velocity.X * -0.05f, new Vector2(TextureAssets.Confuse.Width() / 2, TextureAssets.Confuse.Height() / 2), Main.essScale + 0.2f, SpriteEffects.None, 0f);
            }
        }
    }
}
