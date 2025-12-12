using Terraria;

namespace Terrafirma.Common.Interfaces
{
    public interface ICustomBlockBehavior
    {
        /// <summary>
        /// What to do when the player blocks this entity
        /// </summary>
        /// <param name="player">The player who's blocking</param>
        /// <param name="Power">The player's block power. It's scale for how effective the block was, the stun a parry applies to a slime's duration is multiplied by this for example</param>
        /// <param name="npc">This is only used for GlobalNPCs, it's the npc being applied to.</param>
        void OnBlocked(Player player, float Power, NPC? npc = null);
    }
}
