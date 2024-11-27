using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.EarlygameArmors
{
    public class MoltenArmor : ModPlayer
    {
        public bool Active = false;
        public override void ResetEffects()
        {
            Active = false;
        }
        public override void PostUpdateEquips()
        {
            if (Player.head == ArmorIDs.Head.MoltenHelmet && Player.body == ArmorIDs.Body.MoltenBreastplate && Player.legs == ArmorIDs.Legs.MoltenGreaves)
            {
                Player.setBonus = Language.GetTextValue("Mods.Terrafirma.VanillaSetBonus.Molten");
                Player.PlayerStats().FlatTensionGain += 2;
                Player.moveSpeed += 0.2f;
                Active = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Active && Main.rand.NextBool(5))
                target.AddBuff(BuffID.OnFire3, 60 * 3);
        }
    }
    public class MoltenArmorItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is ItemID.MoltenBreastplate or ItemID.MoltenGreaves or ItemID.MoltenHelmet;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            switch (item.type)
            {
                case ItemID.MoltenHelmet:
                    player.PlayerStats().MeleeWeaponScale += 0.1f;
                    player.PlayerStats().TensionCostMultiplier -= 0.1f;
                    break;
                case ItemID.MoltenBreastplate:
                    player.PlayerStats().MeleeWeaponScale += 0.1f;
                    player.lavaRose = true;
                    player.buffImmune[BuffID.OnFire] = true;
                    break;
                case ItemID.MoltenGreaves:
                    player.GetJumpState<FieryJump>().Enable();
                    break;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string type = "";
            switch (item.type)
            {
                case ItemID.MoltenHelmet:
                    type = "MoltenHelmet";
                    break;
                case ItemID.MoltenBreastplate:
                    type = "MoltenBreastplate";
                    break;
                case ItemID.MoltenGreaves:
                    type = "MoltenGreaves";
                    break;
            }
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria") && tooltips[i].Name.Equals("Tooltip0"))
                {
                    tooltips[i].Text = Language.GetTextValue("Mods.Terrafirma.VanillaItemTooltips." + type); ;
                    break;
                }
            }
        }
    }
    public class FieryJump : ExtraJump
    {
        public override Position GetDefaultPosition() => new After(CloudInABottle);

        public override float GetDurationMultiplier(Player player)
        {
            return 2;
        }
        public override void UpdateHorizontalSpeeds(Player player)
        {
            player.runAcceleration *= 6;
            player.maxRunSpeed *= 3f;
        }
        public override void OnStarted(Player player, ref bool playSound)
        {
            for(int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustPerfect(player.Bottom + new Vector2(0,player.gfxOffY),DustID.Torch);
                d.velocity = Main.rand.NextVector2Circular(5, 3) + new Vector2(0, 4);
                d.noGravity = true;
                d.scale *= 2;
                d.fadeIn = Main.rand.NextFloat(1.7f);
            }
        }
        public override void ShowVisuals(Player player)
        {
            Dust d = Dust.NewDustPerfect(player.Bottom + new Vector2(0, player.gfxOffY), DustID.Torch);
            d.velocity = Main.rand.NextVector2Circular(5, 3) + player.velocity;
            d.noGravity = true;
            d.scale *= 2;
        }
    }
}
