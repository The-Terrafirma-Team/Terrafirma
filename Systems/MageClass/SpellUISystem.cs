using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Audio;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    public class SpellUISystem : ModSystem
    {
        internal SpellUI spellui;
        private UserInterface spellwheel;

        public Spell SelectedSpell;
        public int Index;

        public override void Load()
        {
            spellui = new SpellUI();
            spellui.Activate();
            spellwheel = new UserInterface();
            spellwheel.SetState(spellui);
        }

        public void Hide()
        {
            spellwheel?.SetState(null);
        }

        public void Show()
        {
            spellwheel?.SetState(spellui);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (spellwheel?.CurrentState != null)
            {
                spellwheel?.Update(gameTime);
            }
        }

        public void Flush()
        {
            spellui?.Flush();
        }

        public void Create( int item, Player player)
        {
            spellui?.Create(item, player);
        }

        public void UpdateMana (float manacost)
        {
            spellui.manacost = manacost;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: Spell Wheel",
                    delegate
                    {
                        if (spellwheel?.CurrentState != null)
                        {
                            spellwheel.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
