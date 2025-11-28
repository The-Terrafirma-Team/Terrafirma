using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
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
            //Main.spriteBatch.Draw(Main.instance.blackTarget, Main.sceneTilePos - Main.screenPosition, Color.Black);
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
            for(int i = 0; i < DecalsSystem.decals.Count; i++)
            {
                DecalsSystem.decals[i].Draw(spriteBatch,Main.screenPosition);
            }
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

        internal static List<Particle> decals = new();
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
            decals.Clear();
        }
        public override void PostUpdateDusts()
        {
            for (int i = 0; i < decals.Count; i++)
            {
                decals[i].Update();
                decals[i].TimeInWorld++;
                if (decals[i].Active == false)
                {
                    decals.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// They're literally just particles but you can't see them if they're not over a tile.
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="position"></param>
        public static void NewDecal(Particle particle, Vector2 position)
        {
            particle.Position = position;
            decals.Add(particle);
            particle.OnSpawn();
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
