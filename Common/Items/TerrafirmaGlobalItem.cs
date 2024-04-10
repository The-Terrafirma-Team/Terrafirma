using Microsoft.Xna.Framework;
using Terrafirma.Particles;
using Terrafirma.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
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
                    if (Main.timeForVisualEffects % 15 == 0)
                    {
                        BigSparkle p = new BigSparkle();
                        p.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                        p.fadeInTime = 20;
                        p.Scale = Main.rand.NextFloat(0.3f, 1.2f);
                        ParticleSystem.AddParticle(new BigSparkle(), new Vector2(Main.rand.NextFloat(line.Text.Length * 9.5f), Main.rand.NextFloat(20f)),null,line.Color,ParticleLayer.UI);
                    }
                }
                ParticleSystem.DrawUIParticle(new Vector2(line.X, line.Y));
            }
        }
    }
}
