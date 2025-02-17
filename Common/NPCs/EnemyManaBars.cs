using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Common.NPCs
{
    public class EnemyManaBars : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                DrawManaBar(npc, Main.spriteBatch);
            }
        }
        private void DrawManaBar(NPC npc, SpriteBatch spriteBatch)
        {
            NPCStats gNPC = npc.NPCStats();
            if (gNPC.Mana >= gNPC.MaxMana || gNPC.MaxMana == 0)
                return;

            float alpha = Lighting.Brightness((int)(npc.Center.X / 16), (int)((npc.Center.Y + npc.gfxOffY) / 16));

            float percent = gNPC.Mana / (float)gNPC.MaxMana;
            Color barColor;

            Vector2 vector = npc.Bottom;
            vector.Y += Main.NPCAddHeight(npc);
            vector.Y += npc.gfxOffY + (npc.life >= npc.lifeMax ? 0 : 30);

            if (percent > 0.5f)
            {
                barColor = Color.Lerp(Color.Cyan, new Color(128, 0, 255), percent * 2 - 1);
            }
            else
            {
                barColor = Color.Lerp(Color.Blue, Color.Cyan, percent * 2);
            }

            spriteBatch.Draw(TextureAssets.Hb2.Value, vector - Main.screenPosition, null, barColor * alpha, 0, TextureAssets.Hb2.Size() / 2, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureAssets.Hb1.Value, vector - Main.screenPosition, new Rectangle(0, 0, (int)(TextureAssets.Hb1.Width() * percent), TextureAssets.Hb1.Height()), barColor * alpha, 0, TextureAssets.Hb1.Size() / 2, 1, SpriteEffects.None, 0);
        }
    }
    [Autoload(Side = ModSide.Client)]
    public class EnemyManaBarsSystem : ModSystem
    {
        private UserInterface _enemyManaBarsUI;
        internal EnemyManaBars EnemyManaBars;
        public override void Load()
        {
            EnemyManaBars = new();
            EnemyManaBars.Activate();
            _enemyManaBarsUI = new();
            _enemyManaBarsUI.SetState(EnemyManaBars);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int Index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Entity Health Bars"));
            if (Index != -1)
            {
                layers.Insert(Index, new LegacyGameInterfaceLayer(
                    "Terrafirma: EnemyManaBar",
                    delegate
                    {
                        _enemyManaBarsUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }
    }
}
