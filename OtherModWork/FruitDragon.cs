using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
/*namespace AAMod.OtherModWork
{
	public class FruitDragon : ModNPC
	{
		
		public override void SetDefaults()
		{
			npc.width = 250;
			npc.height = 210;
			bossBag = mod.ItemType("DragmelBag");
			npc.damage = 43;
			npc.lifeMax = 7000;
			npc.defense = 15;
			npc.alpha = 0;
			npc.knockBackResist = 0f;
			npc.boss = true;
			npc.noGravity = false;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath5;
			music = MusicID.Boss4;
			Main.npcFrameCount[npc.type] = 8;
		}
		public static Rectangle GetFrame(int currentFrame, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
        {
            pixelSpaceY *= currentFrame;
            int startY = (frameHeight * currentFrame) + pixelSpaceY;
            return new Rectangle(0, startY, frameWidth - pixelSpaceX, frameHeight);
        }
		public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.10f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

		public override void AI()
		{
			npc.spriteDirection = npc.direction; //handles sprite flipping
			AIZombie(npc, ref npc.ai, false, false, 1, 0.3f, 6f, 12, 15);//referencing the method above for AI
			npc.ai[0] += 1f;
			if (npc.ai[0] >= 180f)
			{
				bool hasTarget = false;
				Vector2 target = Vector2.Zero;
				float targetRange = 900f;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && !Main.player[i].dead)
					{
						float playerX = Main.player[i].position.X + (float)(Main.player[i].width / 2);
						float playerY = Main.player[i].position.Y + (float)(Main.player[i].height / 2);
						float distOrth = Math.Abs(npc.position.X + (float)(npc.width / 2) - playerX) + Math.Abs(npc.position.Y + (float)(npc.height / 2) - playerY);
						if (distOrth < targetRange)
						{
							targetRange = distOrth;
							target = Main.player[i].Center;
							hasTarget = true;
						}
					}
				}
				if (hasTarget)
				{
					Vector2 delta = target - npc.Center;
					delta.Normalize();
					delta *= 4f;
					int slot = Terraria.Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, mod.ProjectileType("DragonFire"), 32, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[slot].tileCollide = false;
					Main.projectile[slot].netUpdate = true;
					/*if(Main.expertMode)
					{
						=
				}
				npc.ai[0] = 0f;
                npc.netUpdate = true;
			}
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 1, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 250;
				npc.height = 210;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 200; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 400; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.55f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.65f);
		}


		/*
		public override void NPCLoot()
		{
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ArcaneGeyser"), Main.rand.Next(32, 44));
				string[] lootTable = 
				{
					"KingRock",
					"Mountain",
					"TitanboundBulwark",
					"CragboundStaff",
					"QuakeFist",
					"SkeletalonStaff",
					"Earthshatter"
				};
				int loot = Main.rand.Next(lootTable.Length);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(lootTable[loot]));
			}
		}
	}
}
		*/
