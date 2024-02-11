using SubworldLibrary;
using Terrafirma.Projectiles.Tools;
using Terrafirma.Rarities;
using Terrafirma.Subworlds.Tempire;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items
{
    internal class TempireTeleport : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.width = 32;
            Item.height = 32;

            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item23;

            Item.rare = ModContent.RarityType<FinalQuestRarity>();
        }

        public override bool? UseItem(Player player)
        {
            if (SubworldSystem.Current is not TempireSubworld)
            {
                SubworldSystem.Enter<TempireSubworld>();

            }
            else
            {
                SubworldSystem.Exit();
            }
            return true;
        }
    }
}
