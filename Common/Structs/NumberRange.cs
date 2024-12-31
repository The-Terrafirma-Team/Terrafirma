using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrafirma.Common.Structs
{
    public struct NumberRange
    {
        public int start;
        public int end;

        public NumberRange()
        {
            start = 0;
            end = 1;
        }

        public NumberRange(int startInt, int endInt)
        {
            start = startInt;
            end = endInt;
        }

    }

    internal static class NumberRangeMethods
    {
        public static bool ContainsInt(this NumberRange range, int number, bool greaterequals = true)
        {
            if (greaterequals) return (number >= range.start && number <= range.end);
            return (number > range.start && number < range.end);
        }

        public static bool ContainsRange(this NumberRange range, NumberRange otherRange, bool greaterequals = true)
        {
            if (greaterequals) return (range.ContainsInt(otherRange.start) && range.ContainsInt(otherRange.end));
            return (range.ContainsInt(otherRange.start, false) && range.ContainsInt(otherRange.end, false));
        }

        public static bool OverlapsRange(this NumberRange range, NumberRange otherRange)
        {
            return (range.end >= otherRange.start || range.start <= otherRange.end);
        }
        public static void Shift(this NumberRange range, int offset)
        {
            range.start -= offset;
            range.end -= offset;
        }

        public static int Size(this NumberRange range)
        {
            return range.end - range.start;
        }
    }
}
