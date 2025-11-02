using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common;
public class NPCStats : GlobalNPC
{
    private static Asset<Texture2D> SilenceTex;
    private static Asset<Texture2D> StunTex;
    public override void Load()
    {
        SilenceTex = Mod.Assets.Request<Texture2D>("Assets/Silenced");
        StunTex = Mod.Assets.Request<Texture2D>("Assets/Stunned");
    }
    public override bool InstancePerEntity => true;

    public bool NoAnimation = false;
    public bool Immobile = false;
    public bool NoFlight = false;
    public bool Silenced = false;
    public bool StunStarEffects = false;

    public float MoveSpeed = 1f;
    public float AttackSpeed = 1f;
    public override void ResetEffects(NPC npc)
    {
        NoAnimation = false;
        Immobile = false;
        NoFlight = false;
        Silenced = false;
        StunStarEffects = false;

        MoveSpeed = 1f;
        AttackSpeed = 1f;
    }

    public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
        if (target.PlayerStats().ImmuneToContactDamage)
            return false;
        return base.CanHitPlayer(npc, target, ref cooldownSlot);
    }
    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        float alpha = Lighting.Brightness((int)(npc.Center.X / 16), (int)((npc.Center.Y + npc.gfxOffY) / 16));
        if (Silenced)
        {
            spriteBatch.Draw(SilenceTex.Value, npc.Top - screenPos + new Vector2(0, -10), null, Color.White * alpha, 0f, SilenceTex.Size() / 2, 1f + Main.masterColor * 0.1f, SpriteEffects.None, 0);
        }
        if (StunStarEffects)
        {
            Rectangle frame = StunTex.Frame(1, 6, 0, (int)(Main.timeForVisualEffects / 4) % 6);
            spriteBatch.Draw(StunTex.Value, npc.Top - screenPos + new Vector2(0, -10), frame, Color.White * alpha, 0f, frame.Size() / 2, 1f, SpriteEffects.None, 0);
        }
    }
}
