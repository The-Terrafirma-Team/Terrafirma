using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Items.Weapons.Ranged;
using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using TerrafirmaRedux.Items.Weapons.Magic;
using TerrafirmaRedux.Projectiles;
using TerrafirmaRedux.Items.Ammo;

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
                int[] RarityPool = new int[] { 16, 20, 25, 18 };
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
                //Remember that the chances that are listed below refer to the chances after that case has been selected
                //ex. *5-10 Gold Coins - 10%* is in reality a 2.53% chance to drop from a lucky block (25.3% for block to drop coins, 10% chance for those coins to be 5-10 Gold Coins)

                int CaseChance = Main.rand.Next(0, 101);
                if (Main.hardMode == true) //Pre-Tempire Started?
                {
                    switch (DropNum)
                    {
                        //Weapons
                        case 0:
                            if (CaseChance <= 25) { player.QuickSpawnItem(default, ModContent.ItemType<StarRevolver>()); }
                            else if ((CaseChance <= 50)) { player.QuickSpawnItem(default, ModContent.ItemType<KnifeShooter>()); }
                            else if ((CaseChance <= 75)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); }
                            else { player.QuickSpawnItem(default, ModContent.ItemType<WandOfPoisoning>()); }
                            break;
                        //Coins
                        case 1:
                            if ((CaseChance <= 60)) { player.QuickSpawnItem(default, ItemID.SilverCoin, Main.rand.Next(8, 10) * 10); } // 40-80 Silver Coins - 60%
                            else if (CaseChance <= 90) { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(2, 5)); } // 2-4 Gold Coins - 30%
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(5, 11)); } // 5-10 Gold Coins - 10%
                            break;
                        //Ammo
                        case 2:
                            if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.MusketBall, Main.rand.Next(3, 25) * 10); } // Musket Ball - 60%
                            else if (CaseChance <= 73) { player.QuickSpawnItem(default, ItemID.IchorArrow, Main.rand.Next(3, 12) * 10); } // Ichor Arrows - 13%
                            else if (CaseChance <= 86) { player.QuickSpawnItem(default, ItemID.CursedArrow, Main.rand.Next(3, 12) * 10); } // Cursed Arrows - 13%
                            else if (CaseChance <= 60) { player.QuickSpawnItem(default, ModContent.ItemType<PhantasmalArrow>(), Main.rand.Next(3, 12) * 10); } // Phantasmal Arrows - 14%
                            break;
                        //Misc Stuff
                        case 3:
                            if (CaseChance <= 50) { player.QuickSpawnItem(default, ItemID.ThrowingKnife, Main.rand.Next(2, 15) * 10); } // Throwing Knife - 50%
                            else if ((CaseChance <= 70)) { player.QuickSpawnItem(default, ItemID.Shuriken, Main.rand.Next(1, 10) * 10); } // Shuriken - 20%
                            else if ((CaseChance <= 85)) { player.QuickSpawnItem(default, ItemID.Grenade, Main.rand.Next(3, 15)); } // Grenade - 15%
                            else { player.QuickSpawnItem(default, ItemID.Bomb, Main.rand.Next(1, 6) * 10); } // Bomb - 15%
                            break;
                    }
                }
                else if (NPC.downedBoss2 == true) //Eater of Worlds / Brain of Cthulhu downed?
                {
                    switch (DropNum)
                    {
                        //Weapons
                        case 0:
                            if (CaseChance <= 25) { player.QuickSpawnItem(default, ModContent.ItemType<StarRevolver>()); }
                            else if ((CaseChance <= 50)) { player.QuickSpawnItem(default, ModContent.ItemType<KnifeShooter>()); }
                            else if ((CaseChance <= 75)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); }
                            else { player.QuickSpawnItem(default, ModContent.ItemType<WandOfPoisoning>()); }
                            break;
                        //Coins
                        case 1:
                            if ((CaseChance <= 80)) { player.QuickSpawnItem(default, ItemID.SilverCoin, Main.rand.Next(3, 10) * 10); } // 30-90 Silver Coins - 80%
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(1, 4)); } // 1-3 Gold Coins - 20%
                            break;
                        //Ammo
                        case 2:
                            if (CaseChance <= 40) { player.QuickSpawnItem(default, ItemID.MusketBall, Main.rand.Next(3, 25) * 10); } // Musket Ball - 40%
                            else if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.MeteorShot, Main.rand.Next(3, 25) * 10); } // Meteor Shot - 20%
                            else if (CaseChance <= 80) { player.QuickSpawnItem(default, ItemID.JestersArrow, Main.rand.Next(3, 10) * 10); } // Jester's Arrow - 20%
                            else { player.QuickSpawnItem(default, ItemID.UnholyArrow, Main.rand.Next(3, 10) * 10); } // Unholy Arrow - 20%
                            break;
                        //Misc Stuff
                        case 3:
                            if (CaseChance <= 50) { player.QuickSpawnItem(default, ItemID.ThrowingKnife, Main.rand.Next(2, 15) * 10); } // Throwing Knife - 50%
                            else if ((CaseChance <= 70)) { player.QuickSpawnItem(default, ItemID.Shuriken, Main.rand.Next(1, 10) * 10); } // Shuriken - 20%
                            else if ((CaseChance <= 85)) { player.QuickSpawnItem(default, ItemID.Grenade, Main.rand.Next(3, 15)); } // Grenade - 15%
                            else { player.QuickSpawnItem(default, ItemID.Bomb, Main.rand.Next(1, 6) * 10); } // Bomb - 15%
                            break;
                    }
                }
                else //Start of World Progression
                {
                    switch (DropNum)
                    {
                        //Weapons
                        case 0:
                            if (CaseChance <= 25) { player.QuickSpawnItem(default, ModContent.ItemType<StarRevolver>()); } // Star Revolver - 25%
                            else if ((CaseChance <= 50)) { player.QuickSpawnItem(default, ModContent.ItemType<KnifeShooter>()); } // Knife Shooter - 25%
                            else if ((CaseChance <= 75)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); } // Rapier - 25%
                            else { player.QuickSpawnItem(default, ModContent.ItemType<WandOfPoisoning>()); } // Wand of Poisoning - 25%
                            break;
                        //Coins
                        case 1:
                            if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.CopperCoin, Main.rand.Next(2, 5) * 20); } // 40-80 Copper Coins - 60%
                            else if ((CaseChance <= 90)) { player.QuickSpawnItem(default, ItemID.SilverCoin, Main.rand.Next(1, 10) * 10); } // 10-90 Silver Coins - 30%
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(1, 3)); } // 1-2 Gold Coins - 10%
                            break;
                        //Ammo
                        case 2:
                            if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.MusketBall, Main.rand.Next(3, 25) * 10); } // Musket Ball - 60%
                            else { player.QuickSpawnItem(default, ItemID.WoodenArrow, Main.rand.Next(3, 25) * 10); } // Wooden Arrow - 40%
                            break;
                        //Misc Stuff
                        case 3:
                            if (CaseChance <= 50) { player.QuickSpawnItem(default, ItemID.ThrowingKnife, Main.rand.Next(2, 15) * 10); } // Throwing Knife - 50%
                            else if ((CaseChance <= 70)) { player.QuickSpawnItem(default, ItemID.Shuriken, Main.rand.Next(1, 10) * 10); } // Shuriken - 20%
                            else if ((CaseChance <= 85)) { player.QuickSpawnItem(default, ItemID.Grenade, Main.rand.Next(3, 15)); } // Grenade - 15%
                            else { player.QuickSpawnItem(default, ItemID.Bomb, Main.rand.Next(1, 6) * 10); } // Bomb - 15%
                            break;
                    }
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
