using Microsoft.Xna.Framework;
using System.Linq;
using Terrafirma.Particles.LegacyParticles;
using Terrafirma.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    public class TerrafirmaGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.Zenith)
            {
                entity.rare = ModContent.RarityType<FinalQuestRarity>();
            }
             base.SetDefaults(entity);
        }
        public override void PostDrawTooltipLine(Item item, DrawableTooltipLine line)
        {
            if (item.rare == ModContent.RarityType<FinalQuestRarity>())
            {

                if (line.Name == "ItemName") 
                { 
                    if (Main.timeForVisualEffects % 15 == 0) LegacyParticleSystem.AddUIParticle(new BigUISparkle(), Vector2.Zero + new Vector2(Main.rand.NextFloat(line.Text.Length * 9.5f), Main.rand.NextFloat(20f)), Vector2.Zero, line.Color, 0, 20, 0.1f, Main.rand.NextFloat(0.3f,1.2f), Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
                    LegacyParticleSystem.DrawUIParticle(new Vector2(line.X, line.Y));
                    
                }

            }
        }
    }
}
