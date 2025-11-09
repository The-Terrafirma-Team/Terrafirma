using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace Terrafirma.Common.Mechanics
{
    public abstract class EnemyPrefix : ModType, ILocalizedModType
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return Terrafirma.CombatReworkEnabled;
        }
        public string LocalizationCategory => "EnemyPrefix";

        public virtual int Weight => 10;
        public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);
        public int ID { get; internal set; }
        public override void Load()
        {
            EnemyPrefixNPC.Prefixes = EnemyPrefixNPC.Prefixes.Append(this).ToArray();
            EnemyPrefixNPC.Prefixes[EnemyPrefixNPC.Prefixes.Length - 1].ID = EnemyPrefixNPC.Prefixes.Length - 1;
            EnemyPrefixNPC.weightedPrefix.Add(EnemyPrefixNPC.Prefixes.Length - 1, Weight);
        }
        public sealed override void SetupContent()
        {
            SetStaticDefaults();
            _ = DisplayName;
        }
        protected sealed override void Register()
        {
            ModTypeLookup<EnemyPrefix>.Register(this);
        }
        public virtual void Apply(NPC npc)
        {

        }
        public virtual void Update(NPC npc)
        {

        }
    }
    public class EnemyPrefixNPC : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return Terrafirma.CombatReworkEnabled;
        }
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return !entity.friendly && !entity.boss && entity.lifeMax > 5;
        }
        public override bool InstancePerEntity => true;

        public static EnemyPrefix[] Prefixes = [];
        public static WeightedRandom<int> weightedPrefix = new WeightedRandom<int>();
        public int EnemyPrefix = -1;
        public bool appliedPrefix = false;
        public override void ModifyTypeName(NPC npc, ref string typeName)
        {
            if (EnemyPrefix != -1)
                typeName = Prefixes[EnemyPrefix].DisplayName.Value + " " + typeName;
        }
        public override bool PreAI(NPC npc)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!appliedPrefix)
                {
                    if (Main.rand.NextBool())
                        base.PreAI(npc);
                    EnemyPrefix = weightedPrefix.Get();
                    Prefixes[EnemyPrefix].Apply(npc);
                    npc.netUpdate = true;
                }
                appliedPrefix = true;
            }

            if (EnemyPrefix != -1)
            {
                Prefixes[EnemyPrefix].Update(npc);
            }
            return base.PreAI(npc);
        }
        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(EnemyPrefix);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            EnemyPrefix = binaryReader.ReadInt32();
            if(EnemyPrefix != -1 && !appliedPrefix)
            {
                Prefixes[EnemyPrefix].Apply(npc);
                appliedPrefix = true;
            }
        }
    }
}
