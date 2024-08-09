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
        public override int SecondarySummon => ProjectileID.BouncyDynamite;
        public override int FirstSummon => ProjectileID.BouncyBoulder;
        public override void SetDefaults()
        {
            summonColor = Color.Red;
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

            Item.rare = ContentSamples.ItemsByType[ItemID.BloodButcherer].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.BloodButcherer].value;
        }
    }
}
