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
        private int getVanillaOreDrop(int type)
        {
            switch (type)
            {
                case TileID.Copper:
                    return ItemID.CopperOre;
                case TileID.Tin:
                    return ItemID.TinOre;
                case TileID.Iron:
                    return ItemID.IronOre;
                case TileID.Lead:
                    return ItemID.LeadOre;
                case TileID.Silver:
                    return ItemID.SilverOre;
                case TileID.Tungsten:
                    return ItemID.TungstenOre;
                case TileID.Gold:
                    return ItemID.GoldOre;
                case TileID.Platinum:
                    return ItemID.PlatinumOre;
                case TileID.Demonite:
                    return ItemID.DemoniteOre;
                case TileID.Crimtane:
                    return ItemID.CrimtaneOre;
                case TileID.Obsidian:
                    return ItemID.Obsidian;
                case TileID.Hellstone:
                    return ItemID.Hellstone;
                case TileID.Cobalt:
                    return ItemID.CobaltOre;
                case TileID.Palladium:
                    return ItemID.PalladiumOre;
                case TileID.Mythril:
                    return ItemID.MythrilOre;
                case TileID.Orichalcum:
                    return ItemID.OrichalcumOre;
                case TileID.Adamantite:
                    return ItemID.AdamantiteOre;
                case TileID.Titanium:
                    return ItemID.TitaniumOre;
                case TileID.Chlorophyte:
                    return ItemID.ChlorophyteOre;
                case TileID.LunarOre:
                    return ItemID.LunarOre;

                case TileID.Amethyst:
                    return ItemID.Amethyst;
                case TileID.Topaz:
                    return ItemID.Topaz;
                case TileID.Emerald:
                    return ItemID.Topaz;
                case TileID.Sapphire:
                    return ItemID.Topaz;
                case TileID.Ruby:
                    return ItemID.Ruby;
                case TileID.Diamond:
                    return ItemID.Diamond;
                default:
                    return -1;
            }
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
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Point(i, j).ToWorldCoordinates(), getVanillaOreDrop(type));
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
