using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global.Templates;

public abstract class SpearTemplate : ModProjectile // Thanks example mod ! ! ! 
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Spear);
        //Projectile.aiStyle = -1;
    }
    protected virtual float HoldoutRangeMin => 24f;
    protected virtual float HoldoutRangeMax => 96f;
    public override void AI()
    {
        Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
        int duration = (int)(player.itemAnimationMax * 1.3f); // Define the duration the projectile will exist in frames

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
            progress = Projectile.timeLeft / halfDuration;
        }
        else
        {
            progress = (duration - Projectile.timeLeft) / (duration * 0.5f);
        }

        // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
        Projectile.Center = player.MountedCenter + Vector2.Lerp(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
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
    public void DrawPointyStabbyLight(int Intensity, Color color, Vector2 Scale, Vector2 Offset)
    {
        Offset = Offset.RotatedBy(Projectile.rotation);
        Offset.X *= -Projectile.direction;
        Player player = Main.player[Projectile.owner];
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Sparkly").Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;

        float halfDuration = player.itemAnimationMax * 0.5f;
        float timeTillRemove = (float)player.itemAnimation / player.itemAnimationMax;
        Scale.X *= (timeTillRemove * 2);
        Scale.Y /= timeTillRemove;
        if (Projectile.timeLeft > player.itemAnimationMax / 2)
        {
            int j = Intensity;
            for (int i = 1; i < Intensity; i++)
            {
                j--;
                Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + Offset.RotatedBy(Projectile.rotation) + new Vector2(0, i * 10 * Scale.Y * -Projectile.direction).RotatedBy(Projectile.rotation), frame, color * (j), Projectile.rotation, frameOrigin, Scale - new Vector2(i / Intensity), SpriteEffects.None);
            }
        }
    }
}

public abstract class SpearTemplate2 : ModProjectile // Thanks example mod ! ! ! :3
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Spear);
        //Projectile.aiStyle = -1;
    }
    protected virtual float HoldoutRangeMin => 24f;
    protected virtual float HoldoutRangeMax => 96f;
    public override void AI()
    {
        Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
        int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

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
            progress = Projectile.timeLeft / halfDuration;
        }
        else
        {
            progress = (duration - Projectile.timeLeft) / (duration * 0.5f);
        }

        // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
        Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
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
        if (Projectile.timeLeft <= 0)
        {
            Projectile.timeLeft = 0;
            player.SetDummyItemTime(0);
        }
    }
}