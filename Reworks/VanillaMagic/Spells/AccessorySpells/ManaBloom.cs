using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.AccessorySpells
{
    internal class ManaBloom : Spell
    {
        public override int UseAnimation => 45;
        public override int UseTime => 45;
        public override int ManaCost => 0;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Accessories/ManaBloom";
        public override int[] SpellItem => new int[] {
            ItemID.ManaFlower,
            ItemID.ArcaneFlower,
            ItemID.MagnetFlower,
            ItemID.ManaCloak };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<ManaBloomProj>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override bool? UseItem(Item item, Player player)
        {
            CombatText.NewText(player.Hitbox, CombatText.HealMana, 10, false);
            player.statMana += 10;
            return true;
        }
    }

    public class ManaBloomProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.Opacity = 0f;
            Projectile.timeLeft = 60;
            Projectile.Size = new Vector2(4);
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Main.player[Projectile.owner] == Main.LocalPlayer && Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedByRandom(2f), Type, 0, 0, Projectile.owner, 0, 1, 0);
                    newproj.timeLeft = Main.rand.Next(40, 60);
                }
                Projectile.Kill();
            }
            if (Projectile.ai[1] == 1)
            {

                if (Projectile.timeLeft < 30)
                {
                    //Projectile.velocity = Projectile.Center.DirectionTo(Main.player[Projectile.owner].MountedCenter) * 2f;
                    Projectile.velocity += Vector2.Lerp(Projectile.velocity, Main.player[Projectile.owner].MountedCenter - Projectile.Center, 0.5f);
                    Projectile.velocity = Projectile.velocity.LengthClamp(16f);
                    if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox)) Projectile.Kill();
                }
                else
                {
                    Projectile.velocity *= 0.925f;
                }

            }
            Dust newDust = Dust.NewDustPerfect(Projectile.Center, DustID.ManaRegeneration, Vector2.Zero, 0, new Color(255, 255, 255, 0), 1f);
            newDust.noGravity = true;
            newDust.noLight = true;

        }
    }
}
