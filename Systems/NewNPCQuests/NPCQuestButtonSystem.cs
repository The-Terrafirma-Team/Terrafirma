using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Audio;
using Terrafirma.Systems.NPCQuests;

namespace Terrafirma.Systems.NewNPCQuests
{
    [Autoload(Side = ModSide.Client)]
    public class NPCQuestButtonSystem : ModSystem
    {
        internal NPCQuestButtonUIState questbuttonui;
        internal NewNPCQuestMenu questselectorui;
        private UserInterface questuiinterface;
        public override void Load()
        {
            questbuttonui = new NPCQuestButtonUIState();
            questbuttonui.Activate();
            questselectorui = new NewNPCQuestMenu();
            questselectorui.Activate();

            questuiinterface = new UserInterface();
            questuiinterface.SetState(null);
        }

        public string Open()
        {
            if (questuiinterface.CurrentState == questbuttonui) return "Button";
            if (questuiinterface.CurrentState == questselectorui) return "Selector";
            return "";
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (questuiinterface?.CurrentState != null)
            {
                questuiinterface?.Update(gameTime);
            }
        }

        public void FlushButton()
        {
            questbuttonui?.Flush();
            questuiinterface?.SetState(null);
        }

        //Button
        public void CreateButton()
        {
            FlushSelectorUI();

            if (!questbuttonui.UIOpen) questbuttonui?.Create();
            questuiinterface?.SetState(questbuttonui);
        }

        //Selector
        public void OpenSelectorUI(int talknpc)
        {
            FlushButton();

            questselectorui.talknpc = talknpc;
            questselectorui?.Create();         
            questuiinterface?.SetState(questselectorui);

            Main.CloseNPCChatOrSign();
            Main.ClosePlayerChat();
            Main.playerInventory = false;          

        }

        public void FlushSelectorUI()
        {
            questbuttonui?.Flush();
            questuiinterface?.SetState(null);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {            
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: NPC Quest Interface",
                    delegate
                    {
                        if (questuiinterface?.CurrentState != null)
                        {
                            questuiinterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
