using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global.Templates
{
    public abstract class DruidProjectile : ModProjectile
    {
        public Color allignmentColor = Color.White;

        public float[] tileAmounts = new float[7];
        int highestAmount = 0;
        const int pureTiles = 0;
        const int hotTiles = 1;
        const int coldTiles = 2;
        const int hallowTiles = 3;
        const int jungleTiles = 4;
        const int waterTiles = 5;
        const int evilTiles = 6;

        public static Color[] colorList = new Color[] {Color.Green,Color.DarkOrange,Color.AliceBlue,Color.Pink,Color.Lime,Color.Blue, Color.Purple };
        Color getAllignmentColor()
        {
            //Color color = Color.Green;
            //for (int i = 0; i < colorList.Length; i++)
            //{
            //    color = Color.Lerp(color, colorList[i], tileAmounts[i]);
            //}
            //return color;
            Vector4 color = new Vector4(0);
            for (int i = 0; i < colorList.Length; i++)
            {
                color += colorList[i].ToVector4() * tileAmounts[i];
            }
            return new Color(color.X, color.Y, color.Z, color.W);
        }
        void FindAlighnment(Vector2 startPos)
        {
            for (int x = (int)(startPos.X / 16) - 30; x <= (int)(startPos.X / 16) + 30; x++)
            {
                for (int y = (int)(startPos.Y / 16) - 30; y <= (int)(startPos.Y / 16) + 30; y++)
                {
                    #region countTiles
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile || tile.LiquidAmount > 0)
                    {
                        if (TileID.Sets.CorruptBiomeSight[tile.TileType] || TileID.Sets.CrimsonBiomeSight[tile.TileType])
                        {
                            if (tile.TileType == TileID.CorruptGrass || tile.TileType == TileID.CrimsonGrass)
                            {
                                tileAmounts[evilTiles] += 15;
                            }
                            tileAmounts[evilTiles] += 1;
                        }
                        else if (tile.LiquidType == LiquidID.Water && tile.LiquidAmount > 0)
                        {
                            tileAmounts[waterTiles] += 3;
                        }
                        else if(TileID.Sets.HallowBiomeSight[tile.TileType])
                        {
                            tileAmounts[hallowTiles] += 1;
                            if(tile.TileType == TileID.HallowedGrass)
                            {
                                tileAmounts[hallowTiles] += 15;
                            }
                        }
                        else if(TileID.Sets.isDesertBiomeSand[tile.TileType] || (tile.LiquidType == LiquidID.Lava && tile.LiquidAmount > 0))
                        {
                            tileAmounts[hotTiles] += 1;
                        }
                        else if(TileID.Sets.JungleBiome.Contains(tile.TileType) || (tile.LiquidType == LiquidID.Honey && tile.LiquidAmount > 0))
                        {
                            tileAmounts[jungleTiles] += 1;
                            if (tile.TileType == TileID.JungleGrass)
                            {
                                tileAmounts[hallowTiles] += 15;
                            }
                        }
                        else if (TileID.Sets.IcesSnow[tile.TileType])
                        {
                            tileAmounts[coldTiles] += 1;
                        }
                        else
                        {
                            tileAmounts[pureTiles] += 1;
                        }
                    }
                    #endregion countTiles
                }
            }
            for(int i = 0; i < tileAmounts.Length; i++)
            {
                if(tileAmounts[i] > highestAmount)
                {
                    highestAmount = (int)tileAmounts[i];
                }
            }
            for (int i = 0; i < tileAmounts.Length; i++)
            {
                tileAmounts[i] /= highestAmount;
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            //FindAlighnment(Main.player[Projectile.owner].Center);
            //Projectile.ai[0] = 1;
            //allignmentColor = getAllignmentColor();
        }
        public override void AI()
        {
            //FindAlighnment(Main.player[Projectile.owner].Center);
            if (Projectile.ai[0] == 0)
            {
                FindAlighnment(Main.player[Projectile.owner].Center);
                Projectile.ai[0] = 1;
                allignmentColor = getAllignmentColor();
            }
            evilAI(tileAmounts[evilTiles]);
            hotAI(tileAmounts[hotTiles]);
            coldAI(tileAmounts[coldTiles]);
            hallowAI(tileAmounts[hallowTiles]);
            jungleAI(tileAmounts[jungleTiles]);
            waterAI(tileAmounts[waterTiles]);
            pureAI(tileAmounts[pureTiles]);
        }
        public virtual void evilAI(float ratio)
        {

        }
        public virtual void hotAI(float ratio)
        {

        }
        public virtual void coldAI(float ratio)
        {

        }
        public virtual void hallowAI(float ratio)
        {

        }
        public virtual void jungleAI(float ratio)
        {

        }
        public virtual void waterAI(float ratio)
        {

        }
        public virtual void pureAI(float ratio)
        {

        }
    }
}
