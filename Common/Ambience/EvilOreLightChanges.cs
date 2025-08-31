//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Terrafirma.Common.Ambience
//{
//    public class EvilOreLightChanges : GlobalTile
//    {
//        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
//        {
//            if (type == TileID.Demonite)
//            {
//                float extra = (float)Math.Sin((Main.timeForVisualEffects + (Math.Tan(Math.Sin(i * j))) * i) * 0.03f) * 0.2f;
//                r = 0.12f + extra * 0.4f;
//                g = 0.07f + extra * 0.1f;
//                b = 0.32f + extra * 0.3f;
//            }
//            else if (type == TileID.Crimtane)
//            {
//                r = (float)Math.Sin(Main.LocalPlayer.Center.Distance(new Vector2(i * 16f + 8, j * 16f + 8)) * 0.1f + Main.timeForVisualEffects * 0.01f);
//            }
//        }
//    }
//}
