using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Dusts;
using System.IO;

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
            npc.lifeMax = 125000;
            npc.damage = 200;
            npc.defense = 100;
            npc.value = Item.sellPrice(0, 10, 0, 0);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.width = 68;
            npc.height = 68;
            npc.boss = true;
            npc.aiStyle = -1;
			npc.timeLeft = 500;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.DeathSound = null;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox");
            musicPriority = MusicPriority.BossHigh;
            bossBag = mod.ItemType("DBBag");			
		}

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
		{
			rotation = npc.rotation;
		}

		public override bool CheckActive()
		{
			npc.timeLeft--;
			return npc.timeLeft < 50;
		}

		public void HandleDayNightCycle()
		{
			bool daybringerExists = NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<DaybringerHead>());
			bool nightcrawlerExists = NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<NightcrawlerHead>());
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
        public override bool  PreAI()
        {
            if (Main.netMode != 1 && !initCustom)
			{
				initCustom = true;
				customAI[0] += npc.whoAmI % 7 * 12; //so it doesn't pew all at once
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
				int dustType = nightcrawler ? Terraria.ModLoader.ModContent.DustType<NightcrawlerDust>() : Terraria.ModLoader.ModContent.DustType<DaybringerDust>();
				for (int k = 0; k < 10; k++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, dustType, (int)(npc.velocity.X * 0.2f), (int)(npc.velocity.Y * 0.2f), 0, default, 1.5f);
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
					if(npc.velocity.Y < -20f) npc.velocity.Y = -20f;
					return false;
				}
			}else
			{
				npc.timeLeft = 300; //pieces should not despawn naturally, only despawn when the head does
			}
			
			int aiCount = 2;
			npc.damage = 200;
			npc.defense = 100;
            if (wormStronger)
			{
				aiCount = !nightcrawler ? 6 : 4; 
				npc.damage = 300;		
				npc.defense = !nightcrawler ? 120 : 150;
            }	
            if (!isHead && NPC.CountNPCS(Terraria.ModLoader.ModContent.NPCType<Equiprobe>()) < 15)
            {
				SpawnProbe();
			}
			for(int m = 0; m < aiCount; m++)
            {
                WormAI(wormStronger, isHead);
			}			
			npc.spriteDirection = 1;
			prevWormStronger = wormStronger;
			return false;
        }

		public int probeCounter = -1;
        public void SpawnProbe()
        {
			if(probeCounter == -1)
				probeCounter = 500 + Main.rand.Next(750);
			if(Main.netMode == NetmodeID.MultiplayerClient || npc.whoAmI % 3 != 0) return;
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

		public int playerTooFarDist = 16000; //1000 tile radius, these worms move fast!		
		public bool TargetClosest()
		{
			int[] players = BaseAI.GetPlayers(npc.Center, Math.Min(20000f, playerTooFarDist * 3));
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

		bool spawnedGore = false;
        public override void HitEffect(int hitDirection, double damage)
        {
			int dustType = nightcrawler ? Terraria.ModLoader.ModContent.DustType<NightcrawlerDust>() : Terraria.ModLoader.ModContent.DustType<DaybringerDust>();
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, dustType, hitDirection, -1f, 0, default, 1.2f);
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
						Dust.NewDust(npc.position, npc.width, npc.height, dustType, hitDirection, -1f, 0, default, 1.5f);
					}
				}
            }
        }

        public override void NPCLoot()
        {
            int otherWormAlive = nightcrawler ? mod.NPCType("DaybringerHead") : mod.NPCType("NightcrawlerHead");
            if (!nightcrawler)
            {
                AAWorld.downedDB = true;
                BaseAI.DropItem(npc, mod.ItemType("DBTrophy"), 1, 1, 15, true);
            }
            else
            {
                AAWorld.downedNC = true;
                BaseAI.DropItem(npc, mod.ItemType("NCTrophy"), 1, 1, 15, true);
            }
            if (NPC.CountNPCS(otherWormAlive) == 0)
            {
                AAWorld.downedEquinox = true;
            }
			string wormType = nightcrawler ? "Nightcrawler" : "Daybringer";
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(wormType + "Trophy"));
			}
			if (Main.expertMode)
			{
                npc.DropBossBags();
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
			Color c = Color.White * (Main.mouseTextColor / 255f);
			//c.A = 255;
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
				string texName = "NPCs/Bosses/Equinox/";
				if(npc.type == mod.NPCType("DaybringerHead")){ texName += "DaybringerHeadBig"; }else
				if(npc.type == mod.NPCType("DaybringerBody")){ texName += "DaybringerBodyBig"; }else				
				if(npc.type == mod.NPCType("DaybringerTail")){ texName += "DaybringerTailBig"; }else				
				if(npc.type == mod.NPCType("NightcrawlerHead")){ texName += "NightcrawlerHeadBig"; }else
				if(npc.type == mod.NPCType("NightcrawlerBody")){ texName += "NightcrawlerBodyBig"; }else
				if(npc.type == mod.NPCType("NightcrawlerTail")){ texName += "NightcrawlerTailBig"; }
				tex = mod.GetTexture(texName);
				
				int diff = Main.LocalPlayer.miscCounter % 50;
				float diffFloat = diff / 50f;
				float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f); //did it this way so it's syncronized between all the segments
                BaseDrawing.DrawAura(spritebatch, tex, 0, npc, auraPercent, 2f, 0f, 0f, GetAuraAlpha());				
			}
            BaseDrawing.DrawTexture(spritebatch, tex, 0, npc, Color.White); //GetAuraAlpha());				
			return false;
		}	

        public void WormAI(bool wormStronger, bool isHead)
        {
            bool expertMode = Main.expertMode;
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            npc.velocity.Length();
            if (Main.netMode != 1 && isHead)
            {
                if (internalAI[0] !=1)
                {
                    int Length = nightcrawler ? 24 : 30;
                    int whoamI = npc.whoAmI;
                    for (int num36 = 0; num36 < Length; num36++)
                    {
                        int segment;
                        if (num36 >= 0 && num36 < Length)
                        {
                            segment = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), nightcrawler ? mod.NPCType("NightcrawlerBody") : mod.NPCType("DaybringerBody"), npc.whoAmI);
                        }
                        else
                        {
                            segment = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), nightcrawler ? mod.NPCType("NightcrawlerTail") : mod.NPCType("DaybringerTail"), npc.whoAmI);
                        }
                        Main.npc[segment].realLife = npc.whoAmI;
                        Main.npc[segment].ai[2] = npc.whoAmI;
                        Main.npc[segment].ai[1] = whoamI;
                        Main.npc[whoamI].ai[0] = segment;
                        npc.netUpdate = true;
                        whoamI = segment;
                    }
                    internalAI[0] = 1;
                }
                npc.localAI[0] += 1f;
                if (npc.localAI[0] >= 360f)
                {
                    npc.localAI[0] = 0f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                    int damage = expertMode ? 50 : 70;
                    float xPos = (Main.rand.Next(2) == 0 ? npc.position.X + 300f : npc.position.X - 300f);
                    Vector2 vector2 = new Vector2(xPos, npc.position.Y + Main.rand.Next(-300, 301));
                    Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, 465, damage, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
                npc.velocity.Y = npc.velocity.Y - 10f;
                if ((double)npc.position.Y < Main.topWorld + 16f)
                {
                    npc.velocity.Y = npc.velocity.Y - 10f;
                }
            }
            if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 10000f)
            {
                for (int num957 = 0; num957 < 200; num957++)
                {
                    if (Main.npc[num957].aiStyle == npc.aiStyle)
                    {
                        Main.npc[num957].active = false;
                    }
                }
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            else if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
            }

            float num188 = 16f;
            if (wormStronger)
            {
                num188 = !nightcrawler ? 20f : 16f;
            }
            float num189 = .4f;
            Vector2 vector18 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num191 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2;
            float num192 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2;
            int num42 = -1;
            int num43 = (int)(Main.player[npc.target].Center.X / 16f);
            int num44 = (int)(Main.player[npc.target].Center.Y / 16f);
            for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
            {
                for (int num46 = num44; num46 <= num44 + 15; num46++)
                {
                    if (WorldGen.SolidTile2(num45, num46))
                    {
                        num42 = num46;
                        break;
                    }
                }
                if (num42 > 0)
                {
                    break;
                }
            }
            if (num42 > 0)
            {
                num42 *= 16;
                float num47 = num42 - 800;
                if (Main.player[npc.target].position.Y > num47)
                {
                    num192 = num47;
                    if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) < 500f)
                    {
                        if (npc.velocity.X > 0f)
                        {
                            num191 = Main.player[npc.target].Center.X + 600f;
                        }
                        else
                        {
                            num191 = Main.player[npc.target].Center.X - 600f;
                        }
                    }
                }
            }
            else
            {
                num188 = 13f;
                num189 = 0.4f;
            }
            float num48 = num188 * 1.3f;
            float num49 = num188 * 0.7f;
            float num50 = npc.velocity.Length();
            if (num50 > 0f)
            {
                if (num50 > num48)
                {
                    npc.velocity.Normalize();
                    npc.velocity *= num48;
                }
                else if (num50 < num49)
                {
                    npc.velocity.Normalize();
                    npc.velocity *= num49;
                }
            }
            if (num42 > 0)
            {
                for (int num51 = 0; num51 < 200; num51++)
                {
                    if (Main.npc[num51].active && Main.npc[num51].type == npc.type && num51 != npc.whoAmI)
                    {
                        Vector2 vector3 = Main.npc[num51].Center - npc.Center;
                        if (vector3.Length() < 400f)
                        {
                            vector3.Normalize();
                            vector3 *= 1000f;
                            num191 -= vector3.X;
                            num192 -= vector3.Y;
                        }
                    }
                }
            }
            else
            {
                for (int num52 = 0; num52 < 200; num52++)
                {
                    if (Main.npc[num52].active && Main.npc[num52].type == npc.type && num52 != npc.whoAmI)
                    {
                        Vector2 vector4 = Main.npc[num52].Center - npc.Center;
                        if (vector4.Length() < 60f)
                        {
                            vector4.Normalize();
                            vector4 *= 200f;
                            num191 -= vector4.X;
                            num192 -= vector4.Y;
                        }
                    }
                }
            }
            num191 = (int)(num191 / 16f) * 16;
            num192 = (int)(num192 / 16f) * 16;
            vector18.X = (int)(vector18.X / 16f) * 16;
            vector18.Y = (int)(vector18.Y / 16f) * 16;
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num191 = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - vector18.X;
                    num192 = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - vector18.Y;
                }
                catch
                {
                }
                npc.rotation = (float)Math.Atan2(num192, num191) + 1.57f;
                int num194 = npc.width;
                num193 = (num193 - num194) / num193;
                num191 *= num193;
                num192 *= num193;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num191;
                npc.position.Y = npc.position.Y + num192;
                if (num191 < 0f)
                {
                    npc.spriteDirection = -1;
                }
                else if (num191 > 0f)
                {
                    npc.spriteDirection = 1;
                }
            }
            else
            {
                num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
                float num196 = Math.Abs(num191);
                float num197 = Math.Abs(num192);
                float num198 = num188 / num193;
                num191 *= num198;
                num192 *= num198;
                if ((npc.velocity.X > 0f && num191 > 0f) || (npc.velocity.X < 0f && num191 < 0f) || (npc.velocity.Y > 0f && num192 > 0f) || (npc.velocity.Y < 0f && num192 < 0f))
                {
                    if (npc.velocity.X < num191)
                    {
                        npc.velocity.X = npc.velocity.X + num189;
                    }
                    else
                    {
                        if (npc.velocity.X > num191)
                        {
                            npc.velocity.X = npc.velocity.X - num189;
                        }
                    }
                    if (npc.velocity.Y < num192)
                    {
                        npc.velocity.Y = npc.velocity.Y + num189;
                    }
                    else
                    {
                        if (npc.velocity.Y > num192)
                        {
                            npc.velocity.Y = npc.velocity.Y - num189;
                        }
                    }
                    if (Math.Abs(num192) < num188 * 0.2 && ((npc.velocity.X > 0f && num191 < 0f) || (npc.velocity.X < 0f && num191 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num189 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num189 * 2f;
                        }
                    }
                    if (Math.Abs(num191) < num188 * 0.2 && ((npc.velocity.Y > 0f && num192 < 0f) || (npc.velocity.Y < 0f && num192 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 2f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 2f;
                        }
                    }
                }
                else
                {
                    if (num196 > num197)
                    {
                        if (npc.velocity.X < num191)
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                        }
                        else if (npc.velocity.X > num191)
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                        }
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num188 * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num189;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - num189;
                            }
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num192)
                        {
                            npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
                        }
                        else if (npc.velocity.Y > num192)
                        {
                            npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
                        }
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num188 * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num189;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num189;
                            }
                        }
                    }
                }
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
        }
    }
}