using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Terrafirma.Global.Items;
using Terraria;
using Terraria.Graphics.Effects;
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
            Ref<Effect> vaporref = new Ref<Effect>(ModContent.Request<Effect>($"{Mod.Name}" + "/Effects/CookingPotVaporShader", AssetRequestMode.ImmediateLoad).Value);
            Filters.Scene["Terrafirma:VaporShader"] = new Filter(new ScreenShaderData(vaporref, "VaporShader"), EffectPriority.Low);

            On_Player.UpdateMaxTurrets += On_Player_UpdateMaxTurrets;
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