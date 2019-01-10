using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using BaseMod;
using Terraria.Graphics.Shaders;
using System;
using Terraria.Graphics.Effects;
using Terraria.DataStructures;
using Terraria.Graphics;

namespace AAMod.NPCs.Bosses.Zero
{
	public class ZeroDeactivated : ModNPC
	{
        public static int ZeroShieldStrength = 0;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Strange Machine");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 94;
			npc.lifeMax = 20000;
			npc.damage = 0;
			npc.defense = 20;
			npc.knockBackResist = 0f;
			npc.width = 170;
			npc.height = 359;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.noGravity = true;
			npc.npcSlots = 0;
			npc.noTileCollide = true;
			npc.alpha = 0;
            npc.immortal = true;
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
		}

       
        
		public override void AI()
		{
            if (AAWorld.zeroUS == false)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Zero"));
                npc.active = false;
            }
            npc.timeLeft = 1;
            if (npc.ai[2] == 1f)
            {
                npc.velocity = Vector2.UnitY * npc.velocity.Length();
                if (npc.velocity.Y < 0.25f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.02f;
                }
                if (npc.velocity.Y > 0.25f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.02f;
                }
                npc.dontTakeDamage = true;
            }
        }

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
							 Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
			return false;
		}

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float num88 = ZeroHandler.Shield / (float)NPC.ShieldStrengthTowerMax;
            if (ZeroHandler.Shield > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                var center = npc.Center - Main.screenPosition;
                float num89 = 0f;
                if (npc.ai[3] > 0f && npc.ai[3] <= 30f)
                {
                    num89 = 1f - npc.ai[3] / 30f;
                }
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), Color.White * (num88 * 0.8f + 0.2f), npc.rotation, new Vector2(300f, 300f), npc.scale * (1f + num89 * 0.05f), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + num89 * 0.5f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
            if (npc.ai[3] > 0f)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                var center = npc.Center - Main.screenPosition;
                float num90 = npc.ai[3] / 120f;
                float num91 = Math.Min(npc.ai[3] / 30f, 1f);
                Filters.Scene["Tremor:Zero"].GetShader().UseIntensity(Math.Min(5f, 15f * num90) + 1f).UseProgress(num90);
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), new Color(new Vector4(1f - (float)Math.Sqrt(num91))), npc.rotation, new Vector2(300f, 300f), npc.scale * (1f + num91), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(2f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
        }
    }

    public class ZeroData : ScreenShaderData
    {
        int ZeroIndex;

        public ZeroData(string passName) : base(passName) { }

        void UpdatePuritySpiritIndex()
        {
            int Zero = AAMod.instance.NPCType("ZeroDeactivated");
            if (ZeroIndex >= 0 && Main.npc[ZeroIndex].active && Main.npc[ZeroIndex].type == Zero)
            {
                return;
            }
            ZeroIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == Zero)
                {
                    ZeroIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdatePuritySpiritIndex();
            if (ZeroIndex != -1)
            {
                UseTargetPosition(Main.npc[ZeroIndex].Center);
            }
            base.Apply();
        }
    }

    public class ZeroHandler : ModWorld
    {
        public static int ZX = -1;
        public static int ZY = -1;
        public static bool ZeroSleep;
        public static int Shield;
        public static bool ZeroUS;

        public override void Initialize()
        {
            ZeroUS = AAWorld.zeroUS;
            ZX = -1;
            ZY = -1;
        }

        public override void PreUpdate()
        {
            ZeroSleep = NPC.AnyNPCs(mod.NPCType("ZeroDeactivated"));
        }

        public override TagCompound Save()
        {
            var tag = new TagCompound
            {
                {"ZeroSleep", ZeroSleep}
            };
            if (ZX != -1)
            {
                tag.Add("ZX", ZX);
                tag.Add("ZY", ZY);
            }
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            ZeroSleep = tag.GetBool("ZeroSleep");
            if (tag.ContainsKey("ZX"))
            {
                ZX = tag.GetInt("ZX");
                ZY = tag.GetInt("ZY");
                NPC.NewNPC(ZX, ZY, mod.NPCType("ZeroDeactivated"));
            }
        }

        public override void PostUpdate()
        {
            if (!ZeroUS)
            {
                PutZeroHerelul(1, mod.NPCType<ZeroDeactivated>());
            }
        }

        public void PutZeroHerelul(int position, int whoAmI)
        {
            int x = Main.maxTilesX / 6 * (1 + position);
            Point spawnPos = AAWorld.WHERESDAVOIDAT;

            Vector2 SpawnHereYaDingus()
            {
                Vector2 pt = new Vector2(AAWorld.WHERESDAVOIDAT.X, AAWorld.WHERESDAVOIDAT.Y);
                return pt;
            }

            if (whoAmI == -1)
            {
                whoAmI = NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, mod.NPCType<ZeroDeactivated>());
                ZX = spawnPos.X;
                ZY = spawnPos.Y;
            }
            else
            {
                Main.npc[whoAmI].Center = SpawnHereYaDingus();
                ZeroSleep = true;
            }
            if (Main.netMode == 2 && whoAmI < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, number: whoAmI);
            }
        }
    }
}
