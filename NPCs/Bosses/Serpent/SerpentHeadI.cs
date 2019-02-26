using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    [AutoloadBossHead]	
	public class SerpentHeadI : ModNPC
	{
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentHeadI"; } }
        bool TailSpawned = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.damage = 40;
			npc.npcSlots = 5f;
            npc.damage = 35;
            npc.width = 38;
            npc.height = 38;
            npc.defense = 25;
            npc.lifeMax = 6000;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 0, 10, 0);
            bossBag = mod.ItemType<Items.Boss.Serpent.SerpentBag>();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6");
        }

        private bool fireAttack;
        private int attackCounter;
        private int attackTimer;

		public bool tongueFlick = false;
		public bool tongueFlickDir = false;
		public int tongueFlickCounter = 0;
        private int RunOnce = 0;
        private int StopSnow = 0;

        public override void AI()
        {
            if (RunOnce == 0)
            {
                RainStart();
                RunOnce = 1;
            }
            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f || Main.player[npc.target].dead)
            {
                if (StopSnow == 0)
                {
                    RainStop();
                    StopSnow = 1;
                }
            }
            BaseMod.BaseAI.AIWorm(npc, new int[]{ mod.NPCType("SerpentHeadI"), mod.NPCType("SerpentBodyI"), mod.NPCType("SerpentTailI") }, 12, 8f, 12f, 0.1f, false, false);

            bool isHead = npc.type == mod.NPCType("SerpentHeadI");
			bool isBody = npc.type == mod.NPCType("SerpentBodyI");			
			if(isHead)
			{
				if(Main.netMode != 2 && !tongueFlick && Main.rand.Next(20) == 0)
				{
					tongueFlick = true;
				}
				if(Main.netMode != 1) //frost breath attack
				{
					FrostAttack();
				}

				if(tongueFlick)
				{
					if(tongueFlickDir)
					{
						tongueFlickCounter--;
						if(tongueFlickCounter <= 0)
						{
							tongueFlickCounter = 8;
							npc.frame.Y -= npc.frame.Height;
							if(npc.frame.Y <= 0) 
								tongueFlick = tongueFlickDir = false;
						}			
					}else
					{
						tongueFlickCounter--;
						if(tongueFlickCounter <= 0)
						{
							tongueFlickCounter = 8;
							npc.frame.Y += npc.frame.Height;
							if(npc.frame.Y >= (npc.frame.Height * 3)) 
								tongueFlickDir = true;
						}
					}
				}
			}else
			if(isBody)
			{
				if(npc.localAI[0] == 0)
				{
					npc.localAI[0] = 1;
					npc.localAI[1] = Main.rand.Next(4);
				}
				npc.frame.Y = (int)npc.localAI[1] * npc.frame.Height;
			}
        }

        private void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }

        private void RainStop()
        {
            if (Main.raining)
            {
                Main.rainTime = 0;
                Main.raining = false;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }

        public void FrostAttack()
		{
			attackCounter++;
			if (attackCounter == 400 && fireAttack == false)
			{
				attackCounter = 0;
				fireAttack = true;
			}
			if (fireAttack == true)
			{
				attackTimer++;
				if ((attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79) && !npc.HasBuff(103))
				{
					for (int i = 0; i < 5; ++i)
					{
						int num429 = 1;
						if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
						{
							num429 = -1;
						}
						Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
						float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
						float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
						float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
						float num433 = 6f;
						PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
						PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
						PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
						PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
						PlayerPos = num433 / PlayerPos;
						PlayerPosX *= PlayerPos;
						PlayerPosY *= PlayerPos;
						PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
						PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
						PlayerPosY += npc.velocity.Y * 0.5f;
						PlayerPosX += npc.velocity.X * 0.5f;
						PlayerDistance.X -= PlayerPosX * 1f;
						PlayerDistance.Y -= PlayerPosY * 1f;
						Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType("SnowBreath"), npc.damage, 0, Main.myPlayer);
					}
				}
				if (attackTimer >= 80)
				{
					fireAttack = false;
					attackTimer = 0;
					attackCounter = 0;
				}
			}
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				player.AddBuff(BuffID.Chilled, 100, true);
			}
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;   //boss drops
            AAWorld.downedSerpent = true;
        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDustLight>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                AAWorld.downedSerpent = true;
                npc.DropLoot(mod.ItemType("SnowMana"), 10, 15);
                string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(9) == 0)
                {
                    npc.DropLoot(mod.ItemType("SnowflakeShuriken"), 90, 120);
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
        }
    }
    public class SerpentBodyI : SerpentHeadI
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentBodyI"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
    public class SerpentTailI : SerpentHeadCo
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentTailI"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}