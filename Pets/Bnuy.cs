using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Pets
{
    public class Bnuy : ModItem
    {
        public override string Texture => "Terrafirma/Pets/BnuyItem";
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.Size = new Vector2(22);
            Item.UseSound = SoundID.Item20;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 3, 0);
            Item.DefaultToVanitypet(ModContent.BuffType<BnuyBuff>(), ModContent.BuffType<BnuyBuff>());
        }
        public override bool? UseItem(Player player)
        {
            if (player == Main.LocalPlayer)
                player.AddBuff(ModContent.BuffType<BnuyBuff>(),36000);
            return base.UseItem(player);
        }
    }
    public class BnuyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.projPet[Type] = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<BnuyProj>());
        }
    }
    public class BnuyProj : ModProjectile
    {
        int AnimSpeed = 0;
        bool flyup = false;
        string[] quotes = {
            "KISS MY NUTS",
            "YAY",
            "good lird",
            "fond of gay people",
            "boy you so jolly",
            "goin on a journey",
            "got all my snacks packed",
            "reconsider",
            "i am at my limit",
            "I dont feel real",
            "I'm losing my mind",
            "me when i me",
            "i overthink too much",
            "face reveal",
            "U dont even care or understand",
            "i love you",
            "give me a hug, please",
            "its an evil world out hear",
            "we are but slaves to work",
            "do u wanna see my everskies?",
            "i am just so shy",
            "this is me if u even care",
            "sometimes",
            "me when the ground is firm",
            "wanna collab in geometry dash",
            "things",
            "my uncle works for relogic you're getting banned",
            "I am Ayumu Kasuga"
        };
        public override string Texture => "Terrafirma/Pets/Bnuy";
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 360000;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(34, 30);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = Projectile.Bottom.Y + 20 < Main.player[Projectile.owner].Bottom.Y;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            AnimSpeed = Math.Abs(Projectile.velocity.X) > 0.5f? 10 - (int)Math.Clamp(Math.Abs(Projectile.velocity.X * 2),0,9) : 0;

            if (Projectile.ai[0] == 0) Projectile.velocity.Y = 2f;

            if (Projectile.ai[0] % AnimSpeed == 0 && AnimSpeed > 0) Projectile.frame = Projectile.frame == 6 ? 0 : Projectile.frame + 1;
            if (AnimSpeed == 0) Projectile.frame = 0;

            if (  Math.Abs(Projectile.Center.X - Main.player[Projectile.owner].MountedCenter.X) > 100f) Projectile.velocity.X = ( Main.player[Projectile.owner].MountedCenter.X - Projectile.Center.X) / 100f;
            else Projectile.velocity.X = MathHelper.Lerp(Projectile.velocity.X, 0, 0.05f);

            if (Projectile.Bottom.Y - Main.player[Projectile.owner].Bottom.Y > 200f) flyup = true; Projectile.tileCollide = false;

            if (Projectile.Bottom.Y - Main.player[Projectile.owner].Bottom.Y <= 0) flyup = false; Projectile.tileCollide = true;

            if (flyup) 
            { 
                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y - 0.2f, -7f, 0f); 
            }
            else Projectile.velocity.Y += 0.2f;

            if (Math.Abs(Projectile.velocity.Y) > 0.5f) Projectile.frame = 4;

            if (Projectile.ai[0] % (60 * 30) == 0) CombatText.NewText(Projectile.Hitbox, Color.White, quotes[Main.rand.Next(quotes.Length)], false);


            Projectile.spriteDirection = Projectile.direction;

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0,1), new Rectangle(0, (tex.Height / 7) * Projectile.frame, tex.Width, tex.Height / 7), lightColor ,0, new Vector2(tex.Width, tex.Height / 7) / 2f, 1, Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            return false;
        }
    }
}
