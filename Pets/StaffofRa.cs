using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Pets
{
    public class StaffofRa : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<SunofRa>();
            Item.Size = new Vector2(22);
            Item.UseSound = SoundID.Item20;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 3, 0);
            Item.buffType = ModContent.BuffType<SunofRaBuff>();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
        }
        public override bool? UseItem(Player player)
        {
            if (player == Main.LocalPlayer)
                player.AddBuff(ModContent.BuffType<SunofRaBuff>(),3600);
            return base.UseItem(player);
        }
    }
    public class SunofRaBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<SunofRa>());
        }
    }
    public class SunofRa : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(40);
        }
        public override void AI()
        {
            Projectile.ai[0]+= 0.1f;
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<SunofRaBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            Projectile.Center = Vector2.SmoothStep(Projectile.Center,player.MountedCenter + new Vector2(-player.width * player.direction,-22 + (float)(Math.Sin(Projectile.ai[0]) * 4)),0.1f + (0.01f * player.velocity.Length()));

            if (Projectile.position.Distance(player.position) > 5000)
                Projectile.position = player.MountedCenter;
            
            Lighting.AddLight(Projectile.Center, new Vector3(5,4.6f,1));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height),new Color(255,255,255,0),0,tex.Size() / 2f, 1, SpriteEffects.None);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 128, 128, 0) * 0.5f, (float)Main.timeForVisualEffects * 0.05f, tex.Size() / 2f, 1 + (float)(Math.Sin(Main.timeForVisualEffects * 0.01f) * 0.3f), SpriteEffects.None);
            return false;
        }
    }
    public class SunOfRaSystem : ModSystem
    {
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            if(Main.LocalPlayer.HasBuff(ModContent.BuffType<SunofRaBuff>()))
            {
                tileColor = Color.White;
                backgroundColor = Color.White;
            }
        }
    }
}
