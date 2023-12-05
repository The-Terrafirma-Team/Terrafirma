using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class WingChanges : GlobalItem
{
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        if (item.type == ItemID.AngelWings)
        {
            player.wingTimeMax = 1;

        }
    }
}
