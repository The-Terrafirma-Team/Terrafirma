using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Items.Weapons.Ranged;
using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using TerrafirmaRedux.Items.Weapons.Magic;
using TerrafirmaRedux.Projectiles;

namespace TerrafirmaRedux.Items.Consumable
{
    internal class LuckyBlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.width = 26;
            Item.height = 26;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.maxStack = 999;
            Item.consumable = true;

        }

        public override bool? UseItem(Player player)
        {
            //Random Chance System
            float LuckyNumber = Main.rand.NextFloat(-0.8f, 1f) + (player.luck / 2);

            //Good Outcome :D
            if (LuckyNumber >= 0f)
            {

                //Weight for each DropNum case
                int[] RarityPool = new int[10];
                RarityPool = [16, 20, 25, 18];
                //

                //Not Important
                int RarityPoolSum = 0;
                for (int i = 0; i < RarityPool.Length; i++)
                {
                    RarityPoolSum += RarityPool[i];
                }

                int CheckNum = 0;
                int DropNum = Main.rand.Next(0, RarityPoolSum + 1);
                for (int i = 0; i < RarityPool.Length; i++)
                {
                    CheckNum += RarityPool[i];
                    if (DropNum < CheckNum)
                    {
                        DropNum = i;
                        break;
                    }
                }
                //

                //Cases
                int CaseChance = Main.rand.Next(0, 101);
                switch (DropNum)
                {
                    //Weapons
                    case 0:
                        if (CaseChance <= 25) { player.QuickSpawnItem(default, ModContent.ItemType<StarRevolver>() ); }
                        else if ((CaseChance <= 50)) { player.QuickSpawnItem(default, ModContent.ItemType<KnifeShooter>() ); }
                        else if ((CaseChance <= 75)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); }
                        else { player.QuickSpawnItem(default, ModContent.ItemType<WandOfPoisoning>() ); }
                        break;
                    //Coins
                    case 1:
                        if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.CopperCoin, Main.rand.Next(2, 5) * 20); }
                        else if ((CaseChance <= 90)) { player.QuickSpawnItem(default, ItemID.SilverCoin, Main.rand.Next(1, 10) * 10); }
                        else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(1, 3)); }
                        break;
                    //Ammo
                    case 2: 
                        if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.MusketBall, Main.rand.Next(3, 25) * 10); }
                        else { player.QuickSpawnItem(default, ItemID.WoodenArrow, Main.rand.Next(3, 25) * 10); }
                        break;
                    //Misc Stuff
                    case 3:
                        if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.ThrowingKnife, Main.rand.Next(2, 15) * 10); }
                        else if ((CaseChance <= 90)) { player.QuickSpawnItem(default, ItemID.Shuriken, Main.rand.Next(1, 10) * 10); }
                        else if ((CaseChance <= 90)) { player.QuickSpawnItem(default, ItemID.Grenade, Main.rand.Next(3, 15)); }
                        else { player.QuickSpawnItem(default, ItemID.Bomb, Main.rand.Next(1, 6) * 10); }
                        break;
                }
            }
            //Bad Outcome :(
            else
            {
                int CaseChance = Main.rand.Next(0, 101);
                if (CaseChance <= 1)
                {
                    Projectile.NewProjectile(default, player.position, new Vector2(player.velocity.X, player.velocity.Y - 3f), ModContent.ProjectileType<HolyHandGrenade>(), default, default, player.whoAmI, default, default, default);
                }
                else
                {
                    Projectile.NewProjectile(default, player.position, new Vector2(player.velocity.X, player.velocity.Y - 3f), ProjectileID.Bomb, default, default, player.whoAmI, default, default, default);
                }
            }

            return true;
        }


    }
}
