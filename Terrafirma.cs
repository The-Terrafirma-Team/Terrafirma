using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Utils;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using Terrafirma.Common;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma
{
    public class Terrafirma : Mod
	{
        public static string SetBonusKey
        {
            get { return Main.setKey == 1 ? "UP" : "DOWN"; }
        }
        public static Terrafirma Mod { get; private set; } = ModContent.GetInstance<Terrafirma>();
        public const string AssetPath = "Terrafirma/Assets/";
        public static Asset<Texture2D> QuestButtonBG;
        public static Asset<Texture2D> QuestButtonBGBorder;
        public static Asset<Texture2D> QuestDifficultyStarEmpty;
        public static Asset<Texture2D> QuestDifficultyStarFull;

        //public static RenderTarget2D pixelRenderTarget;
        public override void Load()
        {
            //Main.RunOnMainThread(() =>
            //{
            //    pixelRenderTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2);
            //});

            QuestButtonBG = ModContent.Request<Texture2D>("Terrafirma/Systems/NewNPCQuests/QuestButtonPanel");
            QuestButtonBGBorder = ModContent.Request<Texture2D>("Terrafirma/Systems/NewNPCQuests/QuestButtonPanelBorder");
            QuestDifficultyStarEmpty = ModContent.Request<Texture2D>("Terrafirma/Assets/QuestDifficultyStarEmpty");
            QuestDifficultyStarFull = ModContent.Request<Texture2D>("Terrafirma/Assets/QuestDifficultyStarFull");

            On_Player.UpdateMaxTurrets += On_Player_UpdateMaxTurrets;
        }

        public override void Unload()
        {
            //Main.RunOnMainThread(() =>
            //{
            //    pixelRenderTarget.Dispose();
            //});
        }
        private void On_Player_UpdateMaxTurrets(On_Player.orig_UpdateMaxTurrets orig, Player player)
        {

            List<Projectile> list = new List<Projectile>();
            float usedslots = 0f;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].WipableTurret && Main.projectile[i].active)
                {
                    list.Add(Main.projectile[i]);
                    usedslots += Main.projectile[i].GetGlobalProjectile<SentryStats>().SentrySlots;
                }
            }
            while (usedslots > player.maxTurrets)
            {
                Projectile projectile = null;
                for (int j = 0; j < list.Count; j++)
                {
                    if (projectile == null && !list[j].GetGlobalProjectile<SentryStats>().Priority && list[j].GetGlobalProjectile<SentryStats>().SentrySlots > 0f)
                    {
                        projectile = list[j];
                    }
                    else if (projectile != null && list[j].timeLeft < projectile.timeLeft && !list[j].GetGlobalProjectile<SentryStats>().Priority && list[j].GetGlobalProjectile<SentryStats>().SentrySlots > 0f)
                    {
                        projectile = list[j];
                    }
                }
                list.Remove(projectile);
                projectile.Kill();
                usedslots -= projectile.GetGlobalProjectile<SentryStats>().SentrySlots;
            }

        }

        List<int> BulletList = new List<int>();
        public static int[] BulletArray = new int[] {};
        public override void PostSetupContent()
        {
            Item newitem = new Item();

            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                newitem.SetDefaults(i);
                if (newitem.ammo == AmmoID.Bullet)
                {
                    BulletList.Add(i);
                }
            }

            BulletArray = BulletList.ToArray();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) => Network.HandlePacket(reader, whoAmI);
        
        
    }
}