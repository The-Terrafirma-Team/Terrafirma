using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Ambience
{
    public struct Ember
    {
        private Vector2 Position;
        private Vector2 Velocity;
        private int Frame;
        private float Parallax;
        private float Rotation;

        public static float Opacity = 0f;
        public void Initialize()
        {
            Position.X = Main.rand.NextFloat(0, Main.LogicCheckScreenWidth);
            Position.Y = Main.rand.NextFloat(0, Main.LogicCheckScreenHeight);
            Frame = Main.rand.Next(3);
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Parallax = Main.rand.NextFloat(0.5f, 1f);
            Velocity = Main.rand.NextVector2Circular(2,2);
        }
        public void Update() 
        {
            Velocity += Main.rand.NextVector2Circular(0.4f, 0.4f);
            Velocity.Y -= 0.02f * Parallax;
            Velocity.X += Main.windSpeedCurrent * 0.06f;

            //Velocity = Velocity.RotatedByRandom(0.4f);
            if(Velocity.Y > 0.2f)
            {
                Velocity.Y = 0.2f;
            }

            Position += Velocity;
            Rotation += Velocity.X * 0.1f;

            if(Velocity.Length() > 2)
            {
                Velocity = Vector2.Normalize(Velocity) * 2;
            }

            Position += (Main.screenLastPosition - Main.screenPosition) * (Parallax * 2);
            if (Position.X > Main.LogicCheckScreenWidth)
            {
                Position.X -= Main.LogicCheckScreenWidth;
            }
            else if (Position.X < 0)
            {
                Position.X += Main.LogicCheckScreenWidth;
            }
            if (Position.Y > Main.LogicCheckScreenHeight)
            {
                Position.Y -= Main.LogicCheckScreenHeight;
            }
            else if (Position.Y < 0)
            {
                Position.Y += Main.LogicCheckScreenHeight;
            }
        }
        private void DrawEmber(Vector2 drawPosition)
        {
            Main.spriteBatch.Draw(TextureAssets.Dust.Value, drawPosition, new Rectangle(60, 10 * Frame, 10, 10), new Color(1f, 1f, 1f, 0f) * Velocity.Length() * 0.5f * Opacity, Rotation, new Vector2(4), Parallax, SpriteEffects.None, 0);
        }
        public void Draw()
        {
            Vector2 drawPosition = Position;
            drawPosition.X = drawPosition.X % Main.LogicCheckScreenWidth;
            drawPosition.Y = drawPosition.Y % Main.LogicCheckScreenHeight;
            drawPosition += new Vector2(Main.screenWidth,Main.screenHeight) / 4;
            DrawEmber(drawPosition);
        }
    }
    public class EmberEffect : ModSystem
    {
        private Ember[] Embers;
        public override void Load()
        {
            Embers = new Ember[45];
            for (int i = 0; i < Embers.Length; i++)
            {
                Embers[i].Initialize();
            }
        }

        public override void PostDrawTiles()
        {
            if (Ember.Opacity <= 0)
            {
                return;
            }
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            for (int i = 0; i < Embers.Length; i++)
            {
                Embers[i].Draw();
            }
            Main.spriteBatch.End();
        }
        public override void PostUpdateDusts()
        {
            if ((Main.LocalPlayer.position.Y > (Main.UnderworldLayer - 40) * 16 || Main.LocalPlayer.ZoneMeteor) && ModContent.GetInstance<ClientConfig>().EnableAmbientParticles)
            {
                Ember.Opacity += 0.02f;
            }
            else
            {
                Ember.Opacity -= 0.01f;
            }
            Ember.Opacity = MathHelper.Clamp(Ember.Opacity,0,1);

            for (int i = 0; i < Embers.Length; i++)
            {
                Embers[i].Update();
            }
        }
    }
}
