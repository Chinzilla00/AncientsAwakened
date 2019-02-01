using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]	
	public class DaybringerHead : ModNPC
	{
		public bool nightcrawler = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daybringer");
            Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 130000;
            npc.damage = 200;
            npc.defense = 100;
            npc.knockBackResist = 0f;
            npc.width = 68;
            npc.height = 68;
            npc.value = Item.buyPrice(0, 55, 0, 0);
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.DeathSound = null;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox");
            musicPriority = MusicPriority.BossHigh;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            bossBag = mod.ItemType("DBBag");			
		}

		public void HandleDayNightCycle()
		{
			bool daybringerExists = NPC.AnyNPCs(mod.NPCType<DaybringerHead>());
			bool nightcrawlerExists = NPC.AnyNPCs(mod.NPCType<NightcrawlerHead>());
			if (daybringerExists && nightcrawlerExists)
            {
                if (Main.expertMode)
                {
                    Main.fastForwardTime = true;
                    Main.dayRate = 20;
                }else
                {
                    Main.fastForwardTime = true;
                    Main.dayRate = 15;
                }
            }else
            if (daybringerExists && !nightcrawlerExists)
            {
                Main.fastForwardTime = true;
                Main.dayTime = true;
                Main.dayRate = 0;
            }else
            if (!daybringerExists && nightcrawlerExists)
            {
                Main.fastForwardTime = true;
                Main.dayTime = false;
                Main.dayRate = 0;
            }else
            {
                Main.dayRate = 1;
                Main.fastForwardTime = false;
            }
		}

        public override void AI()
        {
			bool isHead = npc.type == mod.NPCType("DaybringerHead") || npc.type == mod.NPCType("NightcrawlerHead");
			if(isHead)
			{
				HandleDayNightCycle();
			}
			bool isDay = Main.dayTime;
			bool wormStronger = (nightcrawler && !isDay) ||  (!nightcrawler && isDay);
			float wormDistance = -26f;
			int aiCount = (wormStronger ? 4 : 2);
			float moveSpeedMax = 16f;
			for(int m = 0; m < aiCount; m++)
			{
				int[] wormTypes = (nightcrawler ? new int[]{ mod.NPCType("NightcrawlerHead"), mod.NPCType("NightcrawlerBody"), mod.NPCType("NightcrawlerTail") } : new int[]{ mod.NPCType("DaybringerHead"), mod.NPCType("DaybringerBody"), mod.NPCType("DaybringerTail") });
				BaseMod.BaseAI.AIWorm(npc, wormTypes, 30, wormDistance, moveSpeedMax, 0.07f, true, false, false, true, true, false);	
			}
			npc.spriteDirection = 1;
        }

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

        public override void HitEffect(int hitDirection, double damage)
        {
			//TODO: ADD DUST
            for (int k = 0; k < 5; k++)
            {
                //Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life == 0)
            {
				Main.dayRate = 1;
                Main.fastForwardTime = false;			
                for (int k = 0; k < 5; k++)
                {
                    //Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDustLight>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            int otherWormAlive = (nightcrawler ? mod.NPCType("DaybringerHead") : mod.NPCType("NightcrawlerHead"));
            if (NPC.CountNPCS(otherWormAlive) == 0)
            {
                AAWorld.downedEquinox = true;
            }
			string wormType = (nightcrawler ? "Nightcrawler" : "Daybringer");
            if (nightcrawler)
            {
                AAWorld.downedDB = true;
            }
            if (!nightcrawler)
            {
                AAWorld.downedNC = true;
            }
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(wormType + "Trophy"));
			}
			if (Main.expertMode)
			{
                if (!nightcrawler)
                {
                    npc.DropBossBags();
                }
                if (nightcrawler)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NCBag"));
                }
			}
			else
			{
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(wormType + "Mask"));
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"), Main.rand.Next(30, 75));
			}
        }

		public Color GetAuraAlpha()
		{
			Color c = (Color.White * ((float)Main.mouseTextColor / 255f));
			c.A = 255;
			return c;
		}

		public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
		{
			bool wormStronger = (nightcrawler && !Main.dayTime) ||  (!nightcrawler && Main.dayTime);
			Texture2D tex = Main.npcTexture[npc.type];
			if(wormStronger)
			{
				string texName = ("NPCs/Bosses/Equinox/");
				if(npc.type == mod.NPCType("DaybringerHead")){ texName += "DaybringerHeadBig"; }else
				if(npc.type == mod.NPCType("DaybringerBody")){ texName += "DaybringerBodyBig"; }else				
				if(npc.type == mod.NPCType("DaybringerTail")){ texName += "DaybringerTailBig"; }else				
				if(npc.type == mod.NPCType("NightcrawlerHead")){ texName += "NightcrawlerHeadBig"; }else
				if(npc.type == mod.NPCType("NightcrawlerBody")){ texName += "NightcrawlerBodyBig"; }else
				if(npc.type == mod.NPCType("NightcrawlerTail")){ texName += "NightcrawlerTailBig"; }
				tex = mod.GetTexture(texName);
				
				int diff = (Main.player[Main.myPlayer].miscCounter % 50);
				float diffFloat = (float)diff / 50f;
				float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f); //did it this way so it's syncronized between all the segments
				BaseMod.BaseDrawing.DrawAura(spritebatch, tex, 0, npc, auraPercent, 2f, 0f, 0f, GetAuraAlpha());				
			}
			
			BaseMod.BaseDrawing.DrawTexture(spritebatch, tex, 0, npc, GetAuraAlpha());				
			return false;
		}	
    }
}