using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ID;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terrafirma.Systems.UIElements;
using Terrafirma.Common;
using Terrafirma.Common.Items;
using Terrafirma.Common.NPCs;
using Terrafirma.Systems.AccessorySynergy;

namespace Terrafirma.Systems.MageClass.SideSpellMenu
{
    public class SpellSideMenuUI : UIState
    {
        public Item selecteditem = new Item(0);
        int selectedspellID = 0;
        int iconshift = 0;

        public Vector2 UIOffset = new Vector2(1400, 0);

        bool spellchangeswitch = true;

        UIImage_Terrafirma spellicon;
        UIImage_Terrafirma[] spelliconlist = new UIImage_Terrafirma[]{};

        UIText Spellname;

        UIText SpellKeyBind1;
        UIText SpellKeyBind2;

        ClientConfig clientConfig = ModContent.GetInstance<ClientConfig>();


        public void Flush()
        {
            RemoveAllChildren();
            spelliconlist = new UIImage_Terrafirma[] { };
            selectedspellID = 0;
            iconshift = 0;
        }
        public void Create(Item item)
        {
            if (!SpellID.itemcatalogue.ContainsKey(item.type)) return;

            selecteditem = item;
            selectedspellID = 0;
            iconshift = 0;
            UIOffset = new Vector2(1400, 0);

            for (int i = 0; i < SpellID.itemcatalogue[item.type].Length; i++)
            {

                spellicon = new UIImage_Terrafirma(ModContent.Request<Texture2D>(SpellID.itemcatalogue[item.type][i].TexurePath, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                spellicon.HAlign = 0f;
                spellicon.VAlign = 0.03f;
                spellicon.Top.Pixels = UIOffset.Y;
                spellicon.Left.Pixels = UIOffset.X;
                Append(spellicon);

                spelliconlist = spelliconlist.Append(spellicon).ToArray();

            }

            for (int i = 0; i < Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
            {
                if (SpellID.itemcatalogue.ContainsKey(Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                {
                    int accessory = Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                    for (int j = 0; j < SpellID.itemcatalogue[accessory].Length; j++)
                    {
                        spellicon = new UIImage_Terrafirma(ModContent.Request<Texture2D>(SpellID.itemcatalogue[Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]][j].TexurePath, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                        spellicon.HAlign = 0f;
                        spellicon.VAlign = 0.03f;
                        spellicon.Top.Pixels = UIOffset.Y;
                        spellicon.Left.Pixels = UIOffset.X;
                        Append(spellicon);

                        spelliconlist = spelliconlist.Append(spellicon).ToArray();
                    }
                }
            }

            Spellname = new UIText("");
            Spellname.HAlign = 0f;
            Spellname.VAlign = 0.03f;
            Spellname.Top.Pixels = UIOffset.Y + 30;
            Spellname.Left.Pixels = UIOffset.X;
            Append(Spellname);

            SpellKeyBind1 = new UIText("");
            SpellKeyBind1.HAlign = 0f;
            SpellKeyBind1.VAlign = 0.03f;
            SpellKeyBind1.Top.Pixels = UIOffset.Y + 5;
            SpellKeyBind1.Left.Pixels = UIOffset.X;
            Append(SpellKeyBind1);

            SpellKeyBind2 = new UIText("");
            SpellKeyBind2.HAlign = 0f;
            SpellKeyBind2.VAlign = 0.03f;
            SpellKeyBind2.Top.Pixels = UIOffset.Y + 5;
            SpellKeyBind2.Left.Pixels = UIOffset.X;
            Append(SpellKeyBind2);

        }

        public override void Update(GameTime gameTime)
        {
            if (SpellID.GetMaxSpellsforWeaponwithAccessory(selecteditem.type) != spelliconlist.Length) 
            { 
                Flush();
                Create(selecteditem);
            }

            if (SpellID.itemcatalogue.ContainsKey(selecteditem.type) && selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell != null  && selectedspellID != SpellID.GetWeaponSpellIndexWithAccessory(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell, selecteditem.type))
            {
                spellchangeswitch = false;
            }

            if (!spellchangeswitch)
            {
                if (selectedspellID == 0 && SpellID.GetWeaponSpellIndexWithAccessory(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell, selecteditem.type) == spelliconlist.Length - 1)
                {
                    iconshift--;
                }
                else if (selectedspellID == spelliconlist.Length - 1 && SpellID.GetWeaponSpellIndexWithAccessory(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell, selecteditem.type) == 0)
                {
                    iconshift++;
                }
                else
                {
                    iconshift += SpellID.GetWeaponSpellIndexWithAccessory(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell, selecteditem.type) - selectedspellID;
                }
            }

            if (SpellID.itemcatalogue.ContainsKey(selecteditem.type) && selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell != null) 
            { 
                selectedspellID = SpellID.GetWeaponSpellIndexWithAccessory(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell, selecteditem.type);
                spellchangeswitch = true;
            }

            for (int i = 0; i < spelliconlist.Length; i++)
            {
                spelliconlist[i].Left.Pixels = MathHelper.Lerp(spelliconlist[i].Left.Pixels , UIOffset.X + (i - iconshift) * 48, 0.2f);
                spelliconlist[i].Top.Pixels = UIOffset.Y;
            }

            //Spell Title Name
            if (SpellID.itemcatalogue.ContainsKey(selecteditem.type) && selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                Spellname.SetText(selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell.GetSpellName());
                Spellname.Left.Pixels = UIOffset.X - FontAssets.MouseText.Value.MeasureString(Spellname.Text).X / 2;
                Spellname.Top.Pixels = UIOffset.Y + 30;

                if (Keybinds.previousSpell.GetAssignedKeys().Count > 0)
                {
                    SpellKeyBind1.SetText(TFUtils.NicenUpKeybindNameIfApplicable(Keybinds.previousSpell.GetAssignedKeys()[0]), Math.Clamp(1f - FontAssets.MouseText.Value.MeasureString(SpellKeyBind1.Text).X / 200f, 0f, 1f), false);
                    SpellKeyBind1.Left.Pixels = UIOffset.X - 38 - (FontAssets.MouseText.Value.MeasureString(SpellKeyBind1.Text).X / 2);
                    SpellKeyBind1.Top.Pixels = UIOffset.Y + 5;
                }
                if (Keybinds.nextSpell.GetAssignedKeys().Count > 0)
                {
                    SpellKeyBind2.SetText(TFUtils.NicenUpKeybindNameIfApplicable(Keybinds.nextSpell.GetAssignedKeys()[0]), Math.Clamp(1f - FontAssets.MouseText.Value.MeasureString(SpellKeyBind1.Text).X / 200f, 0f, 1f), false);
                    SpellKeyBind2.Left.Pixels = UIOffset.X + 60 - FontAssets.MouseText.Value.MeasureString(SpellKeyBind2.Text).X / 2;
                    SpellKeyBind2.Top.Pixels = UIOffset.Y + 5;
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            UIOffset = new Vector2(ModContent.GetInstance<ClientConfig>().ExtraSpellUiPosition.X * (Main.ViewSize / new Vector2(1920, 1080)).X * Main.GameZoomTarget,
                                   ModContent.GetInstance<ClientConfig>().ExtraSpellUiPosition.Y * (Main.ViewSize / new Vector2(1920, 1080)).Y * Main.GameZoomTarget);

            if (!SpellID.itemcatalogue.ContainsKey(selecteditem.type) || selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell == null || spelliconlist.Length == 0) return;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            if (SpellID.itemcatalogue.ContainsKey(selecteditem.type) && selecteditem.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                for (int k = -1 + (iconshift / spelliconlist.Length); k <= 1 + (iconshift / spelliconlist.Length); k++)
                {
                    for (int i = 0; i < spelliconlist.Length; i++)
                    {
                        Texture2D iconBorder = (Texture2D)ModContent.Request<Texture2D>(Terrafirma.AssetPath + "SpellIconBorders");
                        Vector2 position = new Vector2(spelliconlist[i].HAlign * Main.ViewSize.X, spelliconlist[i].VAlign * Main.ViewSize.Y) + new Vector2(spelliconlist[i].Left.Pixels, spelliconlist[i].Top.Pixels);
                        position.X += (48 * spelliconlist.Length) * k;
                        float transparencyfloat = 1f - Math.Abs((spelliconlist[i].HAlign * Main.ViewSize.X + UIOffset.X) - position.X) / 90f;

                        if (transparencyfloat > 0.1f)
                        {
                            if ((iconshift + spelliconlist.Length * 10000) % spelliconlist.Length == i)
                            {
                                int borderCount = iconBorder.Width / iconBorder.Height;
                                //Icon
                                spriteBatch.Draw(spelliconlist[i].texture,
                                    position,
                                    spelliconlist[i].texturebounds,
                                    spelliconlist[i].Color * transparencyfloat,
                                    spelliconlist[i].Rotation,
                                    spelliconlist[i].texture.Size() / 2,
                                    spelliconlist[i].ImageScale * Math.Clamp(transparencyfloat * 1.5f, 0f, 1f),
                                    SpriteEffects.None,
                                    0f);
                                //Border
                                spriteBatch.Draw(iconBorder,
                                    position,
                                    new Rectangle((iconBorder.Width / borderCount) * clientConfig.SpellBorder, 0, iconBorder.Width / borderCount, iconBorder.Height),
                                    spelliconlist[i].Color * transparencyfloat,
                                    spelliconlist[i].Rotation,
                                    new Vector2(iconBorder.Width / borderCount, iconBorder.Height) / 2,
                                    spelliconlist[i].ImageScale * Math.Clamp(transparencyfloat * 1.5f, 0f, 1f),
                                    SpriteEffects.None,
                                    0f);
                            }
                            else
                            {
                                int borderCount = iconBorder.Width / iconBorder.Height;
                                //Icon
                                spriteBatch.Draw(spelliconlist[i].texture,
                                    position,
                                    spelliconlist[i].texturebounds,
                                    spelliconlist[i].Color * 0.7f * transparencyfloat,
                                    spelliconlist[i].Rotation,
                                    spelliconlist[i].texture.Size() / 2,
                                    spelliconlist[i].ImageScale * Math.Clamp(transparencyfloat * 1.5f, 0f, 1f),
                                    SpriteEffects.None,
                                    0f);
                                //Border
                                spriteBatch.Draw(iconBorder,
                                    position,
                                    new Rectangle((iconBorder.Width / borderCount) * clientConfig.SpellBorder, 0, iconBorder.Width / borderCount, iconBorder.Height),
                                    spelliconlist[i].Color * 0.7f * transparencyfloat,
                                    spelliconlist[i].Rotation,
                                    new Vector2(iconBorder.Width / borderCount, iconBorder.Height) / 2,
                                    spelliconlist[i].ImageScale * Math.Clamp(transparencyfloat * 1.5f, 0f, 1f),
                                    SpriteEffects.None,
                                    0f);
                            }
                        }
                    }
                }
            }

            Spellname.Draw(spriteBatch);
            SpellKeyBind1.Draw(spriteBatch);
            SpellKeyBind2.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
        }
    }
}
