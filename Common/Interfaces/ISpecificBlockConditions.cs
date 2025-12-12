using Terraria;

namespace Terrafirma.Common.Interfaces
{
    public interface ISpecificBlockConditions
    {
        /// <summary>
        /// Return true to allow this thing to be blocked.
        /// </summary>
        /// <param name="player">The player who's blocking</param>
        /// <param name="npc">This is only used for GlobalNPCs, it's the npc being applied to.</param>
        bool CanBeBlocked(Player player, NPC? npc = null);
    }
}
