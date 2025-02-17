﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Particles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class VisualChanges : ModSystem
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        const string GoreFolder = "Terrafirma/Gores/Vanilla/";
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
            TextureAssets.Npc[NPCID.UmbrellaSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/UmbrellaSlime_0");
            TextureAssets.Npc[NPCID.DungeonSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/DungeonSlime");
            TextureAssets.Npc[NPCID.CorruptSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/CorruptSlime");
            TextureAssets.Npc[NPCID.SlimeSpiked] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/SpikedSlime");
            TextureAssets.Npc[NPCID.Crimslime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/Crimslime");
            TextureAssets.Npc[NPCID.RainbowSlime] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/RainbowSlime");

            TextureAssets.Npc[NPCID.EaterofSouls] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_0");
            // Gore
            TextureAssets.Gore[14] = ModContent.Request<Texture2D>(GoreFolder + "EaterOfSouls_0_Gore_0");
            TextureAssets.Gore[15] = ModContent.Request<Texture2D>(GoreFolder + "EaterOfSouls_0_Gore_1");
            TextureAssets.Gore[314] = ModContent.Request<Texture2D>(GoreFolder + "Umbrella_0");
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
            TextureAssets.Npc[NPCID.RainbowSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.RainbowSlime}");

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
        const string GoreFolder = "Terrafirma/Gores/Vanilla/";
        public override bool InstancePerEntity => true;

        private static Asset<Texture2D>[] SlimeVariants = new Asset<Texture2D>[3];
        private static Asset<Texture2D>[] SoulEaterVariants = new Asset<Texture2D>[2];
        private static Asset<Texture2D> Pinky;
        private static Asset<Texture2D> UmbrellaSlimeLight;
        private static Asset<Texture2D>[] Umbrella = new Asset<Texture2D>[12];
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
            UmbrellaSlimeLight = ModContent.Request<Texture2D>(AssetFolder + "NPCs/UmbrellaSlime_1");
            for (int i = 0; i < Umbrella.Length; i++)
            {
                Umbrella[i] = ModContent.Request<Texture2D>(GoreFolder + "Umbrella_" + $"{i}");
            }

            Main.npcFrameCount[1] = 6;
            Main.npcFrameCount[NPCID.LavaSlime] = 6;
            Main.npcFrameCount[NPCID.MotherSlime] = 6;
            Main.npcFrameCount[NPCID.DungeonSlime] = 6;
            Main.npcFrameCount[NPCID.IlluminantSlime] = 6;
            Main.npcFrameCount[NPCID.CorruptSlime] = 6;
            Main.npcFrameCount[NPCID.Crimslime] = 6;
            Main.npcFrameCount[NPCID.SlimeSpiked] = 6;
            Main.npcFrameCount[NPCID.UmbrellaSlime] = 6;
            Main.npcFrameCount[NPCID.RainbowSlime] = 11;

            // Corruption
            Main.npcFrameCount[NPCID.EaterofSouls] = 4;
            SoulEaterVariants[0] = TextureAssets.Npc[NPCID.EaterofSouls];
            for (int i = 1; i < SoulEaterVariants.Length; i++)
            {
                SoulEaterVariants[i] = ModContent.Request<Texture2D>(AssetFolder + "NPCs/EaterOfSouls_" + $"{i}");
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
            Main.npcFrameCount[NPCID.RainbowSlime] = 2;
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            switch (npc.type)
            {
                case NPCID.BlueSlime:
                case NPCID.LavaSlime:
                case NPCID.MotherSlime:
                case NPCID.IlluminantSlime:
                case NPCID.DungeonSlime:
                case NPCID.CorruptSlime:
                case NPCID.Crimslime:
                case NPCID.UmbrellaSlime:
                case NPCID.SlimeSpiked:
                    npc.frameCounter += Math.Sin(variant * 0.1f) * 0.2f;
                    npc.frameCounter += (1f - (npc.life / (float)npc.lifeMax)) * 2;
                    if (npc.frame.Y > frameHeight * 3)
                        npc.frame.Y = 0;
                    if (npc.velocity.Y < 0)
                        npc.frame.Y = frameHeight * 5;
                    else if (npc.velocity.Y > 0)
                        npc.frame.Y = frameHeight * 4;
                    return;
                case NPCID.RainbowSlime:
                    if (npc.frame.Y > frameHeight * 3)
                        npc.frame.Y = 0;
                    if ((npc.ai[0] is > -40 and <= -20) || (npc.ai[0] is >= -1040 and <= -1020) || (npc.ai[0] is >= -2040 and <= -2020))
                    {
                        npc.frame.Y = frameHeight * 4;
                    }
                    else if ((npc.ai[0] is >= -20 and <= 0) || (npc.ai[0] is >= -1020 and <= -1000) || (npc.ai[0] is >= -2020 and <= -2000))
                    {
                        npc.frame.Y = frameHeight * 5;
                    }
                    if (npc.velocity.Y != 0)
                    {
                        npc.frame.Y = frameHeight * (8 + (int)MathHelper.Clamp(npc.velocity.Y,-2,2));
                    }
                    return;

                case NPCID.EaterofSouls:
                    npc.frameCounter += Math.Sin(variant * 0.1f) * 0.2f;
                    npc.frameCounter += (1f - (MathHelper.Clamp(npc.Center.Distance(Main.player[npc.target].Center), 0, 200) / 200f)) * 2;
                    return;
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
                    if (npc.netID == NPCID.Pinky)
                        spriteBatch.Draw(Pinky.Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, drawColor, npc.rotation, new Vector2(16, 26), npc.scale * 2, SpriteEffects.None, 0);
                    else
                    {
                        spriteBatch.Draw(SlimeVariants[variant % 3].Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, drawColor * npc.Opacity, npc.rotation, new Vector2(16, 26), npc.scale + (variant - 128f) / 128f * 0.1f, SpriteEffects.None, 0);
                        spriteBatch.Draw(SlimeVariants[variant % 3].Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, npc.GetColor(npc.GetNPCColorTintedByBuffs(drawColor)), npc.rotation, new Vector2(16, 26), npc.scale + (variant - 128f) / 128f * 0.1f, SpriteEffects.None, 0);
                    }
                    return false;

                case NPCID.UmbrellaSlime:
                    UmbrellaRotation = MathHelper.Lerp(UmbrellaRotation, npc.velocity.X * -0.1f, 0.1f);
                    spriteBatch.Draw(Umbrella[variant % Umbrella.Length].Value, npc.Center - screenPos, null, drawColor, npc.rotation + UmbrellaRotation + (float)Math.Sin(Main.timeForVisualEffects * 0.01f + variant) * 0.1f, new Vector2(23, 44),npc.scale,SpriteEffects.None,0);
                    spriteBatch.Draw(variant % 3 == 0? TextureAssets.Npc[npc.type].Value : UmbrellaSlimeLight.Value, npc.Bottom - screenPos + new Vector2(0, 2), npc.frame, drawColor, npc.rotation, new Vector2(23, 32), npc.scale, SpriteEffects.None, 0);
                    return false;

                case NPCID.EaterofSouls:
                    spriteBatch.Draw(SoulEaterVariants[variant % 2].Value, npc.Center - screenPos, npc.frame, drawColor/*.MultiplyRGB(Color.Lerp(Color.White, new Color(0.85f, 0.8f, 0.9f), Math.Abs(MathF.Sin(variant * 0.1f))))*/, npc.rotation, new Vector2(25, 56), npc.scale, SpriteEffects.None, 0);
                    return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void Load()
        {
            On_Gore.NewGore_IEntitySource_Vector2_Vector2_int_float += ReplaceGore;
        }
        private int ReplaceGore(On_Gore.orig_NewGore_IEntitySource_Vector2_Vector2_int_float orig, Terraria.DataStructures.IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale)
        {
            if (source is EntitySource_Parent s && s.Entity is NPC npc && npc.ModNPC == null)
            {
                byte v = npc.GetGlobalNPC<VanillaNPCSpriteChanges>().variant;
                switch (npc.type)
                {
                    case NPCID.EaterofSouls:
                        if(v % 2 == 1)
                        {
                            if (Type == 14)
                                Type = Mod.Find<ModGore>("EaterOfSouls_1_Gore_0").Type;
                            else
                                Type = Mod.Find<ModGore>("EaterOfSouls_1_Gore_1").Type;
                        }
                        break;
                    case NPCID.UmbrellaSlime:
                        Type = Mod.Find<ModGore>("Umbrella_" + $"{v % Umbrella.Length}").Type;
                        break;
                    case NPCID.Slimer:
                        Type = Mod.Find<ModGore>("SlimerWing").Type;
                        break;
                }
            }
            return orig(source,Position,Velocity,Type,Scale);
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
            ProjectileID.Sets.TrailCacheLength[ProjectileID.SniperBullet] = 16;
            ProjectileID.Sets.TrailCacheLength[ProjectileID.CrystalBullet] = 12;
            ProjectileID.Sets.TrailCacheLength[ProjectileID.BulletHighVelocity] = 16;
            ProjectileID.Sets.TrailCacheLength[ProjectileID.MoonlordBullet] = 16;
        }
        public static void drawBullet(Projectile p, Color brightColor, Color darkColor, float scale = 1f)
        {
            drawBullet(p, brightColor, darkColor, Vector2.Zero, scale);
        }
        public static void drawBullet(Projectile p, Color brightColor, Color darkColor, Vector2 Offset, float scale = 1f)
        {
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, null, darkColor, p.rotation, tex.Size() / 2, scale, SpriteEffects.None);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[p.type] - 1; i++)
            {
                //Main.NewText(p.oldPos[7]);
                if (p.oldPos[i + 1] != Vector2.Zero)
                {
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition + Offset, new Rectangle(0, 8, 14, 2), darkColor * (1 - ((i - 1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale * p.scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                    Main.EntitySpriteDraw(tex.Value, p.oldPos[i] + (p.Size / 2) - Main.screenPosition + Offset, new Rectangle(0, 8, 14, 2), brightColor * (0.5f - ((i - 1) / (float)ProjectileID.Sets.TrailCacheLength[p.type])), p.oldPos[i].DirectionFrom(p.oldPos[i + 1]).ToRotation() + MathHelper.PiOver2, new Vector2(7, 0), new Vector2((1f - (i / (float)ProjectileID.Sets.TrailCacheLength[p.type])) * scale * p.scale, p.oldPos[i].Distance(p.oldPos[i + 1]) / 2), SpriteEffects.None);
                }
            }
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition + Offset, null, brightColor, p.rotation, tex.Size() / 2, scale * p.scale, SpriteEffects.None);
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
                    drawBullet(projectile, new Color(1f, 0.5f, 0.5f, 0f), new Color(0.8f, 0f, 0f, 0.7f));
                    break;
                case 89: // Crystal
                    drawBullet(projectile, projectile.whoAmI % 2 == 0 ? new Color(0.4f, 1f, 1f, 0f) : new Color(1f, 0.4f, 1f, 0f), projectile.whoAmI % 2 == 0 ? new Color(0.3f, 0.5f, 0.8f, 0.5f) : new Color(0.8f, 0.6f, 0.8f, 0.5f));
                    break;
                case 104: // Cursed
                    for(int i = 0; i < 2; i++)
                    {
                        drawBullet(projectile, new Color(0.9f, 1f, 0f, 0f), new Color(0f, 0.5f, 0f, 0f), Main.rand.NextVector2Circular(6,6), 0.5f);
                    }
                    drawBullet(projectile, new Color(0.9f, 1f, 0f, 0f), new Color(0f, 0.5f, 0f, 0f));
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
                          //drawBullet(projectile, new Color(255, 128, 255, 128), new Color(200, 0, 255, 128));
                    drawBullet(projectile, new Color(1f, 1f, 1f, 0f), Main.DiscoColor);
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
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            switch (projectile.type)
            {
                case ProjectileID.CrystalBullet:
                    ParticleSystem.AddParticle(new BigSparkle() { Scale = 0.6f, Rotation = Main.rand.NextFloat(-1f, 1f), TimeLeft = 30, fadeInTime = 10f, secondaryColor = Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()) }, projectile.Center, Vector2.Zero, Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()));
                    for (int e = 0; e < 5; e++)
                    {
                        ParticleSystem.AddParticle(new ImpactSparkle() { Scale = 0.6f, LifeTime = 30, secondaryColor = Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()) }, projectile.Center, Main.rand.NextVector2Circular(4, 4), Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()));
                    }
                    break;
            }
        }
    }
}
