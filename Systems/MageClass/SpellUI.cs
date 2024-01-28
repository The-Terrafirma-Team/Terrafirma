using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TerrafirmaRedux.Global.Structs;
using Terraria.GameContent.UI.Elements;
using TerrafirmaRedux.Global;
using System.Collections.Generic;

namespace TerrafirmaRedux.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class SpellUI : UIState
    {

        UIText Title;
        UIText Description;
        UIText ManaCost;
        UIPanel TextPanel;
        public float manacost;
        public void Flush()
        {
            RemoveAllChildren();
        }

        public void Create(int weapon, Player player, bool accessoriesincluded = true)
        {
            Flush();
            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(weapon))
            {
                int SpellAmount = ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon].Length;
                int WeaponSpellAmount = ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon].Length;
                int AccessorySpellAmount = 0;

                if (accessoriesincluded)
                {
                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                                SpellAmount += ModContent.GetInstance<SpellIndex>().ItemCatalogue[accessory].Length;
                                AccessorySpellAmount += ModContent.GetInstance<SpellIndex>().ItemCatalogue[accessory].Length;
                            }
                        }

                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];

                                for (int j = 0; j < ModContent.GetInstance<SpellIndex>().ItemCatalogue[accessory].Length; j++)
                                {
                                    SpellButton accessoryspellicon = new SpellButton();
                                    accessoryspellicon.angle = (360 / SpellAmount) * j;
                                    accessoryspellicon.anglespace = 360 / SpellAmount;
                                    accessoryspellicon.icon = ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[accessory][j]].Item2;
                                    accessoryspellicon.Index = new int[2] { j, accessory };
                                    accessoryspellicon.Accessory = 1;
                                    accessoryspellicon.SelectedSpell = ModContent.GetInstance<SpellIndex>().ItemCatalogue[accessory][j];
                                    Append(accessoryspellicon);
                                }
                                
                            }
                        }
                }

                for (int i = 0; i < ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon].Length; i++)
                {
                    SpellButton spellicon = new SpellButton();
                    spellicon.angle = (360 / SpellAmount) * (i + AccessorySpellAmount);
                    spellicon.anglespace = 360 / SpellAmount;
                    spellicon.icon = ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon][i]].Item2;
                    spellicon.Index = new int[2] { i, weapon };
                    spellicon.SelectedSpell = ModContent.GetInstance<SpellIndex>().ItemCatalogue[weapon][i];
                    spellicon.Accessory = 0;
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

            ManaCost = new UIText("", 1f, false);
            ManaCost.SetText("");
            ManaCost.HAlign = 0.5f;
            ManaCost.Top.Set(50, 0);

            TextPanel = new UIPanel();
            TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
            TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + ManaCost.MinHeight.Pixels + 40, 0);

            TextPanel.HAlign = 0.5f;
            TextPanel.VAlign = 0.7f;

            TextPanel.Append(ManaCost);
            TextPanel.Append(Title);
            TextPanel.Append(Description);
            
            Append(TextPanel);

        }
        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<SpellIndex>().SpellCatalogue.ContainsKey(ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]))
            {
                if (ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]].Item5 > 0)
                {
                    ManaCost.SetText("Costs " + (int)(ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]].Item5 * manacost) + " Mana");
                }
                else
                {
                    ManaCost.SetText("");
                }
                Title.SetText(ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]].Item4);
                Description.SetText(ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]].Item3);
                
                TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
                TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + ManaCost.MinHeight.Pixels + 40, 0);
            }
            base.Update(gameTime);
        }

    }
}
