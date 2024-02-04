using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Terrafirma.Global;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma
{
	public class Terrafirma : Mod
	{
        public static Terrafirma Mod { get; private set; } = ModContent.GetInstance<Terrafirma>();
        public const string AssetPath = "Terrafirma/Assets/";
        public override void Load()
        {
            GameShaders.Misc["Terrafirma:FlameShader"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>($"{Mod.Name}" + "/Effects/FlameShader", AssetRequestMode.ImmediateLoad).Value),"Effect").UseShaderSpecificData(new Vector4());
            
            On_Player.UpdateMaxTurrets += On_Player_UpdateMaxTurrets;
        }

        private void On_Player_UpdateMaxTurrets(On_Player.orig_UpdateMaxTurrets orig, Player player)
        {
            List<Projectile> list = new List<Projectile>();
            float usedslots = 0f;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].WipableTurret)
                {
                    list.Add(Main.projectile[i]);
                    usedslots += Main.projectile[i].GetGlobalProjectile<SentryChanges>().SentrySlots;
                }
            }
            int num = 0;
            while (usedslots > player.maxTurrets && ++num < Main.projectile.Length)
            {
                Projectile projectile = list[0];
                for (int j = 1; j < list.Count; j++)
                {
                    if (list[j].timeLeft < projectile.timeLeft && list[j].GetGlobalProjectile<SentryChanges>().Priority == false)
                    {
                        projectile = list[j];
                    }
                }
                projectile.Kill();
                usedslots -= projectile.GetGlobalProjectile<SentryChanges>().SentrySlots;
                list.Remove(projectile);
            }
        }

        List<int> BulletList = new List<int>();
        public int[] BulletArray = new int[] {};
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



    }
}