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
            //TextureAssets.Item[ItemID.GoldBroadsword] = ModContent.Request<Texture2D>(AssetFolder + "GoldSword");
            //TextureAssets.Item[ItemID.PlatinumBroadsword] = ModContent.Request<Texture2D>(AssetFolder + "PlatinumSword");
            //TextureAssets.Item[ItemID.SilverBroadsword] = ModContent.Request<Texture2D>(AssetFolder + "SilverSword");
            //TextureAssets.Item[ItemID.TungstenBroadsword] = ModContent.Request<Texture2D>(AssetFolder + "TungstenSword");
        }
    }
}
