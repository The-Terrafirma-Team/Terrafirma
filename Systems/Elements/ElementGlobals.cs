using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terrafirma.Data;
using Terrafirma.Systems.Elements.Beastiary;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace Terrafirma.Systems.Elements
{
    public class ElementGlobals
    {
        public class ElementPlayer : ModPlayer
        {
            public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
            {
                modifiers.HideCombatText();
                float mod = ElementData.getElementalBonus(proj.GetElementProjectile().elementData, target.GetElementNPC().elementData);
                modifiers.FinalDamage *= mod;
            }
            public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
            {
                modifiers.HideCombatText();
                float mod = ElementData.getElementalBonus(item.GetElementItem().elementData, target.GetElementNPC().elementData);
                modifiers.FinalDamage *= mod;
            }
            public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
            {
                float mod = ElementData.getElementalBonus(item.GetElementItem().elementData, target.GetElementNPC().elementData);
                DamageNumber(target, mod, hit, damageDone);
            }
            public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
            {
                float mod = ElementData.getElementalBonus(proj.GetElementProjectile().elementData, target.GetElementNPC().elementData);
                DamageNumber(target, mod, hit, damageDone);
            }
            private void DamageNumber(NPC target, float Mod, NPC.HitInfo hit, int damageDone)
            {
                Color color = hit.Crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;

                color = Color.Lerp(color, (Mod > 1) ? Color.Red : Color.Gray, MathF.Abs(Mod - 1) / (Mod > 1 ? 2 : 1));

                CombatText.NewText(target.Hitbox, color,damageDone,hit.Crit);
            }
        }
        public class ElementItem : GlobalItem
        {
            public ElementData elementData = new ElementData();
            private static Asset<Texture2D> elementIcons;
            public override bool InstancePerEntity => true;
            public override void OnCreated(Item item, ItemCreationContext context)
            {
                elementData = new ElementData();
                base.OnCreated(item, context);
            }
            public override void SetDefaults(Item item)
            {

                if(item.ModItem == null)
                {
                    if (AddElementsToVanillaContent.fireItem.Contains(item.type))
                        elementData.Fire = true;
                    if (AddElementsToVanillaContent.waterItem.Contains(item.type))
                        elementData.Water = true;
                    if (AddElementsToVanillaContent.earthItem.Contains(item.type))
                        elementData.Earth = true;
                    if (AddElementsToVanillaContent.airItem.Contains(item.type))
                        elementData.Air = true;
                    if (AddElementsToVanillaContent.lightItem.Contains(item.type))
                        elementData.Light = true;
                    if (AddElementsToVanillaContent.darkItem.Contains(item.type))
                        elementData.Dark = true;
                    if (AddElementsToVanillaContent.iceItem.Contains(item.type))
                        elementData.Ice = true;
                    if (AddElementsToVanillaContent.poisonItem.Contains(item.type))
                        elementData.Poison = true;
                    if (AddElementsToVanillaContent.electricItem.Contains(item.type))
                        elementData.Electric = true;
                    if (AddElementsToVanillaContent.arcaneItem.Contains(item.type))
                        elementData.Arcane = true;
                }
            }
            public override void SetStaticDefaults()
            {
                elementIcons = ModContent.Request<Texture2D>("Terrafirma/Assets/ElementIcons");
            }
            public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
            {
                if (!elementData.Typeless)
                {
                    TooltipLine elementLine = new TooltipLine(Mod, "elementLine", "a");
                    tooltips.Add(elementLine);
                }
            }
            public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
            {
                if (line.Name == "elementLine")
                {
                    int xOffset = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 0 && elementData.Arcane
                            || i == 1 && elementData.Fire
                            || i == 2 && elementData.Water
                            || i == 3 && elementData.Earth
                            || i == 4 && elementData.Air
                            || i == 5 && elementData.Light
                            || i == 6 && elementData.Dark
                            || i == 7 && elementData.Ice
                            || i == 8 && elementData.Poison
                            || i == 9 && elementData.Electric) // The scary block
                        {
                            Main.spriteBatch.Draw(elementIcons.Value, new Vector2(line.X + xOffset, line.Y - 2), new Rectangle(i * 26, 0, 26, 24), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                            xOffset += 26;
                        }
                    }
                    return false;
                }
                return base.PreDrawTooltipLine(item, line, ref yOffset);
            }
        }
        public class ElementProjectile : GlobalProjectile
        {
            public override bool InstancePerEntity => true;

            public ElementData elementData = new ElementData();
            public override void OnSpawn(Projectile projectile, IEntitySource source)
            {
                if (!ProjectileSets.DontInheritElementFromWeapon[projectile.type])
                {
                    if(source is EntitySource_ItemUse_WithAmmo src)
                        elementData = ElementData.cloneElements(Main.player[projectile.owner].HeldItem.GetElementItem().elementData);
                }
            }
        }
        public class ElementNPC : GlobalNPC
        {
            public ElementData elementData = new ElementData();
            public override bool InstancePerEntity => true;
            public override void SetDefaults(NPC npc)
            {
                if (npc.ModNPC == null)
                {
                    if (AddElementsToVanillaContent.fireNPC.Contains(npc.type))
                        elementData.Fire = true;
                    if (AddElementsToVanillaContent.waterNPC.Contains(npc.type))
                        elementData.Water = true;
                    if (AddElementsToVanillaContent.earthNPC.Contains(npc.type))
                        elementData.Earth = true;
                    if (AddElementsToVanillaContent.airNPC.Contains(npc.type))
                        elementData.Air = true;
                    if (AddElementsToVanillaContent.lightNPC.Contains(npc.type))
                        elementData.Light = true;
                    if (AddElementsToVanillaContent.darkNPC.Contains(npc.type))
                        elementData.Dark = true;
                    if (AddElementsToVanillaContent.iceNPC.Contains(npc.type))
                        elementData.Ice = true;
                    if (AddElementsToVanillaContent.poisonNPC.Contains(npc.type))
                        elementData.Poison = true;
                    if (AddElementsToVanillaContent.electricNPC.Contains(npc.type))
                        elementData.Electric = true;
                    if (AddElementsToVanillaContent.arcaneNPC.Contains(npc.type))
                        elementData.Arcane = true;
                }
            }
            private void AddIcon(BestiaryEntry bestiaryEntry, ModBiome element)
            {
                bestiaryEntry.Info.Add(new ModBiomeBestiaryInfoElement(Mod, element.DisplayName.Value, element.BestiaryIcon, "", null));
            }
            public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
            {
                npc.SetDefaults(npc.type);
                ElementData data = npc.GetElementNPC().elementData;
                if (data.Arcane) AddIcon(bestiaryEntry, ModContent.GetInstance<Arcane>());
                if (data.Light) AddIcon(bestiaryEntry, ModContent.GetInstance<Light>());
                if (data.Dark) AddIcon(bestiaryEntry, ModContent.GetInstance<Dark>());
                if (data.Fire) AddIcon(bestiaryEntry, ModContent.GetInstance<Fire>());
                if (data.Water) AddIcon(bestiaryEntry, ModContent.GetInstance<Water>());
                if (data.Earth) AddIcon(bestiaryEntry, ModContent.GetInstance<Earth>());
                if (data.Air) AddIcon(bestiaryEntry, ModContent.GetInstance<Air>());
                if (data.Ice) AddIcon(bestiaryEntry, ModContent.GetInstance<Ice>());
                if (data.Poison) AddIcon(bestiaryEntry, ModContent.GetInstance<Poison>());
                if (data.Electric) AddIcon(bestiaryEntry, ModContent.GetInstance<Electric>());
            }
        }
    }
}
