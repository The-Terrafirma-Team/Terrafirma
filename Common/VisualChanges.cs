using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class VisualChanges : ModSystem
    {
        const string AssetFolder = "Terrafirma/Assets/Resprites/";
        public override void Load()
        {
            TextureAssets.Item[ItemID.DemonScythe] = ModContent.Request<Texture2D>(AssetFolder + "ShadowCodex");
        }
    }
}
