using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Vanity.Dedicated
{
    [AutoloadEquip(EquipType.Head)]
    public class KinMask : ModItem
    {
        public bool Janky = false;
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("[c/A58CFF:Dedicated to Inkgum]");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Red; //ModContent.RarityType<KinRarity>();
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
    }

    public class KinEyeGlow : PlayerDrawLayer
    {
        private Texture2D GlowTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Items/Vanity/Dedicated/KinMask_Head_Glow").Value;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return true;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;
            SpriteEffects spriteEffects;
            if (p.gravDir == 1f)
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.None;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 0f;
                    p.headPosition.Y = 0f;
                    p.bodyPosition.Y = 0f;
                }
            }
            else
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 6f;
                    p.headPosition.Y = 6f;
                    p.bodyPosition.Y = 6f;
                }
            }
            Color EyeColor = Color.White;
            if(p.statLife <= p.statLifeMax / 2 && Main.rand.NextBool(1))
            {
                int gray = Main.rand.Next(64,256);
                EyeColor = new Color(gray, gray, gray,0);
            }
            var vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);
            if (p.head == EquipLoader.GetEquipSlot(ModContent.GetInstance<TerrafirmaRedux>(), "KinMask", EquipType.Head))
            {
                var value = new DrawData(GlowTexture,
                    new Vector2(
                        (int)(drawInfo.Position.X - Main.screenPosition.X - (p.bodyFrame.Width / 2) + (p.width / 2)),
                        (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height + 4f)) +
                    p.headPosition + vector3, p.bodyFrame, EyeColor, p.headRotation, vector3, 1f, spriteEffects, 0);
                value.shader = drawInfo.cHead;
                drawInfo.DrawDataCache.Add(value);
            }
        }
    }
    public class KinPlayer : ModPlayer
    {
        public bool KinSet = false;

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            int j;
            if(Player.direction > 0)
            {
                j = -10;
            }
            else
            {
                j = 0;
            }
            if (KinSet && Player.statLife <= Player.statLifeMax / 2 && Main.rand.NextBool(3))
            {
                int d3 = Dust.NewDust(Player.Center + new Vector2(j,-20), 0, 0, DustID.Smoke, Main.rand.NextFloat(0, 0), Main.rand.NextFloat(-2, -1), 165, Color.Black, Main.rand.NextFloat(0.5f, 1.5f));
                Main.dust[d3].noGravity = true;
            }
            if (KinSet && Player.statLife <= Player.statLifeMax / 3 && Main.rand.NextBool(18))
            {
                int d3 = Dust.NewDust(Player.Center + new Vector2(j, -20), 0, 0, DustID.Torch, Main.rand.NextFloat(0, 0), Main.rand.NextFloat(0, 0), 165, Color.White, Main.rand.NextFloat(0.5f, 1.5f));
                Main.dust[d3].noGravity = true;
                Main.dust[d3].noLight = true;
            }
        }
        //public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        //{
        //    if (KinSet == true)
        //    {
        //        SoundEngine.PlaySound(SoundID.NPCHit4);
        //        playSound = false;
                
        //    }
        //    return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        //}
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (KinSet == true)
            {
                playSound = false;
                SoundEngine.PlaySound(SoundID.NPCDeath44);
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

                for (int i = 0; i < 45; i++)
                {
                    int d = Dust.NewDust(Player.Center, 0, 0, DustID.InfernoFork, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 0, default, Main.rand.NextFloat(0.5f, 1.5f));
                    Main.dust[d].noGravity = true;

                    int d2 = Dust.NewDust(Player.Center, 0, 0, DustID.InfernoFork, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 0), 0, default, Main.rand.NextFloat(0.5f, 1.5f));
                    Main.dust[d2].noGravity = false;

                    int d3 = Dust.NewDust(Player.Center, 0, 0, DustID.Smoke, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-2, 1), 165, Color.Black, Main.rand.NextFloat(0.5f, 1.5f));
                    Main.dust[d3].noGravity = false;
                }
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void ResetEffects()
        {
            if (Player.head == EquipLoader.GetEquipSlot(ModContent.GetInstance<TerrafirmaRedux>(), "KinMask", EquipType.Head) && Player.body == EquipLoader.GetEquipSlot(ModContent.GetInstance<TerrafirmaRedux>(), "KinStripedSweater", EquipType.Body) && Player.legs == EquipLoader.GetEquipSlot(ModContent.GetInstance<TerrafirmaRedux>(), "KinMechanicalLegs", EquipType.Legs))
            {
                KinSet = true;
            }
            else
            {
                KinSet = false;
            }
        }
    }
}
