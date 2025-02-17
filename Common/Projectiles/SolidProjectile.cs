using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.Common.Projectiles
{
    public enum SolidProjectileCollisionType
    {
        Solid = 0,
        SemiSolid = 1
    }

    public abstract class SolidProjectile : ModProjectile
    {
        public virtual SolidProjectileCollisionType CollisionType => SolidProjectileCollisionType.Solid;
        public bool DoCollision = true;
        public override void OnSpawn(IEntitySource source)
        {
            SolidProjectiles.Projectiles.Add(this);
            base.OnSpawn(source);
        }

        public override void OnKill(int timeLeft)
        {
            SolidProjectiles.Projectiles.Remove(this);
            base.OnKill(timeLeft);
        }
    }
    public class SolidProjectiles
    {
        public static List<SolidProjectile> Projectiles = new List<SolidProjectile>() { };

    }

    public class SolidProjectilePlayer : ModPlayer
    {
        SolidProjectile TouchedProjectile = null;
        bool OnTopOfSolidProjectile = false;
        bool SolidProjectile_switch = false;
        int FallThroughTimer = 6;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            base.ProcessTriggers(triggersSet);
        }
        public override void PreUpdateMovement()
        {
            for (int i = 0; i < SolidProjectiles.Projectiles.Count; i++)
            {
                Projectile Proj = SolidProjectiles.Projectiles[i].Projectile;
                if (SolidProjectiles.Projectiles[i].Projectile.active)
                {
                    if (SolidProjectiles.Projectiles[i].CollisionType == SolidProjectileCollisionType.Solid && SolidProjectiles.Projectiles[i].DoCollision)
                    {
                        //Top Side
                        if (Player.Bottom.Y > Proj.position.Y &&
                            Player.Top.Y < Proj.position.Y + 16 &&
                            Player.Right.X > Proj.position.X + 2 &&
                            Player.position.X < Proj.Right.X - 2)
                        {

                            Player.position.Y = Proj.position.Y - Player.height + Player.gravity + Proj.velocity.Y;
                            Player.velocity.Y = Math.Clamp(Player.velocity.Y, -10000, 0);
                            TouchedProjectile = SolidProjectiles.Projectiles[i];
                            OnTopOfSolidProjectile = true;
                            SolidProjectile_switch = false;

                            if (Math.Abs(Proj.velocity.X) > 0)
                            {
                                Player.position.X += Proj.velocity.X;
                            }
                        }

                        //Bottom Side
                        if (Player.Bottom.Y > Proj.position.Y + Proj.height - 2 &&
                            Player.position.Y < Proj.Bottom.Y &&
                            Player.Right.X > Proj.position.X + 2 &&
                            Player.position.X < Proj.Right.X - 2 &&
                            Player.velocity.Y < 0)
                        {
                            Player.position.Y = Proj.position.Y + Proj.height;
                            Player.velocity.Y = Math.Clamp(Player.velocity.Y, Proj.velocity.Y <= 0 ? Proj.velocity.Y : 0, 10000);
                        }

                        //Left Side
                        if (Player.Right.X > Proj.position.X &&
                            Player.Left.X < Proj.position.X + Proj.width / 2 &&
                            Player.Bottom.Y > Proj.position.Y + 16 &&
                            Player.position.Y < Proj.Bottom.Y - 2)
                        {
                            Player.position.X = Proj.position.X - Player.width + Player.runAcceleration;
                            Player.velocity.X = Math.Clamp(Player.velocity.X, -10000, Proj.velocity.X >= 0 ? Proj.velocity.X : 0);
                        }

                        //Right Side
                        if (Player.position.X < Proj.Right.X &&
                            Player.Right.X > Proj.position.X + Proj.width / 2 &&
                            Player.Bottom.Y > Proj.position.Y + 16 &&
                            Player.position.Y < Proj.Bottom.Y - 2)
                        {
                            Player.position.X = Proj.Right.X - Player.runAcceleration;
                            Player.velocity.X = Math.Clamp(Player.velocity.X, Proj.velocity.X <= 0 ? Proj.velocity.X : 0, 10000);
                        }
                    }
                    else if (SolidProjectiles.Projectiles[i].CollisionType == SolidProjectileCollisionType.SemiSolid && SolidProjectiles.Projectiles[i].DoCollision)
                    {
                        //Top Side
                        if (Player.Bottom.Y > Proj.position.Y &&
                            Player.Bottom.Y < Proj.position.Y + 6 &&
                            Player.Right.X > Proj.position.X &&
                            Player.position.X < Proj.Right.X &&
                            Player.velocity.Y > 0 &&
                            FallThroughTimer <= 0)
                        {

                            Player.position.Y = Proj.position.Y - Player.height + Player.gravity + Proj.velocity.Y;
                            Player.velocity.Y = Math.Clamp(Player.velocity.Y, -10000, 0);
                            TouchedProjectile = SolidProjectiles.Projectiles[i];
                            OnTopOfSolidProjectile = true;
                            SolidProjectile_switch = false;

                            if (Math.Abs(Proj.velocity.X) > 0)
                            {
                                Player.position.X += Proj.velocity.X;
                            }
                        }
                    }
                }
            }

            if (PlayerInput.Triggers.JustPressed.Down) FallThroughTimer = 6;
            if (PlayerInput.Triggers.Current.Down) FallThroughTimer = 6;
            FallThroughTimer--;

            if (!OnTopOfSolidProjectile && !SolidProjectile_switch && TouchedProjectile != null && TouchedProjectile.Projectile.active)
            {
                Player.velocity += TouchedProjectile.Projectile.velocity;
                SolidProjectile_switch = true;
            }

            if (TouchedProjectile != null && !TouchedProjectile.Projectile.active) TouchedProjectile = null;

            OnTopOfSolidProjectile = false;
            base.PreUpdateMovement();
        }
    }
}
