//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using ReLogic.Content;
//using Terrafirma.Common;
//using Terrafirma.Utilities;
//using Terraria;
//using Terraria.GameContent;
//using Terraria.ModLoader;

//namespace Terrafirma.Content.Buffs.Debuffs
//{
//    public class Frozen : ModBuff
//    {
//        public override void SetStaticDefaults()
//        {
//            Main.debuff[Type] = true;
//            DataSets.RegisterStunDebuff(Type);
//        }
//        public override void Update(NPC npc, ref int buffIndex)
//        {
//            NPCStats stats = npc.NPCStats();
//            stats.NoFlight = true;
//            stats.Immobile = true;
//            stats.NoAnimation = true;
//            stats.NoParticles = true;
//        }
//    }
//    public class FrozenNPCTarget : ARenderTargetContentByRequest
//    {
//        public static bool behindTiles = false;
//        protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
//        {
//            PrepareARenderTarget_AndListenToEvents(ref _target, device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, RenderTargetUsage.PlatformContents);
//            GraphicsDevice graphics = Main.instance.GraphicsDevice;
//            var oldTargets = graphics.GetRenderTargets();
//            graphics.Clear(Color.Transparent);
//            graphics.SetRenderTarget(_target);
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, default, Matrix.Identity);
//            graphics.Clear(Color.Transparent);

//            int type = ModContent.BuffType<Frozen>();
//            foreach(NPC n in Main.ActiveNPCs)
//            {
//                if (n.HasBuff(type))
//                    Main.instance.DrawNPCDirect(spriteBatch, n, behindTiles, Main.screenPosition);
//            }

//            spriteBatch.End();
//            graphics.SetRenderTargets(oldTargets);
//            _wasPrepared = true;
//        }
//    }
//    public class FrozenNPCSystem : ModSystem
//    {
//        public static FrozenNPCTarget Target;
//        public static Asset<Effect> IceShader;
//        public override void Load()
//        {
//            IceShader = Mod.Assets.Request<Effect>("Assets/Effects/IceShader", AssetRequestMode.ImmediateLoad);
//            Main.ContentThatNeedsRenderTargets.Add(Target = new FrozenNPCTarget());
//            On_Main.DrawNPCs += On_Main_DrawNPCs;
//        }

//        private void On_Main_DrawNPCs(On_Main.orig_DrawNPCs orig, Main self, bool behindTiles)
//        {
//            orig.Invoke(self, behindTiles);
//            Target.Request();
//            if (!Target.IsReady)
//                return;
//            FrozenNPCTarget.behindTiles = behindTiles;

//            Main.spriteBatch.End();

//            Effect shader = IceShader.Value;
//            shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition * Main.GameZoomTarget);
//            shader.Parameters["uImageSize0"].SetValue(Target.GetTarget().Size());
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader, Main.Transform);
//            //Main.spriteBatch.GraphicsDevice.Textures[1] = Target.GetTarget();
//            Main.spriteBatch.Draw(Target.GetTarget(), Main.screenLastPosition - Main.screenPosition, Color.White);
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, default, Main.Transform);
//        }

//        public override void Unload()
//        {
//            Main.ContentThatNeedsRenderTargets.Remove(Target);
//        }
//    }
//}
