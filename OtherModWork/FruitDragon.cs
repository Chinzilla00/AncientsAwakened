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
namespace AAMod.OtherModWork
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
		public static bool AttemptOpenDoor(NPC npc, ref float doorBeatCounter, ref float doorCounter, ref float tickUpdater, float ticksUntilBoredom, int doorBeatCounterMax = 10, int doorCounterMax = 60, int interactDoorStyle = 0)
        {
            bool hitTile = HitTileOnSide(npc, 3);
            if (hitTile)
            {
                int tileX = (int)((npc.Center.X + (float)(((npc.width / 2) + 8f) * npc.direction)) / 16f);
                int tileY = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                for (int m = 1; m >= -3; m--)
                {
                    if (m == 1 && Main.tile[tileX + npc.direction, tileY + m] == null) { Main.tile[tileX + npc.direction, tileY + m] = new Tile(); }
                    else
                        if (m == -1 && Main.tile[tileX + npc.direction, tileY + m] == null) { Main.tile[tileX + npc.direction, tileY + m] = new Tile(); }
                    if (Main.tile[tileX, tileY + m] == null) { Main.tile[tileX, tileY + m] = new Tile(); }
                }
                if (Main.tile[tileX, tileY - 1].nactive() && Main.tile[tileX, tileY - 1].type == 10)
                {
                    doorCounter += 1f;
                    tickUpdater = 0f;
                    if (doorCounter >= doorCounterMax)
                    {
                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        doorBeatCounter += 1f;
                        doorCounter = 0f;
                        bool attemptOpenDoor = false;
                        if (doorBeatCounter >= doorBeatCounterMax)
                        {
                            attemptOpenDoor = true;
                            doorBeatCounter = 10f;
                        }
                        WorldGen.KillTile(tileX, tileY - 1, true, false, false);
                        if (attemptOpenDoor && Main.netMode != 1)
                        {
                            bool openedDoor = false;
                            if (interactDoorStyle != 0)
                            {
                                if (interactDoorStyle == 1)
                                {
                                    WorldGen.KillTile(tileX, tileY);
                                    openedDoor = !Main.tile[tileX, tileY].nactive();
                                }
                                else
                                {
                                    openedDoor = WorldGen.OpenDoor(tileX, tileY, npc.direction);
                                }
                            }
                            if (!openedDoor)
                            {
                                tickUpdater = (float)ticksUntilBoredom;
                                npc.netUpdate = true;
                            }
							/*
                            if (Main.netMode == 2 && openedDoor)
                            {
                                NetMessage.SendData(19, -1, -1, "", 0, (float)tileX, (float)tileY, (float)npc.direction, 0);
                            }
							*/
                        }
                    }
                    return true;
                }
            }
            return false;
        }
		public static bool HitTileOnSide(Vector2 position, int width, int height, int dir, ref Vector2 hitTilePos)
        {
            int tilePosX = 0;
            int tilePosY = 0;
            int tilePosWidth = 0;
            int tilePosHeight = 0;
            if (dir == 0) //left
            {
                tilePosX = (int)(position.X - 8f) / 16;
                tilePosY = (int)position.Y / 16;
                tilePosWidth = tilePosX + 1;
                tilePosHeight = (int)(position.Y + (float)height) / 16;
            }else
            if (dir == 1) //right
            {
                tilePosX = (int)(position.X + (float)width + 8f) / 16;
                tilePosY = (int)position.Y / 16;
                tilePosWidth = tilePosX + 1;
                tilePosHeight = (int)(position.Y + (float)height) / 16;
            }else
            if (dir == 2) //up, ie ceiling
            {
                tilePosX = (int)position.X / 16;
                tilePosY = (int)(position.Y - 8f) / 16;
                tilePosWidth = (int)(position.X + (float)width) / 16;
                tilePosHeight = tilePosY + 1;
            }else
            if (dir == 3) //down, ie floor
            {
                tilePosX = (int)position.X / 16;
                tilePosY = (int)(position.Y + (float)height + 8f) / 16;
                tilePosWidth = (int)(position.X + (float)width) / 16;
                tilePosHeight = tilePosY + 1;
            }
            for (int x2 = tilePosX; x2 < tilePosWidth; x2++)
            {
                for (int y2 = tilePosY; y2 < tilePosHeight; y2++)
                {
                    if (Main.tile[x2, y2] == null) { return false; }
                    if (Main.tile[x2, y2].nactive() && Main.tileSolid[(int)Main.tile[x2, y2].type])
                    {
                        hitTilePos = new Vector2(x2, y2);
                        return true;
                    }
                }
            }
            return false;
        }
		public static void AIZombie(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
        {
            bool xVelocityChanged = false;
            //This block of code checks for major X velocity/directional changes as well as periodically updates the npc.
            if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
            {
                xVelocityChanged = true;
            }
            if (npc.position.X == npc.oldPosition.X || ai[3] >= (float)ticksUntilBoredom || xVelocityChanged)
            {
                ai[3] += 1f;
            }else
            if ((double)Math.Abs(npc.velocity.X) > 0.9 && ai[3] > 0f) { ai[3] -= 1f; }
            if (ai[3] > (float)(ticksUntilBoredom * 10)) { ai[3] = 0f; }
            if (npc.justHit) { ai[3] = 0f; }
            if (ai[3] == (float)ticksUntilBoredom) { npc.netUpdate = true; }

            bool notBored = ai[3] < (float)ticksUntilBoredom;
            //if npc does not flee when it's day, if is night, or npc is not on the surface and it hasn't updated projectile pass, update target.
            if (targetPlayers && (!fleeWhenDay || !Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0) && (fleeWhenDay && Main.dayTime ? notBored : (!allowBoredom || notBored)))
            {
                npc.TargetClosest(true);
            }else
            if (ai[2] <= 0f)//if 'bored'
            {
                if (fleeWhenDay && Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        ai[0] += 1f;
                        if (ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            ai[0] = 0f;
                        }
                    }
                }else { ai[0] = 0f; }
                if (npc.direction == 0) { npc.direction = 1; }
            }
            //if velocity is less than -1 or greater than 1...
            if (npc.velocity.X < -velMax || npc.velocity.X > velMax)
            {
                //...and npc is not falling or jumping, slow down x velocity.
                if (npc.velocity.Y == 0f) { npc.velocity *= 0.8f; }
            }else
            if (npc.velocity.X < velMax && npc.direction == 1) //handles movement to the right. Clamps at velMaxX.
            {
                npc.velocity.X += moveInterval;
                if (npc.velocity.X > velMax) { npc.velocity.X = velMax; }
            }else
            if (npc.velocity.X > -velMax && npc.direction == -1) //handles movement to the left. Clamps at -velMaxX.
            {
                npc.velocity.X -= moveInterval;
                if (npc.velocity.X < -velMax) { npc.velocity.X = -velMax; }
            }
			WalkupHalfBricks(npc);
            //if allowed to open doors and is currently doing so, reduce npc velocity on the X axis to 0. (so it stops moving)
            if (openDoors != -1 && AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
            {
                npc.velocity.X = 0;
            }else //if no door to open, reset ai.
            if (openDoors != -1){ ai[1] = 0f; ai[2] = 0f; }
            //if there's a solid floor under us...
            if (HitTileOnSide(npc, 3))
            {
                //if the npc's velocity is going in the same direction as the npc's direction...
                if ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1))
                {
                    //...attempt to jump if needed.
                    Vector2 newVec = AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, jumpUpPlatforms && notBored ? Main.player[npc.target] : null, ignoreJumpTiles);
                    if(!npc.noTileCollide)
                    {
                        newVec = Collision.TileCollision(npc.position, newVec, npc.width, npc.height);
						Vector4 slopeVec = Collision.SlopeCollision(npc.position, newVec, npc.width, npc.height);
						Vector2 slopeVel = new Vector2(slopeVec.Z, slopeVec.W);
						if(onTileCollide != null && npc.velocity != slopeVel) onTileCollide(npc.velocity.X != slopeVel.X, npc.velocity.Y != slopeVel.Y, npc.velocity, slopeVel);					
						npc.position = new Vector2(slopeVec.X, slopeVec.Y);
						npc.velocity = slopeVel;
                    }
                    if (npc.velocity != newVec) { npc.velocity = newVec; npc.netUpdate = true; }
                }
            }
        }
		public static void WalkupHalfBricks(NPC npc)
		{
			WalkupHalfBricks(npc, ref npc.gfxOffY, ref npc.stepSpeed);
		}
		public static Vector2 AttemptJump(Vector2 position, Vector2 velocity, int width, int height, int direction, float directionY = 0, int tileDistX = 3, int tileDistY = 4, float maxSpeedX = 1f, bool jumpUpPlatforms = false, Entity target = null, bool ignoreTiles = false)
        {
            try
            {
                tileDistX -= 2;
                Vector2 newVelocity = velocity;
                int tileX = Math.Max(10, Math.Min(Main.maxTilesX - 10, (int)((position.X + (width * 0.5f) + (float)(((width * 0.5f) + 8f) * direction)) / 16f)));
                int tileY = Math.Max(10, Math.Min(Main.maxTilesY - 10, (int)((position.Y + (float)height - 15f) / 16f)));
                int tileItX = Math.Max(10, Math.Min(Main.maxTilesX - 10, tileX + (direction * tileDistX)));
                int tileItY = Math.Max(10, Math.Min(Main.maxTilesY - 10, tileY - tileDistY));
                int lastY = tileY;
                int tileHeight = (int)(height / 16f);
                if (height > tileHeight * 16) { tileHeight += 1; }

                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
                //attempt to jump over walls if possible.

				if(ignoreTiles && target != null && Math.Abs((position.X + (width * 0.5f)) - target.Center.X) < width + 120)
				{
					float dist = (int)Math.Abs(position.Y + ((float)height * 0.5f) - target.Center.Y) / 16;
					if (dist < tileDistY + 2){ newVelocity.Y = -8f + (dist * -0.5f); } // dist +=2; newVelocity.Y = -(5f + dist * (dist > 3 ? 1f - ((dist - 2f) * 0.0525f) : 1f)); }
				}
				if(newVelocity.Y == velocity.Y)
				{
					for (int y = tileY; y >= tileItY; y--)
					{
						Tile tile = Main.tile[tileX, y];
						Tile tileNear = Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y];
						if (tile == null) { tile = Main.tile[tileX, y] = new Tile(); }
						if (tileNear == null) { tileNear = Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y] = new Tile(); }
						if (tile.nactive() && (y != tileY || (!tile.halfBrick() && tile.slope() == 0)) && Main.tileSolid[tile.type] && (jumpUpPlatforms || !Main.tileSolidTop[tile.type]))
						{
							if (!Main.tileSolidTop[tile.type])
							{
								Rectangle tileHitbox = new Rectangle(tileX * 16, y * 16, 16, 16);
								tileHitbox.Y = hitbox.Y;
								if (tileHitbox.Intersects(hitbox)) { newVelocity = velocity; break; }
							}			
							if (tileNear.nactive() && Main.tileSolid[tileNear.type] && !Main.tileSolidTop[tileNear.type]){ newVelocity = velocity; break; }
							if (target != null && y * 16 < target.Center.Y){ continue; }								
							lastY = y;
							newVelocity.Y = -(5f + (float)(tileY - y) * (tileY - y > 3 ? 1f - ((tileY - y - 2) * 0.0525f) : 1f));
						}else
						if (lastY - y >= tileHeight){ break; }
					}
				}
                // if the npc isn't jumping already...
                if (newVelocity.Y == velocity.Y)
                {
                    if (Main.tile[tileX, tileY + 1] == null) { Main.tile[tileX, tileY + 1] = new Tile(); }
                    if (Main.tile[tileX + direction, tileY + 1] == null) { Main.tile[tileX, tileY + 1] = new Tile(); }
					if (Main.tile[tileX + direction, tileY + 2] == null) { Main.tile[tileX, tileY + 2] = new Tile(); }
                    //...and there's a gap in front of the npc, attempt to jump across it.
                    if (directionY < 0 && (!Main.tile[tileX, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 1].type]) && (!Main.tile[tileX + direction, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX + direction, tileY + 1].type]))
                    {
						if (!Main.tile[tileX + direction, tileY + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 2].type] || (target == null || ((target.Center.Y + (target.height * 0.25f)) < tileY * 16f)))
						{
							newVelocity.Y = -8f;
							newVelocity.X *= 1.5f * (1f / maxSpeedX);
							if (tileX <= tileItX)
							{
								for (int x = tileX; x < tileItX; x++)
								{
									Tile tile = Main.tile[x, tileY + 1];
									if (tile == null) { tile = Main.tile[x, tileY + 1] = new Tile(); }
									if (x != tileX && !tile.nactive())
									{
										newVelocity.Y -= 0.15f;
										newVelocity.X += direction * 0.255f;
									}
								}
							}else
							if (tileX > tileItX)
							{
								for (int x = tileItX; x < tileX; x++)
								{
									Tile tile = Main.tile[x, tileY + 1];
									if (tile == null) { tile = Main.tile[x, tileY + 1] = new Tile(); }
									if (x != tileItX && !tile.nactive())
									{
										newVelocity.Y -= 0.15f;
										newVelocity.X += direction * 0.255f;
									}
								}
							}
						}
                    }
                }
                return newVelocity;
            }catch(Exception e) { ErrorLogger.Log(e.Message); ErrorLogger.Log(e.StackTrace); return velocity; }
        }
		public static void WalkupHalfBricks(Entity codable, ref float gfxOffY, ref float stepSpeed)
		{
			if (codable.velocity.Y >= 0f)
			{
				int offset = 0;
				if (codable.velocity.X < 0f) offset = -1;
				if (codable.velocity.X > 0f) offset = 1;
				Vector2 pos = codable.position;
				pos.X += codable.velocity.X;
				int tileX = (int)(((double)pos.X + (double)(codable.width / 2) + (double)((codable.width / 2 + 1) * offset)) / 16.0);
				int tileY = (int)(((double)pos.Y + (double)codable.height - 1.0) / 16.0);
				if (Main.tile[tileX, tileY] == null) Main.tile[tileX, tileY] = new Tile();
				if (Main.tile[tileX, tileY - 1] == null) Main.tile[tileX, tileY - 1] = new Tile();
				if (Main.tile[tileX, tileY - 2] == null) Main.tile[tileX, tileY - 2] = new Tile();
				if (Main.tile[tileX, tileY - 3] == null) Main.tile[tileX, tileY - 3] = new Tile();
				if (Main.tile[tileX, tileY + 1] == null) Main.tile[tileX, tileY + 1] = new Tile();
				if (Main.tile[tileX - offset, tileY - 3] == null) Main.tile[tileX - offset, tileY - 3] = new Tile();
				if ((double)(tileX * 16) < (double)pos.X + (double)codable.width && (double)(tileX * 16 + 16) > (double)pos.X && (Main.tile[tileX, tileY].nactive() && (int)Main.tile[tileX, tileY].slope() == 0 && ((int)Main.tile[tileX, tileY - 1].slope() == 0 && Main.tileSolid[(int)Main.tile[tileX, tileY].type]) && !Main.tileSolidTop[(int)Main.tile[tileX, tileY].type] || Main.tile[tileX, tileY - 1].halfBrick() && Main.tile[tileX, tileY - 1].nactive()) && ((!Main.tile[tileX, tileY - 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 1].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 1].type] || Main.tile[tileX, tileY - 1].halfBrick() && (!Main.tile[tileX, tileY - 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 4].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 4].type])) && ((!Main.tile[tileX, tileY - 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 2].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 2].type]) && (!Main.tile[tileX, tileY - 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 3].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 3].type]) && (!Main.tile[tileX - offset, tileY - 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX - offset, tileY - 3].type]))))
				{
					float tileWorldY = (float)(tileY * 16);
					if (Main.tile[tileX, tileY].halfBrick())
						tileWorldY += 8f;
					if (Main.tile[tileX, tileY - 1].halfBrick())
						tileWorldY -= 8f;
					if ((double)tileWorldY < (double)pos.Y + (double)codable.height)
					{
						float tileWorldYHeight = pos.Y + (float)codable.height - tileWorldY;
						float heightNeeded = 16.1f;
						if ((double)tileWorldYHeight <= (double)heightNeeded)
						{
							gfxOffY += codable.position.Y + (float)codable.height - tileWorldY;
							codable.position.Y = tileWorldY - (float)codable.height;
							stepSpeed = (double)tileWorldYHeight >= 9.0 ? 2f : 1f;
						}
					}else
					{
						gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
					}
				}else
				{
					gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
				}
			}else
			{
				gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
			}
		}
		public static bool HitTileOnSide(Entity codable, int dir, bool noYMovement = true)
        {
            if (!noYMovement || codable.velocity.Y == 0f)
            {
                Vector2 dummyVec = default(Vector2);
                return HitTileOnSide(codable.position, codable.width, codable.height, dir, ref dummyVec);
            }
            return false;
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
						
					}*/
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
		*/
	}
}