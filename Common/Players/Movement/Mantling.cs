using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Players.Movement
{
    public class Mantling : ModPlayer
    {
        public int MantleTimer = 0;
        public int MantleDirection = 0;
        public override void ResetEffects()
        {

            if (MantleTimer > 0)
                MantleTimer--;
            else
                MantleDirection = Player.direction;
        }
        public override void PostUpdate()
        {
            if (MantleTimer > 0)
            {
                Player.velocity.X += MantleDirection * 0.1f;

                if (!Collision.SolidCollision(new Vector2(Player.position.X + MantleDirection * 0.7f,Player.position.Y - 16), Player.width, Player.height))
                    Player.position.X += MantleDirection * 0.7f;
                if (!Collision.SolidCollision(new Vector2(Player.position.X, Player.position.Y + Player.gravDir * -0.4f), Player.width, Player.height))
                    Player.position.Y += Player.gravDir * -0.4f;

                if (!Player.ItemAnimationActive)
                {
                    switch (MantleTimer / 3)
                    {
                        case 5:
                            Player.bodyFrame.Y = Player.bodyFrame.Height * PlayerAnimation.PointUpRight;
                            break;
                        case 4:
                            Player.bodyFrame.Y = Player.bodyFrame.Height * PlayerAnimation.PointRight;
                            break;
                        case 3:
                            Player.bodyFrame.Y = Player.bodyFrame.Height * PlayerAnimation.PointDownRight;
                            break;
                        case 2:
                            Player.bodyFrame.Y = Player.bodyFrame.Height * PlayerAnimation.PointDown;
                            break;
                    }
                }
            }
        }
        public override void PostUpdateEquips()
        {
            Point worldPos = Player.Center.ToTileCoordinates();
            if (Player.grapCount == 0 && (WorldGen.SolidTile(worldPos.X + Player.direction, worldPos.Y - (int)(Player.gravDir * 1)) || WorldGen.SolidTile(worldPos.X + Player.direction, worldPos.Y)) && !WorldGen.SolidTile(worldPos.X + Player.direction, worldPos.Y - (int)(Player.gravDir * 2f)))
            {
                Player.blockExtraJumps = true;
                if (PlayerInput.Triggers.JustPressed.Jump)
                {
                    Player.RefreshExtraJumps();
                    if (Player.gravDir == 1)
                    {
                        Player.velocity.Y = MathF.Min(Player.velocity.Y, -7);
                    }
                    else
                    {
                        Player.velocity.Y = MathF.Max(Player.velocity.Y, 7);
                    }
                    SoundEngine.PlaySound(SoundID.Dig, Player.position);
                    MantleTimer = 15;
                }
            }
        }
    }
}
