using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Content.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Mechanics
{
    public class LivingFireIgnitingArrows : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.arrow;
        }
        public bool Fire = false;
        public bool Cursed = false;
        public bool Demon = false;
        public bool Frost = false;
        public bool Ichor = false;
        public bool Ultrabright = false;
        private void spawnDust(Projectile projectile, int type)
        {
            Dust d = Dust.NewDustDirect(projectile.position - projectile.velocity * Main.rand.NextFloat(), projectile.width, projectile.height, type);
            d.noGravity = true;
            d.scale = 1.5f;
            d.velocity *= 0.5f;
            d.velocity += projectile.velocity * 0.3f;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!Main.rand.NextBool(3))
                return;
            int duration = 60 * 2;
            if (Main.rand.NextBool(3))
                duration *= 3;

            if (Fire)
                target.AddBuff(BuffID.OnFire3, duration);
            if (Cursed)
                target.AddBuff(BuffID.CursedInferno, duration);
            if (Demon)
                target.AddBuff(BuffID.ShadowFlame, duration);
            if (Frost)
                target.AddBuff(BuffID.Frostburn2, duration);
            if (Ichor)
                target.AddBuff(BuffID.Ichor, duration);
            if (Ultrabright && Main.rand.NextBool())
            {
                if (Terrafirma.CombatReworkEnabled)
                    target.AddBuff(ModContent.BuffType<Stunned>(), duration / 2);
                else
                    target.AddBuff(BuffID.OnFire3, duration * 2);
            }
        }
        public override void AI(Projectile projectile)
        {
            SoundStyle style = SoundID.DD2_FlameburstTowerShot.WithVolumeScale(0.4f);
            style.PitchVariance = 0.7f;
            style.MaxInstances = 10;

            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingFire && !Fire)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Fire = true;
            }
            else if (Fire)
            {
                spawnDust(projectile, DustID.DesertTorch);
            }
            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingCursedFire && !Cursed)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Cursed = true;
            }
            else if (Cursed)
            {
                spawnDust(projectile, DustID.CursedTorch);
            }
            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingDemonFire && !Demon)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Demon = true;
            }
            else if (Demon)
            {
                spawnDust(projectile, DustID.DemonTorch);
            }
            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingFrostFire && !Frost)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Frost = true;
            }
            else if (Frost)
            {
                spawnDust(projectile, DustID.IceTorch);
            }
            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingIchor && !Ichor)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Ichor = true;
            }
            else if (Ichor)
            {
                spawnDust(projectile, DustID.IchorTorch);
            }
            if (Main.tile[projectile.Center.ToTileCoordinates()].TileType == TileID.LivingUltrabrightFire && !Ultrabright)
            {
                SoundEngine.PlaySound(style, projectile.position);
                Ultrabright = true;
            }
            else if (Ultrabright)
            {
                spawnDust(projectile, DustID.UltraBrightTorch);
            }
        }
    }
    public class LivingFireGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is ItemID.LivingFireBlock or ItemID.LivingFrostFireBlock or ItemID.LivingCursedFireBlock or ItemID.LivingDemonFireBlock or ItemID.LivingIchorBlock or ItemID.LivingUltrabrightFireBlock;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.Terrafirma.VanillaItemTooltips.LivingFireBlock")));
        }
    }
}
