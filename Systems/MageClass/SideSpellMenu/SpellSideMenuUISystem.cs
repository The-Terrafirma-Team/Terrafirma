using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Audio;
using Terrafirma.Systems.MageClass.SideSpellMenu;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    public class SpellSideMenuUISystem : ModSystem
    {
        internal SpellSideMenuUI spellsideui;
        private UserInterface spellsidemenu;

        public Spell selectedspell;
        public Item spellitem { get => spellsideui.selecteditem; }

        public override void Load()
        {
            spellsideui = new SpellSideMenuUI();
            spellsideui.Activate();
            spellsidemenu = new UserInterface();
            spellsidemenu.SetState(spellsideui);
        }

        public void Hide()
        {
            spellsidemenu?.SetState(null);
        }

        public void Show()
        {
            spellsidemenu?.SetState(spellsideui);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (spellsidemenu?.CurrentState != null)
            {
                spellsidemenu?.Update(gameTime);
            }
        }

        public void Flush()
        {
            spellsideui.Flush();
        }

        public void Create(Item item)
        {
            spellsideui.Create(item);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: Spell Side Menu",
                    delegate
                    {
                        if (spellsidemenu?.CurrentState != null)
                        {
                            spellsidemenu.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
