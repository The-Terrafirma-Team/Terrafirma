using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global.Templates;

public abstract class ChakramTemplate : ModProjectile 
{
    protected virtual int BounceAmount => 3;
    /// <summary>
    /// Moddifies how the Projectile will act when hitting an enemy. 
    /// 0 to make the projectile Bounce off the enemy.
    /// 1 to make the Projectile home in to the next closest enemy. 
    /// 2 to make the projectile Bounce off the enemy and home in to the next closest enemy 
    /// </summary>
    protected virtual int BounceMode => 0;
    /// <summary>
    /// How long the Projectile will fly before going back to the player
    /// </summary>
    protected virtual int AttackTime => 50;
    protected virtual float ReturnSpeed => 5f;

    /// <summary>
    /// How much time the projectile takes to accelerate back to the player, default value is 0.015f
    /// </summary>
    protected virtual float ReturnAcc => 0.015f;

    NPC targetNPC = null;
    public override void SetDefaults()
    {
        Projectile.penetrate = -1;
        Projectile.timeLeft = 3600;
        Projectile.friendly = true;
    }
    public override void AI()
    {
        Projectile.ai[0]++;
        Projectile.ai[1] = Math.Clamp(Projectile.ai[1] += ReturnAcc, 0f, 1f);
        if (BounceMode == 1 || BounceMode == 2)
        {
            if (targetNPC != null && Projectile.penetrate > -BounceAmount - 1 && Projectile.ai[0] < AttackTime)
            {
                Projectile.velocity += Projectile.Center.DirectionTo(targetNPC.Center) * (ReturnSpeed * (Projectile.ai[1] / 1f)) ;
                Projectile.velocity = Projectile.velocity.LengthClamp(ReturnSpeed);
            }
        }
        if (Projectile.ai[0] == AttackTime)
        {
            Projectile.ai[1] = 0f;
        }
        if (Projectile.ai[0] > AttackTime)
        {
            Projectile.velocity += Projectile.Center.DirectionTo(Main.player[Projectile.owner].MountedCenter) * (ReturnSpeed * (Projectile.ai[1] / 1f)) ;
            Projectile.velocity = Projectile.velocity.LengthClamp(ReturnSpeed);
        }
        Projectile.rotation += Projectile.velocity.Length() / 20f * Projectile.direction ;
        if (Projectile.ai[0] % 8 == 0)
        {
            SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
        }

        if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox) && Projectile.ai[0] > AttackTime) Projectile.Kill();
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Projectile.penetrate > -BounceAmount - 1)
        {
            switch (BounceMode)
            {
                case 0:
                    Projectile.velocity.X = Projectile.oldVelocity.X * -1;
                    Projectile.velocity.Y = Projectile.oldVelocity.Y * -1;
                    break;
                case 1:
                    if (targetNPC != null) targetNPC = Utils.FindClosestNPC(600f, Projectile.Center, excludedNPCs: new NPC[1] { targetNPC });
                    else targetNPC = Utils.FindClosestNPC(600f, Projectile.Center);
                    break;

                case 2:
                    Projectile.velocity.X = Projectile.oldVelocity.X * -1;
                    Projectile.velocity.Y = Projectile.oldVelocity.Y * -1;
                    if (targetNPC != null) targetNPC = Utils.FindClosestNPC(600f, Projectile.Center, excludedNPCs: new NPC[1] { targetNPC });
                    else targetNPC = Utils.FindClosestNPC(600f, Projectile.Center);
                    break;
                    
            }
            Projectile.penetrate -= 1;
            Projectile.ai[1] = 0f;
        }
        else Projectile.ai[0] = AttackTime;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.penetrate > -BounceAmount - 1)
        {
            if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = oldVelocity.X * -1;
            if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = oldVelocity.Y * -1;
        }
        else Projectile.ai[0] = AttackTime;
        Projectile.penetrate -= 1;


        return false;
    }
}