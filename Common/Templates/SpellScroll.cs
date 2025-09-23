using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class SpellScroll : ModItem
    {
        public virtual Skill SkillToLearn => null;
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.Size = new Vector2(16);
            Item.UseSound = SoundID.Item4;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                SkillsPlayer p = player.GetModPlayer<SkillsPlayer>();
                for(int i = 0; i < p.MaxSkills; i++)
                {
                    if (p.Skills[i] == null)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            ParticleSystem.NewParticle(new StarSparkle(Main.rand.Next(14, 42), Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(0.4f, 1f), SkillToLearn.RechargeFlashColor with { A = 0 } * Main.rand.NextFloat(0.2f,1f), Main.rand.NextVector2Circular(5, 5) + new Vector2(0, -3)), player.Bottom);
                        }
                        //Main.NewText(SkillsSystem.Skills[0]);
                        p.Skills[i] = (Skill)Activator.CreateInstance(SkillToLearn.GetType());
                        break;
                    }
                }
            }
            return base.UseItem(player);
        }
    }
}
