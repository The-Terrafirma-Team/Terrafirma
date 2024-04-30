using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Items.Weapons.Melee.Shortswords;
using Terrafirma.Items.Weapons.Magic;
using Terrafirma.Items.Ammo;
using Terrafirma.Projectiles.Hostile;
using Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terrafirma.Common.Items;
using Terraria.Audio;

namespace Terrafirma.Items.Consumable
{
    internal class RepairKit : ModItem
    {
        public override void SetDefaults()
        {
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 11;
            Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.maxStack = 9999;
            Item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
    }
}
