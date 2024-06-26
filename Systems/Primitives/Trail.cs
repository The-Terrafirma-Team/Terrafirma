using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace Terrafirma.Systems.Primitives
{
    /// <summary>
    /// Class to create a trail based on a set of vector points
    /// </summary>
    public class Trail
    {
        public BasicEffect effect;
        private GraphicsDevice GraphicsDevice = Main.graphics.GraphicsDevice;
        public TrailSegment[] trailsegments;
        public Vector2[] pointarray;


        public Texture2D trailtexture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/GlowTrail").Value;

        public delegate float WidthDelegate(float trailpart);
        /// <summary>
        /// Set this to any float returning method to modify the trail's width per segment.
        /// </summary>
        public WidthDelegate widthmodifier = null;
        public float basewidth = 30f;

        public delegate Color ColorDelegate(float trailpart);
        /// <summary>
        /// Set this to any Color returning method to modify the trail's color per segment.
        /// </summary>
        public ColorDelegate color = null;


        public Trail(Vector2[] segmentpoints)
        {
            Main.RunOnMainThread(() =>
            {
                effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                effect.TextureEnabled = true;
                Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
            });

            pointarray = segmentpoints;

            trailsegments = new TrailSegment[segmentpoints.Length];
            for (int i = 0; i < segmentpoints.Length;  i++)
            {
                trailsegments[i] = new TrailSegment();
            }
        }

        public Trail(Vector2[] segmentpoints, WidthDelegate _widthmodifier)
        {
            widthmodifier = _widthmodifier; 

            Main.RunOnMainThread(() =>
            {
                effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                effect.TextureEnabled = true;
                Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
            });

            pointarray = segmentpoints;

            trailsegments = new TrailSegment[segmentpoints.Length];
            for (int i = 0; i < segmentpoints.Length; i++)
            {
                trailsegments[i] = new TrailSegment();
            }
        }

        public Trail(Vector2[] segmentpoints, WidthDelegate _widthmodifier, float _basewidth = 30f)
        {
            widthmodifier = _widthmodifier;
            basewidth = _basewidth;

            Main.RunOnMainThread(() =>
            {
                effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                effect.TextureEnabled = true;
                Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
            });

            pointarray = segmentpoints;

            trailsegments = new TrailSegment[segmentpoints.Length];
            for (int i = 0; i < segmentpoints.Length; i++)
            {
                trailsegments[i] = new TrailSegment();
            }
        }
        public void Draw(Vector2 position)
        {
            if (widthmodifier == null) widthmodifier = TrailWidth.FlatWidth;
            if (color == null) color = f => new Color(1f,1f,1f,0f);

            //Create vertices and indices
            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[]{};
            short[] indices = new short[]{};

            //Since we're starting from the left end of the trail, we need to shift it
            //to the left to make the right most part of the trail attach to the position instead
            position -= new Vector2((trailsegments.Length - 1) * 30, 0);

            for (int i = 0; i < trailsegments.Length; i++)
            {
                //If trailsegment has trailsegment in front of it, set it to follow that segment
                //Update trailsegment then add the two vertices from the segment to the main vertex array
                trailsegments[i].width = basewidth;
                if (i < trailsegments.Length - 1) 
                    trailsegments[i].UpdatePoint(pointarray[i], 
                    pointarray[i+1], 
                    1f - (i / (float)trailsegments.Length), 
                    widthmodifier(1f - (i  / (float)trailsegments.Length)),
                    color(1f - (i / (float)trailsegments.Length))
                    );
                else trailsegments[i].UpdatePoint(pointarray[i], 
                    pointarray[i], 
                    1f - (i / (float)trailsegments.Length), 
                    widthmodifier(1f - (i / (float)trailsegments.Length)),
                    color(1f - (i / (float)trailsegments.Length))
                    );

                vertices = vertices.Concat(trailsegments[i].vertices).ToArray();

                //Append Indices to draw two triangles, 0 > 2 > 3 / 0 > 3 > 1
                //Multiply by 2 with each new trailsegment to continue to the next two primitives
                //
                //  0       2   2       4
                //   ┌┐────┐     ┌┐────┐     
                //   │ \   │     │ \   │     
                //   │  \  │     │  \  │     
                //   │   \ │     │   \ │     
                //   └────└┘     └────└┘     
                //  1       3   3       5

                indices = indices.Append((short)(0 + i * 2)).ToArray();
                indices = indices.Append((short)(2 + i * 2)).ToArray();
                indices = indices.Append((short)(3 + i * 2)).ToArray();

                indices = indices.Append((short)(0 + i * 2)).ToArray();
                indices = indices.Append((short)(3 + i * 2)).ToArray();
                indices = indices.Append((short)(1 + i * 2)).ToArray();
            }

            //Base primitive drawing code
            GraphicsDevice.Textures[0] = trailtexture;

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

    public class TrailSegment
    {
        public float width = 40;
        public Vector2 center = Vector2.Zero;
        public float rotation = 0f;

        public VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[] { };

        //Update the vertex points for this trail segment
        public void UpdatePoint(Vector2 position, Vector2 nextposition, float segmentpiece, float segmentwidthmultiplier, Color color)
        {
            center = position;
            float rot = position.DirectionTo(nextposition).ToRotation();

            //Create two points next to the center point
            Vector2 upperpoint = new Vector2(0, -width / 2 * segmentwidthmultiplier).RotatedBy(rot);
            Vector2 lowerpoint = new Vector2(0, width / 2 * segmentwidthmultiplier).RotatedBy(rot);

            vertices = new VertexPositionColorTexture[]
            {
              new VertexPositionColorTexture(
                    new Vector3(center + upperpoint, 0f),
                    color,
                    new Vector2(segmentpiece, 0f)),
                new VertexPositionColorTexture(
                    new Vector3(center + lowerpoint, 0f),
                    color,
                    new Vector2(segmentpiece, 1f))
            };
        }

    }

    public static class TrailWidth
    {
        public static float FlatWidth(float trailpart)
        {
            return 1f;
        }

        public static float SpikeWidth(float trailpart)
        {
            return trailpart;
        }

        public static float WobblyWidth(float trailpart)
        {
            return (float)((Math.Sin(trailpart * 60f) + 1f) / 4f) + 0.25f;
        }
    }

    public static class TrailColor
    {

    }
}
