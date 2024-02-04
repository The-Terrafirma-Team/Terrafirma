using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Items.Weapons.Melee.Shortswords;
using Terrafirma.Items.Weapons.Magic;
using Terrafirma.Items.Ammo;
using Terrafirma.Projectiles.Hostile;
using Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode;

namespace Terrafirma.Items.Consumable
{
    internal class LuckyBlock : ModItem
    {
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
            Item.maxStack = 9999;
            Item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override bool? UseItem(Player player)
        {
            //Random Chance System
            float LuckyNumber = Main.rand.NextFloat(-0.9f, 1f) + (player.luck / 2);

            //Good Outcome :D
            if (LuckyNumber >= 0f)
            {

                //Weight for each DropNum case
                int[] RarityPool = new int[] { 12, 20, 25, 18, 12 };
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

                float CaseChance = Main.rand.NextFloat(0, 101);
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
                            else if (CaseChance <= 90) { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(5, 11)); } // 5-10 Gold Coins - 30%
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(10, 21)); } // 10-20 Gold Coins - 10%
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
                        //Accessories
                        case 4:
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
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(4, 11)); } // 4-10 Gold Coins - 20%
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
                        //Accessories
                        case 4:
                            break;
                    }
                }
                else //Start of World Progression
                {
                    switch (DropNum)
                    {
                        //Weapons
                        case 0:
                            if (CaseChance <= 20) { player.QuickSpawnItem(default, ModContent.ItemType<StarRevolver>()); } // Star Revolver - 20%
                            else if ((CaseChance <= 40)) { player.QuickSpawnItem(default, ModContent.ItemType<KnifeShooter>()); } // Knife Shooter - 20%
                            else if ((CaseChance <= 60)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); } // Rapier - 20%
                            else if ((CaseChance <= 80)) { player.QuickSpawnItem(default, ModContent.ItemType<Rapier>()); } // Rapier - 20%
                            else { player.QuickSpawnItem(default, ModContent.ItemType<WandOfPoisoning>()); } // Wand of Poisoning - 20%
                            break;
                        //Coins
                        case 1:
                            if (CaseChance <= 60) { player.QuickSpawnItem(default, ItemID.CopperCoin, Main.rand.Next(2, 5) * 20); } // 40-80 Copper Coins - 60%
                            else if ((CaseChance <= 90)) { player.QuickSpawnItem(default, ItemID.SilverCoin, Main.rand.Next(1, 10) * 10); } // 10-90 Silver Coins - 30%
                            else { player.QuickSpawnItem(default, ItemID.GoldCoin, Main.rand.Next(2, 6)); } // 2-5 Gold Coins - 10%
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
                        //Accessories
                        case 4:
                            break;
                    }
                }
            }







            //Bad Outcome :(
            else
            {

                //Weight for each DropNum case
                int[] RarityPool = new int[] { 3, 8, 1, 3};
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


                int CaseChance = Main.rand.Next(0, 101);
                switch (DropNum)
                {
                    // Bomb Spawn
                    case 0:
                        if (CaseChance <= 1) { Projectile.NewProjectile(default, player.position, new Vector2(player.velocity.X, player.velocity.Y - 3f), ModContent.ProjectileType<HolyHandGrenade>(), default, default, player.whoAmI, default, default, default); } // Holy Hand Grenade Spawn - 1%
                        else { Projectile.NewProjectile(default, player.position, new Vector2(player.velocity.X, player.velocity.Y - 3f), ProjectileID.Bomb, default, default, player.whoAmI, default, default, default); } // Bomb Spawn - 1%
                        break;
                    // Debuff - Each Debuff can apply separately
                    case 1:
                        if (CaseChance <= 20) { player.AddBuff(BuffID.OnFire, Main.rand.Next(8, 17) * 60, false, false); } // 8-16s OnFire - 20%
                        else if (CaseChance <= 40) { player.AddBuff(BuffID.CursedInferno, Main.rand.Next(5, 10) * 60, false, false); } // 5-9s Cursed Inferno - 20%
                        
                        if (CaseChance <= 25) { player.AddBuff(BuffID.BrokenArmor, Main.rand.Next(50, 71) * 60, false, false); } // 50-70s Broken Armor - 20%
                        else if (CaseChance <= 50) { player.AddBuff(BuffID.Bleeding, Main.rand.Next(35, 51) * 60, false, false); } // 35-50s Bleeding - 20%
                        
                        if (CaseChance > 40) { player.AddBuff(BuffID.Frostburn, Main.rand.Next(8, 13) * 60, false, false); } // 8-12s Frostburn - 40%
                        else if (CaseChance > 90) { player.AddBuff(BuffID.Chilled, Main.rand.Next(25, 36) * 60, false, false); } // 25-35s Chilled - 50%
                        else { player.AddBuff(BuffID.Frozen, Main.rand.Next(4, 9) * 60, false, false); } // 4-8s Frozen - 10%
                       
                        break;
                    // Teleport Player at random Location
                    case 2:
                        player.Teleport(new Vector2(Main.rand.Next(0, (int)Main.rightWorld), Main.rand.Next(0, (int)Main.bottomWorld))  ,0,0);
                        break;
                    // Spawn a bunch of NPCs
                    case 3:
                        
                        if (CaseChance <= 20) { for (int i = 0; i < Main.rand.Next(0,11);  i++) { NPC.NewNPC(default, (int)player.position.X, (int)player.position.Y, NPCID.Zombie, 0, 0, 0, 0, 0, player.whoAmI); } } // 1-10 Zombies - 20%
                        else if (CaseChance <= 30) { NPC.NewNPC(default, (int)player.position.X, (int)player.position.Y, NPCID.RockGolem, 0, 0, 0, 0, 0, player.whoAmI); } // 1-10 Zombies - 10%



                        break;
                }
            }

            return true;
        }


    }
}
