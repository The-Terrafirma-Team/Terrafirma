//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terrafirma.Common;
//using Terraria;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;

//namespace Terrafirma.Systems
//{
//    public class CameraSystem : ModSystem
//    {
//        public override void Load()
//        {
//            On_Main.DoDraw_UpdateCameraPosition += On_Main_DoDraw_UpdateCameraPosition;
//            CameraShake = new List<CameraShake>();
//        }
//        public override void Unload()
//        {
//            CameraShake.Clear();
//        }
//        public static List<CameraShake> CameraShake = new List<CameraShake> { };

//        public override void PostUpdateEverything()
//        {
//            for (int i = 0; i < CameraShake.Count; i++)
//            {
//                CameraShake cam = CameraShake[i];
//                cam.TimeLeft += 1;

//                if (cam.TimeLeft >= cam.Duration)
//                {
//                    CameraShake.RemoveAt(i);
//                }
//            }
//        }
//        private void On_Main_DoDraw_UpdateCameraPosition(On_Main.orig_DoDraw_UpdateCameraPosition orig)
//        {
//            orig();
//            Main.NewText(CameraShake.Count);
//            for (int i = 0; i < CameraShake.Count; i++)
//            {
//                CameraShake cam = CameraShake[i];
//                float intense = -MathF.Pow((float)cam.TimeLeft / cam.Duration,cam.FalloffFactor) + 1;
//                Main.screenPosition += new Vector2(intense);
//            }
//        }
//        public static void AddShake(int duration, float intensity, float falloffFactor, Vector2? position = null)
//        {
//            if (!ModContent.GetInstance<ClientConfig>().EnableScreenshake)
//            {
//                return;
//            }
//            CameraShake.Add(new CameraShake() { TimeLeft = 0, Duration = duration, Intensity = intensity, FalloffFactor = falloffFactor, Position = position });
//        }
//    }
//    public class CameraShake
//    {
//        public int Duration;
//        public int TimeLeft;
//        public float Intensity;
//        public float FalloffFactor;
//        public Vector2? Position;
//    }
//}
