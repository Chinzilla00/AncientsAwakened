using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenDoragon : ModNPC
    {
        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {				
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();				
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.height = 50;
            npc.width = 444;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.knockBackResist = 0f;
            npc.damage = 180;
            npc.defense = 210;
            npc.lifeMax = 800000;
            if (Main.expertMode)
            {
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            else
            {
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            //npc.behindTiles = true;
            npc.alpha = 255;
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Shen");
            musicPriority = MusicPriority.BossHigh;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
            npc.damage = (int)(npc.damage * 1.2f);
        }

        public bool spawnalpha = false;
		public bool isAwakened = false;
		public float normalSpeed = 10f;
		public float _chargeSpeed = 18f; //you can change this to change the max charge speed outside of people running away
		public float chargeSpeed 
		{
			get
			{
				if(Main.player[npc.target].active && !Main.player[npc.target].dead) //if you have a target, speed up to keep up
				{
					float playerRunAcceleration = Main.player[npc.target].velocity.Y == 0f ? Math.Abs(Main.player[npc.target].moveSpeed * 0.8f) : (Main.player[npc.target].runAcceleration * 1.2f);
					if (playerRunAcceleration <= 1f) playerRunAcceleration = 1f;
					return playerRunAcceleration * _chargeSpeed;
				}
				return _chargeSpeed;
			}
			set
			{
				_chargeSpeed = value;
			}
		}
		public int spawnTimerMax = 100; //time to sit when you spawn
        public int discordianInfernoTimerMax = 80; //shoot fireballs timer
        public int discordianInfernoPercent = 5; //the % amount to shoot fireballs
        public int discordianBreathTimerMax = 90; //shoot breath timer
		public int aiChangeRate = 100; //the rate to jump to another ai. (in truth this is ai[2], this is what it is checked against by default.)
		public int aiTooLongCheck = 60; //if he takes too long to change ai states this forces it to happen soon. smaller value == faster change.
		
		public int damageDiscordianInferno = 120;
		
		//clientside stuff
		public Rectangle wingFrame = new Rectangle(0, 0, 444, 364);
		public int wingFrameY = 364;
		public int frameY = 130;

        public override void AI()
        {
			if(isAwakened) //set awakened stats
			{
				normalSpeed = 12f;
				chargeSpeed = 20f;
				discordianInfernoPercent = 4;
				aiTooLongCheck = 50;
			}
            if (npc.alpha > 0 && !spawnalpha)
            {
                npc.alpha -= 5;
            }
            if (npc.alpha <= 0)
            {
                npc.alpha = 0;
                spawnalpha = true;
            }
			
            Main.fastForwardTime = true;
            Main.dayRate = 20;

            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }
            if (player.dead)
            {
                npc.velocity.Y = npc.velocity.Y - 0.4f;
                if (npc.timeLeft > 150)
                {
                    npc.timeLeft = 150;
                }
                npc.ai[0] = 0f;
                npc.ai[2] = 0f;
            }
            else if (npc.timeLeft > 1800)
            {
                npc.timeLeft = 1800;
            }
            if (npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                npc.alpha = 255;
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                }
            }
            if (npc.ai[0] != -1f && npc.ai[0] < 9f)
            {
                bool colliding = Collision.SolidCollision(npc.position, npc.width, npc.height);
                if (colliding)
                {
                    npc.alpha += 15;
                }else
                {
                    npc.alpha -= 15;
                }
                if (npc.alpha < 0) npc.alpha = 0;
                if (npc.alpha > 150) npc.alpha = 150;
            }
            if (npc.ai[0] == -1f) //initial spawn effects
            {
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                if (npc.ai[2] > 20f)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.ai[2] == discordianBreathTimerMax - 30)
                {
                    Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= spawnTimerMax)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 0f && !player.dead)
            {
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = 400 * Math.Sign((npc.Center - player.Center).X);
                }
				Vector2 playerPoint = player.Center + new Vector2(npc.ai[1], -300f);
				MoveToPoint(playerPoint);
				
				npc.ai[2] += 1f;
				if((playerPoint - npc.Center).Length() < 100f && npc.ai[2] < (aiChangeRate - aiTooLongCheck))
				{
					npc.ai[2] = aiChangeRate - aiTooLongCheck;
				}
                if (npc.ai[2] >= aiChangeRate)
                {
                    int aiChoice = 0;
                    switch ((int)npc.ai[3]) //switch for attack modes
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            aiChoice = 1;
                            break;
                        case 7:
                            npc.ai[3] = 1f;
                            aiChoice = 2;
                            break;
                        case 8:
                            npc.ai[3] = 0f;
                            aiChoice = 3;
                            break;
                    }
                    if (aiChoice == 1)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
						Vector2 vel = player.Center - npc.Center;
						vel = Vector2.Normalize(vel) * 500f;
						customAI[0] = player.Center.X + vel.X;
						customAI[1] = player.Center.Y + vel.Y;
                    }else
                    {
                        npc.ai[0] = aiChoice;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 1f) //charge attack
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
				Vector2 point = new Vector2(customAI[0], customAI[1]);
				MoveToPoint(point);
				if(Main.netMode != 1 && (point - npc.Center).Length() < 100f)
				{
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 40f; //head start to rapidly charge
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 2f) //fire discordian infernos
            {
				npc.velocity *= 0.8f;
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = 400 * Math.Sign((npc.Center - player.Center).X);
                }
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
                }
                if (npc.ai[2] % discordianInfernoPercent == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60);
                    if (Main.netMode != 1)
                    {
                        Vector2 infernoPos = npc.Center + new Vector2(200f * npc.direction, 60f);
                        int projectile = Projectile.NewProjectile((int)infernoPos.X, (int)infernoPos.Y - 100, 0f, 0f, mod.ProjectileType("DiscordianInferno"), damageDiscordianInferno, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[projectile].velocity = new Vector2(npc.direction * MathHelper.Lerp(1f, 8f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-7f, 7f, (float)Main.rand.NextDouble()));
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= discordianInfernoTimerMax)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = -40f; //take longer before swapping to another AI
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 3f) //Fire discordian breath (NOTE: BREATH SEEMS TO BE BROKEN)
            {
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == discordianBreathTimerMax - 30)
                {
                    Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
					if (Main.netMode != 1)
					{
						Vector2 secondPosition = npc.rotation.ToRotationVector2() * (Vector2.UnitX * npc.direction) * (npc.width + 20) / 2f + npc.Center;
						int projectile = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 1f, npc.target + 1); //changed
						int projectile2 = Projectile.NewProjectile(secondPosition.X, secondPosition.Y, (float)npc.direction * 2, 8f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 0f, 0f); //changed
					}		
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= discordianBreathTimerMax)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
			HandleFrames(player);
			HandleRotations();			
        }
		
		public bool Charging
		{
			get
			{
				return npc.ai[0] == 1;
			}
		}
		
		public void MoveToPoint(Vector2 point)
		{
			float velMultiplier = 1f;
			Vector2 dist = point - npc.Center;
			if(dist.Length() < chargeSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, dist.Length() / chargeSpeed);
			}
			npc.velocity = Vector2.Normalize(point - npc.Center);
			npc.velocity *= (Charging ? chargeSpeed : normalSpeed) * velMultiplier;		
		}
	
		public void HandleFrames(Player player)
		{
			npc.frame = new Rectangle(0, 0, 444, 130);
			if(Charging)
			{
				npc.frameCounter = 0;
				wingFrame.Y = wingFrameY * 1;
			}else
			{
				npc.frameCounter++;
				if (npc.frameCounter >= 3)
				{
					npc.frameCounter = 0;
					wingFrame.Y += wingFrameY;
					if (wingFrame.Y > (wingFrameY * 4))
					{
						npc.frameCounter = 0;
						wingFrame.Y = 0;
					}
				}
			}
			npc.direction = (npc.Center.X < player.Center.X ? 1 : -1);
		}

		public void HandleRotations()
		{
			if(Charging)
			{
				BaseAI.LookAt(npc.Center - npc.velocity, npc, 0, 0f, 0f, false);			
			}else
			{
				BaseAI.LookAt(npc.Center + new Vector2(-npc.direction * 200, 0f), npc, 3, 0f, 0.12f, false);
			}
		}
    
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int num621 = 0; num621 < 60; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 90; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }
            }else
			{
                for (int num621 = 0; num621 < 5; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }				
                for (int num623 = 0; num623 < 3; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }				
			}
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                AAWorld.downedShen = true;
                npc.DropLoot(mod.ItemType("ChaosScale"), 20, 30);
                string[] lootTable = { "ChaosSlayer" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                //npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                //npc.DropLoot(Items.Boss.Yamata.YamataTrophy.type, 1f / 10);
                npc.DropLoot(Items.Boss.EXSoul.type);
                Main.NewText("Heh, alright. I’ll leave you alone I guess. But if you come back stronger, I’ll show you the power of true unyielding chaos…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (Main.expertMode)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<ShenTransition>());
            }
            npc.value = 0f;
            npc.boss = false;
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
			Texture2D currentTex = (npc.spriteDirection == 1 ? mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlue") : Main.npcTexture[npc.type]);
			Texture2D currentWingTex = (npc.spriteDirection == 1 ? mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlueWings") : mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonWings"));

			//offset
			npc.position.Y += 40f;

			//draw body/charge afterimage
			if(Charging)
			{
				BaseDrawing.DrawAfterimage(sb, currentTex, 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, (byte)150));	
			}
			BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);
			//draw wings
			float wingOffset = (wingFrameY * 0.5f) - frameY + 44;			
			BaseDrawing.DrawTexture(sb, currentWingTex, 0, npc.position + new Vector2(0f, npc.gfxOffY + wingOffset), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, drawColor);

			//deoffset
			npc.position.Y -= 40f;

            return false;
        }
    }
    
}
