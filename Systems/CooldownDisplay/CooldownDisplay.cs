//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using Terraria.ModLoader;
//using Terraria.UI;

//namespace Terrafirma.Systems.CooldownDisplay
//{
//    [Autoload(Side = ModSide.Client)]
//    public class CooldownDisplay : UIState
//    {
//    }

//    public class Cooldown
//    {
//        public int Timer = 0;
//        public int TimerMax = 0;
//        public virtual Color barColor => Color.AliceBlue;
//    }
//    public class CooldownSystem : ModSystem
//    {
//        public List<Cooldown> Cooldowns;

//        public void AddCooldown(Cooldown Type, int Time)
//        {
//            Type.Timer = Type.TimerMax = Time;
//            Cooldowns.Add(Type);
//        }
//        public override void PreUpdatePlayers()
//        {
//            for(int i = 0; i < Cooldowns.Count; i++)
//            {
//                Cooldowns[i].Timer--;
//                if (Cooldowns[i].Timer <= 0)
//                    Cooldowns.RemoveAt(i);
//            }
//        }
//        public override void Load()
//        {
//            Cooldowns = new List<Cooldown>();
//        }
//        public override void Unload()
//        {
//            Cooldowns.Clear();
//        }
//    }
//}
