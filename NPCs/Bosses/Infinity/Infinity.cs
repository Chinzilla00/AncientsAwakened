using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Projectiles;

namespace CalamityMod.NPCs.Providence
{
	[AutoloadBossHead]
	public class Providence : ModNPC
	{
		public int text = 0;
		public float bossLife;
		internal int dpsCap = CalamityWorld.downedProvidence ? 72000 : 7200; //50
		private int damageTotal = 0;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Providence, the Profaned God");
			Main.npcFrameCount[npc.type] = 6;
		}
		
		public override void SetDefaults()
		{
			npc.npcSlots = 18f;
			npc.damage = 130;
			npc.width = 150;
			npc.height = 200;
			npc.scale = 1.5f;
			npc.defense = 120;
			npc.lifeMax = 270000;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1; //new
            aiType = -1; //new
			npc.value = Item.buyPrice(3, 0, 0, 0);
			npc.boss = true;
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			npc.chaseable = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ProvidenceTheme");
			npc.HitSound = SoundID.NPCHit44;
			npc.DeathSound = SoundID.NPCDeath46;
			bossBag = mod.ItemType("ProvidenceBag");
		}
		
		public override void AI()
		{
			CalamityGlobalNPC.holyBoss = npc.whoAmI;
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			Player player = Main.player[npc.target];
			Vector2 vector = npc.Center;
			bool isHoly = player.ZoneHoly;
			bool isHell = player.ZoneUnderworldHeight;
			npc.dontTakeDamage = (isHoly || isHell) ? false : true;
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			if (!Main.dayTime) 
			{
				if (npc.timeLeft > 150)
				{
					npc.timeLeft = 150;
				}
				if (npc.velocity.X > 0f) 
				{
					npc.velocity.X = npc.velocity.X + 0.25f;
				} 
				else 
				{
					npc.velocity.X = npc.velocity.X - 0.25f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.1f;
			} 
			else if (npc.timeLeft > 2400)
			{
				npc.timeLeft = 2400;
			}
			if (bossLife == 0f && npc.life > 0)
			{
				bossLife = (float)npc.lifeMax;
			}
	       	if (npc.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)npc.lifeMax * 0.075);
					if ((float)(npc.life + num660) < bossLife)
					{
						bossLife = (float)npc.life;
						int guardianSpawn = 1;
						for (int num662 = 0; num662 < guardianSpawn; num662++)
						{
							int spawnType = Main.rand.Next(3);
							if (spawnType == 0)
							{
								spawnType = mod.ProjectileType("ProvSpawnOffense");
							}
							else if (spawnType == 1)
							{
								spawnType = mod.ProjectileType("ProvSpawnDefense");
							}
							else
							{
								spawnType = mod.ProjectileType("ProvSpawnHealer");
							}
							Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, spawnType, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
	       	}
			if (npc.ai[0] == 0f) 
			{
				npc.defense = 120;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = true;
				if (npc.ai[2] == 0f) 
				{
					npc.TargetClosest(true);
					if (npc.Center.X < player.Center.X) 
					{
						npc.ai[2] = 1f;
					} 
					else 
					{
						npc.ai[2] = -1f;
					}
				}
				npc.TargetClosest(true);
				int num851 = 800;
				float num852 = Math.Abs(npc.Center.X - player.Center.X);
				if (npc.Center.X < player.Center.X && npc.ai[2] < 0f && num852 > (float)num851) 
				{
					npc.ai[2] = 0f;
				}
				if (npc.Center.X > player.Center.X && npc.ai[2] > 0f && num852 > (float)num851) 
				{
					npc.ai[2] = 0f;
				}
				float num853 = expertMode ? 1.5f : 1.45f;
				float num854 = expertMode ? 20f : 19f;
				if ((double)npc.life < (double)npc.lifeMax * 0.75) 
				{
					num853 = expertMode ? 1.55f : 1.5f;
					num854 = expertMode ? 21f : 20f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.5) 
				{
					num853 = expertMode ? 1.6f : 1.55f;
					num854 = expertMode ? 22f : 21f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.25) 
				{
					num853 = expertMode ? 1.7f : 1.6f;
					num854 = expertMode ? 24f : 23f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.1) 
				{
					num853 = expertMode ? 1.8f : 1.7f;
					num854 = expertMode ? 28f : 27f;
				}
				if (revenge)
				{
					num853 *= 1.15f;
					num854 *= 1.15f;
				}
				npc.velocity.X = npc.velocity.X + npc.ai[2] * num853;
				if (npc.velocity.X > num854) 
				{
					npc.velocity.X = num854;
				}
				if (npc.velocity.X < -num854) 
				{
					npc.velocity.X = -num854;
				}
				float num855 = player.position.Y - (npc.position.Y + (float)npc.height);
				if (num855 < 200f) //150
				{
					npc.velocity.Y = npc.velocity.Y - 0.2f;
				}
				if (num855 > 250f) //200
				{
					npc.velocity.Y = npc.velocity.Y + 0.2f;
				}
				if (npc.velocity.Y > 8f) 
				{
					npc.velocity.Y = 8f;
				}
				if (npc.velocity.Y < -8f) 
				{
					npc.velocity.Y = -8f;
				}
				if ((num852 < 500f || npc.ai[3] < 0f) && npc.position.Y < player.position.Y) 
				{
					npc.ai[3] += 1f;
					int num856 = expertMode ? 11 : 12;
					if ((double)npc.life < (double)npc.lifeMax * 0.75) 
					{
						num856 = expertMode ? 10 : 11;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.5) 
					{
						num856 = expertMode ? 9 : 10;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.25) 
					{
						num856 = expertMode ? 8 : 9;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1) 
					{
						num856 = expertMode ? 6 : 8;
					}
					num856++;
					if (npc.ai[3] > (float)num856) 
					{
						npc.ai[3] = (float)(-(float)num856);
					}
					if (npc.ai[3] == 0f && Main.netMode != 1) 
					{
						Vector2 vector112 = new Vector2(npc.Center.X, npc.Center.Y);
						vector112.X += npc.velocity.X * 7f;
						float num857 = player.position.X + (float)player.width * 0.5f - vector112.X;
						float num858 = player.Center.Y - vector112.Y;
						float num859 = (float)Math.Sqrt((double)(num857 * num857 + num858 * num858));
						float num860 = expertMode ? 9f : 8f;
						if ((double)npc.life < (double)npc.lifeMax * 0.75) 
						{
							num860 = expertMode ? 10.25f : 9f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.5) 
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.25) 
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.1) 
						{
							num860 = expertMode ? 14f : 12f;
						}
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 65 : 70;
						Projectile.NewProjectile(vector112.X, vector112.Y, num857, num858, mod.ProjectileType("HolyBlast"), holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (npc.ai[3] < 0f) 
				{
					npc.ai[3] += 1f;
				}
				if (Main.netMode != 1) 
				{
					npc.ai[1] += (float)Main.rand.Next(1, 4);
					if (npc.ai[1] > 600f && num852 < 600f) 
					{
						npc.ai[0] = -1f;
					}
				}
			}
			else if (npc.ai[0] == 1f) 
			{
				npc.defense = 120;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = true;
				npc.TargetClosest(true);
				float num861 = expertMode ? 0.35f : 0.32f;
				float num862 = expertMode ? 17f : 16f;
				if ((double)npc.life < (double)npc.lifeMax * 0.75) 
				{
					num861 = expertMode ? 0.36f : 0.33f;
					num862 = expertMode ? 18f : 17f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.5) 
				{
					num861 = expertMode ? 0.38f : 0.35f;
					num862 = expertMode ? 19f : 18f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.25) 
				{
					num861 = expertMode ? 0.4f : 0.37f;
					num862 = expertMode ? 20f : 19f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.1) 
				{
					num861 = expertMode ? 0.45f : 0.4f;
					num862 = expertMode ? 22f : 20f;
				}
				if (revenge)
				{
					num861 *= 1.15f;
					num862 *= 1.15f;
				}
				num861 -= 0.05f;
				num862 -= 1f;
				if (npc.Center.X < player.Center.X) 
				{
					npc.velocity.X = npc.velocity.X + num861;
					if (npc.velocity.X < 0f) 
					{
						npc.velocity.X = npc.velocity.X * 0.98f;
					}
				}
				if (npc.Center.X > player.Center.X) 
				{
					npc.velocity.X = npc.velocity.X - num861;
					if (npc.velocity.X > 0f) 
					{
						npc.velocity.X = npc.velocity.X * 0.98f;
					}
				}
				if (npc.velocity.X > num862 || npc.velocity.X < -num862) 
				{
					npc.velocity.X = npc.velocity.X * 0.95f;
				}
				float num863 = player.position.Y - (npc.position.Y + (float)npc.height);
				if (num863 < 280f) //180
				{
					npc.velocity.Y = npc.velocity.Y - 0.1f;
				}
				if (num863 > 300f) //200
				{
					npc.velocity.Y = npc.velocity.Y + 0.1f;
				}
				if (npc.velocity.Y > 6f) 
				{
					npc.velocity.Y = 6f;
				}
				if (npc.velocity.Y < -6f) 
				{
					npc.velocity.Y = -6f;
				}
				if (Main.netMode != 1) 
				{
					npc.ai[3] += 1f;
					int num864 = expertMode ? 28 : 30;
					if ((double)npc.life < (double)npc.lifeMax * 0.75) 
					{
						num864 = expertMode ? 26 : 29;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.5) 
					{
						num864 = expertMode ? 23 : 27;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.25) 
					{
						num864 = expertMode ? 20 : 24;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1) 
					{
						num864 = expertMode ? 15 : 18;
					}
					num864 += 3;
					if (npc.ai[3] >= (float)num864) 
					{
						npc.ai[3] = 0f;
						Vector2 vector113 = new Vector2(npc.Center.X, npc.position.Y + (float)npc.height - 14f);
						int i2 = (int)(vector113.X / 16f);
						int j2 = (int)(vector113.Y / 16f);
						if (!WorldGen.SolidTile(i2, j2)) 
						{
							float num865 = npc.velocity.Y;
							if (num865 < 0f) 
							{
								num865 = 0f;
							}
							num865 += expertMode ? 4f : 3f;
							float speedX2 = npc.velocity.X * 0.25f;
							int fireDamage = expertMode ? 50 : 54;
							Projectile.NewProjectile(vector113.X, vector113.Y, speedX2, num865, mod.ProjectileType("HolyFire"), fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != 1) 
				{
					npc.ai[1] += (float)Main.rand.Next(1, 4);
					if (npc.ai[1] > 600f) 
					{
						npc.ai[0] = -1f;
					}
				}
			}
			else if (npc.ai[0] == 2f) 
			{
				npc.defense = 99999;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = false;
				npc.TargetClosest(true);
				Vector2 vector114 = new Vector2(npc.Center.X, npc.Center.Y - 20f);
				float num866 = (float)Main.rand.Next(-1000, 1001);
				float num867 = (float)Main.rand.Next(-1000, 1001);
				float num868 = (float)Math.Sqrt((double)(num866 * num866 + num867 * num867));
				float num869 = 3f;
				npc.velocity *= 0.95f;
				num868 = num869 / num868;
				num866 *= num868;
				num867 *= num868;
				vector114.X += num866 * 4f;
				vector114.Y += num867 * 4f;
				npc.ai[3] += 1f;
				int num870 = expertMode ? 11 : 13;
				if (revenge)
				{
					num870--;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.75) 
				{
					num870--;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.5) 
				{
					num870 -= 2;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.25) 
				{
					num870 -= expertMode ? 4 : 3;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.1) 
				{
					num870 -= expertMode ? 5 : 4;
				}
				if (npc.ai[3] > (float)num870) 
				{
					npc.ai[3] = 0f;
					if (Main.netMode != 1)
					{
						if (Main.rand.Next(3) == 0)
						{
							Projectile.NewProjectile(vector114.X, vector114.Y, num866, num867, mod.ProjectileType("HolyLight"), 0, 0f, Main.myPlayer, 0f, 0f);
						}
						else
						{
							Projectile.NewProjectile(vector114.X, vector114.Y, num866, num867, mod.ProjectileType("HolyBurnOrb"), 0, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				if (Main.netMode != 1)
				{
					npc.ai[1] += (float)Main.rand.Next(1, 4);
					if (npc.ai[1] > 750f && text == 0)
					{
						text++;
						string key = "Mods.CalamityMod.ProfanedBossText";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					if (npc.ai[1] > 900f) 
					{
						Main.PlaySound(SoundID.Item20, player.position);
						player.AddBuff(mod.BuffType("ExtremeGravity"), 600, true);
						for (int num621 = 0; num621 < 40; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num622].scale = 0.5f;
								Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
						}
						for (int num623 = 0; num623 < 60; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num624].noGravity = true;
							Main.dust[num624].velocity *= 5f;
							num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
						}
						text = 0;
						npc.ai[0] = -1f;
					}
				}
			}
			if (npc.ai[0] == 3f) 
			{
				npc.defense = 120;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = true;
				if (npc.ai[2] == 0f) 
				{
					npc.TargetClosest(true);
					if (npc.Center.X < player.Center.X) 
					{
						npc.ai[2] = 1f;
					} 
					else 
					{
						npc.ai[2] = -1f;
					}
				}
				npc.TargetClosest(true);
				int num851 = 800;
				float num852 = Math.Abs(npc.Center.X - player.Center.X);
				if (npc.Center.X < player.Center.X && npc.ai[2] < 0f && num852 > (float)num851) 
				{
					npc.ai[2] = 0f;
				}
				if (npc.Center.X > player.Center.X && npc.ai[2] > 0f && num852 > (float)num851) 
				{
					npc.ai[2] = 0f;
				}
				float num853 = expertMode ? 1.5f : 1.45f;
				float num854 = expertMode ? 20f : 19f;
				if ((double)npc.life < (double)npc.lifeMax * 0.75) 
				{
					num853 = expertMode ? 1.55f : 1.5f;
					num854 = expertMode ? 21f : 20f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.5) 
				{
					num853 = expertMode ? 1.6f : 1.55f;
					num854 = expertMode ? 22f : 21f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.25) 
				{
					num853 = expertMode ? 1.7f : 1.6f;
					num854 = expertMode ? 24f : 23f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.1) 
				{
					num853 = expertMode ? 1.8f : 1.7f;
					num854 = expertMode ? 28f : 27f;
				}
				if (revenge)
				{
					num853 *= 1.15f;
					num854 *= 1.15f;
				}
				npc.velocity.X = npc.velocity.X + npc.ai[2] * num853;
				if (npc.velocity.X > num854) 
				{
					npc.velocity.X = num854;
				}
				if (npc.velocity.X < -num854) 
				{
					npc.velocity.X = -num854;
				}
				float num855 = player.position.Y - (npc.position.Y + (float)npc.height);
				if (num855 < 200f) //150
				{
					npc.velocity.Y = npc.velocity.Y - 0.2f;
				}
				if (num855 > 250f) //200
				{
					npc.velocity.Y = npc.velocity.Y + 0.2f;
				}
				if (npc.velocity.Y > 8f) 
				{
					npc.velocity.Y = 8f;
				}
				if (npc.velocity.Y < -8f) 
				{
					npc.velocity.Y = -8f;
				}
				if ((num852 < 500f || npc.ai[3] < 0f) && npc.position.Y < player.position.Y) 
				{
					npc.ai[3] += 1f;
					int num856 = expertMode ? 11 : 12;
					if ((double)npc.life < (double)npc.lifeMax * 0.75) 
					{
						num856 = expertMode ? 10 : 11;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.5) 
					{
						num856 = expertMode ? 9 : 10;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.25) 
					{
						num856 = expertMode ? 8 : 9;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1) 
					{
						num856 = expertMode ? 6 : 8;
					}
					num856++;
					if (npc.ai[3] > (float)num856) 
					{
						npc.ai[3] = (float)(-(float)num856);
					}
					if (npc.ai[3] == 0f && Main.netMode != 1) 
					{
						Vector2 vector112 = new Vector2(npc.Center.X, npc.Center.Y);
						vector112.X += npc.velocity.X * 7f;
						float num857 = player.position.X + (float)player.width * 0.5f - vector112.X;
						float num858 = player.Center.Y - vector112.Y;
						float num859 = (float)Math.Sqrt((double)(num857 * num857 + num858 * num858));
						float num860 = expertMode ? 9f : 8f;
						if ((double)npc.life < (double)npc.lifeMax * 0.75) 
						{
							num860 = expertMode ? 10.25f : 9f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.5) 
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.25) 
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.1) 
						{
							num860 = expertMode ? 14f : 12f;
						}
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 65 : 72;
						Projectile.NewProjectile(vector112.X, vector112.Y, num857, num858, mod.ProjectileType("MoltenBlast"), holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (npc.ai[3] < 0f) 
				{
					npc.ai[3] += 1f;
				}
				if (Main.netMode != 1) 
				{
					npc.ai[1] += (float)Main.rand.Next(1, 4);
					if (npc.ai[1] > 600f && num852 < 600f) 
					{
						npc.ai[0] = -1f;
					}
				}
			}
			else if (npc.ai[0] == 4f) 
			{
				npc.defense = 120;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = true;
				npc.TargetClosest(true);
				float num861 = expertMode ? 0.35f : 0.32f;
				float num862 = expertMode ? 17f : 16f;
				if ((double)npc.life < (double)npc.lifeMax * 0.75) 
				{
					num861 = expertMode ? 0.36f : 0.33f;
					num862 = expertMode ? 18f : 17f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.5) 
				{
					num861 = expertMode ? 0.38f : 0.35f;
					num862 = expertMode ? 19f : 18f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.25) 
				{
					num861 = expertMode ? 0.4f : 0.37f;
					num862 = expertMode ? 20f : 19f;
				}
				if ((double)npc.life < (double)npc.lifeMax * 0.1) 
				{
					num861 = expertMode ? 0.45f : 0.4f;
					num862 = expertMode ? 22f : 20f;
				}
				if (revenge)
				{
					num861 *= 1.15f;
					num862 *= 1.15f;
				}
				num861 -= 0.05f;
				num862 -= 1f;
				if (npc.Center.X < player.Center.X) 
				{
					npc.velocity.X = npc.velocity.X + num861;
					if (npc.velocity.X < 0f) 
					{
						npc.velocity.X = npc.velocity.X * 0.98f;
					}
				}
				if (npc.Center.X > player.Center.X) 
				{
					npc.velocity.X = npc.velocity.X - num861;
					if (npc.velocity.X > 0f) 
					{
						npc.velocity.X = npc.velocity.X * 0.98f;
					}
				}
				if (npc.velocity.X > num862 || npc.velocity.X < -num862) 
				{
					npc.velocity.X = npc.velocity.X * 0.95f;
				}
				float num863 = player.position.Y - (npc.position.Y + (float)npc.height);
				if (num863 < 280f) //180
				{
					npc.velocity.Y = npc.velocity.Y - 0.1f;
				}
				if (num863 > 300f) //200
				{
					npc.velocity.Y = npc.velocity.Y + 0.1f;
				}
				if (npc.velocity.Y > 6f) 
				{
					npc.velocity.Y = 6f;
				}
				if (npc.velocity.Y < -6f) 
				{
					npc.velocity.Y = -6f;
				}
				if (Main.netMode != 1) 
				{
					npc.ai[3] += 1f;
					int num864 = expertMode ? 74 : 78;
					if ((double)npc.life < (double)npc.lifeMax * 0.75) 
					{
						num864 = expertMode ? 70 : 74;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.5) 
					{
						num864 = expertMode ? 64 : 70;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.25) 
					{
						num864 = expertMode ? 56 : 60;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1) 
					{
						num864 = expertMode ? 46 : 50;
					}
					num864 += 3;
					if (npc.ai[3] >= (float)num864) 
					{
						npc.ai[3] = 0f;
						Vector2 vector113 = new Vector2(npc.Center.X, npc.position.Y + (float)npc.height - 14f);
						int i2 = (int)(vector113.X / 16f);
						int j2 = (int)(vector113.Y / 16f);
						if (!WorldGen.SolidTile(i2, j2)) 
						{
							float num865 = npc.velocity.Y;
							if (num865 < 0f) 
							{
								num865 = 0f;
							}
							num865 += expertMode ? 4f : 3f;
							float speedX2 = npc.velocity.X * 0.25f;
							int fireDamage = expertMode ? 55 : 61;
							Projectile.NewProjectile(vector113.X, vector113.Y, speedX2, num865, mod.ProjectileType("HolyBomb"), fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != 1) 
				{
					npc.ai[1] += (float)Main.rand.Next(1, 4);
					if (npc.ai[1] > 600f) 
					{
						npc.ai[0] = -1f;
					}
				}
			}
			if (npc.ai[0] == -1f) 
			{
				npc.defense = 120;
				npc.noGravity = true;
				npc.noTileCollide = true;
				npc.chaseable = true;
				int num871 = Main.rand.Next(5);
				npc.TargetClosest(true);
				if (Math.Abs(npc.Center.X - player.Center.X) > 1000f) 
				{
					num871 = 0;
				}
				npc.ai[0] = (float)num871;
				npc.ai[1] = 0f;
				npc.ai[2] = 0f;
				npc.ai[3] = 0f;
				return;
			}
		}
		
		public override void NPCLoot()
		{
			bool isHoly = Main.player[npc.target].ZoneHoly;
			bool isHell = Main.player[npc.target].ZoneUnderworldHeight;
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ProvidenceTrophy"));
			}
			if (Main.expertMode)
			{
				if (isHoly)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ElysianWings"));
				}
				if (isHell)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ElysianAegis"));
				}
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("UnholyEssence"), Main.rand.Next(20, 30));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuneofCos"));
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ProvidenceMask"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlissfulBombardier"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HolyCollider"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MoltenAmputator"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PurgeGuzzler"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SolarFlare"));
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TelluricGlare"));
				}
			}
			if (Main.netMode != 1)
			{
				int num52 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
				int num53 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
				int num54 = npc.width / 2 / 16 + 1;
				for (int num55 = num52 - num54; num55 <= num52 + num54; num55++)
				{
					for (int num56 = num53 - num54; num56 <= num53 + num54; num56++)
					{
						if ((num55 == num52 - num54 || num55 == num52 + num54 || num56 == num53 - num54 || num56 == num53 + num54) && !Main.tile[num55, num56].active())
						{
							Main.tile[num55, num56].type = 226;
							Main.tile[num55, num56].active(true);
						}
						Main.tile[num55, num56].lava(false);
						Main.tile[num55, num56].liquid = 0;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num55, num56, 1, TileChangeType.None);
						}
						else
						{
							WorldGen.SquareTileFrame(num55, num56, true);
						}
					}
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
			ModifyHit(ref damage);
		}

		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			OnHit(damage);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[npc.target];
			if (player.vortexStealthActive && projectile.ranged)
			{
				damage /= 2;
				crit = false;
			}
			ModifyHit(ref damage);
		}
		
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			OnHit(damage);
		}
		
		private void ModifyHit(ref int damage)
		{
			if (damage > npc.lifeMax / 8)
			{
				damage = npc.lifeMax / 8;
			}
		}
		
		private void OnHit(int damage)
		{
			damageTotal += damage * 60;
			if (Main.netMode != 0)
			{
				ModPacket netMessage = GetPacket(ProvidenceMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == 1)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
		}
		
		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (damageTotal >= dpsCap * 60)
			{
				damage = 0;
				return false;
			}
			return true;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityMod");
			Texture2D texture = mod.GetTexture("NPCs/Providence/ProvidenceAlt");
			CalamityMod.DrawTexture(spriteBatch, (npc.ai[0] == 2f ? texture : Main.npcTexture[npc.type]), 0, npc, drawColor);
			return false;
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			if (npc.ai[0] == 2f)
			{
				npc.frameCounter += 1.0;
				if (npc.frameCounter > 4.0)
				{
					npc.frame.Y = npc.frame.Y + frameHeight;
					npc.frameCounter = 0.0;
				}
				if (npc.frame.Y > frameHeight * 5)
				{
					npc.frame.Y = frameHeight * 5;
				}
				else if (npc.frame.Y < frameHeight * 0)
				{
					npc.frame.Y = frameHeight * 0;
				}
				return;
			}
			int num84 = 5; //5
			npc.frameCounter += 1.0;
			if (npc.frameCounter > (double)num84)
			{
				npc.frameCounter = 0.0;
				npc.frame.Y = npc.frame.Y + frameHeight;
			}
			if (npc.frame.Y >= frameHeight * 6) //6
			{
				npc.frame.Y = 0;
			}
			return;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2f;
			return null;
		}
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.AddBuff(mod.BuffType("HolyLight"), 120, true);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 244, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-50, 50) / 100);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Providence"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Providence2"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Providence3"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Providence4"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Providence5"), 1f);
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 400;
				npc.height = 350;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 244, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		private ModPacket GetPacket(ProvidenceMessageType type)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)CalamityModMessageType.Providence);
			packet.Write(npc.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			ProvidenceMessageType type = (ProvidenceMessageType)reader.ReadByte();
			if (type == ProvidenceMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == 2)
				{
					ModPacket netMessage = GetPacket(ProvidenceMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum ProvidenceMessageType : byte
	{
		Damage
	}
}