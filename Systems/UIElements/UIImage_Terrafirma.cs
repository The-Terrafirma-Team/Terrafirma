using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;

namespace Terrafirma.Systems.UIElements
{
    public class UIImage_Terrafirma : UIElement
    {
        public Texture2D texture;
        public Rectangle texturebounds;
        public float ImageScale = 1f;
        public float Rotation;
        public bool ScaleToFit;
        public bool AllowResizingDimensions = true;
        public Color Color = Color.White;
        public Vector2 NormalizedOrigin = Vector2.Zero;
        public bool RemoveFloatingPointsFromDrawPosition;
        private Texture2D _nonReloadingTexture;

        public UIImage_Terrafirma(Texture2D texture)
        {
            SetImage(texture);
        }

        public void SetImage(Texture2D texture)
        {
            this.texture = texture;
            texturebounds = texture.Bounds;
            if (AllowResizingDimensions)
            {
                Width.Set(this.texture.Width, 0f);
                Height.Set(this.texture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Texture2D texture2D = null;
            if (texture != null)
                texture2D = texture;

            if (_nonReloadingTexture != null)
                texture2D = _nonReloadingTexture;

            if (ScaleToFit)
            {
                spriteBatch.Draw(texture2D, dimensions.ToRectangle(), Color);
                return;
            }

            Vector2 vector = texture2D.Size();
            Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
            if (RemoveFloatingPointsFromDrawPosition)
                vector2 = vector2.Floor();

            spriteBatch.Draw(texture2D, vector2, texturebounds, Color, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
        }
    }
}
