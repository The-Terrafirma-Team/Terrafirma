using Microsoft.Xna.Framework.Graphics;
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
            TextureAssets.Item[ItemID.Vilethorn] = ModContent.Request<Texture2D>(AssetFolder + "VileStaff");
        }
    }
}
