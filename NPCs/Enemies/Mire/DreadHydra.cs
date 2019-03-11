using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Mire
{
	public class DreadHydra : DreadHydraHandler
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dread Hydra");
            Main.npcFrameCount[npc.type] = 2;
		}	
		
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 30;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 60);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 10000;
            npc.defense = 40;
            npc.damage = 70;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.6f;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.behindTiles = true;
        }

		public bool isGazer;
		public int dustDelay = 0;
		public static Texture2D chainTex;

		public Vector2 pos = default(Vector2);
        
        public override bool G_CanSpawn(int x, int y, int type, Player player)
        {
            return player.GetModPlayer<AAPlayer>(mod).ZoneMire && AAWorld.downedYamata && Main.rand.Next(5) == 0 && Main.tile[x, y] != null && Main.tileSolid[Main.tile[x, y].type] && !Main.tileSolidTop[Main.tile[x, y].type];
        }

        public override void NPCLoot()
		{
			BaseMod.BaseAI.DropItem(npc, mod.ItemType("DreadScale"), Main.expertMode ? 1 + Main.rand.Next(2) : Main.rand.Next(1), 3, 100, true); 			
		}

		public override void AI()
		{
			if (pos == default(Vector2)) { pos = (npc.Center + new Vector2(0f, 24)) / 16f; pos.X = (int)pos.X; pos.Y = (int)pos.Y; }
            BaseAI.AIPlant(npc, ref npc.ai, true, pos, true, 100f, 350f, 50, 200, 0.07f, 5f);
            npc.rotation = 0;
            BaseAI.ShootPeriodic(npc, npc.position, npc.width, npc.height, mod.ProjectileType<Bosses.Hydra.HydraBreath>(), ref npc.ai[3], 120f, npc.damage / 2, 7, true);

            if(npc.ai[3] <= 30)
            {
                npc.frame.Y = Main.npcTexture[npc.type].Height / 2;
            }
            else
            {
                npc.frame.Y = 0;
            }
        }

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
            chainTex = mod.GetTexture("NPCs/Enemies/Mire/DreadHydra_Neck");
			Vector2 endPoint = BaseMod.BaseUtility.RotateVector(npc.Center, npc.Center + new Vector2(-2f, 0), npc.rotation + (npc.spriteDirection == -1 ? (float)Math.PI : 0));
			BaseMod.BaseDrawing.DrawChain(sb, new Texture2D[] { null, chainTex, null }, 0, endPoint, (pos * 16) + new Vector2(8f, 8f));
			return true;
		}
	}
}