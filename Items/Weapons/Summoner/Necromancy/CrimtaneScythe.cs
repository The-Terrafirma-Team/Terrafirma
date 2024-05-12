using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.Items.Weapons.Summoner.Necromancy
{
    public class CrimtaneScythe : NecromancerScythe
    {
        public override string SoulName => "Crimtane";

        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.damage = 20;
            Item.knockBack = 3;
            Item.useStyle = 1;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.shoot = ModContent.ProjectileType<ScytheSwing>();
            Item.UseSound = SoundID.Item1;
            Item.Size = new Vector2(16, 16);
            Item.shootSpeed = 8;
        }
    }
}
