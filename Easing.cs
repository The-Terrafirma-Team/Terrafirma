using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrafirma
{
    public static class Easing // Thanx https://easings.net/ :3
    {
        public static float SineIn(float x)
        {
            return 1 - MathF.Cos((x * MathHelper.Pi) / 2);
        }
        public static float SineOut(float x)
        {
            return MathF.Sin((x * MathHelper.Pi) / 2);
        }
        public static float SineInOut(float x)
        {
            return -(MathF.Cos(MathHelper.Pi * x) - 1) / 2;
        }
        //public static float InCubic(float x)
        //{
        //    return x*x*x; HAHAHAHA
        //}
        public static float OutPow(float x, float pow)
        {
            return 1 - MathF.Pow(1 - x, pow);
        }
        public static float InOutPow(float x, float pow)
        {
            if (x > 0.5f)
                return 1 - MathF.Pow(1 - x, pow);
            else
                return MathF.Pow(x,pow);
        }
    }
}
