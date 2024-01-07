using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TerrafirmaRedux.Global.Structs;
using Terraria.GameContent.UI.Elements;

namespace TerrafirmaRedux.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class SpellUI : UIState
    {
        UIText Title;
        UIText Description;
        UIPanel TextPanel;
        public void Flush()
        {
            RemoveAllChildren();
        }

        public void Create(int weapon)
        {
            Flush();
            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(weapon))
            {
                int SpellAmount = ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon].Length;

                for (int i = 0; i < SpellAmount; i++)
                {
                    SpellButton spellicon = new SpellButton();
                    spellicon.angle = (360 / SpellAmount) * i;
                    spellicon.anglespace = 360 / SpellAmount;
                    spellicon.icon = ModContent.GetInstance<SpellIndex>().SpellCatalogue[ ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon][i] ].Item2;
                    spellicon.SelectedSpell = ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon][i];
                    Append(spellicon);
                }

            }

            Title = new UIText("", 1f, false);
            Title.SetText("");
            Title.HAlign = 0.5f;
            Title.Top.Set(10, 0);

            Description = new UIText("", 1.2f, false);
            Description.SetText("");
            Description.HAlign = 0.5f;
            Title.Top.Set(30, 0);

            TextPanel = new UIPanel();
            TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
            TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + 40, 0);

            TextPanel.HAlign = 0.5f;
            TextPanel.VAlign = 0.7f;

            TextPanel.Append(Title);
            TextPanel.Append(Description);
            
            Append(TextPanel);

        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<SpellIndex>().SpellDescription.ContainsKey(ModContent.GetInstance<SpellUISystem>().SelectedSpell))
            {
                Title.SetText(ModContent.GetInstance<SpellIndex>().SpellDescription[ModContent.GetInstance<SpellUISystem>().SelectedSpell].Item2);
                Description.SetText(ModContent.GetInstance<SpellIndex>().SpellDescription[ModContent.GetInstance<SpellUISystem>().SelectedSpell].Item1);
                TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
                TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + 40, 0);
            }
            base.Update(gameTime);
        }

    }
}
