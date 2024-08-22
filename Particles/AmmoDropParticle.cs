using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ID;
using System.Linq;
using System.Collections.Generic;
using Terrafirma.Items.Ammo;
using Terrafirma.Common.Interfaces;

namespace Terrafirma.Particles
{
    public class AmmoDropParticle : Particle
    {
        public int timeleft = 40;
        public float scale = 1f;
        public float rotation = 0f;
        public float opacity = 1f;
        /// <summary>
        /// If not null, the particle will use the Item's texture as its base texture. Keep this as null if you want to use a custom texture instead
        /// </summary>
        public int? itemID = null;
        public Asset<Texture2D> AmmoTex = null;

        /// <summary>
        /// List for all ammo types that are allowed to spawn
        /// </summary>
        public List<int> alloweditemIDs = new List<int>{
            ModContent.ItemType<Buckshot>(), ModContent.ItemType<Birdshot>(), ModContent.ItemType<ExplosiveBuckshot>(), ModContent.ItemType<BouncyBuckshot>(),
            ModContent.ItemType<GraniteBullet>(), ItemID.HighVelocityBullet, ModContent.ItemType<HallowPointBullet>()
        };


        public override void OnSpawn()
        {
            if (itemID != null && alloweditemIDs.Contains((int)itemID))
            {

                if (new Item((int)itemID).ModItem is ITFBullet item) AmmoTex = item.casingTexture;
                else AmmoTex = TextureAssets.Item[(int)itemID];

                switch (itemID)
                {
                    case ItemID.HighVelocityBullet: AmmoTex = ModContent.Request<Texture2D>("Terrafirma/Assets/BulletCasings/HighVelocityBulletCasing"); break;
                }
            }
            base.OnSpawn();
        }
        public override void Update()
        {

             velocity.X *= 0.97f;
            velocity.Y = Math.Clamp(velocity.Y + 0.2f, -1000f, 15f);
            rotation += velocity.Length() / 10f;

            position += velocity;

            if (TimeInWorld > timeleft) Active = false;
            if (timeleft - TimeInWorld < 10) opacity -= 0.1f;

            if (Collision.SolidCollision(position - new Vector2(1) + velocity, 2, 2))
            {
                Vector2 mhm = Collision.AnyCollision(position - new Vector2(1), velocity, 2, 2);
                if (mhm.X != velocity.X)
                    velocity.X *= -1;
                if (mhm.X != velocity.Y)
                    velocity.Y *= -0.7f;
            }

            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (AmmoTex == null || timeleft <= 0) return;

            spriteBatch.Draw(AmmoTex.Value, 
                position - Main.screenPosition,
                AmmoTex.Value.Bounds, 
                Lighting.GetColor(position.ToTileCoordinates()) * opacity,
                rotation,
                AmmoTex.Size() / 2,
                scale * 0.7f, 
                SpriteEffects.None, 
                0);
        }
    }
}
