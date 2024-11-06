using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace Terrafirma.Common.Interfaces
{
    public interface IHasTertriaryFunction
    {
        bool canUseTertriary(Player player);
        void TertriaryShoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback);
    }
}
