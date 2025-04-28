using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Particles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.Primitives
{
    public static class TFTrailSystem
    {
        public static List<TFTrail> trails = new List<TFTrail>() { };
        public static List<Action> trailDrawActions { get; } = new();
        public static void DrawTrails(bool pixellate)
        {
            foreach(Action action in trailDrawActions)
            {
                action.Invoke();
            }

            //Main.NewText(trails.Count);
            //for (int i = 0; i < trails.Count; i++)
            //{
            //    if (trails[i].pixellate == pixellate)  trails[i].Draw();
            //}

        }

        //Queues the Trail to draw this frame
        public static void QueueDraw( this TFTrail trail, Vector2[] points)
        {
            //TFTrailSystem.trails.Add(trail);
            TFTrailSystem.trailDrawActions.Add(
                () => { trail.Draw(points); }
                );
        }

    }
    
    public abstract class TFTrail : ModType, IDisposable
    {
        public BasicEffect effect;
        private GraphicsDevice GraphicsDevice;

        public Vector2[] points = new Vector2[]{ };

        public bool pixellate = true;
        public Texture2D texture = null;
        public Vector2 textureRepeat = Vector2.One;
        public bool flipTextureX = false;
        public bool flipTextureY = false;

        private VertexPositionColorTexture[] vertices;
        private short[] indices;

        public TFTrail()
        {
            if (Main.netMode == NetmodeID.Server) return;

            GraphicsDevice GraphicsDevice = Main.graphics.GraphicsDevice;

            Main.RunOnMainThread(() =>
            {
                effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                effect.TextureEnabled = true;
            });
        }

        public virtual float Width(float segment)
        {
            return 1.0f;
        }

        public virtual Vector2 TrailOffset(float segment, float segmentRotation)
        {
            return Vector2.Zero;
        }

        public virtual Vector2 TextureOffset(float segment)
        {
            return Vector2.Zero;
        }

        public virtual Color TrailColor(float segment)
        {
            return Color.White;
        }

        public override void Unload()
        {
            Dispose();
        }

        public bool isDisposed = false;
        public event EventHandler<EventArgs> Disposing;
        public void Dispose()
        {

            texture?.Dispose();
            texture = null;
            effect?.Dispose();
            effect = null;
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing && this.Disposing != null)
                {
                    this.Disposing(this, EventArgs.Empty);
                }

                isDisposed = true;
            }
        }

        //If you want to have effects like pixellation please use QueueDraw instead :)
        public void Draw(Vector2[] Points)
        {           
            points = Points;

            if (Main.netMode == NetmodeID.Server || points.Length <= 1 || isDisposed)
            {
                return;
            }

            GraphicsDevice = Main.instance.GraphicsDevice;
            vertices = new VertexPositionColorTexture[] {};
            indices = new short[] {};

            //Set Indices and Vertices
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X == 0f && points[i].Y == 0f) continue;

                //Vector2 screenDiff = new Vector2(((Main.screenWidth * Main.GameZoomTarget) - Main.screenWidth), ((Main.screenHeight * Main.GameZoomTarget) - Main.screenHeight));
                float pixelScaling = pixellate ? 2 * Main.GameZoomTarget : 1 * Main.GameZoomTarget;

                Vector2 pixelScaledPoint = (points[i] - Main.screenPosition + ((Main.ScreenSize.ToVector2() / 2f) * (Main.GameZoomTarget - 1f))) / pixelScaling;
                TFTrailSegment segment = new TFTrailSegment(Main.screenPosition + pixelScaledPoint);

                //Rotation
                if (i == 0) segment.rotation = points[0].AngleTo(points[1]);
                //else if (i > 0 && i < points.Length - 1) segment.rotation = (points[i].AngleTo(points[i - 1]) - MathHelper.Pi + points[i].AngleTo(points[i + 1])) / 2f;
                else segment.rotation = points[i].AngleTo(points[i - 1]) - MathHelper.Pi;

                //Width
                segment.width = Width(i / ((float)points.Length - 1)) / pixelScaling;

                //Color
                Color segmentColor;
                segmentColor = TrailColor(i / ((float)points.Length - 1));

                //Texture Position
                Vector2 textureOffset;
                textureOffset = TextureOffset(i / ((float)points.Length - 1)) / pixelScaling;

                //Segment Position
                Vector2 segmentOffset;
                segmentOffset = TrailOffset(i / ((float)points.Length - 1), segment.rotation) / pixelScaling;

                segment.SetPoints();

                Vector2 TopTexCoord = new Vector2( (flipTextureX? 1f - (i / ((float)points.Length / textureRepeat.X)) : (i / ((float)points.Length / textureRepeat.X))) + textureOffset.X, 
                    (flipTextureY? (0.5f + textureRepeat.Y / 2f) : (0.5f - textureRepeat.Y / 2f)) + textureOffset.Y);
                Vector2 BottomTextCoord = new Vector2( (flipTextureX ? 1f - (i / ((float)points.Length / textureRepeat.X)) : (i / ((float)points.Length / textureRepeat.X))) + textureOffset.X,
                    (flipTextureY ? (0.5f - textureRepeat.Y / 2f) : (0.5f + textureRepeat.Y / 2f)) + textureOffset.Y);

                vertices = vertices.Append(
                    new VertexPositionColorTexture(
                        new Vector3(segment.topPoint + segmentOffset, 0f), 
                        segmentColor,
                        TopTexCoord)
                    ).ToArray();
                vertices = vertices.Append(
                    new VertexPositionColorTexture(
                        new Vector3(segment.bottomPoint + segmentOffset, 0f), 
                        segmentColor,
                        BottomTextCoord)
                    ).ToArray();

                if (i > 0)
                {
                    indices = indices.Append((short)(0 + (i - 1) * 2)).ToArray();
                    indices = indices.Append((short)(2 + (i - 1) * 2)).ToArray();
                    indices = indices.Append((short)(3 + (i - 1) * 2)).ToArray();

                    indices = indices.Append((short)(0 + (i - 1) * 2)).ToArray();
                    indices = indices.Append((short)(3 + (i - 1) * 2)).ToArray();
                    indices = indices.Append((short)(1 + (i - 1) * 2)).ToArray();
                }

            }


            //Prevent non-existend Trails from trying to read or write protected memory :)
            if (indices.Length <= 0 || vertices.Length <= 0)
            {
                return;
            }


            //Final           

            Viewport viewport = GraphicsDevice.Viewport;
            effect.World = Matrix.CreateTranslation(new Vector3(-Main.screenPosition, 0));
            effect.View = Main.GameViewMatrix.TransformationMatrix;
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, -1, 10);

            effect.TextureEnabled = true;
            effect.Texture = texture == null? TextureAssets.MagicPixel.Value : texture;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
                //GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
            }


        }

        protected override void Register()
        {
            ModTypeLookup<TFTrail>.Register(this);
        }
    }
    
    public class TFTrailSegment
    {
        public Vector2 position;
        public Vector2 topPoint;
        public Vector2 bottomPoint;

        public float width = 50f;

        public float rotation = 0f;

        public TFTrailSegment(Vector2 pos)
        {
            position = pos;
            SetPoints();
        }
        public void SetPoints()
        {
                topPoint = position + new Vector2(0f, -width / 2).RotatedBy(rotation);
                bottomPoint = position + new Vector2(0f, width / 2).RotatedBy(rotation);
        }
    }
}
