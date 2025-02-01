using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terrafirma.Items.Materials;
using Terrafirma.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Tiles
{
    public class TerrafirmaGlobalTile : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (type == TileID.LivingMahoganyLeaves && Main.rand.NextBool(3))
            {
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<MahoganyLeaf>());
            }
            base.Drop(i, j, type);
        }
        public override bool CanExplode(int i, int j, int type)
        {
            if(type is TileID.Ebonstone or TileID.Crimstone && !NPC.downedBoss2)
            {
                return false;
            }
            return base.CanExplode(i, j, type);
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {

            if (!fail)
            {
                // Granite Pickaxe Ore Duplication
                if (Main.LocalPlayer.PlayerStats().TileBeingPicked != null && !noItem && !TilePlacedByPlayerSystem.TilesPlacedByPlayers.Contains(new Point(i, j)) && (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GranitePickaxe>() && Main.LocalPlayer.PlayerStats().TileBeingPicked == new Point(i, j)) && Main.netMode != NetmodeID.MultiplayerClient && TileID.Sets.Ore[type] && Main.rand.NextBool(4))
                {
                    if (Main.tile[i, j].TileType < 692)
                    {
                        int dropType = 0;
                        switch (type)
                        {
                            case TileID.Copper:
                                dropType = ItemID.CopperOre;
                                break;
                            case TileID.Tin:
                                dropType = ItemID.TinOre;
                                break;
                            case TileID.Iron:
                                dropType = ItemID.IronOre;
                                break;
                            case TileID.Lead:
                                dropType = ItemID.LeadOre;
                                break;
                            case TileID.Silver:
                                dropType = ItemID.SilverOre;
                                break;
                            case TileID.Tungsten:
                                dropType = ItemID.TungstenOre;
                                break;
                            case TileID.Gold:
                                dropType = ItemID.GoldOre;
                                break;
                            case TileID.Platinum:
                                dropType = ItemID.PlatinumOre;
                                break;
                            case TileID.Demonite:
                                dropType = ItemID.DemoniteOre;
                                break;
                            case TileID.Crimtane:
                                dropType = ItemID.CrimtaneOre;
                                break;
                            case TileID.Amethyst:
                                dropType = ItemID.Amethyst;
                                break;
                            case TileID.Topaz:
                                dropType = ItemID.Topaz;
                                break;
                            case TileID.Emerald:
                                dropType = ItemID.Topaz;
                                break;
                            case TileID.Sapphire:
                                dropType = ItemID.Topaz;
                                break;
                            case TileID.Ruby:
                                dropType = ItemID.Ruby;
                                break;
                            case TileID.Diamond:
                                dropType = ItemID.Diamond;
                                break;
                        }
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Point(i, j).ToWorldCoordinates(), dropType);
                    }
                    else
                    {
                        TileLoader.Drop(i,j,type,false);
                    }
                    for(int e = 0; e < 10; e++)
                    {
                        Dust d = Dust.NewDustPerfect(new Vector2(i * 16 + 8, j * 16 + 8),DustID.Electric,Main.rand.NextVector2Circular(3,3));
                        d.noGravity = true;
                    }
                }
                TilePlacedByPlayerSystem.TilesPlacedByPlayers.Remove(new Point(i, j));
            }
        }
        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            TilePlacedByPlayerSystem.TilesPlacedByPlayers.Add(new Point(i, j));
        }
    }
}
