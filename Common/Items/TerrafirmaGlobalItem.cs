using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
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
            switch (entity.type)
            {
                case ItemID.Zenith:
                    entity.rare = ModContent.RarityType<FinalQuestRarity>();
                    break;
                case ItemID.MusketBall:
                case ItemID.SilverBullet:
                case ItemID.TungstenBullet:
                    entity.shootSpeed = 5.25f;
                    break;
            }
        }
        public override void UpdateEquip(Item item, Player player)
        {
            PlayerStats stats = player.PlayerStats();
            switch (item.type)
            {
                case ItemID.FeralClaws:
                    if(stats.FeralChargeMax < 3)
                        stats.FeralChargeMax = 3f;
                    break;
                case ItemID.PowerGlove:
                    if (stats.FeralChargeMax < 4)
                        stats.FeralChargeMax = 4f;
                    break;
                case ItemID.BerserkerGlove:
                    if (stats.FeralChargeMax < 4)
                        stats.FeralChargeMax = 4f;

                    if (stats.FeralChargeSpeed < 0.66f / 60f)
                        stats.FeralChargeSpeed = 0.66f / 60f;
                    break;
                case ItemID.FireGauntlet:
                case ItemID.MechanicalGlove:
                    if (stats.FeralChargeMax < 5)
                        stats.FeralChargeMax = 5f;
                    break;
            }
        }
        public override bool? UseItem(Item item, Player player)
        {
            player.PlayerStats().TimesHeldWeaponHasBeenSwung++;
            return base.UseItem(item, player);
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
