using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PreHM
{
    public class TerraCrawler : ModNPC
    {
        public override string Texture { get { return "AAMod/NPCs/Enemies/Terrarium/PreHM/TerraCrawler"; } }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Crawler");
			Main.npcFrameCount[npc.type] = 5;
		}

        public bool Val = false;
        public int[] subNPCs = new int[0];
        public int swapTicks = 0, swapTicksMax = 20;

        public override void SetDefaults()
		{
            npc.lifeMax =  350;
            npc.defense = 20;
            npc.damage = 50;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
        }

        public override void AI()
        {
            if (npc.type == mod.NPCType<TerraCrawler>())
            {
                if (npc.alpha != 0)
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SummonDust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].noLight = true;
                    }
                }
                npc.alpha -= 12;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
                npc.noGravity = false;
                BaseAI.AIZombie(npc, ref npc.ai, false, true, -1, .07f, 1f, 9, 5, 60, true, 10, 60, true, onTileCollide);
                BaseAI.AIPounce(npc, Main.player[npc.target], Math.Max(3.3f, npc.velocity.X), 2f, -5.2f, 50, 60);
                if (Main.netMode != 1 && Val)
                {
                    int target = npc.target;
                    npc.Transform(mod.NPCType("TerraCrawlerCrawling"));
                    npc.target = target;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else
            {
                npc.noGravity = true;
                int status = -1;
                BaseAI.AISnail(npc, ref npc.ai, ref status, 1.2f, 0.2f);
                if (Val)
                {
                    npc.position += npc.velocity;
                    BaseAI.AISnail(npc, ref npc.ai, ref status, 1.2f, 0.2f);
                    npc.netUpdate = true;
                }
                subNPCs[0] = status;
                if (Main.netMode != 1) swapTicks--;
                if (Main.netMode != 1 && swapTicks <= 0 && npc.rotation == 0 && status == 0)
                {
                    int target = npc.target;
                    npc.Transform(mod.NPCType("TerraCrawler"));
                    npc.target = target;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            
        }
        public void onTileCollide(bool xCollision, bool yCollision, Vector2 oldVel, Vector2 newVel)
        {
            if (!Val && (oldVel.X != newVel.X || oldVel.X != npc.velocity.X)) { Val = true; }
        }
    }

    public class TerraCrawlerCrawling : TerraCrawler
    {
        public override string Texture { get { return "AAMod/NPCs/Enemies/Terrarium/PreHM/TerraCrawler"; } }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}
