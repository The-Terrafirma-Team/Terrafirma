using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Audio;

namespace Terrafirma.Systems.Cooking
{
    [Autoload(Side = ModSide.Client)]

    public class CookingPotUISystem : ModSystem
    {
        internal Vector2 PotPosition = Vector2.Zero;

        internal CookingPotUI cookingpotui;
        private UserInterface cookingpot;

        public override void Load()
        {
            cookingpotui = new CookingPotUI();
            cookingpotui.Activate();
            cookingpot = new UserInterface();
            cookingpot.SetState(cookingpotui);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (cookingpot?.CurrentState != null)
            {
                cookingpot?.Update(gameTime);
            }
            if (Main.LocalPlayer.Center.Distance(PotPosition) > 150f && cookingpot.CurrentState != null || Main.playerInventory == false && cookingpot.CurrentState != null)
            {
                cookingpotui?.Flush();
                cookingpot.SetState(null);
            }
        }

        public void Flush()
        {
            cookingpotui?.Flush();
        }

        public void Create(Vector2 tilepos)
        {
            cookingpot.SetState(cookingpotui);
            cookingpotui?.Create(tilepos);
            PotPosition = tilepos;
            Main.playerInventory = true;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: Cooking Pot UI",
                    delegate
                    {
                        if (cookingpot?.CurrentState != null)
                        {
                            cookingpot.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
