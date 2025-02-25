using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Systems.Primitives
{
    internal class DrawPrimitives : ModSystem
    {
        public static RenderTarget2D primitiveRenderTarget;
        public static RenderTarget2D pixelPrimitiveRenderTarget;
        private GraphicsDevice GraphicsDevice;
        public override void Unload()
        {
            primitiveRenderTarget.Dispose();
            //pixelPrimitiveRenderTarget.Dispose();
        }
        public override void Load()
        {
            On_Main.CheckMonoliths += DrawPrimTarget;
            On_Main.DrawProjectiles += Main_OnPreDraw;
            Main.QueueMainThreadAction(SetRenderTarget);
            Main.instance.GraphicsDevice.DeviceReset += GraphicsDevice_DeviceReset;
        }

        private void Main_OnPreDraw(On_Main.orig_DrawProjectiles orig, Main self)
        {
            orig.Invoke(self);

            if (primitiveRenderTarget == null || primitiveRenderTarget.IsDisposed) return;

            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null, Main.Transform * Matrix.CreateScale(1f));
            Main.spriteBatch.Draw(primitiveRenderTarget, new Rectangle(0, 1, Main.screenWidth,Main.screenHeight), Color.White);
            Main.spriteBatch.End();

            if (pixelPrimitiveRenderTarget == null || pixelPrimitiveRenderTarget.IsDisposed) return;

            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null, Main.Transform * Matrix.CreateScale(1f));
            Main.spriteBatch.Draw(pixelPrimitiveRenderTarget, new Rectangle(0, 1, Main.screenWidth, Main.screenHeight), Color.White);
            Main.spriteBatch.End();

            SetRenderTarget();
        }

        public void SetRenderTarget()
        {
            pixelPrimitiveRenderTarget?.Dispose();
            primitiveRenderTarget?.Dispose();
            primitiveRenderTarget = new RenderTarget2D(
                Main.instance.GraphicsDevice,
                Main.screenWidth,
                Main.screenHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.PreserveContents);           
            pixelPrimitiveRenderTarget = new RenderTarget2D(
                Main.instance.GraphicsDevice,
                Main.screenWidth / 2,
                Main.screenHeight / 2,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.PreserveContents);
        }

        private void GraphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            primitiveRenderTarget.Dispose();
            SetRenderTarget();
        }

        public void DrawPrimTarget(On_Main.orig_CheckMonoliths orig)
        {
            orig.Invoke();

            GraphicsDevice = Main.graphics.GraphicsDevice;
            var oldTargets = GraphicsDevice.GetRenderTargets();          

            GraphicsDevice.SetRenderTarget(primitiveRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            //Put all Primitive Draw methods here
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null, Matrix.Identity);
            TFTrailSystem.DrawTrails(false);         
            Main.spriteBatch.End();

            GraphicsDevice.SetRenderTarget(pixelPrimitiveRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            //Put all Pixel Primitive Draw methods here
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null, Matrix.Identity);
            TFTrailSystem.DrawTrails(true);
            Main.spriteBatch.End();

            GraphicsDevice.SetRenderTargets(oldTargets);
            TFTrailSystem.trails.Clear();
        }
    }
}
