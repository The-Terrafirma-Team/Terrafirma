using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerrafirmaRedux.Global.Structs
{
    /// <summary>
    /// Struct for better organising Progress (ex. Progress from 0 to 100). This Struct is mostly used to keep track of quest progress like enemy kill.
    /// </summary>
    public struct Progress
    {
        public float StartProgress = 0;
        public float EndProgress = 100;
        public float CurrentValue = 0;

        /// <summary>
        /// Whether or not to allow the Current progress value to go over the End value
        /// </summary>
        public bool AllowOverProgression = false;
 
        public Progress(float endprogress, float startprogress = 0, float currentvalue = 0)
        {
            this.StartProgress = startprogress;
            this.EndProgress = endprogress;
            this.CurrentValue = currentvalue;
        }

        public Progress(float i)
        {
            this.StartProgress = i;
            this.EndProgress = i;
            this.CurrentValue = i;
        }

    }

    internal static class ProgressMethods
    {
        /// <summary>
        /// Increment this Progress by the percentage. The percentage will automatically turn negative if the start value is bigger than the end value of the Progress 
        /// so there's no need to set a negative percentage.
        /// </summary>
        /// <param name="percentage"> Value from 0 to 1 </param>
        public static void Increment(this Progress progress, float percentage)
        {
            if (progress.AllowOverProgression) progress.CurrentValue += (percentage / 100f) * (progress.EndProgress - progress.StartProgress);
            else
            {
                if (progress.EndProgress > progress.StartProgress)
                {
                    progress.CurrentValue = Math.Clamp(progress.CurrentValue + (percentage / 100f) * (progress.EndProgress - progress.StartProgress), progress.StartProgress, progress.EndProgress);
                }
                else
                {
                    progress.CurrentValue = Math.Clamp(progress.CurrentValue + (percentage / 100f) * (progress.EndProgress - progress.StartProgress), progress.EndProgress, progress.StartProgress);
                }
            }
        }

        public static float CompletionPercentage(this Progress progress) => (progress.CurrentValue - progress.StartProgress) / (progress.EndProgress - progress.StartProgress);
    
        public static bool IsEmpty(this Progress progress)
        {
            if (progress.EndProgress == 0 && progress.StartProgress == 0) return true;
            return false;
        }

        public static bool IsCompleted(this Progress progress)
        {
            if (progress.EndProgress > progress.StartProgress)
            {
                if (progress.StartProgress >= progress.EndProgress) return true;
            }
            else
            {
                if (progress.StartProgress <= progress.EndProgress) return true;
            }
            return false;
        }

        public static float TimesCompleted(this Progress progress)
        {
            return progress.CurrentValue / progress.EndProgress;
        }

    }
}
