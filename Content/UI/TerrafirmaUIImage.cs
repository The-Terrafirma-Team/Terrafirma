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

namespace Terrafirma.Content.UI
{
    internal class TerrafirmaUIImage : UIElement
    {
        private Asset<Texture2D> _texture;
        private Texture2D _nonReloadingTexture;

        public float ImageScale = 1f;
        public float Rotation;
        public bool ScaleToFit;
        public bool AllowResizingDimensions = true;
        public Color Color = Color.White;
        public Vector2 NormalizedOrigin = Vector2.Zero;
        public bool RemoveFloatingPointsFromDrawPosition;

        //Terrafirma specific Vars
        private Asset<Texture2D> _overlayTexture;
        private Texture2D _nonReloadingOverlayTexture;

        public Rectangle? frame = null;
        /// <summary>
        /// If true, the overlay will use overlayFrame instead
        /// </summary>
        public bool customOverlayFrame = false;
        public Rectangle? overlayFrame = null;
        public bool showOverlay = true;
        public Object customData;

        public TerrafirmaUIImage(Asset<Texture2D> texture, Asset<Texture2D> overlayTexture = null, bool customOverFrame = false)
        {
            SetImage(texture, overlayTexture);
            customOverlayFrame = customOverFrame;
        }

        public TerrafirmaUIImage(Texture2D nonReloadingTexture, Texture2D nonReloadingOverlayTexture = null, bool customOverFrame = false)
        {
            SetImage(nonReloadingTexture, nonReloadingOverlayTexture);
            customOverlayFrame = customOverFrame;
        }

        public void SetImage(Asset<Texture2D> texture, Asset<Texture2D> overlayTexture)
        {
            _texture = texture;
            _nonReloadingTexture = null;
            _overlayTexture = overlayTexture;
            _nonReloadingOverlayTexture = null;

            if (AllowResizingDimensions)
            {
                Width.Set(_texture.Width(), 0f);
                Height.Set(_texture.Height(), 0f);
            }
        }

        public void SetImage(Texture2D nonReloadingTexture, Texture2D nonReloadingOverlayTexture = null)
        {
            _texture = null;
            _nonReloadingTexture = nonReloadingTexture;
            _overlayTexture = null;
            _nonReloadingOverlayTexture = nonReloadingOverlayTexture;

            if (AllowResizingDimensions)
            {
                Width.Set(_nonReloadingTexture.Width, 0f);
                Height.Set(_nonReloadingTexture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Texture2D texture2D = null;
            Texture2D overlayTexture2D = null;
            if (_texture != null) texture2D = _texture.Value;
            if (_nonReloadingTexture != null) texture2D = _nonReloadingTexture;
            if (_overlayTexture != null) overlayTexture2D = _overlayTexture.Value;
            if (_nonReloadingOverlayTexture != null) overlayTexture2D = _nonReloadingOverlayTexture;

            if (ScaleToFit)
            {
                spriteBatch.Draw(texture2D, dimensions.ToRectangle(), frame, Color);
                if (overlayTexture2D != null && showOverlay) spriteBatch.Draw(overlayTexture2D, dimensions.ToRectangle(), customOverlayFrame? overlayFrame : frame, Color);
                return;
            }

            Vector2 vector = texture2D.Size();
            Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
            if (RemoveFloatingPointsFromDrawPosition)
                vector2 = vector2.Floor();

            spriteBatch.Draw(texture2D, vector2, frame, Color, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
            if (overlayTexture2D != null && showOverlay) spriteBatch.Draw(overlayTexture2D, vector2, customOverlayFrame ? overlayFrame : frame, Color, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
        }
    }
}
