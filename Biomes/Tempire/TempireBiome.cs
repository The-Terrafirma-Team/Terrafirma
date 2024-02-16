using Terraria.Graphics.Capture;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terrafirma.Subworlds.Tempire;
using Terrafirma.Backgrounds;

namespace Terrafirma.Biomes.Tempire
{
    internal class TempireBiome : ModBiome
    {
        // Select all the scenery
        public override ModWaterStyle WaterStyle => ModContent.GetInstance<TempireBiomeWaterStyle>();
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<TempireBiomeBackgroundStyle>();
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;


        public override int BiomeTorchItemType => ItemID.Torch;
        public override int BiomeCampfireItemType => ItemID.Campfire;

        // Populate the Bestiary Filter
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath; // Re-uses Bestiary Background for Map Background

        // Calculate when the biome is active.
        public override bool IsBiomeActive(Player player)
        {
            return SubworldSystem.IsActive<TempireSubworld>();
        }

        // Declare biome priority. The default is BiomeLow so this is only necessary if it needs a higher priority.
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
    }
}
