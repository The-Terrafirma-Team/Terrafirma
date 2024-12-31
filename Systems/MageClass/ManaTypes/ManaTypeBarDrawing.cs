using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Structs;
using Terrafirma.ManaTypes;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.MageClass.ManaTypes
{
    internal class ManaTypeBarDrawing : ModResourceOverlay
    {
        private Dictionary<string, Asset<Texture2D>> vanillaAssetCache = new();

        public override void PostDrawResource(ResourceOverlayDrawContext context)
        {

            Asset<Texture2D> asset = context.texture;
            string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";
            string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

            float manaPerSegment = context.snapshot.ManaPerSegment;
            float segmentNumber = context.resourceNumber + 1;

            NumberRange segmentRange = new NumberRange((int)(context.resourceNumber * manaPerSegment), 
                                                       (int)(segmentNumber * manaPerSegment) - (context.resourceNumber == 0 ? 1 : 0));
            Dictionary<ManaType, NumberRange> playerManaTypes = Main.LocalPlayer.PlayerStats().playerManaTypes;
            Vector2 originalPosition = context.position;

            //Main.NewText(Main.LocalPlayer.PlayerStats().playerManaTypes.Count);


            for (int i = 0; i < Main.LocalPlayer.PlayerStats().playerManaTypes.Count; i++)
            {
                //Main.NewText("Start: " + segmentRange.start + ", End: " + segmentRange.end);
                //Main.NewText("Name: " + playerManaTypes.Keys.ToArray()[i].Name + ", Start: " + playerManaTypes.Values.ToArray()[i].start + ", End: " + playerManaTypes.Values.ToArray()[i].end);

                Player player = Main.LocalPlayer;
                int manaTypeEnd = (int)playerManaTypes.Values.ToArray()[i].end;
                int manaTypeStart = (int)playerManaTypes.Values.ToArray()[i].start;

                if (CompareAssets(asset, barsFolder + "MP_Fill"))
                {                    
                    if (playerManaTypes.Values.ToArray()[i].OverlapsRange(segmentRange))
                    {

                        float leftMana = 0f;
                        float leftPixelMana = 0f;
                        if (segmentRange.ContainsInt(manaTypeEnd))
                        {
                            leftMana = segmentRange.end - manaTypeEnd;
                            leftPixelMana = leftMana / (20f / 12f);
                        }

                        float rightMana = 0f;
                        float rightPixelMana = 0f;
                        if (segmentRange.ContainsInt(manaTypeStart))
                        {
                            rightMana = -(segmentRange.start - manaTypeStart);
                            rightPixelMana = rightMana / (20f / 12f);
                        }

                        float manaLeftInSegment = Math.Clamp(segmentRange.end - context.snapshot.Mana, 0, 20);
                        float manaLeftInSegmentPixel = manaLeftInSegment / (20f / 12f);

                        int segmentLoopOffset = playerManaTypes.Keys.ToArray()[i].loopingBarTextureSegments > 0 ? (12 * context.resourceNumber) % (12 * (playerManaTypes.Keys.ToArray()[i].loopingBarTextureSegments + 1)) : 0;

                        context.texture = ModContent.Request<Texture2D>(playerManaTypes.Keys.ToArray()[i].TexurePath);
                        context.position.X = originalPosition.X + (int)leftPixelMana - (int)manaLeftInSegmentPixel;
                        context.source = new Rectangle(10 + segmentLoopOffset + (int)leftPixelMana,
                            44,
                            12 - (int)leftPixelMana - (int)rightPixelMana,
                            12);
                        context.Draw();
                    }

                }
                else if (asset == TextureAssets.Mana
                    && manaTypeStart <= (manaPerSegment * segmentNumber)
                    && manaTypeEnd >= (manaPerSegment * segmentNumber))
                {
                    // Draw over the Classic stars
                    context.texture = ModContent.Request<Texture2D>(playerManaTypes.Keys.ToArray()[i].TexurePath);
                    context.source = new Rectangle(40, 4, 22, 24);
                    context.Draw();
                }
                else if (CompareAssets(asset, fancyFolder + "Star_Fill")
                    && manaTypeStart <= (manaPerSegment * segmentNumber) 
                    && manaTypeEnd >= (manaPerSegment * segmentNumber))
                {
                    // Draw over the Fancy stars
                    context.texture = ModContent.Request<Texture2D>(playerManaTypes.Keys.ToArray()[i].TexurePath);
                    context.source = new Rectangle(6, 4, 22, 24);
                    context.Draw();
                } 

            }

            base.PostDrawResource(context);
        }

        private bool CompareAssets(Asset<Texture2D> existingAsset, string compareAssetPath)
        {
            // This is a helper method for checking if a certain vanilla asset was drawn
            if (!vanillaAssetCache.TryGetValue(compareAssetPath, out var asset))
                asset = vanillaAssetCache[compareAssetPath] = Main.Assets.Request<Texture2D>(compareAssetPath);

            return existingAsset == asset;
        }
    }
}
