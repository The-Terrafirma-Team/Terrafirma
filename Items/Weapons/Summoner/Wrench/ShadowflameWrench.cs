using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Sentry;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class ShadowflameWrench : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(80, 26);
            Item.rare = ContentSamples.ItemsByType[ItemID.ShadowFlameKnife].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.ShadowFlameKnife].value;
            Buff = new ShadowflameWrenchBuff();
            BuffDuration = 60 * 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Summon.ShadowflameWrench>();
            Item.shootSpeed = 6;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 60 * 6);
        }
    }
}
