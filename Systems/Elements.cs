using Microsoft.Xna.Framework;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terrafirma.Items.Weapons.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static tModPorter.ProgressUpdate;

namespace Terrafirma.Systems
{
    public class Elements: ModSystem
    {
        public static HashSet<int> fireNPC = new HashSet<int>();
        public static HashSet<int> fireItem = new HashSet<int>();
        public static HashSet<int> waterNPC = new HashSet<int>();
        public static HashSet<int> waterItem = new HashSet<int>();
        public static HashSet<int> earthNPC = new HashSet<int>();
        public static HashSet<int> earthItem = new HashSet<int>();
        public static HashSet<int> airNPC = new HashSet<int>();
        public static HashSet<int> airItem = new HashSet<int>();
        public static HashSet<int> iceNPC = new HashSet<int>();
        public static HashSet<int> iceItem = new HashSet<int>();
        public static HashSet<int> poisonNPC = new HashSet<int>();
        public static HashSet<int> poisonItem = new HashSet<int>();
        public static HashSet<int> lightNPC = new HashSet<int>();
        public static HashSet<int> lightItem = new HashSet<int>();
        public static HashSet<int> darkNPC = new HashSet<int>();
        public static HashSet<int> darkItem = new HashSet<int>();
        public static HashSet<int> electricNPC = new HashSet<int>();
        public static HashSet<int> electricItem = new HashSet<int>();
        public static HashSet<int> magicNPC = new HashSet<int>();
        public static HashSet<int> magicItem = new HashSet<int>();

        public override void Load()
        {
            AddNPCSToSets();
            AddMeleeItemsToSets();
            AddMagicItemsToSets();
            AddRangedItemsToSets();
            AddSummonerItemsToSets();
        }
        private void AddNPCSToSets()
        {
            #region fire
            AddIDRange(fireNPC, 23, 25);
            AddIDRange(fireNPC, 29, 30);
            AddIDRange(fireNPC, 59, 60);
            fireNPC.Add(72);
            fireNPC.Add(151);
            AddIDRange(earthNPC, 245, 249);
            AddIDRange(fireNPC, 277, 280);
            fireNPC.Add(325);
            fireNPC.Add(378);
            AddIDRange(fireNPC, 412, 418);
            AddIDRange(fireNPC, 471, 472);
            AddIDRange(fireNPC, 516, 519);
            fireNPC.Add(527);
            fireNPC.Add(533);
            fireNPC.Add(551);
            AddIDRange(fireNPC, 572, 575);
            fireNPC.Add(614);
            fireNPC.Add(665);
            #endregion fire
            #region water
            //Should take another look through this at some point
            AddIDRange(waterNPC, -55, -54);
            AddIDRange(waterNPC, 32, 33);
            AddIDRange(waterNPC, 63, 64);
            waterNPC.Add(166);
            AddIDRange(waterNPC, 220, 221);
            AddIDRange(waterNPC, 223, 225);
            waterNPC.Add(250);
            waterNPC.Add(244);
            waterNPC.Add(268);
            AddIDRange(waterNPC, 370, 373);
            waterNPC.Add(465);
            waterNPC.Add(526);
            waterNPC.Add(586);
            waterNPC.Add(587);
            AddIDRange(waterNPC, 618, 623);
            #endregion water
            #region earth
            AddIDRange(earthNPC, -53, -46);
            AddIDRange(earthNPC, 7, 15);
            earthNPC.Add(21);
            earthNPC.Add(23);
            AddIDRange(earthNPC, 43, 45);
            earthNPC.Add(56);
            earthNPC.Add(69);
            earthNPC.Add(77);
            AddIDRange(earthNPC, 95, 101);
            earthNPC.Add(153);
            AddIDRange(earthNPC, 163, 165);
            AddIDRange(earthNPC, 201, 203);
            AddIDRange(earthNPC, 218, 220);
            AddIDRange(earthNPC, 236, 238);
            AddIDRange(earthNPC, 245, 249);
            AddIDRange(earthNPC, 254, 261);
            AddIDRange(earthNPC, 262, 265);
            AddIDRange(earthNPC, 449, 452);
            AddIDRange(earthNPC, 494, 506);
            earthNPC.Add(8);
            AddIDRange(earthNPC, 510, 515);
            AddIDRange(earthNPC, 524, 532);
            earthNPC.Add(537);
            AddIDRange(earthNPC, 542, 545);
            AddIDRange(earthNPC, 568, 569);
            AddIDRange(earthNPC, 580, 582);
            earthNPC.Add(635);
            earthNPC.Add(631);
            #endregion earth
            #region air
            AddIDRange(airNPC, -65, -56);
            AddIDRange(airNPC, -43, -38);
            AddIDRange(airNPC, -23, -16);
            AddIDRange(airNPC, -12, -11);
            airNPC.Add(2);
            AddIDRange(airNPC, 4, 6);
            airNPC.Add(34);
            airNPC.Add(42);
            AddIDRange(airNPC, 48, 49);
            airNPC.Add(51);
            AddIDRange(airNPC, 60, 62);
            airNPC.Add(66);
            AddIDRange(airNPC, 87, 94);
            AddIDRange(airNPC, 121, 122);
            airNPC.Add(133);
            airNPC.Add(137);
            AddIDRange(airNPC, 150, 152);
            airNPC.Add(156);
            AddIDRange(airNPC, 170, 171);
            airNPC.Add(173);
            airNPC.Add(176);
            airNPC.Add(180);
            airNPC.Add(182);
            AddIDRange(airNPC, 190, 194);
            airNPC.Add(205);
            airNPC.Add(224);
            airNPC.Add(226);
            AddIDRange(airNPC, 231, 235);
            airNPC.Add(252);
            airNPC.Add(258);
            AddIDRange(airNPC, 317, 318);
            AddIDRange(airNPC, 327, 328);
            airNPC.Add(347);
            airNPC.Add(370);
            airNPC.Add(388);
            AddIDRange(airNPC, 407, 408);
            AddIDRange(airNPC, 412, 414);
            AddIDRange(airNPC, 418, 421);
            AddIDRange(airNPC, 426, 428);
            AddIDRange(airNPC, 454, 459);
            airNPC.Add(509);
            airNPC.Add(541);
            airNPC.Add(551);
            AddIDRange(airNPC, 558, 560);
            AddIDRange(airNPC, 574, 575);
            airNPC.Add(581);
            airNPC.Add(587);
            #endregion air
            #region ice
            iceNPC.Add(147);
            iceNPC.Add(150);
            AddIDRange(iceNPC, 154, 155);
            iceNPC.Add(161);
            AddIDRange(iceNPC, 168, 171);
            iceNPC.Add(180);
            AddIDRange(iceNPC, 184, 185);
            iceNPC.Add(206);
            iceNPC.Add(218);
            iceNPC.Add(243);
            iceNPC.Add(343);
            iceNPC.Add(345);
            iceNPC.Add(352);
            iceNPC.Add(431);
            iceNPC.Add(628);
            #endregion ice
            #region poison
            AddIDRange(poisonNPC, -65, -56);
            AddIDRange(poisonNPC, -21, -16);
            poisonNPC.Add(41);
            poisonNPC.Add(141);
            AddIDRange(poisonNPC, 163, 165);
            poisonNPC.Add(176);
            poisonNPC.Add(204);
            AddIDRange(poisonNPC, 231, 238);
            AddIDRange(poisonNPC, 262, 265);
            poisonNPC.Add(468);
            AddIDRange(poisonNPC, 530, 531);
            #endregion poison
            #region light
            lightNPC.Add(44);
            lightNPC.Add(75);
            lightNPC.Add(84);
            lightNPC.Add(86);
            lightNPC.Add(120);
            lightNPC.Add(122);
            lightNPC.Add(137);
            lightNPC.Add(138);
            lightNPC.Add(171);
            lightNPC.Add(244);
            AddIDRange(lightNPC, 254, 261);
            lightNPC.Add(290);
            lightNPC.Add(400);
            AddIDRange(lightNPC, 402, 411);
            lightNPC.Add(493);
            lightNPC.Add(521);
            lightNPC.Add(522);
            lightNPC.Add(527);
            lightNPC.Add(545);
            AddIDRange(lightNPC, 634, 636);
            AddIDRange(lightNPC, 657, 660);
            lightNPC.Add(667);
            lightNPC.Add(676);

            #endregion light
            #region dark
            // add mimics and dungeon skeletons to this list plz
            AddIDRange(darkNPC, -43, -38);
            AddIDRange(darkNPC, -25, -22);
            AddIDRange(darkNPC, -14, -11);
            AddIDRange(darkNPC, -6, -5);
            AddIDRange(darkNPC, -2, -1);
            darkNPC.Add(2);
            AddIDRange(darkNPC, 4, 9);
            AddIDRange(darkNPC, 13, 16);
            AddIDRange(darkNPC, 29, 36);
            darkNPC.Add(45);
            darkNPC.Add(62);
            darkNPC.Add(66);
            darkNPC.Add(68);
            darkNPC.Add(71);
            darkNPC.Add(79);
            AddIDRange(darkNPC, 81, 83);
            darkNPC.Add(85);
            darkNPC.Add(94);
            AddIDRange(darkNPC, 98, 101);
            darkNPC.Add(109);
            AddIDRange(darkNPC, 112, 119);
            darkNPC.Add(121);
            darkNPC.Add(133);
            darkNPC.Add(140);
            AddIDRange(darkNPC, 156, 159);
            darkNPC.Add(162);
            darkNPC.Add(168);
            darkNPC.Add(178);
            darkNPC.Add(180);
            AddIDRange(darkNPC, 173, 174);
            AddIDRange(darkNPC, 179, 183);
            AddIDRange(darkNPC, 190, 194);
            AddIDRange(darkNPC, 195, 196);
            AddIDRange(darkNPC, 239, 242);
            darkNPC.Add(250);
            darkNPC.Add(253);
            AddIDRange(darkNPC, 266, 296);
            darkNPC.Add(301);
            AddIDRange(darkNPC, 304, 318);
            AddIDRange(darkNPC, 325, 330);
            darkNPC.Add(342);
            darkNPC.Add(351);
            darkNPC.Add(376);
            AddIDRange(darkNPC, 379, 380);
            AddIDRange(darkNPC, 396, 401);
            AddIDRange(darkNPC, 437, 441);
            AddIDRange(darkNPC, 454, 479);
            AddIDRange(darkNPC, 489, 490);
            darkNPC.Add(523);
            AddIDRange(darkNPC, 525, 526);
            AddIDRange(darkNPC, 528, 529);
            AddIDRange(darkNPC, 533, 534);
            AddIDRange(darkNPC, 543, 544);
            darkNPC.Add(546);
            darkNPC.Add(549);
            AddIDRange(darkNPC, 564, 567);
            AddIDRange(darkNPC, 586, 587);
            AddIDRange(darkNPC, 618, 624);
            AddIDRange(darkNPC, 629, 630);
            darkNPC.Add(662);
            AddIDRange(darkNPC, 665, 666);
            darkNPC.Add(668);
            #endregion dark
            #region electric
            // basically just martians and jellyfish
            AddIDRange(electricNPC, 63, 64);
            electricNPC.Add(103);
            AddIDRange(electricNPC, 125, 131);
            AddIDRange(electricNPC, 134, 136);
            electricNPC.Add(139);
            electricNPC.Add(324);
            AddIDRange(electricNPC, 345, 347);
            AddIDRange(electricNPC, 381, 395);
            electricNPC.Add(422);
            AddIDRange(electricNPC, 425, 429);
            electricNPC.Add(467);
            electricNPC.Add(578);
            #endregion electric
            #region magic
            AddIDRange(magicNPC, 29, 30);
            AddIDRange(magicNPC, 32, 34);
            magicNPC.Add(45);
            AddIDRange(magicNPC, 83, 84);
            magicNPC.Add(120);
            magicNPC.Add(179);
            AddIDRange(magicNPC, 281, 286);
            AddIDRange(magicNPC, 420, 421);
            AddIDRange(magicNPC, 423, 424);
            AddIDRange(magicNPC, 437, 440);
            AddIDRange(magicNPC, 454, 459);
            AddIDRange(magicNPC, 471, 472);
            magicNPC.Add(533);
            magicNPC.Add(549);
            AddIDRange(magicNPC, 564, 565);
            
            #endregion magic
        }
        private void AddMeleeItemsToSets()
        {
            #region fire
            fireItem.Add(ItemID.AshWoodSword);
            fireItem.Add(ItemID.FieryGreatsword);
            fireItem.Add(ItemID.DD2SquireDemonSword);
            fireItem.Add(ItemID.TheHorsemansBlade);
            fireItem.Add(ItemID.DD2SquireBetsySword);
            fireItem.Add(ItemID.HelFire);
            fireItem.Add(ItemID.ObsidianSwordfish);
            fireItem.Add(ItemID.Flamarang);
            fireItem.Add(ItemID.FlamingMace);
            fireItem.Add(ItemID.Sunfury);
            fireItem.Add(ItemID.ShadowFlameKnife);
            fireItem.Add(3858);
            fireItem.Add(ItemID.DayBreak);
            fireItem.Add(ItemID.SolarEruption);
            #endregion fire
            #region water
            waterItem.Add(ItemID.Muramasa);
            waterItem.Add(ItemID.PurpleClubberfish);
            waterItem.Add(ItemID.Bladetongue);
            waterItem.Add(ItemID.Kraken);
            waterItem.Add(ItemID.Swordfish);
            waterItem.Add(ItemID.BlueMoon);
            waterItem.Add(ItemID.DripplerFlail);
            waterItem.Add(ItemID.Flairon);
            #endregion water
            #region earth
            earthItem.Add(ItemID.BoneSword);
            earthItem.Add(ItemID.ChlorophyteClaymore);
            earthItem.Add(ItemID.ChlorophyteSaber);
            earthItem.Add(ItemID.Seedler);
            earthItem.Add(ItemID.TerraBlade);
            earthItem.Add(ItemID.JungleYoyo);
            earthItem.Add(ItemID.Yelets);
            earthItem.Add(ItemID.Terrarian);
            earthItem.Add(ItemID.ChlorophytePartisan);
            earthItem.Add(ItemID.ThornChakram);
            earthItem.Add(ItemID.FlowerPow);
            earthItem.Add(3835);
            #endregion earth
            #region air
            airItem.Add(ItemID.BatBat);
            airItem.Add(ItemID.BouncingShield);
            #endregion air
            #region ice
            iceItem.Add(ItemID.IceBlade);
            iceItem.Add(ItemID.IceSickle);
            iceItem.Add(ItemID.Frostbrand);
            iceItem.Add(ItemID.Amarok);
            iceItem.Add(ItemID.NorthPole);
            iceItem.Add(ItemID.IceBoomerang);
            #endregion ice
            #region poison
            poisonItem.Add(ItemID.Flymeal);
            poisonItem.Add(ItemID.BladeofGrass);
            poisonItem.Add(ItemID.BeeKeeper);
            poisonItem.Add(ItemID.HiveFive);
            #endregion poison
            #region light
            lightItem.Add(ItemID.BluePhaseblade);
            lightItem.Add(ItemID.GreenPhaseblade);
            lightItem.Add(ItemID.OrangePhaseblade);
            lightItem.Add(ItemID.PurplePhaseblade);
            lightItem.Add(ItemID.RedPhaseblade);
            lightItem.Add(ItemID.WhitePhaseblade);
            lightItem.Add(ItemID.YellowPhaseblade);
            lightItem.Add(ItemID.Pearlwood);
            lightItem.Add(ItemID.BluePhasesaber);
            lightItem.Add(ItemID.GreenPhasesaber);
            lightItem.Add(ItemID.OrangePhasesaber);
            lightItem.Add(ItemID.PurplePhasesaber);
            lightItem.Add(ItemID.RedPhasesaber);
            lightItem.Add(ItemID.WhitePhasesaber);
            lightItem.Add(ItemID.YellowPhasesaber);
            lightItem.Add(ItemID.Excalibur);
            lightItem.Add(ItemID.TrueExcalibur);
            lightItem.Add(ItemID.ChristmasTreeSword);
            lightItem.Add(ItemID.StarWrath);
            lightItem.Add(ItemID.Meowmere);
            lightItem.Add(ItemID.Gungnir);
            lightItem.Add(ItemID.MushroomSpear);
            lightItem.Add(ItemID.Shroomerang);
            lightItem.Add(ItemID.FlyingKnife);
            lightItem.Add(ItemID.BouncingShield);
            lightItem.Add(ItemID.LightDisc);
            lightItem.Add(ItemID.PaladinsHammer);
            lightItem.Add(ItemID.DaoofPow);
            lightItem.Add(ItemID.HallowJoustingLance);
            lightItem.Add(ItemID.PiercingStarlight);
            #endregion light
            #region dark
            darkItem.Add(ItemID.EbonwoodSword);
            darkItem.Add(ItemID.ShadewoodSword);
            darkItem.Add(ItemID.TentacleSpike);
            darkItem.Add(ItemID.LightsBane);
            darkItem.Add(ItemID.BloodButcherer);
            darkItem.Add(ItemID.PurpleClubberfish);
            darkItem.Add(ItemID.NightsEdge);
            darkItem.Add(ItemID.FetidBaghnakhs);
            darkItem.Add(ItemID.Bladetongue);
            darkItem.Add(ItemID.TrueNightsEdge);
            darkItem.Add(ItemID.CorruptYoyo);
            darkItem.Add(ItemID.CrimsonYoyo);
            darkItem.Add(ItemID.Kraken);
            darkItem.Add(ItemID.TheEyeOfCthulhu);
            darkItem.Add(ItemID.TheRottedFork);
            darkItem.Add(ItemID.DarkLance);
            darkItem.Add(ItemID.BloodyMachete);
            darkItem.Add(ItemID.PossessedHatchet);
            darkItem.Add(ItemID.BallOHurt);
            darkItem.Add(ItemID.TheMeatball);
            darkItem.Add(ItemID.DripplerFlail);
            darkItem.Add(ItemID.DaoofPow);
            darkItem.Add(ItemID.ShadowFlameKnife);
            darkItem.Add(ItemID.ScourgeoftheCorruptor);
            darkItem.Add(ItemID.VampireKnives);
            darkItem.Add(ItemID.ShadowJoustingLance);
            #endregion dark
            #region electric
            electricItem.Add(ItemID.InfluxWaver);
            electricItem.Add(ItemID.ThunderSpear);
            #endregion electric
            #region magic
            magicItem.Add(ItemID.Starfury);
            magicItem.Add(ItemID.EnchantedSword);
            magicItem.Add(ItemID.BeamSword);
            magicItem.Add(ItemID.TerraBlade);
            magicItem.Add(ItemID.StarWrath);
            magicItem.Add(ItemID.Meowmere);
            magicItem.Add(ItemID.Terrarian);
            magicItem.Add(3836);
            magicItem.Add(ItemID.EnchantedBoomerang);
            magicItem.Add(ItemID.Trimarang);
            #endregion magic
        }

        private void AddRangedItemsToSets()
        {
            #region fire
            fireItem.Add(ItemID.AshWoodBow);
            fireItem.Add(ItemID.HellwingBow);
            fireItem.Add(ItemID.MoltenFury);
            fireItem.Add(ItemID.ShadowFlameBow);
            fireItem.Add(ItemID.DD2PhoenixBow);
            fireItem.Add(ItemID.DD2BetsyBow);
            fireItem.Add(ItemID.PhoenixBlaster);
            fireItem.Add(ItemID.NailGun);
            fireItem.Add(ItemID.JackOLanternLauncher);
            fireItem.Add(ItemID.SnowmanCannon);
            fireItem.Add(3546);
            fireItem.Add(ItemID.ElectrosphereLauncher);
            fireItem.Add(ItemID.Celeb2);
            fireItem.Add(ItemID.MolotovCocktail);
            fireItem.Add(ItemID.FlareGun);
            fireItem.Add(ItemID.Flamethrower);
            fireItem.Add(ItemID.ElfMelter);
            #endregion fire
            #region water
            waterItem.Add(ItemID.BloodRainBow);
            waterItem.Add(ItemID.Tsunami);
            waterItem.Add(ItemID.Megashark);
            waterItem.Add(ItemID.Xenopopper);
            waterItem.Add(ItemID.SDMG);
            waterItem.Add(ItemID.AleThrowingGlove);
            waterItem.Add(ItemID.PiranhaGun);
            #endregion water
            #region earth
            earthItem.Add(ItemID.Marrow);
            earthItem.Add(ItemID.ChlorophyteShotbow);
            earthItem.Add(ItemID.VenusMagnum);
            earthItem.Add(ItemID.Stinger);
            earthItem.Add(ItemID.BoneJavelin);
            earthItem.Add(3378);
            earthItem.Add(ItemID.Sandgun);
            earthItem.Add(ItemID.PiranhaGun);
            #endregion earth
            #region air

            #endregion air
            #region ice
            iceItem.Add(ItemID.IceBow);
            iceItem.Add(ItemID.SnowmanCannon);
            iceItem.Add(ItemID.Snowball);
            iceItem.Add(ItemID.FrostDaggerfish);
            iceItem.Add(ItemID.SnowballCannon);
            iceItem.Add(ItemID.ElfMelter);
            #endregion ice
            #region poison
            poisonItem.Add(ItemID.PoisonedKnife);
            poisonItem.Add(ItemID.RottenEgg);
            poisonItem.Add(ItemID.BeesKnees);
            poisonItem.Add(ItemID.Beenade);
            poisonItem.Add(ItemID.Blowgun);
            poisonItem.Add(ItemID.Toxikarp);
            poisonItem.Add(ItemID.DartPistol);
            poisonItem.Add(ItemID.DartRifle);
            #endregion poison
            #region light
            lightItem.Add(ItemID.PearlwoodBow);
            lightItem.Add(ItemID.DaedalusStormbow);
            lightItem.Add(ItemID.HallowedRepeater);
            lightItem.Add(ItemID.StakeLauncher);
            lightItem.Add(4953);
            lightItem.Add(3546);
            lightItem.Add(ItemID.Celeb2);
            lightItem.Add(ItemID.StarCannon);
            lightItem.Add(ItemID.SuperStarCannon);
            lightItem.Add(ItemID.HolyWater);
            #endregion light
            #region dark
            darkItem.Add(ItemID.EbonwoodBow);
            darkItem.Add(ItemID.ShadewoodBow);
            darkItem.Add(ItemID.DemonBow);
            darkItem.Add(ItemID.TendonBow);
            darkItem.Add(ItemID.BloodRainBow);
            darkItem.Add(ItemID.ShadowFlameBow);
            darkItem.Add(ItemID.TheUndertaker);
            darkItem.Add(ItemID.OnyxBlaster);
            darkItem.Add(ItemID.Toxikarp);
            darkItem.Add(ItemID.UnholyWater);
            darkItem.Add(ItemID.BloodWater);
            #endregion dark
            #region electric
            electricItem.Add(ItemID.PulseBow);
            electricItem.Add(ItemID.Phantasm);
            electricItem.Add(ItemID.VortexBeater);
            electricItem.Add(ItemID.ElectrosphereLauncher);
            #endregion electric
            #region magic
            
            #endregion magic
        }

        private void AddMagicItemsToSets()
        {
            #region fire
            fireItem.Add(ItemID.WandofSparking);
            fireItem.Add(ItemID.FlowerofFire);
            fireItem.Add(ItemID.Flamelash);
            fireItem.Add(ItemID.MeteorStaff);
            fireItem.Add(ItemID.InfernoFork);
            fireItem.Add(3870);
            fireItem.Add(ItemID.HeatRay);
            fireItem.Add(ItemID.CursedFlames);
            fireItem.Add(ItemID.ClingerStaff);
            fireItem.Add(ItemID.SpiritFlame);
            fireItem.Add(ItemID.ShadowFlameHexDoll);
            #endregion fire
            #region water
            waterItem.Add(ItemID.AquaScepter);
            waterItem.Add(ItemID.CrystalSerpent);
            waterItem.Add(ItemID.BubbleGun);
            waterItem.Add(ItemID.WaterBolt);
            waterItem.Add(ItemID.GoldenShower);
            waterItem.Add(ItemID.RazorbladeTyphoon);
            waterItem.Add(ItemID.CrimsonRod);
            waterItem.Add(ItemID.NimbusRod);
            #endregion water
            #region earth
            earthItem.Add(ItemID.MeteorStaff);
            earthItem.Add(ItemID.NettleBurst);
            earthItem.Add(ItemID.Razorpine);
            earthItem.Add(ItemID.StaffofEarth);
            earthItem.Add(ItemID.LeafBlower);
            earthItem.Add(4270);
            #endregion earth
            #region air
            airItem.Add(ItemID.WeatherPain);
            airItem.Add(ItemID.SkyFracture);
            airItem.Add(3852);
            airItem.Add(ItemID.BatScepter);
            #endregion air
            #region ice
            iceItem.Add(ItemID.WandofFrosting);
            iceItem.Add(ItemID.FlowerofFrost);
            iceItem.Add(ItemID.FrostStaff);
            iceItem.Add(ItemID.BlizzardStaff);
            iceItem.Add(ItemID.IceRod);
            #endregion ice
            #region poison
            poisonItem.Add(ModContent.ItemType<WandOfPoisoning>());
            poisonItem.Add(ItemID.PoisonStaff);
            poisonItem.Add(ItemID.VenomStaff);
            poisonItem.Add(ItemID.BeeGun);
            poisonItem.Add(ItemID.WaspGun);
            poisonItem.Add(ItemID.ToxicFlask);
            #endregion poison
            #region light
            lightItem.Add(ItemID.MagicMissile);
            lightItem.Add(ItemID.CrystalSerpent);
            lightItem.Add(ItemID.CrystalVileShard);
            lightItem.Add(ItemID.RainbowRod);
            lightItem.Add(5065);
            lightItem.Add(ItemID.RainbowGun);
            lightItem.Add(ItemID.CrystalStorm);
            lightItem.Add(ItemID.MedusaHead);
            lightItem.Add(4852);
            lightItem.Add(ItemID.SparkleGuitar);
            lightItem.Add(ItemID.LastPrism);
            #endregion light
            #region dark
            darkItem.Add(ItemID.Vilethorn);
            darkItem.Add(3006);
            darkItem.Add(ItemID.UnholyTrident);
            darkItem.Add(ItemID.BatScepter);
            darkItem.Add(ItemID.ShadowbeamStaff);
            darkItem.Add(3870);
            darkItem.Add(ItemID.BookofSkulls);
            darkItem.Add(ItemID.DemonScythe);
            darkItem.Add(ItemID.CursedFlames);
            darkItem.Add(ItemID.GoldenShower);
            darkItem.Add(ItemID.CrimsonRod);
            darkItem.Add(ItemID.ClingerStaff);
            darkItem.Add(ItemID.SpiritFlame);
            darkItem.Add(ItemID.ShadowflameHadesDye);
            darkItem.Add(4270);
            #endregion dark
            #region electric
            electricItem.Add(ItemID.ThunderStaff);
            electricItem.Add(ItemID.ZapinatorGray);
            electricItem.Add(ItemID.ZapinatorOrange);
            electricItem.Add(ItemID.SpaceGun);
            electricItem.Add(ItemID.LaserRifle);
            electricItem.Add(ItemID.LaserMachinegun);
            electricItem.Add(ItemID.ChargedBlasterCannon);
            electricItem.Add(ItemID.MagnetSphere);
            #endregion electric
            #region magic
            magicItem.Add(3852);
            magicItem.Add(ItemID.SpectreStaff);
            magicItem.Add(ItemID.LunarFlareBook);
            magicItem.Add(ItemID.MagicDagger);
            magicItem.Add(ItemID.MagicalHarp);
            magicItem.Add(ItemID.NebulaArcanum);
            magicItem.Add(ItemID.NebulaBlaze);
            magicItem.Add(ItemID.LastPrism);
            #endregion magic
        }

        private void AddSummonerItemsToSets()
        {
            #region fire
            fireItem.Add(ItemID.ImpStaff);
            fireItem.Add(ItemID.DD2FlameburstTowerT1Popper);
            fireItem.Add(ItemID.DD2FlameburstTowerT2Popper);
            fireItem.Add(ItemID.DD2FlameburstTowerT3Popper);
            fireItem.Add(ItemID.DD2ExplosiveTrapT1Popper);
            fireItem.Add(ItemID.DD2ExplosiveTrapT2Popper);
            fireItem.Add(ItemID.DD2ExplosiveTrapT3Popper);
            fireItem.Add(ItemID.FireWhip);
            #endregion fire
            #region water
            waterItem.Add(ItemID.VampireFrogStaff);
            waterItem.Add(ItemID.SanguineStaff);
            waterItem.Add(ItemID.TempestStaff);
            #endregion water
            #region earth
            earthItem.Add(ItemID.PygmyStaff);
            earthItem.Add(4607);
            #endregion earth
            #region air
            airItem.Add(ItemID.BabyBirdStaff);
            airItem.Add(ItemID.RavenStaff);
            #endregion air
            #region ice
            iceItem.Add(ItemID.FlinxStaff);
            iceItem.Add(ItemID.StaffoftheFrostHydra);
            iceItem.Add(ItemID.CoolWhip);
            #endregion ice
            #region poison
            poisonItem.Add(ItemID.HornetStaff);
            poisonItem.Add(ItemID.SpiderStaff);
            poisonItem.Add(ItemID.QueenSpiderStaff);
            poisonItem.Add(4913);
            #endregion poison
            #region light
            lightItem.Add(4758);
            lightItem.Add(5005);
            lightItem.Add(ItemID.StardustCellStaff);
            lightItem.Add(ItemID.StardustDragonStaff);
            lightItem.Add(ItemID.RainbowCrystalStaff);
            lightItem.Add(ItemID.SwordWhip);
            lightItem.Add(ItemID.RainbowWhip);
            lightItem.Add(ItemID.MaceWhip);
            #endregion light
            #region dark
            darkItem.Add(ItemID.VampireFrogStaff);
            darkItem.Add(ItemID.SanguineStaff);
            darkItem.Add(ItemID.RavenStaff);
            darkItem.Add(ItemID.BoneWhip);
            darkItem.Add(ItemID.ScytheWhip);
            #endregion dark
            #region electric
            electricItem.Add(ItemID.OpticStaff);
            electricItem.Add(ItemID.DeadlySphereStaff);
            electricItem.Add(ItemID.XenoStaff);
            electricItem.Add(ItemID.DD2LightningAuraT1Popper);
            electricItem.Add(ItemID.DD2LightningAuraT2Popper);
            electricItem.Add(ItemID.DD2LightningAuraT3Popper);
            #endregion electric
            #region magic
            magicItem.Add(ItemID.AbigailsFlower);
            magicItem.Add(3569);
            #endregion magic
        }

        private void AddIDRange(HashSet<int> set, int start, int end)
        {
            for(int i = start; i <= end; i++)
            {
                set.Add(i);
            }
        }
        public override void Unload()
        {
            fireNPC.Clear();
            fireItem.Clear();
            waterNPC.Clear();
            waterItem.Clear();
            earthNPC.Clear();
            earthItem.Clear();
            airNPC.Clear();
            airItem.Clear();
            iceNPC.Clear();
            iceItem.Clear();
            poisonNPC.Clear();
            poisonItem.Clear();
            lightNPC.Clear();
            lightItem.Clear();
            darkNPC.Clear();
            darkItem.Clear();
            electricNPC.Clear();
            electricItem.Clear();
            magicNPC.Clear();
            magicItem.Clear();
        }
    }
    public class ElementPlayer : ModPlayer
    {
        const float SuperStrongDamageBonus = 0.5f;
        const float StrongDamageBonus = 0.2f;
        const float WeakDamageBonus = -0.2f;
        public static float getItemToNPCModifer(bool Fire, bool Water, bool Earth, bool Air, bool Ice, bool Poison, bool Light, bool Dark, bool Electric, bool Magic, NPC target)
        {
            float mod = 1f;
            bool Elementless = !Fire && !Water && !Earth && !Air && !Ice && !Poison && !Light && !Dark && !Electric && !Magic;
            bool targetElementless = true;

            if (Elements.fireNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += StrongDamageBonus;
            }
            if (Elements.waterNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.earthNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Earth) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Light) mod += StrongDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.airNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.iceNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Electric) mod += StrongDamageBonus;
            }
            if (Elements.poisonNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += StrongDamageBonus;
                if (Earth) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += WeakDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.lightNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Earth) mod += WeakDamageBonus;
                if (Ice) mod += StrongDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += SuperStrongDamageBonus;
            }
            if (Elements.darkNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Earth) mod += StrongDamageBonus;
                if (Light) mod += WeakDamageBonus;
                if (Dark) mod += WeakDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.electricNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Magic) mod += StrongDamageBonus;
                if (Fire) mod += WeakDamageBonus;
                if (Water) mod += SuperStrongDamageBonus;
                if (Earth) mod += WeakDamageBonus;
                if (Air) mod += WeakDamageBonus;
                if (Ice) mod += WeakDamageBonus;
                if (Poison) mod += StrongDamageBonus;
                if (Dark) mod += SuperStrongDamageBonus;
                if (Electric) mod += WeakDamageBonus;
            }
            if (Elements.magicNPC.Contains(target.netID))
            {
                targetElementless = false;
                if (Magic) mod += WeakDamageBonus;
                if (Fire) mod += StrongDamageBonus;
                if (Water) mod += StrongDamageBonus;
                if (Air) mod += StrongDamageBonus;
                if (Fire) mod += StrongDamageBonus;
                if(Elementless) mod += WeakDamageBonus;
            }
            if (targetElementless)
            {
                if(Magic) mod += StrongDamageBonus;
            }
            return MathHelper.Clamp(mod,0.1f,3f);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            target.life = target.lifeMax;
            ElementProjectile eProj = proj.GetGlobalProjectile<ElementProjectile>();
            modifiers.FinalDamage *= getItemToNPCModifer(eProj.Fire,eProj.Water,eProj.Earth,eProj.Air,eProj.Ice,eProj.Poison,eProj.Light,eProj.Dark,eProj.Electric,eProj.Magic, target);
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            target.life = target.lifeMax;
            modifiers.FinalDamage *= getItemToNPCModifer(Elements.fireItem.Contains(item.type), Elements.waterItem.Contains(item.type), Elements.earthItem.Contains(item.type), Elements.airItem.Contains(item.type), Elements.iceItem.Contains(item.type), Elements.poisonItem.Contains(item.type), Elements.lightItem.Contains(item.type), Elements.darkItem.Contains(item.type), Elements.electricItem.Contains(item.type), Elements.magicItem.Contains(item.type), target);
        }
    }
    public class ElementProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool Fire = false;
        public bool Water = false;
        public bool Earth = false;
        public bool Air = false;
        public bool Ice = false;
        public bool Poison = false;
        public bool Light = false;
        public bool Dark = false;
        public bool Electric = false;
        public bool Magic = false;
        public bool Elementless
        {
            get { return !Fire && !Water && !Earth && !Air && !Ice && !Poison && !Light && !Dark && !Electric && !Magic; }
            set { Fire = false; Water = false; Earth = false; Air = false; Ice = false; Poison = false; Light = false; Dark = false; Electric = false; Magic = false; }
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            // sets the element based on the item
            if (!ProjectileSets.DontInheritElementFromWeapon[projectile.type])
            {
                int item = Main.player[projectile.owner].HeldItem.type;
                if (Elements.fireItem.Contains(item))
                    Fire = true;
                if (Elements.waterItem.Contains(item))
                    Water = true;
                if (Elements.earthItem.Contains(item))
                    Earth = true;
                if (Elements.airItem.Contains(item))
                    Air = true;
                if (Elements.iceItem.Contains(item))
                    Ice = true;
                if (Elements.poisonItem.Contains(item))
                    Poison = true;
                if (Elements.lightItem.Contains(item))
                    Light = true;
                if (Elements.darkItem.Contains(item))
                    Dark = true;
                if (Elements.electricItem.Contains(item))
                    Electric = true;
                if (Elements.magicItem.Contains(item))
                    Magic = true;
            }
        }
    }
}
