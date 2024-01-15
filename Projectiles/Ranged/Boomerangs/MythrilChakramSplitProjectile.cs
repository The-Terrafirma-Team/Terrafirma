using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrafirmaRedux.Global.Templates;
using Terraria.DataStructures;

namespace TerrafirmaRedux.Projectiles.Ranged.Boomerangs
{
    public class MythrilChakramSplitProjectile : ChakramTemplate
    {
        protected override int BounceAmount => 10;
        protected override int BounceMode => 1;
        protected override float ReturnSpeed => 14f;

        public override void SetDefaults()
        {
            AttackTime = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.damage = 16;
            Projectile.width = 20;
            Projectile.height = 20;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;

        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            targetNPC = Utils.FindClosestNPC(600f, Projectile.Center, excludedNPCs: new NPC[1] { Main.npc[(int)Projectile.ai[2]] });
        }
    }
}
