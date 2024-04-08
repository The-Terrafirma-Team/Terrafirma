using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terrafirma.Global.Structs;
using Terraria.GameContent.UI.Elements;
using System.Collections.Generic;
using System;
using System.Linq;
using Terrafirma.Global.Items;

namespace Terrafirma.Systems.MageClass
{
    internal class SpellUI : UIState
    {

        UIText Title;
        UIText Description;
        UIText ManaCost;
        UIPanel TextPanel;
        public float manacost;

        int effecttimer;
        float sizefloat;
        public bool UIOpen = false;
        public Spell[] SpellList = new Spell[]{};
        public void Flush()
        {
            RemoveAllChildren();
        }

        /// <summary>
        /// Creates Spell UI
        /// </summary>
        public void Create(int weapon, bool accessoriesincluded = true)
        {
            Player player = Main.LocalPlayer;
            effecttimer = 0;
            sizefloat = 0f;
            Flush();
            if (SpellIndex.ItemCatalogue.ContainsKey(weapon))
            {
                UIOpen = true;
                SpellList = new Spell[]{};

                if (accessoriesincluded)
                {
                        //Add all Accessory Spells to Spell List
                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (SpellIndex.ItemCatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                                for (int j = 0; j < SpellIndex.ItemCatalogue[accessory].Length; j++)
                                {
                                    if (!SpellList.Contains(SpellIndex.ItemCatalogue[accessory][j]))
                                    {
                                        SpellList = SpellList.Append(SpellIndex.ItemCatalogue[accessory][j]).ToArray();
                                    }
                                }

                            }
                        }
                }

                //Add all Weapon Spells to Spell list
                for (int i = 0; i < SpellIndex.ItemCatalogue[weapon].Length; i++)
                {
                    if (!SpellList.Contains(SpellIndex.ItemCatalogue[weapon][i]))
                    {
                        SpellList = SpellList.Append(SpellIndex.ItemCatalogue[weapon][i]).ToArray();
                    }

                }

                //Create Spell Buttons
                for (int i = 0; i < SpellList.Length; i++)
                {
                    SpellButton spellicon = new SpellButton();
                    spellicon.angle = (360 / SpellList.Length) * i;
                    spellicon.anglespace = 360 / SpellList.Length;
                    spellicon.icon = SpellList[i].TexurePath;
                    spellicon.Index = i;
                    spellicon.SelectedSpell = SpellList[i];
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

            //Description Stuff
            if (SpellIndex.SpellID.ContainsSpell(ModContent.GetInstance<SpellUISystem>().SelectedSpell))
            {
                if (ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost > 0)
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
            if (UIOpen)
            {
                Texture2D UICircle = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle").Value;
                Texture2D UICircle2 = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle3").Value;
                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(UICircle2, new Vector2(Main.screenWidth, Main.screenHeight) / 2, UICircle2.Bounds, new Color(47, 215 / (i + 1), 237 / (i + 1), 0) * (0.5f / i), ((float)Main.timeForVisualEffects / (40f - (i * 5f))), UICircle2.Size() / 2, MathHelper.Lerp(0f, 0.85f, sizefloat) - (i / 10f), SpriteEffects.None, 0f);
                }
            }
            base.Draw(spriteBatch);
        }

    }
}
