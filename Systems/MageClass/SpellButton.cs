﻿using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terrafirma.Common;

namespace Terrafirma.Systems.MageClass
{
    internal class SpellButton : UIElement
    {
        internal Texture2D spellicon;
        ClientConfig clientConfig = ModContent.GetInstance<ClientConfig>();

        public float angle = 0;
        public float anglespace = 90;
        public Vector2 position = Vector2.Zero;
        public string icon = "";

        public int Index;
        public Spell SelectedSpell;

        internal float timer;
        internal float postimer = 0.1f;

        internal float dist;
        internal bool IconSelect = false;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            Texture2D iconglow = (Texture2D)ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellIconGlow");
            Texture2D iconBorder = (Texture2D)ModContent.Request<Texture2D>(Terrafirma.AssetPath + "SpellIconBorders");
            if (icon == ""){ spellicon = (Texture2D)ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/PlaceholderSpellIcon"); }
            else { spellicon = (Texture2D)ModContent.Request<Texture2D>(icon); }
           
            int bordercount = iconBorder.Width / iconBorder.Height;
            spriteBatch.Draw(iconglow, position, new Rectangle(0, 0, iconglow.Width, iconglow.Height), new Color(SpellUI.ringColor.R, SpellUI.ringColor.G, SpellUI.ringColor.B, 0) * MathHelper.Lerp(0f, dist, timer) * 0.5f, 0, iconglow.Size() / 2, MathHelper.Lerp(0.5f, dist, timer) * 0.9f, SpriteEffects.None, 0);
            spriteBatch.Draw(spellicon, position, new Rectangle(0, 0, spellicon.Width, spellicon.Height), new Color(1, 1, 1, postimer), 0, spellicon.Size() / 2, MathHelper.Lerp(0.5f, dist, timer) * 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(iconBorder, position, new Rectangle((iconBorder.Width / bordercount) * clientConfig.SpellBorder, 0, iconBorder.Height, iconBorder.Height), new Color(1, 1, 1, postimer), 0, new Vector2(iconBorder.Height / 2), MathHelper.Lerp(0.5f, dist, timer) * 0.999f, SpriteEffects.None, 0);
            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
        }

        public override void Update(GameTime gameTime)
        {
            position = new Vector2(MathHelper.Lerp(0, 80, Math.Clamp(postimer, 0f, 1f)) , 0).RotatedBy(angle * (Math.PI / 180));
            position = new Vector2((Main.screenWidth / 2) + position.X, (Main.screenHeight / 2) + position.Y);
            postimer *= 1 + (1- postimer) / 5;

            float mouseangle = (Main.ScreenSize.ToVector2() /2 - Main.MouseScreen).ToRotation() + (float)Math.PI;
            if (Math.Abs((angle * (Math.PI / 180)) - mouseangle) < (anglespace * (Math.PI / 180)) / 2)
            {
                if (!IconSelect)
                {
                    IconSelect = true;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
                dist = 1f;
                ModContent.GetInstance<SpellUISystem>().SelectedSpell = SelectedSpell;
                timer = Math.Clamp(timer *= 1 + (1 - timer) / 10, 0.5f, 1f);
            }
            else if (Math.Abs((angle * (Math.PI / 180)) - (mouseangle - Math.PI*2)) < (anglespace * (Math.PI / 180)) / 2)
            {
                if (!IconSelect)
                {
                    IconSelect = true;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
                dist = 1f;
                ModContent.GetInstance<SpellUISystem>().SelectedSpell = SelectedSpell;
                timer = Math.Clamp(timer *= 1 + (1 - timer) / 10, 0.5f, 1f);
            }
            else
            {
                IconSelect = false;
                dist = 0.5f;
                timer = 0f;
            }
            

        }


    }
}
