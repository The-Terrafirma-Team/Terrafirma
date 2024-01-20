using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;

namespace TerrafirmaRedux.Global
{
    public class SentryBuffID
    {
        const int MetalWrench = 0;
    }
    public class SentryChanges : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public int[] BuffTime = new int[1];

        public float SpeedMultiplier = 1f;
        public float RangeMultiplier = 1f;
        void ResetEffects(Projectile entity)
        {
            SpeedMultiplier = 1f;
            RangeMultiplier = 1f;
        }
        public void UpdateBuffs(Projectile entity)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                if (BuffTime[i] > 0)
                    BuffTime[i]--;
            }
            if (BuffTime[0] > 0)
            {
                SpeedMultiplier -= 0.3f;
            }
        }
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.sentry;
        }
        public override bool PreAI(Projectile projectile)
        {
            ResetEffects(projectile);
            UpdateBuffs(projectile);
            return base.PreAI(projectile);
        }
        public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                binaryWriter.Write(BuffTime[i]);
            }
        }
        public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                binaryReader.ReadInt32();
            }
        }
    }
}
