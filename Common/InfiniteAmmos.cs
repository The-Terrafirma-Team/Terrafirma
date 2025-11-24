using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    // i'll do this later lmaoz
    //public class InfiniteAmmoItem : GlobalItem
    //{
    //    public static int[] InfiniteAmmos = [
    //        ItemID.MusketBall,ItemID.MeteorShot,ItemID.SilverBullet,ItemID.CrystalBullet,ItemID.CursedBullet,ItemID.ChlorophyteBullet,ItemID.HighVelocityBullet,ItemID.IchorBullet,ItemID.VenomBullet,ItemID.PartyBullet,ItemID.NanoBullet,ItemID.ExplodingBullet,ItemID.GoldenBullet,ItemID.MoonlordBullet,ItemID.TungstenBullet,
    //        ItemID.WoodenArrow,ItemID.FlamingArrow
    //        ];
    //    public static void RegisterInfiniteAmmo(int type)
    //    {
    //        InfiniteAmmos = InfiniteAmmos.Append(type).ToArray();
    //    }
    //    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    //    {
    //        return InfiniteAmmos.Contains(entity.type);
    //    }
    //    public override void SetDefaults(Item entity)
    //    {
    //        entity.maxStack = 1;
    //        entity.consumable = false;
    //    }
    //    public override void SetStaticDefaults()
    //    {
    //        base.SetStaticDefaults();
    //    }
    //}
}
