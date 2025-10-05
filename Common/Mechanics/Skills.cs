using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrafirma.Common.Mechanics
{
    public enum SkillCategory : byte
    {
        General,
        Melee,
        Magic,
        Ranged
    }
    public abstract class Skill : ModType, ILocalizedModType
    {
        public virtual SkillCategory Category => SkillCategory.General;
        protected Skill()
        {
            ID = GetID();
        }
        public int ID;
        /// <summary>
        /// This is only needed for skills defined before loading, you likely don't need this.
        /// </summary>
        /// <returns></returns>
        public int GetID()
        {
            for (int i = 0; i < SkillsSystem.Skills.Length; i++)
                if (SkillsSystem.Skills[i].GetType().Name == this.GetType().Name)
                    return i;
            return -1;
        }
        public virtual string Texture => (GetType().Namespace + "." + GetType().Name).Replace('.', '/');
        public override void Load()
        {
            ID = SkillsSystem.Skills.Length;
            SkillsSystem.Skills = SkillsSystem.Skills.Append(this).ToArray();
            SkillsSystem.SkillTextures = SkillsSystem.SkillTextures.Append(ModContent.Request<Texture2D>(Texture)).ToArray();
        }
        public sealed override void SetupContent()
        {
            SetStaticDefaults();
            _ = DisplayName;
            _ = Tooltip;
        }
        protected sealed override void Register()
        {
            ModTypeLookup<Skill>.Register(this);
        }
        public string LocalizationCategory => "Skills";
        /// <summary>
        /// Make sure to only access this from the instance in SkillsSystem.Skills.
        /// </summary>
        public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);
        /// <summary>
        /// Make sure to only access this from the instance in SkillsSystem.Skills.
        /// </summary>
        public virtual LocalizedText Tooltip => this.GetLocalization(nameof(Tooltip), () => "");
        public virtual object[] TooltipFormatting() => [];
        public virtual int ManaCost => 0;
        public virtual Color RechargeFlashColor => Color.White;
        public virtual int TensionCost => 0;
        /// <summary>
        /// The amount of frames this skill's cooldown is by default.
        /// Set this to 0 for passive skills.
        /// </summary>
        public virtual int CooldownMax => 60;
        /// <summary>
        /// 0 = off of cooldown, 1 = just activated. 
        /// </summary>
        public float Cooldown = 0;

        /// <summary>
        /// The amount of time it takes to cast
        /// </summary>
        public virtual int CastTimeMax => 0;
        /// <summary>
        /// -1 = finished casting, 0-1 = casting is being done. 
        /// </summary>
        public float CastTime = -1f;
        public virtual bool CanCooldown(Player player)
        {
            return CastTime <= -1f;
        }
        public bool TryToUse(Player player, bool pay = true)
        {
            PlayerStats p = player.PlayerStats();
            if (CastTimeMax > 0 && player.ItemAnimationActive)
                return false;
            if (CooldownMax == 0 || Cooldown > 0)
                return false;
            bool Mana = ManaCost != 0 ? player.CheckMana((int)(ManaCost * p.SkillManaCost), false) : true;
            bool Tension = TensionCost != 0 ? player.CheckTension(TensionCost * p.SkillTensionCost, false) : true;
            if (Mana && Tension)
            {
                Cooldown = 1f;
                if (CastTimeMax > 0)
                    CastTime = 1f;
                else
                {
                    Use(player);
                }
                if (pay)
                {
                    if (TensionCost > 0)
                        player.CheckTension(TensionCost * p.SkillTensionCost, true);
                    if (ManaCost > 0)
                    {
                        player.CheckMana((int)(ManaCost * p.SkillManaCost), true);
                        player.manaRegenDelay = (int)player.maxRegenDelay * 2;
                    }
                }
            }
            return Mana && Tension;
        }
        public virtual void Update(Player player, bool OnCooldown)
        {
        }
        public virtual void Use(Player player)
        {
        }
        public virtual void Casting(Player player)
        {
        }
        public virtual void OnCastInterrupted(Player player, Player.HurtInfo info)
        {

        }
        public virtual void CastingEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
        }
        public int TotalManaCost(Player player)
        {
            return (int)((int)(ManaCost * player.manaCost) * player.PlayerStats().SkillManaCost);
        }
        public int TotalTensionCost(Player player)
        {
            PlayerStats p = player.PlayerStats();
            return (int)(player.ApplyTensionBonusScaling(TensionCost) * p.SkillTensionCost);
        }
    }

    public class SkillsSystem : ModSystem
    {
        public static Skill[] Skills = [];
        public static Asset<Texture2D>[] SkillTextures = [];
        public override void Unload()
        {
            Skills = null;
            SkillTextures = null;
        }
        public static void SyncSkillUse(int player, byte SkillBeingUsed)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return;
            }
            ModPacket packet = ModContent.GetInstance<Terrafirma>().GetPacket();
            packet.Write((byte)Terrafirma.TFNetMessage.SkillActivate);
            packet.Write((byte)player);
            packet.Write(SkillBeingUsed);
            packet.Send(ignoreClient: player);
        }
        public static void RecieveSkillUse(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            byte skill = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                // This check forces the affected player to be whichever client sent the message to the server, this prevents other clients from spoofing a message for another player. This is a typical approach for untrusted messages from clients.
                player = whoAmI;
            }
            if (player != Main.myPlayer)
            {
                Main.player[player].GetModPlayer<SkillsPlayer>().EquippedSkills[skill].TryToUse(Main.player[player]);
            }
            if (Main.netMode == NetmodeID.Server)
                SyncSkillUse(player, skill);
        }

        public static Skill[] GetSkillsOfCategory(SkillCategory[] categories, bool onlyRememberedSkill = true)
        {
            Skill[] skills = new Skill[]{};
            foreach (Skill i in Skills)
            {
                if (categories.Contains(i.Category))
                {
                    if ((SkillsPlayer.RememberedSkillIDs[i.ID] && onlyRememberedSkill) || !onlyRememberedSkill) skills = skills.Append(i).ToArray();
                }
            }
            return skills;
        }

        public static Skill[] GetSkillsOfCategory(SkillCategory category, bool onlyRememberedSkill = true)
        {
            Skill[] skills = new Skill[] { };
            foreach (Skill i in Skills)
            {
                if (category == i.Category)
                {
                    if ((SkillsPlayer.RememberedSkillIDs[i.ID] && onlyRememberedSkill) || !onlyRememberedSkill) skills = skills.Append(i).ToArray();
                }
            }
            return skills;
        }
    }
    public class SkillsPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override void SaveData(TagCompound tag)
        {
            String[] names = ["","","",""];
            for(int i = 0; i < names.Length; i++)
            {
                if (EquippedSkills[i] != null)
                    names[i] = SkillsSystem.Skills[EquippedSkills[i].ID].FullName;
            }
            tag["Terrafirma:EquippedSkills"] = names;

            String[] RememberedSkills = Array.Empty<string>();
            for (int i = 0; i < RememberedSkillIDs.Length; i++)
            {
                if (RememberedSkillIDs[i])
                    RememberedSkills = RememberedSkills.Append(SkillsSystem.Skills[i].FullName).ToArray();
            }
            tag["Terrafirma:RememberedSkills"] = RememberedSkills;
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("Terrafirma:EquippedSkills"))
            {
                String[] FullNames = tag.Get<String[]>("Terrafirma:EquippedSkills");
                for (int i = 0; i < EquippedSkills.Length; i++)
                {
                    Skill s = null;
                    for (int x = 0; x < SkillsSystem.Skills.Length; x++)
                    {
                        if (SkillsSystem.Skills[x].FullName == FullNames[i])
                        {
                            s = (Skill)Activator.CreateInstance(SkillsSystem.Skills[x].GetType());
                            break;
                        }
                    }
                    EquippedSkills[i] = s;
                }
            }
            if (tag.ContainsKey("Terrafirma:RememberedSkills"))
            {
                for(int i = 0; i < SkillsSystem.Skills.Length; i++)
                {
                    String[] names = tag.Get<String[]>("Terrafirma:EquippedSkills");
                    if (names.Contains(SkillsSystem.Skills[i].FullName))
                    {
                        RememberedSkillIDs[i] = true;
                    }
                }
            }
        }
        public override void SetStaticDefaults()
        {
            RememberedSkillIDs = new bool[SkillsSystem.Skills.Length];
        }
        public static bool[] RememberedSkillIDs;
        public Skill[] EquippedSkills = { null, null, null, null };
        public static bool[] HasDoneCooldownChime = { true, true, true, true };
        public static byte[] CooldownFlashLight = { 0, 0, 0, 0 };

        public int MaxSkills = 3;
        public static ModKeybind Skill1 { get; set; }
        public static ModKeybind Skill2 { get; set; }
        public static ModKeybind Skill3 { get; set; }
        public static ModKeybind Skill4 { get; set; }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            for (int i = 0; i < EquippedSkills.Length; i++)
            {
                if (EquippedSkills[i] != null && EquippedSkills[i].CastTime > 0f)
                {
                    EquippedSkills[i].CastingEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
                }
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            for (int i = 0; i < EquippedSkills.Length; i++)
            {
                if (EquippedSkills[i] != null && EquippedSkills[i].CastTime > -1)
                {
                    EquippedSkills[i].CastTime = -1;
                    EquippedSkills[i].Cooldown = 0.5f;
                    EquippedSkills[i].OnCastInterrupted(Player, info);
                }
            }
        }
        public override void PostUpdateBuffs()
        {
            PlayerStats stats = Player.PlayerStats();
            for (int i = 0; i < EquippedSkills.Length; i++)
            {
                if (CooldownFlashLight[i] >= 17)
                    CooldownFlashLight[i] -= 17;

                if (EquippedSkills[i] != null)
                {
                    if (EquippedSkills[i].CanCooldown(Player))
                    {
                        EquippedSkills[i].Cooldown -= 1f / EquippedSkills[i].CooldownMax * stats.SkillCooldown;
                        if (EquippedSkills[i].Cooldown < 0f)
                        {
                            if (!HasDoneCooldownChime[i])
                            {
                                CooldownFlashLight[i] = 255;
                                SoundEngine.PlaySound(SoundID.MaxMana);
                                HasDoneCooldownChime[i] = true;
                            }
                            EquippedSkills[i].Cooldown = 0f;
                        }
                    }
                    if (EquippedSkills[i].CastTimeMax > 0)
                    {
                        if (EquippedSkills[i].CastTime > 0)
                        {
                            EquippedSkills[i].Casting(Player);
                        }
                        if (EquippedSkills[i].CastTime < 0f && EquippedSkills[i].CastTime != -1)
                        {
                            HasDoneCooldownChime[i] = false;
                            EquippedSkills[i].Use(Player);
                            EquippedSkills[i].CastTime = -1f;
                        }
                        if (EquippedSkills[i].CastTime > -1)
                        {
                            Player.PlayerStats().ItemUseBlocked = true;
                            EquippedSkills[i].CastTime -= 1f / EquippedSkills[i].CastTimeMax * stats.SkillCastTime;
                        }
                    }
                    EquippedSkills[i].Update(Player, EquippedSkills[i].Cooldown > 0f);
                }
            }
        }
        public override void Load()
        {
            Skill1 = KeybindLoader.RegisterKeybind(Mod, "First Skill", Keys.Z);
            Skill2 = KeybindLoader.RegisterKeybind(Mod, "Second Skill", Keys.X);
            Skill3 = KeybindLoader.RegisterKeybind(Mod, "Third Skill", Keys.C);
            Skill4 = KeybindLoader.RegisterKeybind(Mod, "Fourth Skill", Keys.V);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Skill1.JustPressed && EquippedSkills[0] != null && EquippedSkills[0].TryToUse(Player))
            {
                if (EquippedSkills[0].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[0] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 0);
            }
            if (Skill2.JustPressed && EquippedSkills[1] != null && EquippedSkills[1].TryToUse(Player))
            {
                if (EquippedSkills[1].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[1] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 1);
            }
            if (MaxSkills >= 3 && Skill3.JustPressed && EquippedSkills[2] != null && EquippedSkills[2].TryToUse(Player))
            {
                if (EquippedSkills[2].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[2] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 2);
            }
            if (MaxSkills >= 4 && Skill4.JustPressed && EquippedSkills[3] != null && EquippedSkills[3].TryToUse(Player))
            {
                if (EquippedSkills[3].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[3] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 3);
            }
        }
        public override void Unload()
        {
            Skill1 = null;
            Skill2 = null;
            Skill3 = null;
            Skill4 = null;
        }
    }
}
