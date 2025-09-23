using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System;
using System.IO;
using System.Linq;
using Terrafirma.Content.Skills;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Mechanics
{
    public abstract class Skill : ILoadable
    {
        public int ID()
        {
            for (int i = 0; i < SkillsSystem.Skills.Length; i++)
                if (SkillsSystem.Skills[i].GetType().Name == this.GetType().Name)
                    return i;
            return -1;
        }
        public virtual string Texture => (GetType().Namespace + "." + GetType().Name).Replace('.', '/');
        public void Load(Mod mod)
        {
            SkillsSystem.Skills = SkillsSystem.Skills.Append(this).ToArray();
            SkillsSystem.SkillTextures = SkillsSystem.SkillTextures.Append(ModContent.Request<Texture2D>(Texture)).ToArray();
        }

        //public string LocalizationCategory => "Skills";
        //public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);
        //public virtual LocalizedText Tooltip => this.GetLocalization(nameof(Tooltip), () => "");
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
            if (CastTimeMax > 0 && player.ItemAnimationActive)
                return false;
            if (CooldownMax == 0 || Cooldown > 0)
                return false;
            bool Mana = ManaCost != 0 ? player.CheckMana(ManaCost, false) : true;
            bool Tension = TensionCost != 0 ? player.CheckTension(TensionCost, false) : true;
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
                        player.CheckTension(TensionCost, true);
                    if (ManaCost > 0)
                    {
                        player.CheckMana(ManaCost, true);
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
        public void Unload()
        {
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
                Main.player[player].GetModPlayer<SkillsPlayer>().Skills[skill].TryToUse(Main.player[player]);
            }
            if (Main.netMode == NetmodeID.Server)
                SyncSkillUse(player, skill);
        }
    }
    public class SkillsPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public Skill[] Skills = { new FocusStrike(), new GroundSlam(), null, null };
        public static bool[] HasDoneCooldownChime = { true, true, true, true };
        public static byte[] CooldownFlashLight = { 0, 0, 0, 0 };

        public int MaxSkills = 3;
        public static ModKeybind Skill1 { get; set; }
        public static ModKeybind Skill2 { get; set; }
        public static ModKeybind Skill3 { get; set; }
        public static ModKeybind Skill4 { get; set; }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                if (Skills[i] != null && Skills[i].CastTime > 0f)
                {
                    Skills[i].CastingEffects(drawInfo,ref r, ref g, ref b, ref a, ref fullBright);
                }
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                if (Skills[i] != null && Skills[i].CastTime > -1)
                {
                    Skills[i].CastTime = -1;
                    Skills[i].Cooldown = 0.5f;
                    Skills[i].OnCastInterrupted(Player, info);
                }
            }
        }
        public override void PostUpdateBuffs()
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                if(CooldownFlashLight[i] >= 17)
                CooldownFlashLight[i]-= 17;

                if (Skills[i] != null)
                {
                    if (Skills[i].CanCooldown(Player))
                    {
                        Skills[i].Cooldown -= 1f / Skills[i].CooldownMax;
                        if (Skills[i].Cooldown < 0f)
                        {
                            if (!HasDoneCooldownChime[i])
                            {
                                CooldownFlashLight[i] = 255;
                                SoundEngine.PlaySound(SoundID.MaxMana);
                                HasDoneCooldownChime[i] = true;
                            }
                            Skills[i].Cooldown = 0f;
                        }
                    }
                    if (Skills[i].CastTimeMax > 0)
                    {
                        if (Skills[i].CastTime > 0)
                        {
                            Skills[i].Casting(Player);
                        }
                        if (Skills[i].CastTime < 0f && Skills[i].CastTime != -1)
                        {
                            HasDoneCooldownChime[i] = false;
                            Skills[i].Use(Player);
                            Skills[i].CastTime = -1f;
                        }
                        if (Skills[i].CastTime > -1)
                        {
                            Player.PlayerStats().ItemUseBlocked = true;
                            Skills[i].CastTime -= 1f / Skills[i].CastTimeMax;
                        }
                    }
                    Skills[i].Update(Player, Skills[i].Cooldown > 0f);
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
            if (Skill1.JustPressed && Skills[0] != null && Skills[0].TryToUse(Player))
            {
                if (Skills[0].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[0] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 0);
            }
            if (Skill2.JustPressed && Skills[1] != null && Skills[1].TryToUse(Player))
            {
                if (Skills[1].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[1] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 1);
            }
            if (MaxSkills >= 3 && Skill3.JustPressed && Skills[2] != null && Skills[2].TryToUse(Player))
            {
                if (Skills[2].CastTimeMax == 0)
                {
                    HasDoneCooldownChime[2] = false;
                }
                SkillsSystem.SyncSkillUse(Player.whoAmI, 2);
            }
            if (MaxSkills >= 4 && Skill4.JustPressed && Skills[3] != null && Skills[3].TryToUse(Player))
            {
                if (Skills[3].CastTimeMax == 0)
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
