using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terrafirma.Systems.Elements.Beastiary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.Elements
{
    public class ElementGlobals
    {
        public class ElementPlayer : ModPlayer
        {
            public void ElementEffect(NPC target, NPC.HitInfo hit, int damageDone, ElementData elementData)
            {
                PlayerStats stats = Player.PlayerStats();
                if (target.lifeMax < 5)
                    return;
                if (stats.FireEnhancement && elementData.Fire)
                {
                    if (hit.Crit)
                        target.AddBuff(BuffID.Oiled, 60 * 15);
                    SoundEngine.PlaySound(SoundID.Item74, target.position);
                    if (target.life <= 0)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            Dust d = Dust.NewDustPerfect(target.Center, DustID.InfernoFork, Main.rand.NextVector2Circular(Math.Max(target.width, target.height) / 6, Math.Max(target.width, target.height) / 6));
                            d.noGravity = !Main.rand.NextBool(3);
                            if (d.noGravity)
                            {
                                d.velocity *= 2;
                                d.fadeIn = 1.4f;
                            }
                        }
                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (!npc.friendly && npc.lifeMax > 5 && npc.Center.Distance(target.Center) <= Math.Max(target.width, target.height) * 3)
                            {
                                if (hit.Crit)
                                    npc.AddBuff(BuffID.Oiled, 60 * 15);
                                npc.AddBuff(BuffID.OnFire, 60 * 5);

                                for (int x = 0; x < 10; x++)
                                {
                                    Dust d = Dust.NewDustPerfect(npc.Center, DustID.InfernoFork, Main.rand.NextVector2Circular(5, 5));
                                    d.noGravity = !Main.rand.NextBool(3);
                                    if (d.noGravity)
                                    {
                                        d.velocity *= 2;
                                        d.fadeIn = 1.4f;
                                    }
                                }
                            }
                        }
                    }
                }
                if (stats.DarkEnhancement && elementData.Dark && target.life <= damageDone)
                {
                    bool previousHideStrike = target.HideStrikeDamage;
                    target.HideStrikeDamage = true;
                    target.StrikeInstantKill();
                    target.HideStrikeDamage = previousHideStrike;

                    for(int i = 0; i < 12; i++)
                    {
                        PixelCircle p = new PixelCircle();
                        p.outlineColor = Color.DarkViolet;
                        p.scale = Main.rand.NextFloat(4, 5);
                        p.deceleration = 0.9f;
                        p.outlineAffectedByLight = true;
                        ParticleSystem.AddParticle(p, target.Center, Main.rand.NextVector2Circular(hit.Crit ? 12 : 6, hit.Crit ? 12 : 6), Color.Black);
                    }
                }
            }
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
                ElementEffect(target,hit,damageDone, item.GetElementItem().elementData);
            }
            public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
            {
                float mod = ElementData.getElementalBonus(proj.GetElementProjectile().elementData, target.GetElementNPC().elementData);
                DamageNumber(target, mod, hit, damageDone);
                ElementEffect(target, hit, damageDone, proj.GetElementProjectile().elementData);
            }
            private void DamageNumber(NPC target, float Mod, NPC.HitInfo hit, int damageDone)
            {
                Color color = hit.Crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;

                color = Color.Lerp(color, (Mod > 1) ? Color.Red : Color.Gray, MathF.Abs(Mod - 1) / (Mod > 1 ? 2 : 1));

                CombatText.NewText(target.Hitbox, color,damageDone,hit.Crit);
                //Main.NewText(Mod);
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
                if (!ProjectileSets.DontInheritElementFromSource[projectile.type])
                {
                    if(source is EntitySource_ItemUse_WithAmmo)
                        elementData = ElementData.cloneElements(Main.player[projectile.owner].HeldItem.GetElementItem().elementData);
                    else if (source is EntitySource_ItemUse itemUse)
                    {
                        elementData = ElementData.cloneElements(itemUse.Item.GetElementItem().elementData);
                    }
                    else if (source is EntitySource_Parent src)
                    {

                        ElementData data = elementData;
                        if (src.Entity is NPC npc)
                        {
                            data = npc.GetElementNPC().elementData;
                        }
                        else if (src.Entity is Item item)
                        {
                            data = item.GetElementItem().elementData;
                        }
                        else if (src.Entity is Projectile proj)
                        {
                            data = proj.GetElementProjectile().elementData;
                        }
                        elementData = ElementData.cloneElements(data);
                    }
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
