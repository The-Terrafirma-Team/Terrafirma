//using Terrafirma.Buffs.Debuffs;
//using Terraria;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.ModLoader;

//namespace Terrafirma.Reworks.HardmodeOres.Cobalt
//{
//    public class CobaltArmor : ModPlayer
//    {
//        public bool CobaltSetMelee = false;
//        public bool CobaltSetRange = false;
//        public bool CobaltSetMage = false;
//        float CobaltSpeed = 0f;
//        public override void ResetEffects()
//        {
//            if (CobaltSpeed > 0f)
//            {
//                CobaltSpeed -= 0.003f;
//            }
//            CobaltSetMelee = false;
//            CobaltSetRange = false;
//            CobaltSetMage = false;
//        }
//        public override void PostUpdateEquips()
//        {
//            if (Player.body == 17 && Player.legs == 16)
//            {
//                if (Player.head == 29)
//                {
//                    Player.setBonus = Language.GetTextValue("ArmorSetBonus.CobaltCaster");
//                    Player.manaCost += 0.14f;
//                }
//                else if (Player.head == 30)
//                {
//                    CobaltSetMelee = true;
//                    Player.setBonus = Language.GetTextValue("ArmorSetBonus.CobaltMelee");
//                    Player.GetAttackSpeed(DamageClass.Melee) += -0.15f + CobaltSpeed;
//                }
//                else if (Player.head == 31)
//                {
//                    Player.setBonus = Language.GetTextValue("ArmorSetBonus.CobaltRanged");
//                }
//            }
//        }
//        public override void ModifyItemScale(Item item, ref float scale)
//        {
//            if(CobaltSetMelee && item.DamageType == DamageClass.Melee) //Melee
//            {
//                scale += 0.45f;
//            }
//        }
//        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
//        {
//            if (CobaltSetMelee && (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed)) //Melee
//            {
//                CobaltSpeed += 0.1f;
//                if (CobaltSpeed >= 0.6)
//                {
//                    CobaltSpeed = 0.8f;
//                    target.AddBuff(ModContent.BuffType<ElectricCharge>(), 60 * 3);
//                }
//            }
//        }
//        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
//        {
//            if (CobaltSetMelee && proj.IsTrueMeleeProjectile()) //Melee
//            {
//                CobaltSpeed += 0.1f;
//                if (CobaltSpeed >= 0.6)
//                {
//                    CobaltSpeed = 0.8f;
//                    target.AddBuff(ModContent.BuffType<ElectricCharge>(), 60 * 3);
//                }
//            }
//        }
//    }
//}
