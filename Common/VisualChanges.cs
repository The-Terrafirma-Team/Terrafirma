using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Particles;
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
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>(AssetFolder + "Items/ShadowCodex");
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>(AssetFolder + "Items/VileStaff");
            // NPCs
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Slime_0");
            TextureAssets.Npc[NPCID.LavaSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/LavaSlime");
            TextureAssets.Npc[NPCID.IlluminantSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/IlluminantSlime");
            TextureAssets.Npc[NPCID.WindyBalloon] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/BalloonSlime");
            TextureAssets.Npc[NPCID.MotherSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/MotherSlime");
            TextureAssets.Npc[NPCID.UmbrellaSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/UmbrellaSlime");
            TextureAssets.Npc[NPCID.DungeonSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/DungeonSlime");
            TextureAssets.Npc[NPCID.CorruptSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/CorruptSlime");
            TextureAssets.Npc[NPCID.SlimeSpiked] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/SpikedSlime");
            TextureAssets.Npc[NPCID.Crimslime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Crimslime");

            TextureAssets.Npc[NPCID.EaterofSouls] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_0");
            // Gore
            TextureAssets.Gore[14] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_0_Gore_0");
            TextureAssets.Gore[15] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_0_Gore_1");
            TextureAssets.Gore[314] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Umbrella");
            // Misc
            TextureAssets.Ninja = ModContent.Request<Texture2D>(AssetFolder + "Misc/Ninja");
            // Armor
            TextureAssets.Item[ItemID.NecroHelmet] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroHelmet");
            TextureAssets.Item[ItemID.NecroBreastplate] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroChest");
            TextureAssets.Item[ItemID.NecroGreaves] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroLegs");
            TextureAssets.ArmorHead[ArmorIDs.Head.NecroHelmet] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroHelmet_Head");
            TextureAssets.ArmorBodyComposite[ArmorIDs.Body.NecroBreastplate] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroChest_Body");
            TextureAssets.ArmorLeg[ArmorIDs.Legs.NecroGreaves] = ModContent.Request<Texture2D>(AssetFolder + "Items/NecroLegs_Legs");
        }
        public override void Unload()
        {
            // Items
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.DemonScythe}");
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.Vilethorn}");
            // NPCs
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.BlueSlime}");
            TextureAssets.Npc[NPCID.LavaSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.LavaSlime}");
            TextureAssets.Npc[NPCID.IlluminantSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.IlluminantSlime}");
            TextureAssets.Npc[NPCID.MotherSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.MotherSlime}");
            TextureAssets.Npc[NPCID.UmbrellaSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.UmbrellaSlime}");
            TextureAssets.Npc[NPCID.DungeonSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.DungeonSlime}");
            TextureAssets.Npc[NPCID.CorruptSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.CorruptSlime}");
            TextureAssets.Npc[NPCID.Crimslime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.Crimslime}");
            TextureAssets.Npc[NPCID.WindyBalloon] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.WindyBalloon}");

            // Gore
            TextureAssets.Gore[14] = ModContent.Request<Texture2D>($"Terraria/Images/Gore_14"); // Eater of
            TextureAssets.Gore[15] = ModContent.Request<Texture2D>($"Terraria/Images/Gore_15"); //   Souls
            TextureAssets.Gore[314] = ModContent.Request<Texture2D>($"Terraria/Images/Gore_314"); //   umbrella slime
            // Misc
            TextureAssets.Ninja = ModContent.Request<Texture2D>($"Terraria/Images/Ninja");
            // Armor
            TextureAssets.Item[ItemID.NecroHelmet] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroHelmet}");
            TextureAssets.Item[ItemID.NecroBreastplate] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroBreastplate}");
            TextureAssets.Item[ItemID.NecroGreaves] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.NecroGreaves}");
            TextureAssets.ArmorHead[ArmorIDs.Head.NecroHelmet] = ModContent.Request<Texture2D>($"Terraria/Images/Armor_Head_{ArmorIDs.Head.NecroHelmet}");
            TextureAssets.ArmorBodyComposite[ArmorIDs.Body.NecroBreastplate] = ModContent.Request<Texture2D>($"Terraria/Images/Armor/Armor_{ArmorIDs.Body.NecroBreastplate}");
            TextureAssets.ArmorLeg[ArmorIDs.Legs.NecroGreaves] = ModContent.Request<Texture2D>($"Terraria/Images/Armor_Legs_{ArmorIDs.Legs.NecroGreaves}");
        }
    }
    public class VanillaNPCSpriteChanges : GlobalNPC
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        public override bool InstancePerEntity => true;

        private static Asset<Texture2D>[] SlimeVariants = new Asset<Texture2D>[3];
        private static Asset<Texture2D>[] SoulEaterVariants = new Asset<Texture2D>[2];
        private static Asset<Texture2D> Pinky;
        private static Asset<Texture2D> Umbrella;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.ModNPC == null;
        }
        public byte variant = 0;
        public float UmbrellaRotation = 0f;
        public override void SetStaticDefaults()
        {
            //Slime
            SlimeVariants[0] = TextureAssets.Npc[1];
            for (int i = 1; i < SlimeVariants.Length; i++)
            {
                SlimeVariants[i] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Slime_" + $"{i}");
            }
            Pinky = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Pinky");
            Umbrella = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Umbrella");

            Main.npcFrameCount[1] = 6;
            Main.npcFrameCount[NPCID.LavaSlime] = 6;
            Main.npcFrameCount[NPCID.MotherSlime] = 6;
            Main.npcFrameCount[NPCID.DungeonSlime] = 6;
            Main.npcFrameCount[NPCID.IlluminantSlime] = 6;
            Main.npcFrameCount[NPCID.CorruptSlime] = 6;
            Main.npcFrameCount[NPCID.Crimslime] = 6;
            Main.npcFrameCount[NPCID.SlimeSpiked] = 6;
            Main.npcFrameCount[NPCID.UmbrellaSlime] = 6;
            // Corruption
            Main.npcFrameCount[NPCID.EaterofSouls] = 4;
            SoulEaterVariants[0] = TextureAssets.Npc[NPCID.EaterofSouls];
            for (int i = 1; i < SoulEaterVariants.Length; i++)
            {
                SoulEaterVariants[i] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_" + $"{i}");
            }
        }
        public override void SetDefaultsFromNetId(NPC npc)
        {
            if (npc.netID == NPCID.BabySlime)
            {
                npc.color = new Color(80, 80, 80, 128);
            }
        }
        public override void SetDefaults(NPC npc)
        {
            if (!npc.IsABestiaryIconDummy)
                variant = (byte)Main.rand.Next(255);
        }
        public override void Unload()
        {
            Main.npcFrameCount[1] = 2;
            Main.npcFrameCount[NPCID.LavaSlime] = 2;
            Main.npcFrameCount[NPCID.MotherSlime] = 2;
            Main.npcFrameCount[NPCID.DungeonSlime] = 2;
            Main.npcFrameCount[NPCID.CorruptSlime] = 2;
            Main.npcFrameCount[NPCID.IlluminantSlime] = 2;
            Main.npcFrameCount[NPCID.SlimeSpiked] = 2;
            Main.npcFrameCount[NPCID.UmbrellaSlime] = 2;
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            if (npc.type is NPCID.BlueSlime or NPCID.LavaSlime or NPCID.MotherSlime or NPCID.IlluminantSlime or NPCID.DungeonSlime or NPCID.CorruptSlime or NPCID.Crimslime or NPCID.UmbrellaSlime or NPCID.SlimeSpiked)
            {
                npc.frameCounter += Math.Sin(variant * 0.1f) * 0.2f;
                npc.frameCounter += (1f - (npc.life / (float)npc.lifeMax)) * 2;
                if (npc.frame.Y > frameHeight * 3)
                    npc.frame.Y = 0;
                if (npc.velocity.Y < 0)
                    npc.frame.Y = frameHeight * 5;
                else if (npc.velocity.Y > 0)
                    npc.frame.Y = frameHeight * 4;
            }
            else if (npc.type == NPCID.EaterofSouls)
            {
                npc.frameCounter += Math.Sin(variant * 0.1f) * 0.2f;
                npc.frameCounter += (1f - (MathHelper.Clamp(npc.Center.Distance(Main.player[npc.target].Center), 0, 200) / 200f)) * 2;
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            switch (npc.type)
            {
                case NPCID.BlueSlime:
                    if (npc.ai[1] > 0)
                    {
                        Main.GetItemDrawFrame((int)npc.ai[1], out var itemTexture, out var rectangle);
                        //Main.GetItemDrawFrame(ItemID.FieryGreatsword, out var itemTexture, out var rectangle);
                        //float itemScale = (float)npc.frame.Height / Math.Max(rectangle.Width,rectangle.Height) * 0.5f;
                        float itemScale = 0.7f;
                        spriteBatch.Draw(itemTexture, npc.Center - screenPos + npc.velocity * -0.3f + new Vector2(0, (float)Math.Sin(Main.timeForVisualEffects * 0.05f)), rectangle, drawColor, npc.rotation + ((float)Math.Sin(Main.timeForVisualEffects * 0.1f) * (float)Math.Cos(Main.timeForVisualEffects * 0.03f) * (npc.velocity.Length() + 1) * 0.1f), rectangle.Size() / 2, itemScale * npc.scale, SpriteEffects.None, 0);
                    }
                    if(npc.netID == NPCID.Pinky)
                        spriteBatch.Draw(Pinky.Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, drawColor * 0.8f, npc.rotation, new Vector2(16, 26), npc.scale * 2, SpriteEffects.None, 0);
                    else
                        spriteBatch.Draw(SlimeVariants[variant % 3].Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, npc.GetColor(drawColor), npc.rotation, new Vector2(16, 26), npc.scale + (variant - 128f) / 128f * 0.1f, SpriteEffects.None, 0);
                    return false;
                case NPCID.UmbrellaSlime:
                    UmbrellaRotation = MathHelper.Lerp(UmbrellaRotation, npc.velocity.X * -0.1f, 0.1f);
                    spriteBatch.Draw(Umbrella.Value, npc.Center - screenPos, null, drawColor, npc.rotation + UmbrellaRotation + (float)Math.Sin(Main.timeForVisualEffects * 0.01f + variant) * 0.1f, new Vector2(23, 44),npc.scale,SpriteEffects.None,0);
                    break;
                case NPCID.EaterofSouls:
                    spriteBatch.Draw(SoulEaterVariants[variant % 2].Value, npc.Center - screenPos, npc.frame, drawColor/*.MultiplyRGB(Color.Lerp(Color.White, new Color(0.85f, 0.8f, 0.9f), Math.Abs(MathF.Sin(variant * 0.1f))))*/, npc.rotation, new Vector2(25, 56), npc.scale, SpriteEffects.None, 0);
                    return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.life > 0)
                return;
            switch (npc.type)
            {
                case NPCID.EaterofSouls:
                    int latest = FindLatestGore();

                    if (variant % 2 == 1)
                    {
                        Main.gore[latest].type = Mod.Find<ModGore>("EaterOfSouls_1_Gore_0").Type;
                        Main.gore[latest - 1].type = Mod.Find<ModGore>("EaterOfSouls_1_Gore_1").Type;
                    }
                    break;
            }
        }
        private int FindLatestGore()
        {
            for(int i = 0; i < Main.maxGore; i++)
            {
                if (Main.gore[i].active == false)
                    return i - 1;
            }
            return Main.maxGore;
        }
    }
    public class ProjectileChanges : GlobalProjectile
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        private static Asset<Texture2D> DemonScythe;
        public override void Load()
        {
            DemonScythe = ModContent.Request<Texture2D>(AssetFolder + "Projectiles/DemonScythe");
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
                    Color color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(0.3f, 0f, 1f, 0f), (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f);
                    for (int i = 0; i < 5; i++)
                    {
                        TFUtils.EasyCenteredProjectileDraw(DemonScythe, projectile, color * (1f - (i / 5f)) * 0.3f, projectile.oldPos[i] + (projectile.Size / 2) - Main.screenPosition, projectile.oldRot[i], 1f);
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

        private static readonly int[] Bullets = { 14, 36, 89, 104, 110, 180, 207, 242, 279, 283, 284, 285, 286, 287, 302, 638, 981 };
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
                if (p.oldPos[i + 1] != Vector2.Zero)
                {
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition, new Rectangle(0, 8, 14, 2), darkColor * (1 - ((i - 1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale * p.scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
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
                    drawBullet(projectile, new Color(255, 128, 255, 128), new Color(200, 0, 255, 128));
                    break;
                case 285: // Nano
                    drawBullet(projectile, new Color(0, 255, 255, 0), new Color(0, 0, 0, 255));
                    break;
                case 286: // Explosive
                    drawBullet(projectile, new Color(1f, Main.masterColor, 0f, 0f), new Color(1f, 0f, 0f, 0f));
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
