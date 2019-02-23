using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Dusts;

namespace AAMod.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]	
	public class DaybringerHead : ModNPC
	{
        public float[] customAI = new float[2];		
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
            npc.DeathSound = null;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;			
            
            musicPriority = MusicPriority.BossHigh;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            bossBag = mod.ItemType("DBBag");			
		}
		
		public override void BossHeadRotation(ref float rotation)
		{
			rotation = npc.rotation;
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

		bool prevWormStronger = false;
		bool initCustom = false;
        public override void AI()
        {
            if (Config.EquinoxMusic)
            {
                music = music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox");
            }
            else
            {
                music = MusicID.Boss5;
            }

            if (Main.netMode != 1 && !initCustom)
			{
				initCustom = true;
				customAI[0] += (npc.whoAmI % 7) * 12; //so it doesn't pew all at once
				npc.velocity.X += 0.1f;
				npc.velocity.Y -= 4f;
			}
			bool isHead = npc.type == mod.NPCType("DaybringerHead") || npc.type == mod.NPCType("NightcrawlerHead");
			if(isHead)
			{
				HandleDayNightCycle();
			}
			bool isDay = Main.dayTime;
			bool wormStronger = (nightcrawler && !isDay) ||  (!nightcrawler && isDay);
			if(wormStronger != prevWormStronger)
			{
				int dustType = (nightcrawler ? mod.DustType<NightcrawlerDust>() : mod.DustType<DaybringerDust>());
				for (int k = 0; k < 10; k++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, dustType, (int)(npc.velocity.X * 0.2f), (int)(npc.velocity.Y * 0.2f), 0, default(Color), 1.5f);
					Main.dust[dustID].noGravity = true;
				}
			}

			if(isHead) //prevents despawn and allows them to run away
			{
				bool foundTarget = TargetClosest();		
				if(foundTarget)
				{
					npc.timeLeft = 300;	
				}else
				{					
					if(npc.timeLeft > 50) npc.timeLeft = 50;
					npc.velocity.Y -= 0.2f;
					if(npc.velocity.Y > 20f) npc.velocity.Y = 20f;
					return;
				}
			}else
			{
				npc.timeLeft = 300; //pieces should not despawn naturally, only despawn when the head does
			}
			
			float wormDistance = -26f;
			int aiCount = 2;
			float moveSpeedMax = 16f;	
			npc.damage = 200;
			npc.defense = 100;
			//int laserFireRate = 300;
			//int laserInterval = 20;
			if(wormStronger)
			{
				aiCount = (!nightcrawler ? 6 : 4); //daybringer is a bit faster
				moveSpeedMax = (!nightcrawler ? 20f : 16f); //ditto as above
				npc.damage = 300;		
				npc.defense = (!nightcrawler ? 120 : 150); //nightcrawler has more defense
				//laserFireRate = 200;
				//laserInterval = 10;
			}

			/*if(Main.netMode != 1 && isHead) //shoot lasers (disabled - probes fire lasers)
			{
				Player player = Main.player[npc.target];
				int projType = (nightcrawler ? mod.ProjType("Moonray") : mod.ProjType("Sunbeam"));
				if((customAI[0] <= (laserInterval * 3)) && customAI[0] % laserInterval == 0 && Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
					BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.2f), 0f, 4f);
				customAI[0]--;
				if(customAI[0] <= 0) customAI[0] = laserFireRate;			
			}*/
			if(!isHead)
			{
				SpawnProbe();
			}
			for(int m = 0; m < aiCount; m++)
			{
				int[] wormTypes = (nightcrawler ? new int[]{ mod.NPCType("NightcrawlerHead"), mod.NPCType("NightcrawlerBody"), mod.NPCType("NightcrawlerTail") } : new int[]{ mod.NPCType("DaybringerHead"), mod.NPCType("DaybringerBody"), mod.NPCType("DaybringerTail") });
				BaseMod.BaseAI.AIWorm(npc, wormTypes, 30, wormDistance, moveSpeedMax, 0.07f, true, false, false, false, false, false);	
			}
			npc.spriteDirection = 1;
			prevWormStronger = wormStronger;
        }

		public int probeCounter = -1;
        public void SpawnProbe()
        {
			if(probeCounter == -1)
				probeCounter = 500 + Main.rand.Next(750);
			if(Main.netMode == 1 || npc.whoAmI % 3 != 0) return;
			probeCounter = Math.Max(0, probeCounter - 1);
            if (probeCounter <= 0)
            {
				probeCounter = 500 + Main.rand.Next(750);
				if(BaseAI.GetNPCs(npc.Center, mod.NPCType("Equiprobe"), 8000f).Length < 6)
				{
					int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Equiprobe"), 0);
					Main.npc[npcID].netUpdate = true;
				}
            }
        }

		public int playerTooFarDist = 3200; //200 tile radius, these worms move fast!		
		public bool TargetClosest()
		{
			int[] players = BaseAI.GetPlayers(npc.Center, Math.Min(6000f, playerTooFarDist * 3));
			float dist = 999999999f;
			int foundPlayer = -1;
			for (int m = 0; m < players.Length; m++)
			{
				Player p = Main.player[players[m]];
				if (Vector2.Distance(p.Center, npc.Center) < dist)
				{
					dist = Vector2.Distance(p.Center, npc.Center);
					foundPlayer = p.whoAmI;
				}
			}
			if (foundPlayer != -1)
			{
				BaseAI.SetTarget(npc, foundPlayer);
				return true;
			}
			return false;
		}		
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

		bool spawnedGore = false;
        public override void HitEffect(int hitDirection, double damage)
        {
			int dustType = (nightcrawler ? mod.DustType<NightcrawlerDust>() : mod.DustType<DaybringerDust>());
            for (int k = 0; k < 5; k++)
            {
                int dustID = Dust.NewDust(npc.position, npc.width, npc.height, dustType, hitDirection, -1f, 0, default(Color), 1.2f);
            }
            if (npc.life <= 0 || (npc.life - damage <= 0))
            {			
				Main.dayRate = 1;
                Main.fastForwardTime = false;	
				if(!spawnedGore)
				{
					spawnedGore = true;
					bool isHead = npc.type == mod.NPCType("DaybringerHead") || npc.type == mod.NPCType("NightcrawlerHead");
					bool isBody = npc.type == mod.NPCType("DaybringerBody") || npc.type == mod.NPCType("NightcrawlerBody");						
					if(nightcrawler)
					{
						if(isHead)
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/NCGore1"), 1f);	
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/NCGore2"), 1f);						
						}else
						if(isBody)
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/NCGore3"), 1f);							
						}else
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/NCGore4"), 1f);						
						}
					}else
					{
						if(isHead)
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/DBGore1"), 1f);	
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/DBGore2"), 1f);						
						}else
						if(isBody)
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/DBGore3"), 1f);							
						}else
						{
							Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/DBGore4"), 1f);						
						}					
					}
					for (int k = 0; k < 15; k++)
					{
						int dustID = Dust.NewDust(npc.position, npc.width, npc.height, dustType, hitDirection, -1f, 0, default(Color), 1.5f);
					}
				}
            }
        }

        public override void NPCLoot()
        {
            int otherWormAlive = (nightcrawler ? mod.NPCType("DaybringerHead") : mod.NPCType("NightcrawlerHead"));
            if (!nightcrawler)
            {
                BaseAI.DropItem(npc, mod.ItemType("DBTrophy"), 1, 1, 15, true);
            }
            else
            {
                BaseAI.DropItem(npc, mod.ItemType("NCTrophy"), 1, 1, 15, true);
            }
            if (NPC.CountNPCS(otherWormAlive) == 0)
            {
                AAWorld.downedEquinox = true;
            }
			string wormType = (nightcrawler ? "Nightcrawler" : "Daybringer");
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
                else
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
                if (!nightcrawler)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"), Main.rand.Next(30, 75));
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DarkEnergy"), Main.rand.Next(30, 75));
                }
			}
        }

		public Color GetAuraAlpha()
		{
			Color c = (Color.White * ((float)Main.mouseTextColor / 255f));
			c.A = 255;
			return c;
		}

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }

        public void MakeSegmentsImmune(NPC npc, int id)
        {
            if (npc.realLife >= 0)
            {
                bool last = false;
                NPC parent = Main.npc[npc.realLife];
                parent.lifeRegen = npc.lifeRegen;
                int i = 0;
                while (parent.ai[0] > 0 || last)
                {
                    parent.immune[id] = npc.immune[id];
                    for (int j = 0; j < npc.buffType.Length; j++)
                    {
                        if (npc.buffType[j] > 0 && npc.buffTime[j] > 0)
                        {
                            parent.buffType[j] = npc.buffType[j];
                            parent.buffTime[j] = npc.buffTime[j];
                        }
                    }
                    if (last) { break; }
                    parent = Main.npc[(int)parent.ai[0]];
                    if (parent.ai[0] == 0) { last = true; }
                    if (i++ > 200) { throw new InvalidOperationException("Recursion detected"); } // Just in case
                }
            }
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