using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Dungeon
{
    internal class BoneFragmentStorm : Spell
    {
        public override int UseAnimation => 5;
        public override int UseTime => 5;
        public override int ManaCost => 4;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/SpellBooks/BoneFragmentStorm";
        public override int[] SpellItem => new int[] { ItemID.BookofSkulls };

        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<BoneFragment>();
            damage = (int)(damage * 0.5f);
            velocity += velocity * 2f + new Vector2(0, Main.rand.NextFloat(-1f, 1f)).RotatedBy(velocity.ToRotation());

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class BoneFragment : ModProjectile
    {
        public override string Texture => $"Terrafirma/Reworks/VanillaMagic/Spells/Dungeon/BoneFragments";

        Vector2 playerpos = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(14, 14);
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.3f;
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.TileCollision(Projectile.position, Projectile.velocity / 2, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnKill(int timeLeft)
        {
            SoundStyle bonesound = SoundID.NPCHit2;
            bonesound.Volume = 0.2f;
            bonesound.PitchRange = (-0.2f, 0.2f);
            SoundEngine.PlaySound(bonesound, Projectile.position);
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }
    }

}
