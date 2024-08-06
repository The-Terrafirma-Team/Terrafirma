using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class VisualChanges : ModSystem
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        public override void Load()
        {
            // Items
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>(AssetFolder + "ShadowCodex");
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>(AssetFolder + "VileStaff");
            // NPCs
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>(AssetFolder + "Slime");
        }
        public override void Unload()
        {
            // Items
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.DemonScythe}");
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.Vilethorn}");
            // NPCs
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.BlueSlime}");
        }
    }

    public class VanillaNPCSpriteChanges : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.ModNPC == null;
        }
        public override void SetStaticDefaults()
        {
            //for(int i = -10; i <= -3; i++) // Increase slime frame count
            //{
            //    Main.npcFrameCount[i] = 6;
            //}
            Main.npcFrameCount[1] = 6;
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            if(npc.type == NPCID.BlueSlime)
            {
                if (npc.frame.Y > frameHeight * 3)
                    npc.frame.Y = 0;
                if (npc.velocity.Y < 0)
                    npc.frame.Y = frameHeight * 4;
                else if (npc.velocity.Y > 0)
                    npc.frame.Y = frameHeight * 5;
            }
        }
        //public int newFrame = 0;
        //public int newFrameCounter = 0;
        //public override void FindFrame(NPC npc, int frameHeight)
        //{
        //    if (npc.type == NPCID.BlueSlime)
        //    {
        //        int num2 = 0;
        //        newFrameCounter++;
        //        if (npc.aiAction == 0)
        //        {
        //            num2 = ((npc.velocity.Y < 0f) ? 2 : ((npc.velocity.Y > 0f) ? 3 : ((npc.velocity.X != 0f) ? 1 : 0)));
        //        }
        //        else if (npc.aiAction == 1)
        //        {
        //            num2 = 4;
        //        }
        //        if (num2 > 0)
        //            newFrameCounter++;
        //        if (num2 == 4)
        //            newFrameCounter++;
        //        if (newFrameCounter >= 8)
        //        {
        //            newFrameCounter = 0;
        //            newFrame++;
        //            if (newFrame > 3)
        //                newFrame = 0;
        //        }
        //        npc.frame = new Rectangle(0, 28 * newFrame, 32, 26);
        //        if (npc.velocity.Y < 0)
        //            npc.frame.Y = 28 * 4;
        //        else if (npc.velocity.Y > 0)
        //            npc.frame.Y = 28 * 5;
        //    }
        //}
        //public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    if(npc.type == NPCID.BlueSlime)
        //    {
        //        if (npc.ai[1] > 0f)
        //        {
        //            DrawNPC_SlimeItem(npc, npc.type, drawColor, npc.rotation);
        //        }
        //        spriteBatch.Draw(Slime.Value, npc.Bottom - Main.screenPosition - new Vector2(0,-2), npc.frame, npc.GetColor(drawColor), npc.rotation, new Vector2(16, 26), npc.scale, SpriteEffects.None, 0);
        //        return false;
        //    }
        //    return true;
        //}
        //private static void DrawNPC_SlimeItem(NPC rCurrentNPC, int typeCache, Microsoft.Xna.Framework.Color npcColor, float addedRotation)
        //{
        //    int num = (int)rCurrentNPC.ai[1];
        //    float num2 = 1f;
        //    float num3 = 22f * rCurrentNPC.scale;
        //    float num4 = 18f * rCurrentNPC.scale;
        //    Main.GetItemDrawFrame(num, out var itemTexture, out var rectangle);
        //    float num5 = rectangle.Width;
        //    float num6 = rectangle.Height;
        //    bool num7 = (int)rCurrentNPC.ai[0] == -999;
        //    if (num7)
        //    {
        //        num3 = 14f * rCurrentNPC.scale;
        //        num4 = 14f * rCurrentNPC.scale;
        //    }
        //    if (num5 > num3)
        //    {
        //        num2 *= num3 / num5;
        //        num5 *= num2;
        //        num6 *= num2;
        //    }
        //    if (num6 > num4)
        //    {
        //        num2 *= num4 / num6;
        //        num5 *= num2;
        //        num6 *= num2;
        //    }
        //    float num8 = -1f;
        //    float num9 = 1f;
        //    int num10 = rCurrentNPC.frame.Y / (TextureAssets.Npc[typeCache].Height() / Main.npcFrameCount[typeCache]);
        //    num9 -= (float)num10;
        //    num8 += (float)(num10 * 2);
        //    float num11 = 0.2f;
        //    num11 -= 0.3f * (float)num10;
        //    if (num7)
        //    {
        //        num11 = 0f;
        //        num9 -= 6f;
        //        num8 -= num5 * addedRotation;
        //    }
        //    if (num == 75)
        //    {
        //        npcColor = new Microsoft.Xna.Framework.Color(255, 255, 255, 0);
        //        num11 *= 0.3f;
        //        num9 -= 2f;
        //    }
        //    npcColor = rCurrentNPC.GetShimmerColor(npcColor);
        //    Main.spriteBatch.Draw(itemTexture, new Vector2(rCurrentNPC.Center.X - Main.screenPosition.X + num8, rCurrentNPC.Center.Y - Main.screenPosition.Y + rCurrentNPC.gfxOffY + num9), rectangle, npcColor, num11, rectangle.Size() / 2f, num2, SpriteEffects.None, 0f);
        //}
    }
    public class BulletVisuals : GlobalProjectile
    {
        private static Asset<Texture2D> tex;

        private static readonly int[] Bullets = {14,36,89,104,110,180,207,242,279,283,284,285,286,287,302,638,981};
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return Bullets.Contains(entity.type);
        }
        public override void SetStaticDefaults()
        {
            tex = Mod.Assets.Request<Texture2D>("Assets/Bullet");
            for (int i = 0; i < Bullets.Length; i++)
            {
                ProjectileID.Sets.TrailCacheLength[Bullets[i]] = 8;
                ProjectileID.Sets.TrailingMode[Bullets[i]] = 0;
            }
            ProjectileID.Sets.TrailCacheLength[302] = 16;
            ProjectileID.Sets.TrailCacheLength[242] = 16;
            ProjectileID.Sets.TrailCacheLength[638] = 16;
        }
        public static void drawBullet(Projectile p, Color brightColor, Color darkColor, float scale = 1f)
        {
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, null, darkColor, p.rotation, tex.Size() / 2, scale, SpriteEffects.None);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[p.type] - 1; i++)
            {
                //Main.NewText(p.oldPos[7]);
                if (p.oldPos[i+1] != Vector2.Zero)
                {
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition, new Rectangle(0, 8, 14, 2), darkColor * (1 - ((i-1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition, new Rectangle(0, 8, 14, 2), brightColor * (0.5f - ((i - 1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                }
            }
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, null, brightColor, p.rotation, tex.Size() / 2, scale, SpriteEffects.None);
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            switch (projectile.type)
            {
                case 14:
                case 110:
                case 180: // normal
                    drawBullet(projectile, new Color(1f, 1f, 0.5f, 0f), new Color(1f, 0.1f, 0f, 0.5f));
                    break;
                case 36: // meteor
                    drawBullet(projectile, new Color(1f, 0.4f, 0.4f, 0f), new Color(0.8f, 0f, 0f, 0.7f));
                    break;
                case 89: // Crystal
                    drawBullet(projectile, new Color(0.4f, 1f, 1f, 0f), new Color(0.3f, 0.5f, 0.8f, 0.5f));
                    break;
                case 104: // Cursed
                    drawBullet(projectile, new Color(0.9f, 1f, 0f, 0f), new Color(0f, 0.5f, 0f, 0.5f));
                    break;
                case 207: // Chloro
                    drawBullet(projectile, new Color(0f, 1f, 0f, 0f), new Color(0f, 0.5f, 0.2f, 0.5f));
                    break;
                case 302:
                case 242: // High Velocity
                    drawBullet(projectile, new Color(1f, 1f, 0.7f, 0f), new Color(0.5f, 0.5f, 0f, 0.5f));
                    break;
                case 279: // Ichor
                    drawBullet(projectile, new Color(1f, 1f, 0.3f, 0f), new Color(0.5f, 0.2f, 0f, 0.5f));
                    break;
                case 283: // Venom
                    drawBullet(projectile, new Color(185, 128, 193, 128), new Color(95, 67, 139, 255));
                    break;
                case 284: // Party
                    drawBullet(projectile, new Color(255,128,255,128), new Color(200, 0, 255, 128));
                    break;
                case 285: // Nano
                    drawBullet(projectile, new Color(0, 255, 255, 0), new Color(0, 0, 0, 255));
                    break;
                case 286: // Explosive
                    drawBullet(projectile, new Color(1f,Main.masterColor,0f, 0f), new Color(1f, 0f, 0f, 0f));
                    break;
                case 287: // Ichor
                    drawBullet(projectile, new Color(1f, 1f, 0.6f, 0f), new Color(0.5f, 0.2f, 0f, 0.5f));
                    break;
                case 638: // Luminite
                    if (projectile.alpha > 236)
                        return false;
                    drawBullet(projectile, new Color(0, 1f, 0.8f, 0f), new Color(0f, 0.7f, 0.5f, 0f));
                    break;
                case 981: // silver
                    if (projectile.timeLeft > 593)
                        return false;
                    drawBullet(projectile, new Color(1f, 1f, 0.8f, 0f), new Color(1f, 0.2f, 0.1f, 0.5f));
                    break;
            }
            return false;
        }
    }
}
