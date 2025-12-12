using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
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

        public virtual int Weight => 100;
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
        public override void Load()
        {
            On_NPC.Transform += On_NPC_Transform;
        }

        private void On_NPC_Transform(On_NPC.orig_Transform orig, NPC self, int newType)
        {
            int prefix = -1;
            if (self.TryGetGlobalNPC(out EnemyPrefixNPC n))
            {
                prefix = n.EnemyPrefix;
            }
            orig.Invoke(self, newType);
            if (prefix > -1 && self.TryGetGlobalNPC(out n))
            {
                n.EnemyPrefix = prefix;
                n.appliedPrefix = true;
                Prefixes[prefix].Apply(self);
            }
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return !entity.friendly && !entity.boss && entity.lifeMax > 5 && entity.type != NPCID.TargetDummy;
        }
        public override bool InstancePerEntity => true;

        public static EnemyPrefix[] Prefixes = [];
        public static WeightedRandom<int> weightedPrefix = new();
        public int EnemyPrefix = -1;
        public bool appliedPrefix = false;
        public override void ModifyTypeName(NPC npc, ref string typeName)
        {
            if (EnemyPrefix != -1)
                typeName = Prefixes[EnemyPrefix].DisplayName.Value + " " + typeName;
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (source is EntitySource_Parent p && p.Entity is NPC n)
            {
                EnemyPrefix = n.GetGlobalNPC<EnemyPrefixNPC>().EnemyPrefix;
                appliedPrefix = true;
            }
        }
        public override GlobalNPC Clone(NPC from, NPC to)
        {
            EnemyPrefixNPC t = to.GetGlobalNPC<EnemyPrefixNPC>();
            EnemyPrefixNPC f = from.GetGlobalNPC<EnemyPrefixNPC>();
            t.EnemyPrefix = f.EnemyPrefix;
            t.appliedPrefix = f.appliedPrefix;
            return base.Clone(from, to);
        }
        public override bool PreAI(NPC npc)
        {
            if (!ModContent.GetInstance<ServerConfig>().CombatPage.NPCPrefixes)
            {
                appliedPrefix = true;
                return base.PreAI(npc);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!appliedPrefix)
                {
                    appliedPrefix = true;
                    if (Main.rand.NextBool())
                        return base.PreAI(npc);
                    EnemyPrefix = weightedPrefix.Get();
                    Prefixes[EnemyPrefix].Apply(npc);
                    npc.netUpdate = true;
                }
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
