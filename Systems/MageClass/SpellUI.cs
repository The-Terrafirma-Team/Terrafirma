using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terrafirma.Global.Structs;
using Terraria.GameContent.UI.Elements;
using Terrafirma.Global;
using System.Collections.Generic;
using System;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class SpellUI : UIState
    {

        UIText Title;
        UIText Description;
        UIText ManaCost;
        UIPanel TextPanel;
        public float manacost;

        int effecttimer;
        float sizefloat;
        public void Flush()
        {
            RemoveAllChildren();
        }

        /// <summary>
        /// Creates Spell UI
        /// </summary>
        public void Create(int weapon, Player player, bool accessoriesincluded = true)
        {
            effecttimer = 0;
            sizefloat = 0f;
            Flush();
            if (SpellIndex.ItemCatalogue.ContainsKey(weapon))
            {
                int SpellAmount = SpellIndex.ItemCatalogue[weapon].Length;
                int WeaponSpellAmount = SpellIndex.ItemCatalogue[weapon].Length;
                int AccessorySpellAmount = 0;

                
                if (accessoriesincluded)
                {
                        //Check all accessory spells so the indexes match later
                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (SpellIndex.ItemCatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                                SpellAmount += SpellIndex.ItemCatalogue[accessory].Length;
                                AccessorySpellAmount += SpellIndex.ItemCatalogue[accessory].Length;
                            }
                        }

                        //Create Spell Buttons for Accessory spells
                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (SpellIndex.ItemCatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];

                                for (int j = 0; j < SpellIndex.ItemCatalogue[accessory].Length; j++)
                                {
                                    SpellButton accessoryspellicon = new SpellButton();
                                    accessoryspellicon.angle = (360 / SpellAmount) * j;
                                    accessoryspellicon.anglespace = 360 / SpellAmount;
                                    accessoryspellicon.icon = SpellIndex.ItemCatalogue[accessory][j].TexurePath;
                                    accessoryspellicon.Index = j;
                                    accessoryspellicon.SelectedSpell = SpellIndex.ItemCatalogue[accessory][j];
                                    Append(accessoryspellicon);
                                    
                                }
                                
                            }
                        }
                }

                //Create Spell Buttons for Weapon spells
                for (int i = 0; i < SpellIndex.ItemCatalogue[weapon].Length; i++)
                {
                    SpellButton spellicon = new SpellButton();
                    spellicon.angle = (360 / SpellAmount) * (i + AccessorySpellAmount);
                    spellicon.anglespace = 360 / SpellAmount;
                    spellicon.icon = SpellIndex.ItemCatalogue[weapon][i].TexurePath;
                    spellicon.Index = i;
                    spellicon.SelectedSpell = SpellIndex.ItemCatalogue[weapon][i];
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
            //Description Stuff
            if ( SpellIndex.SpellID.ContainsSpell(ModContent.GetInstance<SpellUISystem>().SelectedSpell) )
            {
                if ( ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost > 0)
                {
                    ManaCost.SetText("Costs " + (int)(ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost * manacost) + " Mana");
                }
                else
                {
                    ManaCost.SetText("");
                }
                Title.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellDesc());
                Description.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellName());
                
                TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
                TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + ManaCost.MinHeight.Pixels + 40, 0);
            }

            effecttimer++;
            if (effecttimer < 40) sizefloat = (float)Math.Sin(effecttimer / 23f);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D UICircle = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle").Value;
            Texture2D UICircle2 = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle3").Value;
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(UICircle2, new Vector2(Main.screenWidth, Main.screenHeight) / 2, UICircle2.Bounds, new Color(47, 215 / (i+1), 237 / (i+1), 0) * (0.5f / i), ((float)Main.timeForVisualEffects / (40f - (i * 5f)) ), UICircle2.Size() / 2, MathHelper.Lerp(0f, 0.85f, sizefloat) - (i / 10f), SpriteEffects.None, 0f);
            }
            
            base.Draw(spriteBatch);
        }

    }
}
