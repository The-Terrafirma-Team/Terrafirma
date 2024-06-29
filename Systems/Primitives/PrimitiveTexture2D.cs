using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;

namespace Terrafirma.Systems.Primitives
{
    public class PrimitiveTexture2D
    {

        public BasicEffect effect;
        private GraphicsDevice GraphicsDevice = Main.graphics.GraphicsDevice;

        public Texture2D texture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/GlowTrail").Value;

        public PrimitiveTexture2D()
        {
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = true;
            Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
        }
        public void Draw(Vector2 position)
        {

            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[]
            {
                new VertexPositionColorTexture(new Vector3(position + new Vector2(0, 0), 0f), Color.White, new Vector2(0f,0f)),
                new VertexPositionColorTexture(new Vector3(position + new Vector2(100, 0), 0f), Color.White, new Vector2(1f,0f)),
                new VertexPositionColorTexture(new Vector3(position + new Vector2(100, 100), 0f), Color.White, new Vector2(1f,1f)),
                new VertexPositionColorTexture(new Vector3(position + new Vector2(0, 100), 0f), Color.White, new Vector2(0f,1f))
            };

            short[] indices = new short[] { 0, 1, 2, 0, 2, 3 };

            GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;

            Viewport viewport = GraphicsDevice.Viewport;
            effect.World = Matrix.CreateTranslation(new Vector3(-Main.screenPosition, 0));
            effect.View = Main.GameViewMatrix.TransformationMatrix;
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, -1, 10);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
            }
        }
    }
}
