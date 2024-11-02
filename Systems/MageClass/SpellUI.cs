using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using System;
using System.Linq;
using Terrafirma.Common;
using Terrafirma.Systems.AccessorySynergy;
using Terraria.ID;

namespace Terrafirma.Systems.MageClass
{
    public class SpellUI : UIState
    {
        static ClientConfig clientConfig = ModContent.GetInstance<ClientConfig>();

        UIText Title;
        UIText Description;
        UIText ManaCost;
        UIPanel TextPanel;
        public float manacost;
        int itemid;

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
            itemid = weapon;
            Player player = Main.LocalPlayer;
            effecttimer = 0;
            sizefloat = 0f;
            Flush();
            if (SpellID.itemcatalogue.ContainsKey(weapon))
            {
                UIOpen = true;
                SpellList = new Spell[]{};

                if (accessoriesincluded)
                {
                        //Add all Accessory Spells to Spell List
                        for (int i = 0; i < player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                        {
                            if (SpellID.itemcatalogue.ContainsKey(player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                            {
                                int accessory = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                                for (int j = 0; j < SpellID.itemcatalogue[accessory].Length; j++)
                                {
                                    if (!SpellList.Contains(SpellID.itemcatalogue[accessory][j]))
                                    {
                                        SpellList = SpellList.Append(SpellID.itemcatalogue[accessory][j]).ToArray();
                                    }
                                }

                            }
                        }
                }

                //Add all Weapon Spells to Spell list
                for (int i = 0; i < SpellID.itemcatalogue[weapon].Length; i++)
                {
                    if (!SpellList.Contains(SpellID.itemcatalogue[weapon][i]))
                    {
                        SpellList = SpellList.Append(SpellID.itemcatalogue[weapon][i]).ToArray();
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
            Title.Top.Set(30, 0);

            Description = new UIText("", 0.55f, true);
            Description.SetText("");
            Description.HAlign = 0.5f;
            Description.Top.Set(0, 0);

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
            if (SpellID.spells.ContainsSpell(ModContent.GetInstance<SpellUISystem>().SelectedSpell))
            {

                int spellmanacost = ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost == -1 ? Main.LocalPlayer.HeldItem.mana : ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost;

                if (ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost != 0)
                {
                    ManaCost.SetText("Costs " + (int)(spellmanacost * Main.LocalPlayer.manaCost) + " Mana");
                }
                else
                {
                    ManaCost.SetText("");
                }
                Title.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellDesc());
                Description.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellName());

                string DescriptionString = ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellDesc().ReplaceLineEndings("\n");
                int LineEndingsInt = DescriptionString.Count(f => f == '\n');
                Description.MinHeight.Pixels = 28 + 20 * LineEndingsInt;
                ManaCost.Top.Set(55 + 25 * LineEndingsInt, 0);

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
            if ( SpellID.spells.ContainsSpell(ModContent.GetInstance<SpellUISystem>().SelectedSpell) )
            {

                int spellmanacost = ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost == -1 ? Main.LocalPlayer.HeldItem.mana : ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost;

                if ( ModContent.GetInstance<SpellUISystem>().SelectedSpell.ManaCost != 0)
                {
                    ManaCost.SetText("Costs " + ((int)(spellmanacost * Main.LocalPlayer.manaCost)).ToString() + " Mana");
                }
                else
                {
                    ManaCost.SetText("");
                }
                Title.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellDesc());
                Description.SetText(ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellName());

                string DescriptionString = ModContent.GetInstance<SpellUISystem>().SelectedSpell.GetSpellDesc().ReplaceLineEndings("\n");
                int LineEndingsInt = DescriptionString.Count(f => f == '\n');
                Description.MinHeight.Pixels = 28 + 20 * LineEndingsInt;
                ManaCost.Top.Set(55 + 25 * LineEndingsInt, 0);

                TextPanel.Width.Set(Description.MinWidth.Pixels > Title.MinWidth.Pixels * 1.1f ? Description.MinWidth.Pixels : Title.MinWidth.Pixels * 1.1f + 20, 0);
                TextPanel.Height.Set(Description.MinHeight.Pixels + Title.MinHeight.Pixels + ManaCost.MinHeight.Pixels + 40, 0);
            }

            effecttimer++;
            if (effecttimer < 40) sizefloat = (float)Math.Sin(effecttimer / 23f);

            base.Update(gameTime);
        }

        public static Color ringColor
        {
            get 
            {
                Color color = new Color(clientConfig.SpellR, clientConfig.SpellG, clientConfig.SpellB);
                if (Main.LocalPlayer.hasRainbowCursor)
                    color = Main.DiscoColor;
                return color;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (UIOpen)
            {
                //Texture2D UICircle = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle").Value;
                Texture2D UICircle2 = ModContent.Request<Texture2D>("Terrafirma/Systems/MageClass/SpellIcons/SpellUICircle3").Value;
                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(UICircle2, new Vector2(Main.screenWidth, Main.screenHeight) / 2, UICircle2.Bounds, new Color(ringColor.R, ringColor.G / (i + 1), ringColor.B / (i + 1), 0) * (0.5f / i), ((float)Main.timeForVisualEffects / (40f - (i * 5f))), UICircle2.Size() / 2, MathHelper.Lerp(0f, 0.85f, sizefloat) - (i / 10f), SpriteEffects.None, 0f);
                }
            }
            base.Draw(spriteBatch);
        }

    }
}
