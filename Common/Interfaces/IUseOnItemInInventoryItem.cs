using Terraria;

namespace Terrafirma.Common.Interfaces
{
    public interface IUseOnItemInInventoryItem
    {
        bool canBeUsedOnThisItem(Player player, Item item, Item itemBeingUsedOn, int context);
        void useOnItem(Player player, Item item, Item itemBeingUsedOn, int context);
    }
}
