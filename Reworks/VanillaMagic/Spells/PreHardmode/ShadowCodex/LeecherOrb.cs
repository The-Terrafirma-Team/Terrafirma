using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terrafirma.Particles;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.ShadowCodex
{
    internal class LeecherOrb : Spell
    {
        public override int UseAnimation => 48;
        public override int UseTime => 48;
        public override int ManaCost => 22;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => [ItemID.DemonScythe];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LeecherOrbProj>()] > 0)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.type == ModContent.ProjectileType<LeecherOrbProj>() && proj.owner == player.whoAmI && proj.timeLeft > 40) proj.timeLeft = 40;
                }
            }
            Projectile.NewProjectile(source, position, velocity * 10f, ModContent.ProjectileType<LeecherOrbProj>(), damage, knockback, player.whoAmI, 0, 0, 0);

            return false;
        }
    }
    public class LeecherOrbProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.timeLeft = 300;
            Projectile.Size = new Vector2(32, 32);
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            hitinfo.Crit = false;
            hitinfo.DamageType = DamageClass.Magic;
            hitinfo.Knockback = 0f;
        }

        NPC.HitInfo hitinfo = new NPC.HitInfo();
        public override void AI()
        {  
            if (Projectile.timeLeft < 40)
            {
                Projectile.Opacity -= 0.03f;
            }
            else
            {
                if (Projectile.ai[0] % 24 == 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (!Main.npc[i].friendly && Main.npc[i].active && Main.npc[i].Center.Distance(Projectile.Center) <= 200)
                        {
                            hitinfo.Damage = Projectile.damage / 6;
                            hitinfo.HitDirection = Projectile.Center.X > Main.npc[i].Center.X ? -1 : 1;
                            Main.player[Projectile.owner].StrikeNPCDirect(Main.npc[i], hitinfo);

                            int manaGain = Main.npc[i].NPCStats().Mana >= 8 ? 4 : Main.npc[i].NPCStats().Mana / 2;

                            if (manaGain > 0)
                            {
                                CombatText.NewText(Main.player[Projectile.owner].Hitbox, CombatText.HealMana, manaGain / 2, false);
                                Main.player[Projectile.owner].statMana += manaGain / 2;
                                Main.npc[i].DealManaDamage(Main.player[Projectile.owner], manaGain);
                            }

                            Vector2 dir = Projectile.Center.DirectionTo(Main.npc[i].Center);
                            for (int k = 0; k < Projectile.Center.Distance(Main.npc[i].Center) / 4; k++)
                            {
                                Vector2 pos = Projectile.Center + dir * k * 4;
                                Dust d = Dust.NewDustPerfect(pos, DustID.CorruptTorch, Vector2.Lerp(Projectile.velocity, Vector2.Zero, Projectile.Center.Distance(pos) / Projectile.Center.Distance(Main.npc[i].Center)), newColor: new Color(0.5f,0f,0f,0f), Alpha: 0);
                                d.noGravity = true;
                            }

                            for (int k = 0; k < 3; k++)
                            {
                                Dust d = Dust.NewDustPerfect(Main.npc[i].Center, DustID.CorruptTorch, Main.rand.NextVector2Circular(4f, 4f), Scale: 2f);
                                d.noGravity = true;
                            }
                        }
                    }
                }
            }

            if (Projectile.ai[0] % 6 == 0) Projectile.frame = Projectile.frame >= 3 ? 0 : Projectile.frame + 1;

            Projectile.ai[0]++;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 40;
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, (tex.Height() / 4) * Projectile.frame, tex.Width(), tex.Height() / 4),
                new Color(1f, 1f, 1f, 0.75f) * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(tex.Width(), tex.Height() / 4) / 2,
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, (tex.Height() / 4) * Projectile.frame, tex.Width(), tex.Height() / 4),
                new Color(1f,1f,1f,0f) * 0.5f * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(tex.Width(), tex.Height() / 4) / 2,
                Projectile.scale + 0.2f * (float)Math.Sin(Main.timeForVisualEffects / 10f),
                SpriteEffects.None);
            return false;
        }
    }
}