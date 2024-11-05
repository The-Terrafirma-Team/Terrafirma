using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
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
            TextureAssets.Npc[NPCID.WindyBalloon] = ModContent.Request<Texture2D>(AssetFolder + "BalloonSlime");
            // Misc
            TextureAssets.Ninja = ModContent.Request<Texture2D>(AssetFolder + "Ninja");
            // Armor
            TextureAssets.Item[ItemID.NecroHelmet] = ModContent.Request<Texture2D>(AssetFolder + "NecroHelmet");
            TextureAssets.Item[ItemID.NecroBreastplate] = ModContent.Request<Texture2D>(AssetFolder + "NecroChest");
            TextureAssets.Item[ItemID.NecroGreaves] = ModContent.Request<Texture2D>(AssetFolder + "NecroLegs");
            TextureAssets.ArmorHead[ArmorIDs.Head.NecroHelmet] = ModContent.Request<Texture2D>(AssetFolder + "NecroHelmet_Head");
            TextureAssets.ArmorBodyComposite[ArmorIDs.Body.NecroBreastplate] = ModContent.Request<Texture2D>(AssetFolder + "NecroChest_Body");
            TextureAssets.ArmorLeg[ArmorIDs.Legs.NecroGreaves] = ModContent.Request<Texture2D>(AssetFolder + "NecroLegs_Legs");
        }
        public override void Unload()
        {
            // Items
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.DemonScythe}");
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.Vilethorn}");
            // NPCs
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.BlueSlime}");
            TextureAssets.Npc[NPCID.WindyBalloon] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.WindyBalloon}");
            // Misc
            TextureAssets.Ninja = ModContent.Request<Texture2D>($"Terraria/Images/Ninja");
            // Armor
            TextureAssets.Item[ItemID.NecroHelmet] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroHelmet}");
            TextureAssets.Item[ItemID.NecroBreastplate] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroBreastplate}");
            TextureAssets.Item[ItemID.NecroGreaves] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroGreaves}");
            TextureAssets.ArmorHead[ArmorIDs.Head.NecroHelmet] = ModContent.Request<Texture2D>($"Terraria/Images/Armor_Head_{ArmorIDs.Head.NecroHelmet}");
            TextureAssets.ArmorBodyComposite[ArmorIDs.Body.NecroBreastplate] = ModContent.Request<Texture2D>($"Terraria/Images/Armor/Armor_{ArmorIDs.Body.NecroBreastplate}");
            TextureAssets.ArmorLeg[ArmorIDs.Legs.NecroGreaves] = ModContent.Request<Texture2D>($"Terraria/Images/Armor_Legs{ArmorIDs.Legs.NecroGreaves}");
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
            Main.npcFrameCount[1] = 6;
        }
        public override void Unload()
        {
            Main.npcFrameCount[1] = 2;
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
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if(npc.type == NPCID.BlueSlime)
            {
                if (npc.ai[1] > 0)
                {
                    //Asset<Texture2D> itemTex = TextureAssets.Item[(int)npc.ai[1]];
                    Main.GetItemDrawFrame((int)npc.ai[1], out var itemTexture, out var rectangle);
                    spriteBatch.Draw(itemTexture, npc.Center - screenPos + npc.velocity * -0.3f, rectangle, drawColor, npc.rotation + ((float)Math.Sin(Main.timeForVisualEffects * 0.1f) * (npc.velocity.Length() + 1) * 0.1f), rectangle.Size() / 2, 0.7f * npc.scale, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(TextureAssets.Npc[NPCID.BlueSlime].Value, npc.Bottom - screenPos + new Vector2(0,2), npc.frame, npc.GetColor(drawColor), npc.rotation, new Vector2(16, 26), npc.scale, SpriteEffects.None, 0);
                
                return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
    public class ProjectileChanges : GlobalProjectile
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        private static Asset<Texture2D> DemonScythe;
        public override void Load()
        {
            DemonScythe = ModContent.Request<Texture2D>(AssetFolder + "DemonScythe");
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[ProjectileID.DemonSickle] = 2;
            ProjectileID.Sets.TrailCacheLength[ProjectileID.DemonSickle] = 5;
            ProjectileID.Sets.TrailingMode[ProjectileID.DemonScythe] = 2;
            ProjectileID.Sets.TrailCacheLength[ProjectileID.DemonScythe] = 5;
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            switch (projectile.type)
            {
                case ProjectileID.DemonScythe:
                case ProjectileID.DemonSickle:
                    Color color = Color.Lerp(new Color(1f,1f,1f,0f),new Color(0.3f,0f,1f,0f),(float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f);
                    for(int i = 0; i < 5; i++)
                    {
                        TFUtils.EasyCenteredProjectileDraw(DemonScythe, projectile, color * (1f - (i/5f)) * 0.3f, projectile.oldPos[i] + (projectile.Size / 2) - Main.screenPosition, projectile.oldRot[i],1f);
                    }
                    TFUtils.EasyCenteredProjectileDraw(DemonScythe, projectile, color);
                    return false;
            }
            return base.PreDraw(projectile, ref lightColor);
        }
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
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition, new Rectangle(0, 8, 14, 2), darkColor * (1 - ((i-1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale * p.scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition, new Rectangle(0, 8, 14, 2), brightColor * (0.5f - ((i - 1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale * p.scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                }
            }
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, null, brightColor, p.rotation, tex.Size() / 2, scale * p.scale, SpriteEffects.None);
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
