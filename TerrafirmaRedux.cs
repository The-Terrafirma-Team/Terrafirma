using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.Graphics.Shaders;
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
    }
}