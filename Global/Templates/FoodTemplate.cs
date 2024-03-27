using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria.GameContent.UI;
using Terraria.ID;

namespace Terrafirma.Global.Templates
{
    public abstract class FoodTemplate : ModItem
    {
    }

    public class FoodTemplateGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public int grade = -1;

        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (item.buffType == BuffID.WellFed || item.buffType == BuffID.WellFed2 || item.buffType == BuffID.WellFed3)
            {
                if (context is RecipeItemCreationContext) item.GetGlobalItem<FoodTemplateGlobalItem>().grade = Main.rand.Next(3);

                switch (grade)
                {
                    case 0: break;
                    case 1:
                        item.buffTime = (int)(item.buffTime + (3600 * 3));
                        item.rare = Math.Clamp(item.rare + 1, 0, 13);
                        break;
                    case 2:
                        item.buffTime = (int)(item.buffTime + (3600 * 6));
                        item.rare = Math.Clamp(item.rare + 2, 0, 13);
                        break;
                }
            }

            base.OnCreated(item, context);
        }
        public override void SetDefaults(Item entity)
        {
            if (grade == -1) return;

            if (entity.buffType == BuffID.WellFed || entity.buffType == BuffID.WellFed2 || entity.buffType == BuffID.WellFed3)
            {

                switch (grade)
                {
                    case 0: break;
                    case 1:
                        entity.buffTime = (int)(entity.buffTime + (3600 * 3));
                        entity.rare = Math.Clamp(entity.rare + 1, 0, 13);
                        break;
                    case 2:
                        entity.buffTime = (int)(entity.buffTime + (3600 * 6));
                        entity.rare = Math.Clamp(entity.rare + 2, 0, 13);
                        break;
                }
            }
            base.SetDefaults(entity);
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (grade == -1 || grade == 0) return;

            if (item.buffType == BuffID.WellFed || item.buffType == BuffID.WellFed2 || item.buffType == BuffID.WellFed3)
            {
                if (item.buffTime == new Item(item.type).buffTime && item.rare == new Item(item.type).rare)
                {
                    switch (grade)
                    {
                        case 0: break;
                        case 1:
                            item.buffTime = (int)(item.buffTime + (3600 * 3));
                            item.rare = Math.Clamp(item.rare + 1, 0, 13);
                            break;
                        case 2:
                            item.buffTime = (int)(item.buffTime + (3600 * 6));
                            item.rare = Math.Clamp(item.rare + 2, 0, 13);
                            break;
                    }
                }
            }
            base.UpdateInventory(item, player);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (grade == -1) return;

            string gradeprefix = "";
            var nametooltip = tooltips.Where(tooltip => tooltip.Name == "ItemName").FirstOrDefault();
            switch (grade)
            {
                case 0: break;
                case 1: gradeprefix = "Tasty "; break;
                case 2: gradeprefix = "Exquisite "; break;
            }
            nametooltip.Text = gradeprefix + "" + nametooltip.Text;
            base.ModifyTooltips(item, tooltips);
        }

        public override bool CanStack(Item destination, Item source)
        {
            //Testing Purposes
            //grade = Main.rand.Next(3);
            return destination.GetGlobalItem<FoodTemplateGlobalItem>().grade == source.GetGlobalItem<FoodTemplateGlobalItem>().grade;
        }
        public override bool CanStackInWorld(Item destination, Item source)
        {
            return true;
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["grade"] = grade;
            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            grade = tag.Get<int>("grade");
            base.LoadData(item, tag);
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(grade);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            grade = reader.ReadInt32();
        }

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (grade == -1) return;

            Texture2D startexture = ModContent.Request<Texture2D>("Terrafirma/Global/Templates/GradeStars").Value;
            Vector2 Starpos = new Vector2(14f, 14f);
            Rectangle StarBounds = new Rectangle(0, 0, 0, 0);

            switch (grade)
            {
                case 0:
                    StarBounds = new Rectangle(0, 0, 0, 0);
                    break;
                case 1:
                    StarBounds = new Rectangle(0, 0, 14, 14);
                    break;
                case 2:
                    StarBounds = new Rectangle(16, 0, 22, 14);
                    break;
            }

            spriteBatch.Draw(startexture,
                position + Starpos,
                StarBounds,
                Color.White,
                0f,
                new Vector2(StarBounds.Width / 2, StarBounds.Height / 2),
                0.9f,
                SpriteEffects.None,
                0f);

            base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}
