using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.Dusts
{
    internal class PoisonDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = Main.rand.NextFloat(0.3f,0.4f);
            dust.velocity.X *= Main.rand.NextFloat(-1f,1f);
            dust.position += new Vector2(Main.rand.Next(-4, 4));
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.scale *= 0.9f;
            return false;
        }
    }
}
