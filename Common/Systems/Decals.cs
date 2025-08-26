using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Terrafirma.Common.Systems
{
    public class WorldMaskTarget : ARenderTargetContentByRequest
    {
        protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            PrepareARenderTarget_AndListenToEvents(ref _target, device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, RenderTargetUsage.PlatformContents);
            GraphicsDevice graphics = Main.instance.GraphicsDevice;
            var oldTargets = graphics.GetRenderTargets();
            graphics.Clear(Color.White);
            graphics.SetRenderTarget(_target);
            spriteBatch.Begin();
            graphics.Clear(Color.Transparent);
            Main.spriteBatch.Draw(Main.instance.tileTarget, Main.sceneTilePos - Main.screenPosition, Color.Black);
            Main.spriteBatch.Draw(Main.instance.blackTarget, Main.sceneTilePos - Main.screenPosition, Color.Black);
            spriteBatch.End();
            graphics.SetRenderTargets(oldTargets);
            _wasPrepared = true;
        }
    }
    public class DecalsRenderTarget : ARenderTargetContentByRequest
    {
        protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            PrepareARenderTarget_AndListenToEvents(ref _target, device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, RenderTargetUsage.PlatformContents);
            GraphicsDevice graphics = Main.instance.GraphicsDevice;
            var oldTargets = graphics.GetRenderTargets();
            graphics.Clear(Color.White);
            graphics.SetRenderTarget(_target);
            spriteBatch.Begin();
            graphics.Clear(Color.Transparent);
            spriteBatch.End();

            graphics.SetRenderTargets(oldTargets);
            _wasPrepared = true;
        }
    }
    public class DecalsSystem : ModSystem
    {
        public static WorldMaskTarget MaskTarget;
        public static DecalsRenderTarget DecalsTarget;
        public static Asset<Effect> MaskShader;
        public override void Load()
        {
            MaskShader = Mod.Assets.Request<Effect>("Assets/Effects/MaskShader", AssetRequestMode.ImmediateLoad);
            Main.ContentThatNeedsRenderTargets.Add(MaskTarget = new WorldMaskTarget());
            Main.ContentThatNeedsRenderTargets.Add(DecalsTarget = new DecalsRenderTarget());
        }
        public override void Unload()
        {
            Main.ContentThatNeedsRenderTargets.Remove(DecalsTarget);
            Main.ContentThatNeedsRenderTargets.Remove(MaskTarget);
        }
        public override void PostDrawTiles()
        {
            MaskTarget.Request();
            DecalsTarget.Request();

            if (!DecalsTarget.IsReady || !MaskTarget.IsReady)
                return;

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, MaskShader.Value, Main.Transform);
            Main.spriteBatch.GraphicsDevice.Textures[1] = MaskTarget.GetTarget();
            Main.spriteBatch.Draw(DecalsTarget.GetTarget(), Main.screenLastPosition - Main.screenPosition, Color.White);
            Main.spriteBatch.End();
        }
    }
}
