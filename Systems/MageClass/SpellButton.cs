using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class SpellButton : UIElement
    {
        internal Texture2D spellicon;

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
            Texture2D iconglow = (Texture2D)ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellIconGlow");
            if (icon == ""){ spellicon = (Texture2D)ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/PlaceholderSpellIcon"); }
            else { spellicon = (Texture2D)ModContent.Request<Texture2D>(icon); }
           
            spriteBatch.Draw(iconglow, position, new Rectangle(0, 0, iconglow.Width, iconglow.Height), new Color(47, 215, 237, 0) * MathHelper.Lerp(0f, dist, timer) * 0.5f, 0, iconglow.Size() / 2, MathHelper.Lerp(0.5f, dist, timer) * 0.9f, SpriteEffects.None, 0);
            spriteBatch.Draw(spellicon, position, new Rectangle(0, 0, spellicon.Width, spellicon.Height), new Color(1, 1, 1, postimer), 0, spellicon.Size() / 2, MathHelper.Lerp(0.5f, dist, timer) * 1.1f, SpriteEffects.None, 0);
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
                ModContent.GetInstance<SpellUISystem>().Index = Index;
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
                ModContent.GetInstance<SpellUISystem>().Index = Index;
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
