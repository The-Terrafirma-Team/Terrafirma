using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global.Templates;

public abstract class SpearTemplate : ModProjectile // Thanks example mod ! ! ! 
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Spear);
        //Projectile.aiStyle = -1;
    }
    public bool JustSpawned = true;
    bool Turned = false;
    protected virtual float HoldoutRangeMin => 24f;
    protected virtual float HoldoutRangeMax => 96f;
    public override void AI()
    {

        Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
        int duration = (int)(player.itemAnimationMax * 1.3f); // Define the duration the projectile will exist in frames

        if (JustSpawned)
        {
            Projectile.scale *= player.HeldItem.scale;
            JustSpawned = false;
            Projectile.Resize((int)(Projectile.width * Projectile.scale), (int)(Projectile.width * Projectile.scale));
        }

        player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

        // Reset projectile time left if necessary
        if (Projectile.timeLeft > duration)
        {
            Projectile.timeLeft = duration;
        }

        Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

        float halfDuration = duration * 0.5f;
        float progress;
        //if(duration == halfDuration) 
        //{
        //    Projectile.ResetLocalNPCHitImmunity();
        //}

        // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
        if (Projectile.timeLeft < halfDuration)
        {
            if (!Turned)
            {
                Turned = true;
                OnTurnAround(player);
            }
            progress = Projectile.timeLeft / halfDuration;
        }
        else
        {
            progress = (duration - Projectile.timeLeft) / (duration * 0.5f);
        }

        // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
        Projectile.Center = player.MountedCenter + Vector2.Lerp(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax * Projectile.scale, progress);
        // Apply proper rotation to the sprite.
        if (Projectile.spriteDirection == -1)
        {
            // If sprite is facing left, rotate 45 degrees
            Projectile.rotation += MathHelper.ToRadians(45f);
        }
        else
        {
            // If sprite is facing right, rotate 135 degrees
            Projectile.rotation += MathHelper.ToRadians(135f);
        }
        if (Projectile.timeLeft <= duration * 0.3f)
        {
            Projectile.timeLeft = 0;
            player.SetDummyItemTime(0);
        }
    }
    public virtual void OnTurnAround(Player player)
    {

    }
}

//public abstract class SpearTemplate2 : ModProjectile // Thanks example mod ! ! ! :3
//{
//    public override void SetDefaults()
//    {
//        Projectile.CloneDefaults(ProjectileID.Spear);
//        //Projectile.aiStyle = -1;
//    }
//    protected virtual float HoldoutRangeMin => 24f;
//    protected virtual float HoldoutRangeMax => 96f;
//    public override void AI()
//    {
//        Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
//        int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

//        player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

//        // Reset projectile time left if necessary
//        if (Projectile.timeLeft > duration)
//        {
//            Projectile.timeLeft = duration;
//        }

//        Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

//        float halfDuration = duration * 0.5f;
//        float progress;
//        //if(duration == halfDuration) 
//        //{
//        //    Projectile.ResetLocalNPCHitImmunity();
//        //}

//        // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
//        if (Projectile.timeLeft < halfDuration)
//        {
//            progress = Projectile.timeLeft / halfDuration;
//        }
//        else
//        {
//            progress = (duration - Projectile.timeLeft) / (duration * 0.5f);
//        }

//        // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
//        Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
//        // Apply proper rotation to the sprite.
//        if (Projectile.spriteDirection == -1)
//        {
//            // If sprite is facing left, rotate 45 degrees
//            Projectile.rotation += MathHelper.ToRadians(45f);
//        }
//        else
//        {
//            // If sprite is facing right, rotate 135 degrees
//            Projectile.rotation += MathHelper.ToRadians(135f);
//        }
//        if (Projectile.timeLeft <= 0)
//        {
//            Projectile.timeLeft = 0;
//            player.SetDummyItemTime(0);
//        }
//    }
//}