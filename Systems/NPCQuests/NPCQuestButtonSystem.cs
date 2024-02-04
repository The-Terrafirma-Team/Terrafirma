using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Audio;

namespace Terrafirma.Systems.NPCQuests
{
    [Autoload(Side = ModSide.Client)]
    public class NPCQuestButtonSystem : ModSystem
    {
        internal NPCQuestButtonUIState questbuttonui;
        internal NPCQuestSelectorUIState questselectorui;
        private UserInterface questbuttonuiinterface;
        public int UIOpenForNPC = -1;
        public override void Load()
        {
            questbuttonui = new NPCQuestButtonUIState();
            questbuttonui.Activate();
            questselectorui = new NPCQuestSelectorUIState();
            questselectorui.Activate();
            questbuttonuiinterface = new UserInterface();
            questbuttonuiinterface.SetState(null);
        }

        public void HideUI()
        {
            questbuttonuiinterface?.SetState(null);
        }

        public string Open()
        {
            if (questbuttonuiinterface.CurrentState == questbuttonui) return "Button";
            if (questbuttonuiinterface.CurrentState == questselectorui) return "Selector";
            return "";
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (questbuttonuiinterface?.CurrentState != null)
            {
                questbuttonuiinterface?.Update(gameTime);
            }
        }

        public void CreateButton(NPC npc)
        {
            if (!questbuttonui.UIOpen) questbuttonui?.Create(npc);
            questbuttonuiinterface?.SetState(questbuttonui);
        }

        public void FlushButton()
        {
            questbuttonui?.Flush();
        }

        public void OpenSelectorUI(NPC npc)
        {
            Main.CloseNPCChatOrSign();
            Main.ClosePlayerChat();
            Main.playerInventory = false;
            questselectorui?.Create(npc);
            questselectorui.UpdateQuests();
            questbuttonuiinterface?.SetState(questselectorui);
            
        }
        public void FlushSelectorUI()
        {
            questselectorui?.Flush();
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
                        if (questbuttonuiinterface?.CurrentState != null)
                        {
                            questbuttonuiinterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
