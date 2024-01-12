using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux
{
	public class TerrafirmaRedux : Mod
	{
        public static TerrafirmaRedux Mod { get; private set; } = ModContent.GetInstance<TerrafirmaRedux>();
        public const string AssetPath = "TerrafirmaRedux/Assets/";
        public override void Load()
        {
            GameShaders.Misc["Terrafirma:FlameShader"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>($"{Mod.Name}" + "/Effects/FlameShader", AssetRequestMode.ImmediateLoad).Value),"Effect").UseShaderSpecificData(new Vector4());
        
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