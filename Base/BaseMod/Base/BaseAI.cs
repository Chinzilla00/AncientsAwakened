using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace AAMod
{
    public class BaseAI
    {
        //------------------------------------------------------//
        //-------------------BASE AI CLASS----------------------//
        //------------------------------------------------------//
        // Contains methods for various AI functions for both   //
        // NPCs and Projectiles, such as adding lighting,       //
        // movement, etc.                                       //
        //------------------------------------------------------//
        //  Author(s): Grox the Great, Yoraiz0r                 //
        //------------------------------------------------------//
   
        #region Custom AI Methods

		public static void AIDive(Projectile projectile, ref int chargeTime, int chargeTimeMax, Vector2 targetCenter)
		{
			chargeTime = (int)Math.Max(0, chargeTime - 1);
			if (chargeTime > 0)
			{
				//while this is running, the velocity of the proj should not be touched, as doing so will break this AI.
				//this AI also ignores tile collision, just a quick concept
				Vector2 positionOld = projectile.position - projectile.velocity;
				float percent = ((float)chargeTime / (float)chargeTimeMax);
				float place = (float)Math.Sin((float)Math.PI * percent); //where on the sin path it is
				float distX = Math.Abs(targetCenter.X - projectile.Center.X), distY = Math.Abs(targetCenter.Y - projectile.Center.Y);
				float distPercentX = distX / chargeTimeMax, distPercentY = distY / chargeTimeMax;
				projectile.velocity = new Vector2((distPercentX * chargeTime) * projectile.direction, place * distPercentY); //move on the x axis a fixed amount per tick and on the Y axis by how much the sine is multiplied by the a percentage of the distance on the y axis.
				projectile.position = positionOld;
			}
		}

		public static void AIMinionPlant(Projectile projectile, ref float[] ai, Entity owner, Vector2 endPoint, bool setTime = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2), Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (setTime){ projectile.timeLeft = 10; }
			Entity target = (GetTarget == null ? null : GetTarget(projectile, owner));
			if (target == null) { target = owner; }
			bool targetOwner = target == owner;
			ai[0] += 1f;
			if (ai[0] > vineTimeExtend)
			{
				vineLength = vineLengthLong;
				if (ai[0] > vineTimeMax) { ai[0] = 0f; }
			}
			Vector2 targetCenter = target.Center + targetOffset + (target == owner ? new Vector2(0f, (owner is Player ? ((Player)owner).gfxOffY : owner is NPC ? ((NPC)owner).gfxOffY : ((Projectile)owner).gfxOffY)) : default(Vector2));
			if (!targetOwner)
			{
				float distTargetX = targetCenter.X - endPoint.X;
				float distTargetY = targetCenter.Y - endPoint.Y;
				float distTarget = (float)Math.Sqrt((double)(distTargetX * distTargetX + distTargetY * distTargetY));
				if (distTarget > vineLength)
				{
					projectile.velocity *= 0.85f;
					projectile.velocity += owner.velocity;
					distTarget = vineLength / distTarget;
					distTargetX *= distTarget;
					distTargetY *= distTarget;
				}
				bool dontMove = (ShootTarget != null && ShootTarget(projectile, owner, target));
				if (!dontMove)
				{
					if (projectile.position.X < endPoint.X + distTargetX)
					{
						projectile.velocity.X = projectile.velocity.X + moveInterval;
						if (projectile.velocity.X < 0f && distTargetX > 0f) { projectile.velocity.X = projectile.velocity.X + moveInterval * 1.5f; }
					}else
					if (projectile.position.X > endPoint.X + distTargetX)
					{
						projectile.velocity.X = projectile.velocity.X - moveInterval;
						if (projectile.velocity.X > 0f && distTargetX < 0f) { projectile.velocity.X = projectile.velocity.X - moveInterval * 1.5f; }
					}
					if (projectile.position.Y < endPoint.Y + distTargetY)
					{
						projectile.velocity.Y = projectile.velocity.Y + moveInterval;
						if (projectile.velocity.Y < 0f && distTargetY > 0f) { projectile.velocity.Y = projectile.velocity.Y + moveInterval * 1.5f; }
					}else
					if (projectile.position.Y > endPoint.Y + distTargetY)
					{
						projectile.velocity.Y = projectile.velocity.Y - moveInterval;
						if (projectile.velocity.Y > 0f && distTargetY < 0f) { projectile.velocity.Y = projectile.velocity.Y - moveInterval * 1.5f; }
					}
				}else
				{
					projectile.velocity *= 0.85f;
					if (Math.Abs(projectile.velocity.X) < moveInterval + 0.01f) { projectile.velocity.X = 0f; }
					if (Math.Abs(projectile.velocity.Y) < moveInterval + 0.01f) { projectile.velocity.Y = 0f; }
				}
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
				if (distTargetX > 0f) { projectile.spriteDirection = 1; projectile.rotation = (float)Math.Atan2((double)distTargetY, (double)distTargetX); }else
				if (distTargetX < 0f) { projectile.spriteDirection = -1; projectile.rotation = (float)Math.Atan2((double)distTargetY, (double)distTargetX) + 3.14f; }
				if (projectile.tileCollide)
				{
					Vector4 slopeVec = Collision.SlopeCollision(projectile.position, projectile.velocity, projectile.width, projectile.height);
					projectile.position = new Vector2(slopeVec.X, slopeVec.Y);
					projectile.velocity = new Vector2(slopeVec.Z, slopeVec.W);
				}
				projectile.position += (owner.position - owner.oldPosition);
			}else
			{
				projectile.position += (owner.position - owner.oldPosition);
				projectile.spriteDirection = (owner.Center.X > projectile.Center.X ? -1 : 1);
				projectile.velocity = AIVelocityLinear(projectile, targetCenter, moveInterval, speedMax, true);
				if (Vector2.Distance(projectile.Center, targetCenter) < speedMax * 1.1f) 
				{
					projectile.rotation = 0f;
					projectile.velocity *= 0f; projectile.Center = targetCenter; 
				}else
				{
					projectile.rotation = BaseUtility.RotationTo(targetCenter, projectile.Center) + (projectile.spriteDirection == -1 ? 3.14f : 0f);
				}
			}
		}

		public static void TileCollidePlant(Projectile projectile, ref Vector2 velocity, float speedMax)
		{
			if (projectile.velocity.X != velocity.X)
			{
				projectile.netUpdate = true;
				projectile.velocity.X = projectile.velocity.X * -0.7f;
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
			}
			if (projectile.velocity.Y != velocity.Y)
			{
				projectile.netUpdate = true;
				projectile.velocity.Y = projectile.velocity.Y * -0.7f;
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
			}
		}


		public static void AIMinionFlier(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, bool movementFixed = false, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, bool autoSpriteDir = true, bool dummyTileCollide = false, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (moveInterval == -1f) { moveInterval = (0.08f * Main.player[projectile.owner].moveSpeed); }
			if (maxSpeed == -1f) { maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed); }
			if (maxSpeedFlying == -1f) { maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed)); }
			projectile.timeLeft = 10;
			bool tileCollide = projectile.tileCollide;
			AIMinionFlier(projectile, ref ai, owner, ref tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, movementFixed, hover, hoverHeight, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget, ShootTarget);
			if(!dummyTileCollide) projectile.tileCollide = tileCollide;
			if (autoSpriteDir) { projectile.spriteDirection = projectile.direction; }
			float dist = Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1) { projectile.spriteDirection = (owner.velocity.X == 0 ? projectile.spriteDirection : owner.velocity.X > 0 ? 1 : -1); }
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && Math.Abs(projectile.velocity.X + projectile.velocity.Y) <= 0.025f) { projectile.spriteDirection = (owner.Center.X > projectile.Center.X ? 1 : -1); }
		}

		/*
		 * Custom AI that works similarly to fighter minion AI. (uses ai[0, 1])
		 * 
		 * owner : The Projectile or NPC who is this minion's owner.
		 * tileCollide : A bool, set to say wether or not the minion can tile collide or not.
		 * netUpdate : set to say wether or not the minion should sync if in multiplayer.
		 * gfxOffsetY : The graphics offset for Y, used for walking up slopes.
		 * stepSpeed : Used for walking up slopes.
		 * minionPos : The minion's position in the minion lineup.
		 * lineDist : The distance between each minion when they line up.
		 * returnDist : The distance to 'fly' back to the player.
		 * teleportDist : The distance to instantly teleport to the player.
		 * moveInterval : How much to move each tick.
		 * maxSpeed : The maxmimum speed of the minion.
		 * maxSpeedFlying : The maximum speed whist 'flying' back to the player.
		 * GetTarget : a Func(Entity codable, Entity owner), returns a Vector2 of the a target's position. If GetTarget is null or it returns default(Vector2) the target is assumed to be the owner.
		 */
		public static void AIMinionFlier(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, bool movementFixed, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > teleportDist) { codable.Center = owner.Center; }
			int tileX = (int)(codable.Center.X / 16f), tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = (tile != null && tile.nactive() && Main.tileSolid[tile.type]);
			float prevAI = ai[0];
			ai[0] = (((ai[0] == 1 && (dist > (float)Math.Max(lineDist, (float)returnDist / 2f) || !BaseUtility.CanHit(codable.Hitbox, owner.Hitbox))) || dist > returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI) { netUpdate = true; }
			if (ai[0] == 0 || ai[0] == 1)
			{
				if (ai[0] == 1) { moveInterval *= 1.5f; maxSpeedFlying *= 1.5f; }
				tileCollide = (ai[0] == 0);
				Entity target = (GetTarget == null ? owner : GetTarget(codable, owner));
				if (target == null) { target = owner; }
				Vector2 targetCenter = target.Center;
				bool isOwner = target == owner;
				bool dontMove = (ai[0] == 0 && ShootTarget != null && ShootTarget(codable, owner, target));
				if (isOwner)
				{
					targetCenter.Y -= hoverHeight;
					if (hover){ targetCenter.X += (lineDist + (lineDist * minionPos)) * -target.direction; }
				}
				if (!hover || !isOwner)
				{
					float dirDist = (hover ? 1.2f : 1.8f);
					float dir = (dist < ((lineDist * minionPos) + lineDist * dirDist) ? (codable.velocity.X > 0 ? 1f : -1f) : (target.Center.X > codable.Center.X ? 1f : -1f));
					//Semierratic movement so it looks more like a swarm and less like synchronized swimmers.
					targetCenter.X += (minionPos == 0 ? 0f : (minionPos % 5 == 0 ? lineDist / 4f : (minionPos % 4 == 0 ? lineDist / 2f : (minionPos % 3 == 0 ? lineDist / 3f : 0f)))) * dir;
					targetCenter.X += lineDist * 2f * dir;
					targetCenter.Y -= (hoverHeight / 4f) * minionPos;
					targetCenter.Y -= (codable.velocity.X < 0 ? lineDist * 0.25f : -lineDist * 0.25f) * (minionPos % 2 == 0 ? 1 : -1);
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
				bool slowdownX = hover && owner.velocity.X < 0.025f && targetDistX < 8f * (float)Math.Max(1f, (maxSpeed / 4f));
				bool slowdownY = hover && owner.velocity.Y < 0.025f && targetDistY < 8f * (float)Math.Max(1f, (maxSpeed / 4f));
				Vector2 vel = AIVelocityLinear(codable, targetCenter, moveInterval, (ai[0] == 0 ? maxSpeed : maxSpeedFlying), true);
				if(!dontMove && !slowdownX){ codable.velocity.X += vel.X * 0.125f; }
				if(!dontMove && !slowdownY){ codable.velocity.Y += vel.Y * 0.125f; }
				if (dontMove || slowdownX){ codable.velocity.X *= (Math.Abs(codable.velocity.X) > 0.01f ? 0.85f : 0f); }
				if ((vel.X > 0 && codable.velocity.X > vel.X) || (vel.X < 0 && codable.velocity.X < vel.X)){ codable.velocity.X = vel.X; }
				if (dontMove || slowdownY){ codable.velocity.Y *= (Math.Abs(codable.velocity.Y) > 0.01f ? 0.85f : 0f); }
				if ((vel.Y > 0 && codable.velocity.Y > vel.Y) || (vel.Y < 0 && codable.velocity.X < vel.Y)){ codable.velocity.Y = vel.Y; }
			}
		}




		public static void AIMinionFighter(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (moveInterval == -1f) { moveInterval = (0.08f * Main.player[projectile.owner].moveSpeed); }
			if (maxSpeed == -1f){ maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed); }
			if (maxSpeedFlying == -1f) { maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed)); }
			projectile.timeLeft = 10;
			AIMinionFighter(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, ref projectile.gfxOffY, ref projectile.stepSpeed, pet ? 0 : projectile.minionPos, jumpDistX, jumpDistY, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			float dist = Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1) { projectile.spriteDirection = (owner.velocity.X == 0 ? projectile.spriteDirection : owner.velocity.X > 0 ? 1 : -1); }
			if ((GetTarget == null ||  GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0) { projectile.spriteDirection = (owner.Center.X > projectile.Center.X ? 1 : -1); }
		}


		/*
		 * Custom AI that works similarly to fighter minion AI. (uses ai[0, 1])
		 * 
		 * owner : The Projectile or NPC who is this minion's owner.
		 * tileCollide : A bool, set to say wether or not the minion can tile collide or not.
		 * netUpdate : set to say wether or not the minion should sync if in multiplayer.
		 * gfxOffsetY : The graphics offset for Y, used for walking up slopes.
		 * stepSpeed : Used for walking up slopes.
		 * minionPos : The minion's position in the minion lineup.
		 * jumpDistX : The minion's max jump distance on the X axis.
		 * jumpDistY : The minion's max jump distance on the Y axis.
		 * lineDist : The distance between each minion when they line up.
		 * returnDist : The distance to 'fly' back to the player.
		 * teleportDist : The distance to instantly teleport to the player.
		 * moveInterval : How much to move each tick.
		 * maxSpeed : The maxmimum speed of the minion.
		 * maxSpeedFlying : The maximum speed whist 'flying' back to the player.
		 * GetTarget : a Func(Entity codable, Entity owner), returns a Vector2 of the a target's position. If GetTarget is null or it returns default(Vector2) the target is assumed to be the owner.
		 */
		public static void AIMinionFighter(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, ref float gfxOffY, ref float stepSpeed, int minionPos, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > teleportDist) { codable.Center = owner.Center; }
			int tileX = (int)(codable.Center.X / 16f), tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = (tile != null && tile.nactive() && Main.tileSolid[tile.type]);
			float prevAI = ai[0];
			ai[0] = (((ai[0] == 1 && (owner.velocity.Y != 0 || dist > (float)Math.Max(lineDist, (float)returnDist / 10f))) || dist > returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI) { netUpdate = true; }
			if (ai[0] == 0) //walking
			{
				tileCollide = true;
				Entity target = (GetTarget == null ? null : GetTarget(codable, owner));
				Vector2 targetCenter = (target == null ? default(Vector2) : target.Center);
				bool isOwner = (target == null || targetCenter == owner.Center);
				if (targetCenter == default(Vector2))
				{
					targetCenter = owner.Center;
					targetCenter.X += (owner.width + 10 + (lineDist * minionPos)) * -owner.direction;
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
				int moveDirection = (targetCenter.X > codable.Center.X ? 1 : -1);
				int moveDirectionY = (targetCenter.Y > codable.Center.Y ? 1 : -1);
				if (isOwner && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
				{
					codable.velocity.X *= (Math.Abs(codable.velocity.X) > 0.01f ? 0.8f : 0f);
				}else
				if (codable.velocity.X < -maxSpeed || codable.velocity.X > maxSpeed)
				{
					if (codable.velocity.Y == 0f){ codable.velocity *= 0.85f; }
				}else
				if (codable.velocity.X < maxSpeed && moveDirection == 1)
				{
					if(codable.velocity.X < 0){ codable.velocity.X *= 0.85f; }
					codable.velocity.X += moveInterval * (codable.velocity.X < 0 ? 2f : 1f);
					if (codable.velocity.X > maxSpeed){ codable.velocity.X = maxSpeed; }
				}else
				if (codable.velocity.X > -maxSpeed && moveDirection == -1)
				{
					if(codable.velocity.X > 0) { codable.velocity.X *= 0.8f; }
					codable.velocity.X -= moveInterval * (codable.velocity.X > 0 ? 2f : 1f);
					if(codable.velocity.X < -maxSpeed){ codable.velocity.X = -maxSpeed; }
				}
				WalkupHalfBricks(codable, ref gfxOffY, ref stepSpeed);
				if (HitTileOnSide(codable, 3))
				{
					if ((codable.velocity.X < 0f && moveDirection == -1) || (codable.velocity.X > 0f && moveDirection == 1))
					{
						bool test = (target != null && !isOwner && targetDistX < 50f && targetDistY > codable.height + (codable.height / 2) && targetDistY < (16f * (jumpDistY + 1)) && BaseUtility.CanHit(codable.Hitbox, target.Hitbox));
						Vector2 newVec = AttemptJump(codable.position, codable.velocity, codable.width, codable.height, moveDirection, moveDirectionY, jumpDistX, jumpDistY, maxSpeed, true, target, test);
						if (tileCollide)
						{
							newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height);
							Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height);
							codable.position = new Vector2(slopeVec.X, slopeVec.Y);
							codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
						}
						if (codable.velocity != newVec) { codable.velocity = newVec; netUpdate = true; }
					}
				}else{ codable.velocity.Y += 0.35f; } //gravity
			}else //flying
			{
				tileCollide = false;
				Vector2 targetCenter = owner.Center;
				if (owner.velocity.Y != 0f && dist < 80)
				{
					targetCenter = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 newVel = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, targetCenter));
				if (owner.velocity.Y != 0f && ((newVel.X > 0 && codable.velocity.X < 0) || (newVel.X < 0 && codable.velocity.X > 0)))
				{
					codable.velocity *= 0.98f; newVel *= 0.02f; codable.velocity += newVel;
				}else{ codable.velocity = newVel; }
				codable.position += owner.velocity;	
			}
		}

		public static void AIMinionSlime(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = -1f, float jumpVelY = 20f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (jumpVelX == -1f) { jumpVelX = (2f + Main.player[projectile.owner].velocity.X); }
			if (maxSpeedFlying == -1f) { maxSpeedFlying = Math.Max(jumpVelX, jumpVelY); }
			projectile.timeLeft = 10;
			AIMinionSlime(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, lineDist, returnDist, teleportDist, jumpVelX, jumpVelY, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			float dist = Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1) { projectile.spriteDirection = (owner.velocity.X == 0 ? projectile.spriteDirection : owner.velocity.X > 0 ? 1 : -1); }
			if ((GetTarget == null ||  GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0) { projectile.spriteDirection = (owner.Center.X > projectile.Center.X ? 1 : -1); }
		}		
		
		/*
		 * Custom AI that works similarly to slime minion AI. (uses ai[0, 1])
		 * 
		 * owner : The Projectile or NPC who is this minion's owner.
		 * tileCollide : A bool, set to say wether or not the minion can tile collide or not.
		 * netUpdate : set to say wether or not the minion should sync if in multiplayer.
		 * gfxOffsetY : The graphics offset for Y, used for walking up slopes.
		 * stepSpeed : Used for walking up slopes.
		 * minionPos : The minion's position in the minion lineup.
		 * lineDist : The distance between each minion when they line up.
		 * returnDist : The distance to 'fly' back to the player.
		 * teleportDist : The distance to instantly teleport to the player.
		 * jumpVelX : The velocity to bounce on the X axis.
		 * jumpVelY : The velocity to boucne on the Y axis.
		 * maxSpeedFlying : The maximum speed whist 'flying' back to the player.
		 * GetTarget : a Func(Entity codable, Entity owner), returns a Vector2 of the a target's position. If GetTarget is null or it returns default(Vector2) the target is assumed to be the owner.
		 */
		public static void AIMinionSlime(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = 2f, float jumpVelY = 20f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > teleportDist) { codable.Center = owner.Center; }
			int tileX = (int)(codable.Center.X / 16f), tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = (tile != null && tile.nactive() && Main.tileSolid[tile.type]);
			float prevAI = ai[0];
			ai[0] = (((ai[0] == 1 && (owner.velocity.Y != 0 || dist > (float)Math.Max(lineDist, (float)returnDist / 10f))) || dist > returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI) { netUpdate = true; }
			if (ai[0] == 0) //walking
			{
				tileCollide = true;
				Entity target = (GetTarget == null ? null : GetTarget(codable, owner));
				Vector2 targetCenter = (target == null ? default(Vector2) : target.Center);
				bool isOwner = (target == null || targetCenter == owner.Center);
				if (targetCenter == default(Vector2))
				{
					targetCenter = owner.Center;
					targetCenter.X += (lineDist + (lineDist * minionPos)) * -owner.direction;
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
				int moveDirection = (targetCenter.X > codable.Center.X ? 1 : -1);
				int moveDirectionY = (targetCenter.Y > codable.Center.Y ? 1 : -1);					
				if (isOwner && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
				{
					codable.velocity.X *= (Math.Abs(codable.velocity.X) > 0.01f ? 0.8f : 0f);
				}else
				if (codable.velocity.Y == 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.8f;
					if (codable.velocity.X > -0.1f && codable.velocity.X < 0.1f){ codable.velocity.X = 0f; }
					codable.velocity.Y = -jumpVelY;
					codable.velocity.X += jumpVelX * moveDirection;
					codable.position += codable.velocity;
				}
				if (HitTileOnSide(codable, 3))
				{
					if ((codable.velocity.X < 0f && moveDirection == -1) || (codable.velocity.X > 0f && moveDirection == 1))
					{
						Vector2 newVec = codable.velocity;
						if (tileCollide)
						{
							newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height);
							Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height);
							codable.position = new Vector2(slopeVec.X, slopeVec.Y);
							codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
						}
						if (codable.velocity != newVec) { codable.velocity = newVec; netUpdate = true; }
					}
				}else{ codable.velocity.Y += 0.35f; } //gravity*/			
				/*if (isOwner && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
				{
					codable.velocity.X *= (Math.Abs(codable.velocity.X) > 0.01f ? 0.8f : 0f);
				}else
				if (codable.velocity.X < -maxSpeed || codable.velocity.X > maxSpeed)
				{
					if (codable.velocity.Y == 0f){ codable.velocity *= 0.85f; }
				}else
				if (codable.velocity.X < maxSpeed && moveDirection == 1)
				{
					if(codable.velocity.X < 0){ codable.velocity.X *= 0.85f; }
					codable.velocity.X += moveInterval * (codable.velocity.X < 0 ? 2f : 1f);
					if (codable.velocity.X > maxSpeed){ codable.velocity.X = maxSpeed; }
				}else
				if (codable.velocity.X > -maxSpeed && moveDirection == -1)
				{
					if(codable.velocity.X > 0) { codable.velocity.X *= 0.8f; }
					codable.velocity.X -= moveInterval * (codable.velocity.X > 0 ? 2f : 1f);
					if(codable.velocity.X < -maxSpeed){ codable.velocity.X = -maxSpeed; }
				}
				WalkupHalfBricks(codable, ref gfxOffY, ref stepSpeed);
				if (HitTileOnSide(codable, 3))
				{
					if ((codable.velocity.X < 0f && moveDirection == -1) || (codable.velocity.X > 0f && moveDirection == 1))
					{
						Vector2 newVec = AttemptJump(codable.position, codable.velocity, codable.width, codable.height, moveDirection, moveDirectionY, 4, 5, maxSpeed, true, null);
						if (tileCollide)
						{
							newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height);
							Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height);
							codable.position = new Vector2(slopeVec.X, slopeVec.Y);
							codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
						}
						if (codable.velocity != newVec) { codable.velocity = newVec; netUpdate = true; }
					}
				}else{ codable.velocity.Y += 0.35f; } //gravity*/
			}else //flying
			{
				tileCollide = false;
				Vector2 targetCenter = owner.Center;
				if (owner.velocity.Y != 0f && dist < 80)
				{
					targetCenter = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 newVel = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, targetCenter));
				if (owner.velocity.Y != 0f && ((newVel.X > 0 && codable.velocity.X < 0) || (newVel.X < 0 && codable.velocity.X > 0)))
				{
					codable.velocity *= 0.98f; newVel *= 0.02f; codable.velocity += newVel;
				}else{ codable.velocity = newVel; }
				codable.position += owner.velocity;	
			}
		}
				
		
		/*
		 * Custom AI that will cause the npc to rotate around a point in a fixed circle.
		 * 
		 * rotation : The codable's rotation.
		 * moveRot : A value storing the internal rotation of the codable.
		 * rotateCenter : The center to be rotating around.
		 * absolute : If true, moves it by position instead of by velocity.
		 * rotDistance : How far from the rotateCenter to rotate.
		 * rotThreshold : Only used if absolute is false, used to determine how much 'give' the codable has before it forces itself back into the rotation.
		 * rotAmount : How much to rotate each tick.
		 * moveTowards : Only used if absolute is false, if outside the rotation, move towards it.
		 */
		public static void AIRotate(Entity codable, ref float rotation, ref float moveRot, Vector2 rotateCenter, bool absolute = false, float rotDistance = 50f, float rotThreshold = 20f, float rotAmount = 0.024f, bool moveTowards = true)
		{
			if (absolute)
			{
				moveRot += rotAmount;
				Vector2 rotVec = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
				codable.Center = rotVec;
				rotVec.Normalize();
				rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
				codable.velocity *= 0f;
			}else
			{
				float dist = Vector2.Distance(codable.Center, rotateCenter);
				if (dist < rotDistance)//close enough to rotate
				{
					if (rotDistance - dist > rotThreshold) //too close, get back into position
					{
						moveRot += rotAmount;
						Vector2 rotVec = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
						float rot2 = BaseUtility.RotationTo(codable.Center, rotVec);
						codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot2);
						rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
					}else
					{
						moveRot += rotAmount;
						Vector2 rotVec = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
						float rot2 = BaseUtility.RotationTo(codable.Center, rotVec);
						codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot2);
						rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
					}
				}else
				if (moveTowards)
				{
					codable.velocity = AIVelocityLinear(codable, rotateCenter, rotAmount, rotAmount, true);
					rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
				}else { codable.velocity *= 0.95f; }
			}
		}


        public static void AIPounce(Entity codable, Player player, float pounceScalar = 3f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50, float maxDistance = 60)
        {
            if (player == null || !player.active || player.dead) { return; }
            AIPounce(codable, player.Center, pounceScalar, maxSpeed, yBoost, minDistance, maxDistance);
        }
        
        /*
         * Custom AI that will cause the npc to 'pounce' at the given target when it is within range.
         * 
         * pounceCenter : the central point of which to pounce.
         * pounceScalar : How much to scale the X-axis velocity by.
         * maxSpeed : the maximum speed to pounce by on the X-axis of the codable.
         * yBoost : the amount to jump by on the y-axis.
         * minDistance/maxDistance : the minimum and maximum distance from the pounce center that the codable is allowed to pounce, respectively.
         */
        public static void AIPounce(Entity codable, Vector2 pounceCenter, float pounceScalar = 3.5f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50, float maxDistance = 60)
        {
            int direction = (codable is NPC ? ((NPC)codable).direction : codable is Projectile ? ((Projectile)codable).direction : 0);
            float dist = Vector2.Distance(codable.Center, pounceCenter);
            if (pounceCenter.Y <= codable.Center.Y && dist > minDistance && dist < maxDistance)
            {
                bool onLeft = pounceCenter.X < codable.Center.X;
                if (codable.velocity.Y == 0 && ((onLeft && direction == -1) || (!onLeft && direction == 1)))
                {
                    codable.velocity.X *= pounceScalar;
                    if (codable.velocity.X > maxSpeed) { codable.velocity.X = maxSpeed; } if (codable.velocity.X < -maxSpeed) { codable.velocity.X = -maxSpeed; }
                    codable.velocity.Y = yBoost;
                    if(codable is NPC){ ((NPC)codable).netUpdate = true; }
                }
            }
        }

        /*
         * Custom AI that will cause the npc to follow the path of given points. (uses ai[0, 1, 2])
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * points : the array of points to follow.
         * moveInterval : the amount to move by per tick.
         * maxSpeed : the maximum speed of the npc.
         * direct : If true npc's velocity is set so it moves in a straight line. If false, moves similarly to Flier AI.
         */
        public static void AIPath(NPC npc, ref float[] ai, Vector2[] points, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false)
        {
            Vector2 destVec = new Vector2(ai[0], ai[1]);
            if (Main.netMode != 1 && destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, ((npc.width + npc.height) / 2f) * 0.45f))
            {
                ai[0] = 0f; ai[1] = 0f; destVec = default(Vector2);
            }
            if (npc.ai[2] < points.Length)
            {
                //if the destination vec is default (0, 0), get the current point.
                if (destVec == default(Vector2))
                {
                    npc.velocity *= 0.95f;
                    if(Main.netMode != 1)
                    {
                        destVec = points[(int)npc.ai[2]];
                        ai[0] = destVec.X; ai[1] = destVec.Y;
                        ai[2]++;
                        npc.netUpdate = true;
                    }
                }else //otherwise move to the point.
                {
                    npc.velocity = AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
                }
            }
        }

        /*
         * Custom AI that will cause the npc to 'tackle' a specific point given. (uses ai[0, 1, 2])
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * point : the central point of which to 'gravitate'.
         * moveInterval : the amount to move by per tick.
         * maxSpeed : the maximum speed of the npc.
         * direct : If true npc's velocity is set so it moves in a straight line. If false, moves similarly to Flier AI.
         * tackleDelay : the amount of time between tackles in ticks.
         */
        public static void AITackle(NPC npc, ref float[] ai, Vector2 point, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false, int tackleDelay = 50, float drift = 0.95f)
        {
            Vector2 destVec = new Vector2(ai[0], ai[1]);
            if (destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, ((npc.width + npc.height) / 2f) * 0.45f))
            {
                ai[0] = 0f; ai[1] = 0f; destVec = default(Vector2);
            }
            //if the destination vec is default (0, 0), get the current point.
            if (destVec == default(Vector2))
            {
                npc.velocity *= drift;
                ai[2]--;
                if (ai[2] <= 0)
                {
                    ai[2] = tackleDelay;
                    destVec = point;
                    ai[0] = destVec.X; ai[1] = destVec.Y;
                }
                if(Main.netMode == 2){ npc.netUpdate = true; }
            }else //otherwise move to the point.
            {
                npc.velocity = AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
            }
        }

        public static Random GetSyncedRand(NPC npc)
        {
            return new Random(npc.whoAmI);
        }

        /*
         * Custom AI that will cause the npc to 'gravitate' near a specific point given. (uses ai[0, 1, 2])
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * rand : a Random which should be syncronized on both sides of an npc.
         * point : the central point of which to 'gravitate'.
         * moveInterval : the amount to move by per tick.
         * maxSpeed : the maximum speed of the npc.
         * canCrossCenter : If true, npc can cross the central point. If false it will never directly cross the central point.
         * direct : If true npc's velocity is set so it moves in a straight line. If false, moves similarly to Flier AI.
         * minDistance/maxDistance : the minimum and maximum distance the calculated points have to be from the central point, respectively.
         */
        public static void AIGravitate(NPC npc, ref float[] ai, UnifiedRandom rand, Vector2 point, float moveInterval = 0.06f, float maxSpeed = 2f, bool canCrossCenter = true, bool direct = false, int minDistance = 50, int maxDistance = 200)
        {
            Vector2 destVec = new Vector2(ai[0], ai[1]);
            bool idleTooLong = false;
            if (Main.netMode != 1)
            {
                //used to prevent the npc from getting 'stuck' trying to reach a point
                if (!idleTooLong && destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(12f, ((npc.width + npc.height) / 2f) * 3f * (moveInterval / 0.06f)))
                {
                    ai[2]++;
                    if (ai[2] > 100) { ai[2] = 0; idleTooLong = true; }
                }
                //if the destination vec is not null and the npc is close to the point (or has been idle too long), set it to default.
                if (idleTooLong || (destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, ((npc.width + npc.height) / 2f) * 0.75f)))
                {
                    ai[0] = 0f; ai[1] = 0f; destVec = default(Vector2);
                }
            }
            //if the destination vec is default (0, 0)...
            if (destVec == default(Vector2))
            {
                if (npc.velocity.X > 0.3f || npc.velocity.Y > 0.3f) { npc.velocity.X *= 0.95f; }
                if (canCrossCenter)
                {
                    destVec = BaseUtility.GetRandomPosNear(point, rand, minDistance, maxDistance);
                }else
                {
                    int distance = maxDistance - minDistance;
                    Vector2 topLeft = new Vector2(point.X - (minDistance + rand.Next(distance)), point.Y - (minDistance + rand.Next(distance)));
                    Vector2 topRight = new Vector2(point.X + (minDistance + rand.Next(distance)), topLeft.Y);
                    Vector2 bottomLeft = new Vector2(topLeft.X, point.Y + (minDistance + rand.Next(distance)));
                    Vector2 bottomRight = new Vector2(topRight.X, bottomLeft.Y);
                    float tempDist = 9999999f;
                    Vector2 closestPoint = default(Vector2);
                    for (int m = 0; m < 4; m++)
                    {
                        Vector2 corner = (m == 0 ? topLeft : m == 1 ? topRight : m == 2 ? bottomLeft : bottomRight);
                        if (Vector2.Distance(npc.Center, corner) < tempDist)
                        {
                            tempDist = Vector2.Distance(npc.Center, corner);
                            closestPoint = corner;
                        }
                    }
                    if (closestPoint == topLeft || closestPoint == bottomRight) { destVec = rand.Next(2) == 0 ? topRight : bottomLeft; }else
                    if (closestPoint == topRight || closestPoint == bottomLeft) { destVec = rand.Next(2) == 0 ? topLeft : bottomRight; }
                }
                ai[0] = destVec.X; ai[1] = destVec.Y;
                if(Main.netMode == 2){ npc.netUpdate = true; }
            }else
            if (destVec != default(Vector2)) //otherwise move towards the point.
            {
                npc.velocity = AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
            }
        }


        public static Vector2 AIVelocityLinear(Entity codable, Vector2 destVec, float moveInterval, float maxSpeed, bool direct = false)
        {
            Vector2 returnVelocity = codable.velocity;
            bool tileCollide = (codable is NPC ? !(((NPC)codable).noTileCollide) : codable is Projectile ? ((Projectile)codable).tileCollide : false);
            if (direct)
            {
                Vector2 rotVec = BaseUtility.RotateVector(codable.Center, codable.Center + new Vector2(maxSpeed, 0f), BaseUtility.RotationTo(codable.Center, destVec));
                returnVelocity = rotVec - codable.Center;
            }else
            {
                if (codable.Center.X > destVec.X) { returnVelocity.X = Math.Max(-maxSpeed, returnVelocity.X - moveInterval); } else if (codable.Center.X < destVec.X) { returnVelocity.X = Math.Min(maxSpeed, returnVelocity.X + moveInterval); }
                if (codable.Center.Y > destVec.Y) { returnVelocity.Y = Math.Max(-maxSpeed, returnVelocity.Y - moveInterval); } else if (codable.Center.Y < destVec.Y) { returnVelocity.Y = Math.Min(maxSpeed, returnVelocity.Y + moveInterval); }
            }
            if (tileCollide)
            {
                returnVelocity = Collision.TileCollision(codable.position, returnVelocity, codable.width, codable.height);
            }
            return returnVelocity;
        }

        #endregion

        #region Vanilla Projectile AI Copy Methods
        /*-----------------------------------------
         * 
         * These are methods of vanilla projectile AIs
         * cleaned up. If a method has Entity instead
         * of Projectile as it's first argument, it
         * means npcs can use the method too.
         * 
         * ----------------------------------------
         */		

		public static void AIProjWorm(Projectile p, ref float[] ai, int[] wormTypes, int wormLength, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
            int[] wtypes = new int[(wormTypes.Length == 1 ? 1 : wormLength)];
            wtypes[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				wtypes[wtypes.Length - 1] = wormTypes[2];
				for (int m = 1; m < wtypes.Length - 1; m++)
				{
					wtypes[m] = wormTypes[1];
				}
			}
			int dummyNPC = -1;
			AIProjWorm(p, ref ai, ref dummyNPC, wtypes, velScalar, velScalarIdle, velocityMax, velocityMaxIdle);
		}

		public static void AIProjWorm(Projectile p, ref float[] ai, ref int npcTargetToAttack, int[] wormTypes, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
			Player plrOwner = Main.player[p.owner];
			if ((int)Main.time % 120 == 0) p.netUpdate = true;
			if (!plrOwner.active){ p.active = false; return; }
			bool isHead = p.type == wormTypes[0];
			bool isWorm = BaseUtility.InArray(wormTypes, p.type);
			bool isTail = p.type == wormTypes[wormTypes.Length - 1];
			int wormWidthHeight = 10;
			if (isWorm)
			{
				p.timeLeft = 2;
				wormWidthHeight = 30;
			}
			if (isHead)
			{
				Vector2 plrCenter = plrOwner.Center;
				float minAttackDist = 700f;
				float returnDist = 1000f;
				float teleportDist = 2000f;
				int target = -1;
				if (p.Distance(plrCenter) > teleportDist)
				{
					p.Center = plrCenter;
					p.netUpdate = true;
				}
				bool flag66 = true;
				if (flag66)
				{
					NPC ownerMinionAttackTargetNPC5 = p.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(p, false))
					{
						float ownerTargetDist = p.Distance(ownerMinionAttackTargetNPC5.Center);
						if (ownerTargetDist < minAttackDist * 2f)
						{
							target = ownerMinionAttackTargetNPC5.whoAmI;
							/*if (ownerMinionAttackTargetNPC5.boss)
							{
								int whoAmI = ownerMinionAttackTargetNPC5.whoAmI;
							}
							else
							{
								int whoAmI2 = ownerMinionAttackTargetNPC5.whoAmI;
							}*/
						}
					}
					if (target < 0)
					{
						int dummy;
						for (int m = 0; m < 200; m = dummy + 1)
						{
							NPC npcTarget = Main.npc[m];
							if (npcTarget.CanBeChasedBy(p, false) && plrOwner.Distance(npcTarget.Center) < returnDist)
							{
								float npcTargetDist = p.Distance(npcTarget.Center);
								if (npcTargetDist < minAttackDist)
								{
									target = m;
									bool boss = npcTarget.boss;
								}
							}
							dummy = m;
						}
					}
				}
				npcTargetToAttack = target;
				if (target != -1)
				{
					NPC npcTarget2 = Main.npc[target];
					Vector2 npcDist = npcTarget2.Center - p.Center;
					(npcDist.X > 0f).ToDirectionInt();
					(npcDist.Y > 0f).ToDirectionInt();
					float velocityScalar = 0.4f;
					if (npcDist.Length() < 600f) velocityScalar = 0.6f;
					if (npcDist.Length() < 300f) velocityScalar = 0.8f;
					velocityScalar *= velScalar;
					if (npcDist.Length() > npcTarget2.Size.Length() * 0.75f)
					{
						p.velocity += Vector2.Normalize(npcDist) * velocityScalar * 1.5f;
						if (Vector2.Dot(p.velocity, npcDist) < 0.25f)
						{
							p.velocity *= 0.8f;
						}
					}
					if (p.velocity.Length() > velocityMax)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMax;
					}
				}else
				{
					float velocityScalarIdle = 0.2f;
					Vector2 newPointDist = plrCenter - p.Center;
					if (newPointDist.Length() < 200f)
					{
						velocityScalarIdle = 0.12f;
					}
					if (newPointDist.Length() < 140f)
					{
						velocityScalarIdle = 0.06f;
					}
					velocityScalarIdle *= velScalarIdle;
					if (newPointDist.Length() > 100f)
					{
						if (Math.Abs(plrCenter.X - p.Center.X) > 20f)
						{
							p.velocity.X = p.velocity.X + velocityScalarIdle * (float)Math.Sign(plrCenter.X - p.Center.X);
						}
						if (Math.Abs(plrCenter.Y - p.Center.Y) > 10f)
						{
							p.velocity.Y = p.velocity.Y + velocityScalarIdle * (float)Math.Sign(plrCenter.Y - p.Center.Y);
						}
					}
					else if (p.velocity.Length() > 2f)
					{
						p.velocity *= 0.96f;
					}
					if (Math.Abs(p.velocity.Y) < 1f)
					{
						p.velocity.Y = p.velocity.Y - 0.1f;
					}
					if (p.velocity.Length() > velocityMaxIdle)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMaxIdle;
					}
				}
				p.rotation = p.velocity.ToRotation() + 1.57079637f;
				int direction = p.direction;
				p.direction = (p.spriteDirection = ((p.velocity.X > 0f) ? 1 : -1));
				if (direction != p.direction)
				{
					p.netUpdate = true;
				}
				float scaleChange = MathHelper.Clamp(p.localAI[0], 0f, 50f);
				p.position = p.Center;
				p.scale = 1f + scaleChange * 0.01f;
				p.width = (p.height = (int)((float)wormWidthHeight * p.scale));
				p.Center = p.position;
				if (p.alpha > 0)
				{
					p.alpha -= 42;
					if (p.alpha < 0)
					{
						p.alpha = 0;
						return;
					}
				}
			}else
			{
				bool npcInFront = false;
				Vector2 projCenter = Vector2.Zero;
				float projRot = 0f;
				float tileScalar = 0f;
				float projectileScale = 1f;
				if (p.ai[1] == 1f)
				{
					p.ai[1] = 0f;
					p.netUpdate = true;
				}
				int byUUID = Projectile.GetByUUID(p.owner, (int)p.ai[0]);
				if (isWorm && byUUID >= 0 && Main.projectile[byUUID].active && !isTail)
				{
					npcInFront = true;
					projCenter = Main.projectile[byUUID].Center;
					projRot = Main.projectile[byUUID].rotation;
					float projScale = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
					projectileScale = projScale;
					tileScalar = 16f;
					int num1064 = Main.projectile[byUUID].alpha;
					Main.projectile[byUUID].localAI[0] = p.localAI[0] + 1f;
					if (Main.projectile[byUUID].type != wormTypes[0])
					{
						Main.projectile[byUUID].localAI[1] = (float)p.whoAmI;
					}
					if (p.owner == Main.myPlayer && Main.projectile[byUUID].type == wormTypes[0] && p.type == wormTypes[wormTypes.Length - 1])
					{
						Main.projectile[byUUID].Kill();
						p.Kill();
						return;
					}
				}
				if (!npcInFront)
				{
					return;
				}
				p.alpha -= 42;
				if (p.alpha < 0)
				{
					p.alpha = 0;
				}
				p.velocity = Vector2.Zero;
				Vector2 centerDist = projCenter - p.Center;
				if (projRot != p.rotation)
				{
					float rotDist = MathHelper.WrapAngle(projRot - p.rotation);
					centerDist = centerDist.RotatedBy((double)(rotDist * 0.1f), default(Vector2));
				}
				p.rotation = centerDist.ToRotation() + 1.57079637f;
				p.position = p.Center;
				p.scale = projectileScale;
				p.width = (p.height = (int)((float)wormWidthHeight * p.scale));
				p.Center = p.position;
				if (centerDist != Vector2.Zero)
				{
					p.Center = projCenter - Vector2.Normalize(centerDist) * tileScalar * projectileScale;
				}
				p.spriteDirection = ((centerDist.X > 0f) ? 1 : -1);
				return;
			}
		}

		public static void AIProjSpaceOctopus(Projectile p, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, Action<int, Projectile> SpawnDust = null)
		{
			AIProjSpaceOctopus(p, ref ai, parentNPCType, fireProjType, shootVelocity, hoverTime, xMult, yMult, -1, true, false, SpawnDust);
		}

		public static void AIProjSpaceOctopus(Projectile projectile, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, int fireDmg = -1, bool useParentTarget = true, bool noParentHover = false, Action<int, Projectile> SpawnDust = null)
		{
			if(fireDmg == -1) fireDmg = projectile.damage;
			
			ai[0] += 1f;
			if (ai[0] < hoverTime)
			{
				bool parentAlive = true;
				int parentID = (int)ai[1];
				if (Main.npc[parentID].active && Main.npc[parentID].type == parentNPCType)
				{
					if (!noParentHover && Main.npc[parentID].oldPos[1] != Vector2.Zero)
					{
						projectile.position += Main.npc[parentID].position - Main.npc[parentID].oldPos[1];
					}
				}else
				{
					ai[0] = hoverTime;
					parentAlive = false;
				}
				if (parentAlive && !noParentHover)
				{
					projectile.velocity += new Vector2((float)Math.Sign(Main.npc[parentID].Center.X - projectile.Center.X), (float)Math.Sign(Main.npc[parentID].Center.Y - projectile.Center.Y)) * new Vector2(xMult, yMult);
					if (projectile.velocity.Length() > 6f)
					{
						projectile.velocity *= 6f / projectile.velocity.Length();
					}
				}
				if(SpawnDust != null) SpawnDust(0, projectile);
				projectile.rotation = projectile.velocity.X * 0.1f;
			}
			if (ai[0] == hoverTime)
			{
				bool hasParentTarget = true;
				int parentTarget = -1;
				if (!useParentTarget)
				{
					int parentID2 = (int)ai[1];
					if (Main.npc[parentID2].active && Main.npc[parentID2].type == parentNPCType)
					{
						parentTarget = Main.npc[parentID2].target;
					}else
					{
						hasParentTarget = false;
					}
				}else
				{
					hasParentTarget = false;
				}
				if (!hasParentTarget)
				{
					parentTarget = (int)Player.FindClosest(projectile.position, projectile.width, projectile.height);
				}
				Vector2 distanceVec = Main.player[parentTarget].Center - projectile.Center;
				distanceVec.X += (float)Main.rand.Next(-50, 51);
				distanceVec.Y += (float)Main.rand.Next(-50, 51);
				distanceVec.X *= (float)Main.rand.Next(80, 121) * 0.01f;
				distanceVec.Y *= (float)Main.rand.Next(80, 121) * 0.01f;
				Vector2 distVecNormal = Vector2.Normalize(distanceVec);
				if (distVecNormal.HasNaNs())
				{
					distVecNormal = Vector2.UnitY;
				}
				if (fireProjType == -1)
				{
					projectile.velocity = distVecNormal * shootVelocity;
					projectile.netUpdate = true;
				}else
				{
					if (Main.netMode != 1 && Collision.CanHitLine(projectile.Center, 0, 0, Main.player[parentTarget].Center, 0, 0))
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, distVecNormal.X * shootVelocity, distVecNormal.Y * shootVelocity, fireProjType, fireDmg, 1f, Main.myPlayer, 0f, 0f);
					}
					ai[0] = 0f;
				}
			}
			if (ai[0] >= hoverTime)
			{
				projectile.rotation = projectile.rotation.AngleLerp(projectile.velocity.ToRotation() + 1.57079637f, 0.4f);
				if(SpawnDust != null) SpawnDust(1, projectile);
			}
		}
		 
		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, float yoyoTimeMax = -1, float maxRange = -1, float topSpeed = -1, bool dontChannel = false, float rotAmount = 0.45f)
		{
			if(yoyoTimeMax == -1) yoyoTimeMax = ProjectileID.Sets.YoyosLifeTimeMultiplier[p.type];
			if(maxRange == -1) maxRange = ProjectileID.Sets.YoyosMaximumRange[p.type];
			if(topSpeed == -1) topSpeed = ProjectileID.Sets.YoyosTopSpeed[p.type];	
			AIYoyo(p, ref ai, ref localAI, Main.player[p.owner], Main.player[p.owner].channel, default(Vector2), yoyoTimeMax, maxRange, topSpeed, dontChannel, rotAmount);
		}

		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, bool isChanneling, Vector2 targetPos = default(Vector2), float yoyoTimeMax = 120, float maxRange = 150, float topSpeed = 8f, bool dontChannel = false, float rotAmount = 0.45f)
		{
			bool playerYoyo = owner is Player;
			Player powner = (playerYoyo ? (Player)owner : null);
			float meleeSpeed = (playerYoyo ? powner.meleeSpeed : 1f);
			Vector2 targetP = targetPos;
			if(playerYoyo && Main.myPlayer == p.owner && targetPos == default(Vector2)) targetP = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;

			bool yoyoFound = false;
			if(owner is Player)
			{
				for (int i = 0; i < p.whoAmI; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == p.owner && Main.projectile[i].type == p.type) yoyoFound = true;
				}
			}
			if ((playerYoyo && p.owner == Main.myPlayer) || (!playerYoyo && Main.netMode != 1))
			{
				localAI[0] += 1f;
				if (yoyoFound) localAI[0] += (float)Main.rand.Next(10, 31) * 0.1f;
				float yoyoTimeLeft = localAI[0] / 60f;
				yoyoTimeLeft /= (1f + meleeSpeed) / 2f;
				if (yoyoTimeMax != -1f && yoyoTimeLeft > yoyoTimeMax) ai[0] = -1f;
			}
			if ((playerYoyo && powner.dead) || (!playerYoyo && !owner.active)){ p.Kill(); return; }
			if (playerYoyo && !dontChannel && !yoyoFound)
			{
				powner.heldProj = p.whoAmI;
				powner.itemAnimation = 2;
				powner.itemTime = 2;
				if (p.position.X + (float)(p.width / 2) > powner.position.X + (float)(powner.width / 2))
				{
					powner.ChangeDir(1);
					p.direction = 1;
				}else
				{
					powner.ChangeDir(-1);
					p.direction = -1;
				}
			}
			if (p.velocity.HasNaNs()) p.Kill();
			p.timeLeft = 6;
			float pMaxRange = maxRange; 
			float pTopSpeed = topSpeed;
			if (playerYoyo && powner.yoyoString)
			{
				pMaxRange = pMaxRange * 1.25f + 30f;
			}
			pMaxRange /= (1f + meleeSpeed * 3f) / 4f;
			pTopSpeed /= (1f + meleeSpeed * 3f) / 4f;
			float topSpeedX = 14f - pTopSpeed / 2f;
			float topSpeedY = 5f + pTopSpeed / 2f;
			if (yoyoFound)
			{
				topSpeedY += 20f;
			}
			if (ai[0] >= 0f)
			{
				if (p.velocity.Length() > pTopSpeed)
				{
					p.velocity *= 0.98f;
				}
				bool yoyoTooFar = false;
				bool yoyoWayTooFar = false;
				Vector2 centerDist = owner.Center - p.Center;
				if (centerDist.Length() > pMaxRange)
				{
					yoyoTooFar = true;
					if ((double)centerDist.Length() > (double)pMaxRange * 1.3)
					{
						yoyoWayTooFar = true;
					}
				}
				if ((playerYoyo && p.owner == Main.myPlayer) || (!playerYoyo && Main.netMode != 1))
				{
					if ((playerYoyo && (!isChanneling || powner.stoned || powner.frozen)) || (!playerYoyo && !isChanneling))
					{
						ai[0] = -1f;
						ai[1] = 0f;
						p.netUpdate = true;
					}else
					{
						Vector2 mousePos = targetP;
						float x = mousePos.X;
						float y = mousePos.Y;
						Vector2 mouseDist = new Vector2(x, y) - owner.Center;
						if (mouseDist.Length() > pMaxRange)
						{
							mouseDist.Normalize();
							mouseDist *= pMaxRange;
							mouseDist = owner.Center + mouseDist;
							x = mouseDist.X;
							y = mouseDist.Y;
						}
						if (ai[0] != x || ai[1] != y)
						{
							Vector2 coord = new Vector2(x, y);
							Vector2 coordDist = coord - owner.Center;
							if (coordDist.Length() > pMaxRange - 1f)
							{
								coordDist.Normalize();
								coordDist *= pMaxRange - 1f;
								coord = owner.Center + coordDist;
								x = coord.X;
								y = coord.Y;
							}
							ai[0] = x;
							ai[1] = y;
							p.netUpdate = true;
						}
					}
				}
				if (yoyoWayTooFar && p.owner == Main.myPlayer)
				{
					ai[0] = -1f;
					p.netUpdate = true;
				}
				if (ai[0] >= 0f)
				{
					if (yoyoTooFar)
					{
						topSpeedX /= 2f;
						pTopSpeed *= 2f;
						if (p.Center.X > owner.Center.X && p.velocity.X > 0f) p.velocity.X = p.velocity.X * 0.5f;
						if (p.Center.Y > owner.Center.Y && p.velocity.Y > 0f) p.velocity.Y = p.velocity.Y * 0.5f;
						if (p.Center.X < owner.Center.X && p.velocity.X > 0f) p.velocity.X = p.velocity.X * 0.5f;
						if (p.Center.Y < owner.Center.Y && p.velocity.Y > 0f) p.velocity.Y = p.velocity.Y * 0.5f;
					}
					Vector2 coord = new Vector2(ai[0], ai[1]);
					Vector2 coordDist = coord - p.Center;
					p.velocity.Length();
					float coordLength = coordDist.Length();
					if (coordLength > topSpeedY)
					{
						coordDist.Normalize();
						float scaleFactor = (coordLength > pTopSpeed * 2f) ? pTopSpeed : (coordLength / 2f);
						coordDist *= scaleFactor;
						p.velocity = (p.velocity * (topSpeedX - 1f) + coordDist) / topSpeedX;
					}else if (yoyoFound)
					{
						if ((double)p.velocity.Length() < (double)pTopSpeed * 0.6)
						{
							coordDist = p.velocity;
							coordDist.Normalize();
							coordDist *= pTopSpeed * 0.6f;
							p.velocity = (p.velocity * (topSpeedX - 1f) + coordDist) / topSpeedX;
						}
					}else
					{
						p.velocity *= 0.8f;
					}
					if (yoyoFound && !yoyoTooFar && (double)p.velocity.Length() < (double)pTopSpeed * 0.6)
					{
						p.velocity.Normalize();
						p.velocity *= pTopSpeed * 0.6f;
					}
				}
			}else
			{
				topSpeedX = (float)((int)((double)topSpeedX * 0.8));
				pTopSpeed *= 1.5f;
				p.tileCollide = false;
				Vector2 posDist = owner.position - p.Center;
				float posLength = posDist.Length();
				if (posLength < pTopSpeed + 10f || posLength == 0f)
				{
					p.Kill();
				}else
				{
					posDist.Normalize();
					posDist *= pTopSpeed;
					p.velocity = (p.velocity * (topSpeedX - 1f) + posDist) / topSpeedX;
				}
			}
			p.rotation += rotAmount;
		}
		 
		/*
		public static void AICounterweight(Projectile p, ref float[] ai)
		{
			p.timeLeft = 6;
			bool flag = true;
			float num = 250f;
			float scaleFactor = 0.1f;
			float num2 = 15f;
			float num3 = 12f;
			num *= 0.5f;
			num2 *= 0.8f;
			num3 *= 1.5f;
			if (p.owner == Main.myPlayer)
			{
				bool flag2 = false;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == p.owner && Main.projectile[i].aiStyle == 99 && (Main.projectile[i].type < 556 || Main.projectile[i].type > 561))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					p.ai[0] = -1f;
					p.netUpdate = true;
				}
			}
			if (Main.player[p.owner].yoyoString)
			{
				num += num * 0.25f + 10f;
			}
			p.rotation += 0.5f;
			if (Main.player[p.owner].dead)
			{
				p.Kill();
				return;
			}
			if (!flag)
			{
				Main.player[p.owner].heldProj = p.whoAmI;
				Main.player[p.owner].itemAnimation = 2;
				Main.player[p.owner].itemTime = 2;
				if (p.position.X + (float)(p.width / 2) > Main.player[p.owner].position.X + (float)(Main.player[p.owner].width / 2))
				{
					Main.player[p.owner].ChangeDir(1);
					p.direction = 1;
				}
				else
				{
					Main.player[p.owner].ChangeDir(-1);
					p.direction = -1;
				}
			}
			if (p.ai[0] == 0f || p.ai[0] == 1f)
			{
				if (p.ai[0] == 1f)
				{
					num *= 0.75f;
				}
				num3 *= 0.5f;
				bool flag3 = false;
				Vector2 vector = Main.player[p.owner].Center - base.Center;
				if ((double)vector.Length() > (double)num * 0.9)
				{
					flag3 = true;
				}
				if (vector.Length() > num)
				{
					float num4 = vector.Length() - num;
					Vector2 value;
					value.X = vector.Y;
					value.Y = vector.X;
					vector.Normalize();
					vector *= num;
					p.position = Main.player[p.owner].Center - vector;
					p.position.X = p.position.X - (float)(p.width / 2);
					p.position.Y = p.position.Y - (float)(p.height / 2);
					float num5 = p.velocity.Length();
					p.velocity.Normalize();
					if (num4 > num5 - 1f)
					{
						num4 = num5 - 1f;
					}
					p.velocity *= num5 - num4;
					num5 = p.velocity.Length();
					Vector2 vector2 = new Vector2(base.Center.X, base.Center.Y);
					Vector2 vector3 = new Vector2(Main.player[p.owner].Center.X, Main.player[p.owner].Center.Y);
					if (vector2.Y < vector3.Y)
					{
						value.Y = Math.Abs(value.Y);
					}
					else if (vector2.Y > vector3.Y)
					{
						value.Y = -Math.Abs(value.Y);
					}
					if (vector2.X < vector3.X)
					{
						value.X = Math.Abs(value.X);
					}
					else if (vector2.X > vector3.X)
					{
						value.X = -Math.Abs(value.X);
					}
					value.Normalize();
					value *= p.velocity.Length();
					new Vector2(value.X, value.Y);
					if (Math.Abs(p.velocity.X) > Math.Abs(p.velocity.Y))
					{
						Vector2 vector4 = p.velocity;
						vector4.Y += value.Y;
						vector4.Normalize();
						vector4 *= p.velocity.Length();
						if ((double)Math.Abs(value.X) < 0.1 || (double)Math.Abs(value.Y) < 0.1)
						{
							p.velocity = vector4;
						}
						else
						{
							p.velocity = (vector4 + p.velocity * 2f) / 3f;
						}
					}
					else
					{
						Vector2 vector5 = p.velocity;
						vector5.X += value.X;
						vector5.Normalize();
						vector5 *= p.velocity.Length();
						if ((double)Math.Abs(value.X) < 0.2 || (double)Math.Abs(value.Y) < 0.2)
						{
							p.velocity = vector5;
						}
						else
						{
							p.velocity = (vector5 + p.velocity * 2f) / 3f;
						}
					}
				}
				if (Main.myPlayer == p.owner)
				{
					if (Main.player[p.owner].channel)
					{
						Vector2 value2 = new Vector2((float)(Main.mouseX - Main.lastMouseX), (float)(Main.mouseY - Main.lastMouseY));
						if (p.velocity.X != 0f || p.velocity.Y != 0f)
						{
							if (flag)
							{
								value2 *= -1f;
							}
							if (flag3)
							{
								if (base.Center.X < Main.player[p.owner].Center.X && value2.X < 0f)
								{
									value2.X = 0f;
								}
								if (base.Center.X > Main.player[p.owner].Center.X && value2.X > 0f)
								{
									value2.X = 0f;
								}
								if (base.Center.Y < Main.player[p.owner].Center.Y && value2.Y < 0f)
								{
									value2.Y = 0f;
								}
								if (base.Center.Y > Main.player[p.owner].Center.Y && value2.Y > 0f)
								{
									value2.Y = 0f;
								}
							}
							p.velocity += value2 * scaleFactor;
							p.netUpdate = true;
						}
					}
					else
					{
						p.ai[0] = 10f;
						p.netUpdate = true;
					}
				}
				if (flag || p.type == 562 || p.type == 547 || p.type == 555 || p.type == 564 || p.type == 552 || p.type == 563 || p.type == 549 || p.type == 550 || p.type == 554 || p.type == 553 || p.type == 603)
				{
					float num6 = 800f;
					Vector2 vector6 = default(Vector2);
					bool flag4 = false;
					if (p.type == 549)
					{
						num6 = 200f;
					}
					if (p.type == 554)
					{
						num6 = 400f;
					}
					if (p.type == 553)
					{
						num6 = 250f;
					}
					if (p.type == 603)
					{
						num6 = 320f;
					}
					for (int j = 0; j < 200; j++)
					{
						if (Main.npc[j].CanBeChasedBy(p, false))
						{
							float num7 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
							float num8 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
							float num9 = Math.Abs(p.position.X + (float)(p.width / 2) - num7) + Math.Abs(p.position.Y + (float)(p.height / 2) - num8);
							if (num9 < num6 && (p.type != 563 || num9 >= 200f) && Collision.CanHit(p.position, p.width, p.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height) && (double)(Main.npc[j].Center - Main.player[p.owner].Center).Length() < (double)num * 0.9)
							{
								num6 = num9;
								vector6.X = num7;
								vector6.Y = num8;
								flag4 = true;
							}
						}
					}
					if (flag4)
					{
						vector6 -= base.Center;
						vector6.Normalize();
						if (p.type == 563)
						{
							vector6 *= 4f;
							p.velocity = (p.velocity * 14f + vector6) / 15f;
						}
						else if (p.type == 553)
						{
							vector6 *= 5f;
							p.velocity = (p.velocity * 12f + vector6) / 13f;
						}
						else if (p.type == 603)
						{
							vector6 *= 16f;
							p.velocity = (p.velocity * 9f + vector6) / 10f;
						}
						else if (p.type == 554)
						{
							vector6 *= 8f;
							p.velocity = (p.velocity * 6f + vector6) / 7f;
						}
						else
						{
							vector6 *= 6f;
							p.velocity = (p.velocity * 7f + vector6) / 8f;
						}
					}
				}
				if (p.velocity.Length() > num2)
				{
					p.velocity.Normalize();
					p.velocity *= num2;
				}
				if (p.velocity.Length() < num3)
				{
					p.velocity.Normalize();
					p.velocity *= num3;
					return;
				}
			}
			else
			{
				p.tileCollide = false;
				Vector2 vector7 = Main.player[p.owner].Center - base.Center;
				if (vector7.Length() < 40f || vector7.HasNaNs())
				{
					p.Kill();
					return;
				}
				float num10 = num2 * 1.5f;
				if (p.type == 546)
				{
					num10 *= 1.5f;
				}
				if (p.type == 554)
				{
					num10 *= 1.25f;
				}
				if (p.type == 555)
				{
					num10 *= 1.35f;
				}
				if (p.type == 562)
				{
					num10 *= 1.25f;
				}
				float num11 = 12f;
				vector7.Normalize();
				vector7 *= num10;
				p.velocity = (p.velocity * (num11 - 1f) + vector7) / num11;
			}	
		}
	
		*/

		public static void TileCollideYoyo(Projectile p, ref Vector2 velocity, Vector2 newVelocity)
		{
			bool normalizeVelocity = false;
			if (velocity.X != newVelocity.X)
			{
				normalizeVelocity = true;
				velocity.X = newVelocity.X * -1f;
			}
			if (velocity.Y != newVelocity.Y)
			{
				normalizeVelocity = true;
				velocity.Y = newVelocity.Y * -1f;
			}
			if (normalizeVelocity)
			{
				Vector2 centerDist = Main.player[p.owner].Center - p.Center;
				centerDist.Normalize();
				centerDist *= velocity.Length();
				centerDist *= 0.25f;
				velocity *= 0.75f;
				velocity += centerDist;
				if (velocity.Length() > 6f)
				{
					velocity *= 0.5f;
				}
			}	
		}

		public static void EntityCollideYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, Entity target, bool spawnCounterweight = true, float velMult = 1f)
		{
			if(owner is Player && spawnCounterweight){ ((Player)owner).Counterweight(target.Center, p.damage, p.knockBack); }
			if (target.Center.X < owner.Center.X){ p.direction = -1; }else{ p.direction = 1; }
			if (ai[0] >= 0f)
			{
				Vector2 value2 = p.Center - target.Center;
				value2.Normalize();
				float scaleFactor = 16f;
				p.velocity *= -0.5f;
				p.velocity += value2 * scaleFactor;
				p.velocity *= velMult;
				p.netUpdate = true;
				localAI[0] += 20f;
				if (!Collision.CanHit(p.position, p.width, p.height, owner.position, owner.width, owner.height))
				{
					localAI[0] += 40f;
					//num8 = (int)((double)num8 * 0.75);
				}
			}		
		}

		/*
		 * A cleaned up (and edited) copy of Star AI. (Starfury stars, etc.) (AIStyle 5)
		 * 
		 * landingHorizon: The Y value at which the star should begin tile colliding. (-1 == always tile collide)
		 * fadein: If true, projectile fades in when first spawned.
		 * delayedCollide: If true, star does not collide with tiles until it's past the horizon of it's target
		 */
		public static void AIStar(Projectile p, ref float[] ai, float landingHorizon = -1, bool fadein = true)
		{
			if (landingHorizon != -1)
			{
				if (p.position.Y > landingHorizon) p.tileCollide = true;
			}else
			{
				if (ai[0] == 0f && !Collision.SolidCollision(p.position, p.width, p.height))
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}
				if (ai[0] != 0f) p.tileCollide = true;
			}
			if(fadein) p.alpha = (int)Math.Max(0, p.alpha - 25);
		}

		/*
		 * A cleaned up (and edited) copy of Explosives AI. (Grenades, Bombs, etc.)
		 * 
		 * rocket : changes behavior to act like rockets.
		 * rotate : wether to rotate based on velocity or not.
		 * beginGravity : what tick to begin applying gravity. (not applied if rocket == true)
		 * slowdownX : How fast to slow down per tick. (not applied if rocket == true)
		 * gravity: The gravity amount. (not applied if rocket == true)
		 */
		public static void AIExplosive(Projectile p, ref float[] ai, bool rocket = false, bool rotate = true, int beginGravity = 10, float slowdownX = 0.97f, float gravity = 0.2f)
		{
			if (rocket)
			{
				if (Math.Abs(p.velocity.X) < 15f && Math.Abs(p.velocity.Y) < 15f){ p.velocity *= 1.1f; }
			}
			ai[0] += 1f;
			if (rocket)
			{
				if (p.velocity.X < 0f)
				{
					p.spriteDirection = -1;
					p.rotation = (float)Math.Atan2((double)(-(double)p.velocity.Y), (double)(-(double)p.velocity.X)) - 1.57f;
				}else
				{
					p.spriteDirection = 1;
					p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
				}
			}else
			if (ai[0] > beginGravity)
			{
				ai[0] = beginGravity;
				if (p.velocity.Y == 0f && p.velocity.X != 0f)
				{
					p.velocity.X = p.velocity.X * slowdownX;
					if (p.velocity.X > -0.01f && p.velocity.X < 0.01f)
					{
						p.velocity.X = 0f;
						p.netUpdate = true;
					}
				}
				p.velocity.Y = p.velocity.Y + gravity;
			}
			if (rotate){ p.rotation += p.velocity.X * 0.1f; }
		}

		/*
		 * A cleaned up (and edited) copy of tile collison for Explosives.
		 * bomb: Set to true if you want bomblike collision.
		 */
		public static void TileCollideExplosive(Projectile p, ref Vector2 velocity, bool bomb = false)
		{
			if (p.velocity.X != velocity.X){ p.velocity.X = velocity.X * -0.4f; }
			if (p.velocity.Y != velocity.Y && velocity.Y > 0.7f && !bomb){ p.velocity.Y = velocity.Y * -0.4f; }
		}

		/*
		 * A cleaned up (and edited) copy of Arrow AI. (Arrows, etc.) (AIStyle 1) (Can be used with NPCs or Projectiles)
		 * 
		 * gravApplyInterval: The rate at which to apply gravity. Higher values == less gravity, greater values == more gravity.
		 * gravity: the amount to induce gravity upon the codable.
		 * maxSpeedY: The maximum speed the projectile can be doing down.
		 */
		public static void AIArrow(Entity codable, ref float[] ai, int gravApplyInterval = 50, float gravity = 0.1f, float maxSpeedY = 16f)
		{
			ai[0]++;
			if (ai[0] >= gravApplyInterval){ codable.velocity.Y += gravity; }
			if (codable.velocity.Y > maxSpeedY){ codable.velocity.Y = maxSpeedY; }
		}

		/*
		 * A cleaned up (and edited) copy of Demon Scythe AI. (Demon Scythe, etc.) (AIStyle 18) (Can be used with NPCs or Projectiles)
		 * 
		 * startSpeedupInterval : The value to begin velocity speedup.
		 * stopSpeedupInterval : The value to stop velocity speedup.
		 * rotateScalar : The scalar for rotational increase.
		 * speedupScalar : The scalar for the speedup interval.
		 * maxSpeed : The speed to cap the projectile/npc at.
		 */
		public static void AIDemonScythe(Entity codable, ref float[] ai, int startSpeedupInterval = 30, int stopSpeedupInterval = 100, float rotateScalar = 0.8f, float speedupScalar = 1.06f, float maxSpeed = 8f)
		{
			if (codable is Projectile) { ((Projectile)codable).rotation += (float)codable.direction * rotateScalar; }
			if (codable is NPC) { ((NPC)codable).rotation += (float)codable.direction * rotateScalar; } 		
			ai[0] += 1f;
			if (ai[0] >= startSpeedupInterval)
			{
				if (ai[0] < stopSpeedupInterval) { codable.velocity *= speedupScalar; } else { ai[0] = stopSpeedupInterval; }
			}
			if (((Math.Abs(codable.velocity.X) + Math.Abs(codable.velocity.Y)) * 0.5f) > maxSpeed)
			{
				codable.velocity.Normalize(); codable.velocity *= maxSpeed;
			}
		}

        /*
         * A cleaned up (and edited) copy of Vilethorn AI. (Vilethorn, etc.)
         * 
         * alphaInterval : The amount of alpha to add each tick. (higher values == faster spawning)
         * alphaReduction : The amount of alpha to reduce after spawning the next piece. (higher values == faster despawning)
         * length : How many segments to spawn.
         */
        public static void AIVilethorn(Projectile p, int alphaInterval = 50, int alphaReduction = 4, int length = 8)
        {
            if (p.ai[0] == 0f)
            {
                p.rotation = (float)System.Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
                p.alpha -= alphaInterval;
                if (p.alpha <= 0)
                {
                    p.alpha = 0;
                    p.ai[0] = 1f;
                    if (p.ai[1] == 0f){ p.ai[1] += 1f; p.position += p.velocity; }
                    if (p.ai[1] < length && Main.myPlayer == p.owner)
                    {
                        Vector2 rotVec = p.velocity;
                        int id = Projectile.NewProjectile(p.Center.X + p.velocity.X, p.Center.Y + p.velocity.Y, rotVec.X, rotVec.Y, p.type, p.damage, p.knockBack, p.owner);
                        Main.projectile[id].damage = p.damage;
                        Main.projectile[id].ai[1] = p.ai[1] + 1f;
                        NetMessage.SendData(27, -1, -1, NetworkText.FromLiteral(""), id, 0f, 0f, 0f, 0);
						p.position -= p.velocity;
                        return;
                    }
                }
            }else
            {
                p.alpha += alphaReduction;
                if (p.alpha >= 255){ p.Kill(); return; }
            }
			p.position -= p.velocity;
        }

        /*
         * A cleaned up (and edited) copy of Stream AI. (Aqua Scepter, Golden Shower, etc.)
         * 
         * scaleReduce: the amount to reduce the scale of the codable each tick.
         * gravity : the amount of gravity to apply each tick.
		 * goldenShower : If true, acts more like golden shower then aqua specter.
		 * start : The value at which the AI begins running (creates a delay).
		 * SpawnDust: If not null, controlls the dust spawning.
         */
        public static void AIStream(Projectile p, float scaleReduce = 0.04f, float gravity = 0.075f, bool goldenShower = false, int start = 3, Func<Projectile, Vector2, int, int, int> SpawnDust = null)
        {
			if (goldenShower)
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f){ p.Kill(); }
				if (p.ai[0] <= start){ p.ai[0] += 1f; return; }
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null)
				{
					for (int m = 0; m < 3; m++)
					{
						float dustX = p.velocity.X / 3f * (float)m;
						float dustY = p.velocity.Y / 3f * (float)m;
						int offset = 1;
						Vector2 pos = new Vector2(p.position.X - (float)offset, p.position.Y - (float)offset);
						int width = p.width + offset * 2; int height = p.height + offset * 2;
						int dustID = SpawnDust(p, pos, width, height);
						if (dustID != -1)
						{
							Main.dust[dustID].noGravity = true;
							Main.dust[dustID].velocity *= 0.1f;
							Main.dust[dustID].velocity += p.velocity * 0.5f;
							Main.dust[dustID].position.X -= dustX;
							Main.dust[dustID].position.Y -= dustY;
						}
					}
					if (Main.rand.Next(8) == 0)
					{
						int offset = 1;
						Vector2 pos = new Vector2(p.position.X - (float)offset, p.position.Y - (float)offset);
						int width = p.width + offset * 2; int height = p.height + offset * 2;
						int dustID = SpawnDust(p, pos, width, height);
						if (dustID != -1)
						{
							Main.dust[dustID].velocity *= 0.25f;
							Main.dust[dustID].velocity += p.velocity * 0.5f;
						}
					}
				}
			}else
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f) { p.Kill(); }
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null) SpawnDust(p, p.position, p.width, p.height);
			}
        }

        /*
         * A cleaned up (and edited) copy of Thrown Weapon AI. (throwing knife, shuriken, etc.)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * spin : wether to continously spin with velocity or point in the direction of velocity until slowdown.
         * timeUntilDrop : How many ticks to move until slowing down.
         * xScalar : the scalar to slow down on the X axis.
         * yIncrement : the amount to speed up by on the Y axis.
         * maxSpeedY : the max speed of the projectile on the Y axis.
         */
        public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
        {
            p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * (float)p.direction;
            ai[0] += 1f;
            if (ai[0] >= timeUntilDrop)
            {
                p.velocity.Y = p.velocity.Y + yIncrement;
                p.velocity.X = p.velocity.X * xScalar;
            }else
            if (!spin){ p.rotation = BaseUtility.RotationTo(p.Center, p.Center + p.velocity) + 1.57f; }
            if (p.velocity.Y > maxSpeedY){ p.velocity.Y = maxSpeedY; }
        }

        public static void AISpear(Projectile p, ref float[] ai, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false)
        {
            Player plr = Main.player[p.owner];
            Item item = plr.inventory[plr.selectedItem];
            if (Main.myPlayer == p.owner && item != null && item.autoReuse && plr.itemAnimation == 1) { p.Kill(); return; } //prevents a bug with autoReuse and spears
            Main.player[p.owner].heldProj = p.whoAmI;
            Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 gfxOffset = new Vector2(0, plr.gfxOffY);
            AISpear(p, ref ai, plr.Center + gfxOffset, plr.direction, plr.itemAnimation, plr.itemAnimationMax, initialSpeed, moveOutward, moveInward, overrideKill, plr.frozen);
        }

        /*
         * A cleaned up (and edited) copy of Spear AI.
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * center : the center of what is holding the spear.
         * ownerDirection : the direction of the owner of the spear.
         * itemAnimation : the current item animation tick of the spear.
         * itemAnimationMax : the max length of the item animation of the spear.
         * initialSpeed : how fast velocity initially is.
         * moveOutward : how fast to move outward.
         * moveInward : how fast to move inward.
         * overrideKill : If true, prevents the spear's death.
		 * frozen : If the holder of the spear is frozen or not (used to freeze the animation)
         */
        public static void AISpear(Projectile p, ref float[] ai, Vector2 center, int ownerDirection, int itemAnimation, int itemAnimationMax, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false, bool frozen = false)
        {
            p.direction = ownerDirection;
            p.position.X = center.X - (float)(p.width * 0.5f);
            p.position.Y = center.Y - (float)(p.height * 0.5f);
            if (ai[0] == 0f){ ai[0] = initialSpeed; p.netUpdate = true; }
			if (!frozen)
			{
				if (itemAnimation < itemAnimationMax * 0.33f) { ai[0] -= moveInward; } else { ai[0] += moveOutward; }
			}
			p.position += p.velocity * ai[0];
            if (!overrideKill && Main.player[p.owner].itemAnimation == 0){ p.Kill(); }
            p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 2.355f;
			if (p.direction == -1) { p.rotation -= 0f; }else
			if (p.direction == 1) { p.rotation -= 1.57f; }
        }

        /*
         * A cleaned up (and edited) copy of Boomerang AI.
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * center : the center of where the boomerang should return to.
         * playSound : If true, plays the air sound boomerangs make while in the air.
         * maxDistance : the maximum 'distance' for the projectile to go before it rebounds.
         * returnDelay : the amount of time in ticks until the projectile returns to it's source.
         * speedInterval : the amount to move the projectile by each tick.
         * rotationInterval : the amount for the projectile to rotate by each tick.
         * direct : If true, when returning simply reverses the boomerang velocity.
         */
        public static void AIBoomerang(Projectile p, ref float[] ai, Vector2 position = default(Vector2), int width = -1, int height = -1, bool playSound = true, float maxDistance = 9f, int returnDelay = 35, float speedInterval = 0.4f, float rotationInterval = 0.4f, bool direct = false)
        {
            if (position == default(Vector2)) { position = Main.player[p.owner].position; }
            if (width == -1) { width = Main.player[p.owner].width; }
            if (height == -1) { height = Main.player[p.owner].height; }
            Vector2 center = position + new Vector2(width * 0.5f, height * 0.5f);
            if (playSound && p.soundDelay == 0)
            {
                p.soundDelay = 8;
                Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 7);
            }
            if (ai[0] == 0f)
            {
                ai[1] += 1f;
                if (ai[1] >= returnDelay)
                {
                    ai[0] = 1f;
                    ai[1] = 0f;
                    p.netUpdate = true;
                }
            }else
            {
                p.tileCollide = false;
                float distPlayerX = center.X - p.Center.X;
                float distPlayerY = center.Y - p.Center.Y;
                float distPlayer = (float)Math.Sqrt((double)(distPlayerX * distPlayerX + distPlayerY * distPlayerY));
                if (distPlayer > 3000f)
                {
                    p.Kill();
                }
                if (direct)
                {
                    p.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(speedInterval, 0f), BaseUtility.RotationTo(p.Center, center));
                }else
                {
                    distPlayer = maxDistance / distPlayer;
                    distPlayerX *= distPlayer;
                    distPlayerY *= distPlayer;
                    if (p.velocity.X < distPlayerX)
                    {
                        p.velocity.X += speedInterval;
                        if (p.velocity.X < 0f && distPlayerX > 0f) { p.velocity.X += speedInterval; }
                    }else
                    if (p.velocity.X > distPlayerX)
                    {
                        p.velocity.X -= speedInterval;
                        if (p.velocity.X > 0f && distPlayerX < 0f) { p.velocity.X -= speedInterval; }
                    }
                    if (p.velocity.Y < distPlayerY)
                    {
                        p.velocity.Y += speedInterval;
                        if (p.velocity.Y < 0f && distPlayerY > 0f) { p.velocity.Y += speedInterval; }
                    }else
                    if (p.velocity.Y > distPlayerY)
                    {
                        p.velocity.Y -= speedInterval;
                        if (p.velocity.Y > 0f && distPlayerY < 0f) { p.velocity.Y -= speedInterval; }
                    }
                }
                if (Main.myPlayer == p.owner)
                {
                    Rectangle rectangle = p.Hitbox;
                    Rectangle value = new Rectangle((int)position.X, (int)position.Y, width, height);
                    if (rectangle.Intersects(value)) { p.Kill(); }
                }
            }
            p.rotation += rotationInterval * (float)p.direction;
        }

        /*
         * A cleaned up (and edited) copy of tile collison for Boomerangs.
         * bounce : Set to true if your projectile acts like Light Discs or the Thorn Chakram.
         */
        public static void TileCollideBoomerang(Projectile p, ref Vector2 velocity, bool bounce = false)
        {
            if (bounce)
            {
                if (p.velocity.X != velocity.X) { p.velocity.X = -velocity.X; }
                if (p.velocity.Y != velocity.Y) { p.velocity.Y = -velocity.Y; }
            }else
            {
                p.ai[0] = 1f;
                p.velocity.X = -velocity.X;
                p.velocity.Y = -velocity.Y;
            }
            p.netUpdate = true;
        }

        public static void AIFlail(Projectile p, ref float[] ai, bool noKill = false, float chainDistance = 160f)
        {
            if (Main.player[p.owner] != null)
            {
                if (Main.player[p.owner].dead) { p.Kill(); return; }
                Main.player[p.owner].itemAnimation = 10;
                Main.player[p.owner].itemTime = 10;
            }
            AIFlail(p, ref ai, Main.player[p.owner].Center, Main.player[p.owner].velocity, Main.player[p.owner].meleeSpeed, Main.player[p.owner].channel, noKill, chainDistance);
            Main.player[p.owner].direction = p.direction;
        }

        /*
         * A cleaned up (and edited) copy of Flail AI.
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * connectedPoint : The point for the flail to be 'attached' to, and rebound to, etc.
         * connectedPointVelocity : The velocity of the connected point, if it is moving.
         * meleeSpeed : the meleeSpeed of whatever is using the flail.
         * channel : Wether or not the source is 'channeling' (holding down the fire button) projectile flail.
         * noKill : If true, do not kill the projectile when it returns to the connected point.
         * chainDistance : How far for the flail to actually go.
         */
        public static void AIFlail(Projectile p, ref float[] ai, Vector2 connectedPoint, Vector2 connectedPointVelocity, float meleeSpeed, bool channel, bool noKill = false, float chainDistance = 160f)
        {
            p.direction = (p.Center.X > connectedPoint.X ? 1 : -1);
            float pointX = connectedPoint.X - p.Center.X;
            float pointY = connectedPoint.Y - p.Center.Y;
            float pointDist = (float)Math.Sqrt((double)(pointX * pointX + pointY * pointY));
            if (ai[0] == 0f)
            {
                p.tileCollide = true;
                if (pointDist > chainDistance)
                {
                    ai[0] = 1f;
                    p.netUpdate = true;
                }else
                {
                    if (!channel)
                    {
                        if (p.velocity.Y < 0f) { p.velocity.Y *= 0.9f; }
                        p.velocity.Y += 1f;
                        p.velocity.X *= 0.9f;
                    }
                }
            }else
            if (ai[0] == 1f)
            {
                float meleeSpeed1 = 14f / meleeSpeed;
                float meleeSpeed2 = 0.9f / meleeSpeed;
                float maxBallDistance = chainDistance + 140f;
                Math.Abs(pointX);
                Math.Abs(pointY);
                if (ai[1] == 1f) { p.tileCollide = false; }
                if (!channel || pointDist > maxBallDistance || !p.tileCollide)
                {
                    ai[1] = 1f;
                    if (p.tileCollide) { p.netUpdate = true; }
                    p.tileCollide = false;
                    if (!noKill && pointDist < 20f)
                    {
                        p.Kill();
                    }
                }
                if (!p.tileCollide) { meleeSpeed2 *= 2f; }
                if (pointDist > 60f || !p.tileCollide)
                {
                    pointDist = meleeSpeed1 / pointDist;
                    pointX *= pointDist;
                    pointY *= pointDist;
                    new Vector2(p.velocity.X, p.velocity.Y);
                    float pointX2 = pointX - p.velocity.X;
                    float pointY2 = pointY - p.velocity.Y;
                    float pointDist2 = (float)Math.Sqrt((double)(pointX2 * pointX2 + pointY2 * pointY2));
                    pointDist2 = meleeSpeed2 / pointDist2;
                    pointX2 *= pointDist2;
                    pointY2 *= pointDist2;
                    p.velocity.X = p.velocity.X * 0.98f;
                    p.velocity.Y = p.velocity.Y * 0.98f;
                    p.velocity.X = p.velocity.X + pointX2;
                    p.velocity.Y = p.velocity.Y + pointY2;
                }else
                {
                    if (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y) < 6f)
                    {
                        p.velocity.X = p.velocity.X * 0.96f;
                        p.velocity.Y = p.velocity.Y + 0.2f;
                    }
                    if (connectedPointVelocity.X == 0f) { p.velocity.X = p.velocity.X * 0.96f; }
                }
            }
            p.rotation = (float)Math.Atan2((double)pointY, (double)pointX) - p.velocity.X * 0.1f;
        }

        /*
         * A cleaned up (and edited) copy of tile collison for Flails.
         */
        public static void TileCollideFlail(Projectile p, ref Vector2 velocity, bool playSound = true)
        {
            if (velocity != p.velocity)
            {
                bool updateAndCollide = false;
                if (velocity.X != p.velocity.X)
                {
                    if (Math.Abs(velocity.X) > 4f) { updateAndCollide = true; }
                    p.position.X = p.position.X + p.velocity.X;
                    p.velocity.X = -velocity.X * 0.2f;
                }
                if (velocity.Y != p.velocity.Y)
                {
                    if (Math.Abs(velocity.Y) > 4f) { updateAndCollide = true; }
                    p.position.Y = p.position.Y + p.velocity.Y;
                    p.velocity.Y = -velocity.Y * 0.2f;
                }
                p.ai[0] = 1f;
                if (updateAndCollide)
                {
                    p.netUpdate = true;
                    Collision.HitTiles(p.position, p.velocity, p.width, p.height);
					if (playSound) { Main.PlaySound(0, (int)p.position.X, (int)p.position.Y, 1); }
                }
            }
        }

        #endregion

		#region Vanilla Projectile AI Code Excerpts

		/*
		 * (Edited) Sticky code used by projectiles to stick to tiles. Returns true if it is 'sticking' to a tile.
		 * 
		 * beginGravity : the time (in ticks) when to begin applying gravity
		 * 
		 * 
		 */
		public static bool StickToTiles(Vector2 position, ref Vector2 velocity, int width, int height, Func<int, int, bool> CanStick = null)
		{
			int tileLeftX = (int)(position.X / 16f) - 1;
			int tileRightX = (int)((position.X + (float)width) / 16f) + 2;
			int tileLeftY = (int)(position.Y / 16f) - 1;
			int tileRightY = (int)((position.Y + (float)height) / 16f) + 2;
			if (tileLeftX < 0) { tileLeftX = 0; } if (tileRightX > Main.maxTilesX) { tileRightX = Main.maxTilesX; }
			if (tileLeftY < 0) { tileLeftY = 0; } if (tileRightY > Main.maxTilesY) { tileRightY = Main.maxTilesY; }
			bool stick = false;
			for (int x = tileLeftX; x < tileRightX; x++)
			{
				for (int y = tileLeftY; y < tileRightY; y++)
				{
					if (Main.tile[x, y] != null && Main.tile[x, y].nactive() && (CanStick != null ? CanStick(x, y) : (Main.tileSolid[(int)Main.tile[x, y].type] || (Main.tileSolidTop[(int)Main.tile[x, y].type] && Main.tile[x, y].frameY == 0))))
					{
						Vector2 pos = new Vector2((float)(x * 16), (float)(y * 16));
						if (position.X + (float)width - 4f > pos.X && position.X + 4f < pos.X + 16f && position.Y + (float)height - 4f > pos.Y && position.Y + 4f < pos.Y + 16f)
						{
							stick = true; velocity *= 0f; break;
						}
					}
				}
				if (stick) break;
			}
			return stick;
		}


		#endregion

		#region Vanilla NPC AI Copy Methods

		public static void AISpaceOctopus(NPC npc, ref float[] ai, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			npc.TargetClosest(true);		
			AISpaceOctopus(npc, ref ai, Main.player[npc.target].Center, moveSpeed, velMax, hoverDistance, shootProjInterval, FireProj);
		}

		public static void AISpaceOctopus(NPC npc, ref float[] ai, Vector2 targetCenter = default(Vector2), float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			Vector2 wantedVelocity = targetCenter - npc.Center + new Vector2(0f, -hoverDistance);
			float dist = wantedVelocity.Length();
			if (dist < 20f)
			{
				wantedVelocity = npc.velocity;
			}
			else if (dist < 40f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.35f;
			}
			else if (dist < 80f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.65f;
			}
			else
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax;
			}
			npc.SimpleFlyMovement(wantedVelocity, moveSpeed);
			npc.rotation = npc.velocity.X * 0.1f;
			if (FireProj != null && shootProjInterval > -1 && (ai[0] += 1f) >= shootProjInterval)
			{
				ai[0] = 0f;
				if (Main.netMode != 1)
				{
					Vector2 projVelocity = Vector2.Zero;
					while (Math.Abs(projVelocity.X) < 1.5f)
					{
						projVelocity = Vector2.UnitY.RotatedByRandom(1.5707963705062866) * new Vector2(5f, 3f);
					}
					FireProj(npc, projVelocity);
				}
			}	
		}

		public static void AIElemental(NPC npc, ref float[] ai, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			int timerDummy = (int)npc.localAI[0];
			AIElemental(npc, ref ai, ref timerDummy, noDamageMode, noDamageTimeMax, gravityChange, tileCollideChange, startPhaseDist, stopPhaseDist, idleTooLong, velSpeed);
			npc.localAI[0] = timerDummy;
		}
		
		/*
		 * A cleaned up (and edited) copy of Elemental AI. (aiStyle 91) (Granite Elemental, etc.)
		 * 
		 * idleTimer : A localized value, which is randomly ticked up to 5.
		 * noDamageMode : A bool?. Set to true to force on no damage mode, set to false to force it off, return null to have it only on in expert.
		 * noDamageTimeMax : The maximum amount of ticks before no damage mode returns to normal. (default 120)
		 * gravityChange : if true, npc.noGravity is changed during immortality states. If false, nothing is changed.
		 * tileCollideChange : if true, npc.noTileCollide is changed between phasing through tiles and not. If false, nothing is changed.
		 * startPhaseDist : the distance at which the npc begins phasing through tiles to get near the player. (default 800)
		 * stopPhaseDist : The distance at which the npc stops phasing through tiles to get near the player. (default 600)
		 * idleTooLong : The maximum amount of ticks the npc can be 'idle' before it attempts to change movement modes. (default 180)
		 * velSpeed : The speed of the entity when moving to the player. This value is used for all states; changing it speeds or slows the npc in all of them.
		 */
		public static void AIElemental(NPC npc, ref float[] ai, ref int idleTimer, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			bool noDmg = (noDamageMode == null ? Main.expertMode : (bool)noDamageMode);
			if(gravityChange) npc.noGravity = true;
			if(tileCollideChange) npc.noTileCollide = false;
			if(noDmg) npc.dontTakeDamage = false;
			Player targetPlayer = (npc.target < 0 ? null : Main.player[npc.target]);
			Vector2 playerCenter = (targetPlayer == null ? npc.Center + new Vector2(0, 5f) : targetPlayer.Center);
			Vector2 dist = playerCenter - npc.Center;
			
			if (npc.justHit && Main.netMode != 1 && noDmg && Main.rand.Next(6) == 0)
			{
				npc.netUpdate = true;
				ai[0] = -1f;
				ai[1] = 0f;
			}
			if (ai[0] == -1f) //immortal
			{
				if(noDmg) npc.dontTakeDamage = true;
				if(gravityChange) npc.noGravity = false;
				npc.velocity.X = npc.velocity.X * 0.98f;
				ai[1] += 1f;
				if (ai[1] >= noDamageTimeMax)
				{
					ai[0] = (ai[1] = (ai[2] = (ai[3] = 0f)));
					return;
				}
			}
			else if (ai[0] == 0f) //targeting mode (chosing how to act)
			{
				npc.TargetClosest(true);
				targetPlayer = Main.player[npc.target];
				playerCenter = targetPlayer.Center;
				dist = playerCenter - npc.Center;
				if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 1f;
					return;
				}
				Vector2 centerDiff = playerCenter - npc.Center;
				centerDiff.Y -= (float)(targetPlayer.height / 4);
				float centerDist = centerDiff.Length();
				if (centerDist > startPhaseDist)
				{
					ai[0] = 2f;
					return;
				}
				Vector2 npcCenter = npc.Center;
				npcCenter.X = playerCenter.X;
				Vector2 npcCentDiff = npcCenter - npc.Center;
				if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
				{
					ai[0] = 3f;
					ai[1] = npcCenter.X;
					ai[2] = npcCenter.Y;
					Vector2 npcCenter2 = npc.Center;
					npcCenter2.Y = playerCenter.Y;
					if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter2, 1, 1) && Collision.CanHit(npcCenter2, 1, 1, targetPlayer.position, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter2.X;
						ai[2] = npcCenter2.Y;
					}
				}else
				{
					npcCenter = npc.Center;
					npcCenter.Y = playerCenter.Y;
					if ((npcCenter - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter.X;
						ai[2] = npcCenter.Y;
					}
				}
				if (ai[0] == 0f)
				{
					npc.localAI[0] = 0f;
					centerDiff.Normalize();
					centerDiff *= 0.5f;
					npc.velocity += centerDiff;
					ai[0] = 4f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 1f) //move to player
			{
				Vector2 distDiff = playerCenter - npc.Center;
				float distLength = distDiff.Length();
				float velSpeed2 = velSpeed; velSpeed2 += distLength / 200f;
				float speedAdjuster = 50f;
				distDiff.Normalize();
				distDiff *= velSpeed2;
				npc.velocity = (npc.velocity * (float)(speedAdjuster - 1) + distDiff) / speedAdjuster;
				if (!Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 2f) //phase slowly through tiles to player
			{
				npc.noTileCollide = true;
				Vector2 distDiff = playerCenter - npc.Center;
				float distLength = distDiff.Length();
				float velSpeedPhase = velSpeed;
				float speedAdjusterPhase = 4f;
				distDiff.Normalize();
				distDiff *= velSpeedPhase;
				npc.velocity = (npc.velocity * (float)(speedAdjusterPhase - 1) + distDiff) / speedAdjusterPhase;
				if (distLength < stopPhaseDist && !Collision.SolidCollision(npc.position, npc.width, npc.height))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 3f) // horizontal floating to player
			{
				Vector2 targetLoc = new Vector2(ai[1], ai[2]);
				Vector2 targetDiff = targetLoc - npc.Center;
				float targetLength = targetDiff.Length();
				float velSpeedHorizontal = (velSpeed < 1f ? velSpeed * 0.5f : Math.Max(0.1f, velSpeed - 1f));
				float speedAdjusterHorizontal = 3f;
				targetDiff.Normalize();
				targetDiff *= velSpeedHorizontal;
				npc.velocity = (npc.velocity * (speedAdjusterHorizontal - 1f) + targetDiff) / speedAdjusterHorizontal;
				if (npc.collideX || npc.collideY)
				{
					ai[0] = 4f;
					ai[1] = 0f;
				}
				if (targetLength < velSpeedHorizontal || targetLength > startPhaseDist || Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 4f) //idle floating
			{
				if (npc.collideX) npc.velocity.X = npc.velocity.X * -0.8f;
				if (npc.collideY) npc.velocity.Y = npc.velocity.Y * -0.8f;
				Vector2 velVec;
				if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
				{
					velVec = playerCenter - npc.Center;
					velVec.Y -= (float)(targetPlayer.height / 4);
					velVec.Normalize();
					npc.velocity = velVec * 0.1f;
				}
				float velSpeedIdle = (velSpeed < 1f ? velSpeed * 0.75f : Math.Max(0.1f, velSpeed - 0.5f));
				float speedAdjusterIdle = 20f;
				velVec = npc.velocity;
				velVec.Normalize();
				velVec *= velSpeedIdle;
				npc.velocity = (npc.velocity * (speedAdjusterIdle - 1f) + velVec) / speedAdjusterIdle;
				ai[1] += 1f;
				if (ai[1] > idleTooLong)
				{
					ai[0] = 0f;
					ai[1] = 0f;
				}
				if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
				}
				idleTimer += 1;
				if (idleTimer >= 5 && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
				{
					idleTimer = 0;
					Vector2 npcCenter = npc.Center;
					npcCenter.X = playerCenter.X;
					if (Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter.X;
						ai[2] = npcCenter.Y;
						return;
					}
					npcCenter = npc.Center;
					npcCenter.Y = playerCenter.Y;
					if (Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter.X;
						ai[2] = npcCenter.Y;
						return;
					}
				}
			}		
		}
		
		
		public static void AIWeapon(NPC npc, ref float[] ai, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) npc.TargetClosest(true);
			AIWeapon(npc, ref ai, ref npc.rotation, Main.player[npc.target].Center, npc.justHit, rotTime, moveTime, maxSpeed, movementScalar, rotScalar);
		}

		/*
		 * A cleaned up (and edited) copy of Possessed Weapon AI. (aiStyle 23) (Enchanted Sword, Demon Hammer, etc.)
		 * 
		 * targetPos : The center of the target of projectile codable.
		 * justHit : Set true to reset the AI (ie return it to a rotating state), false otherwise
		 * rotTime : The time (in ticks) for the codable to rotate.
		 * moveTime : The time (in ticks) for the codable to move.
		 * movementScalar : A scalar for how much to move per tick.
		 * 
		 */
		public static void AIWeapon(Entity codable, ref float[] ai, ref float rotation, Vector2 targetPos, bool justHit = false, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (ai[0] == 0f)
			{
				Vector2 vector2 = codable.Center;
				float distX = targetPos.X - vector2.X;
				float distY = targetPos.Y - vector2.Y;
				float dist = (float)Math.Sqrt(distX * distX + distY * distY);
				float distMult = 9f / dist;
				codable.velocity.X = distX * distMult * movementScalar;
				codable.velocity.Y = distY * distMult * movementScalar;
				if (codable.velocity.X > maxSpeed) { codable.velocity.X = maxSpeed; } if (codable.velocity.X < -maxSpeed) { codable.velocity.X = -maxSpeed; }
				if (codable.velocity.Y > maxSpeed) { codable.velocity.Y = maxSpeed; } if (codable.velocity.Y < -maxSpeed) { codable.velocity.Y = -maxSpeed; }
				rotation = (float)Math.Atan2(codable.velocity.Y, codable.velocity.X);
				ai[0] = 1f;
				ai[1] = 0.0f;
			}else if (ai[0] == 1f)
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0.0f;
				}
				codable.velocity *= 0.99f;
				++ai[1];
				if (ai[1] < moveTime) return;
				ai[0] = 2f;
				ai[1] = 0.0f;
				codable.velocity.X = 0.0f;
				codable.velocity.Y = 0.0f;
			}else
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0.0f;
				}
				codable.velocity *= 0.96f;
				++ai[1];
				rotation += ((float)(0.1 + (double)(ai[1] / (float)rotTime) * 0.4f) * (float)codable.direction) * rotScalar;
				if (ai[1] < rotTime) return;
				if (codable is NPC) { ((NPC)codable).netUpdate = true; } else if (codable is Projectile) { ((Projectile)codable).netUpdate = true; }
				ai[0] = 0.0f;
				ai[1] = 0.0f;
			}
		}


		/*
         * A cleaned up (and edited) copy of Snail AI. (Snail, etc.) (AIStyle 67)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * snailStatus: An int value which is set to the 'gravity' status of the AI. (0 == down (ground), 1 == left wall, 2 == right wall, 3 == up (ceiling))
		 * moveInterval : The amount to move by per tick.
		 * rotAmt : The amount to rotate by when reaching corners.
         */
		public static void AISnail(NPC npc, ref float[] ai, ref int snailStatus, float moveInterval = 0.3f, float rotAmt = 0.1f)
		{
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true); npc.directionY = 1; ai[0] = 1f;
				if (npc.direction > 0){ npc.spriteDirection = 1; }
			}
			bool collisonOnX = false;
			if (Main.netMode != 1)
			{
				if (ai[2] == 0f && Main.rand.Next(7200) == 0){ ai[2] = 2f; npc.netUpdate = true; }
				if (!npc.collideX && !npc.collideY)
				{
					npc.localAI[3] += 1f;
					if (npc.localAI[3] > 5f){ ai[2] = 2f; npc.netUpdate = true; }
				}else{ npc.localAI[3] = 0f; }
			}
			if (ai[2] > 0f)
			{
				ai[1] = 0f; ai[0] = 1f; npc.directionY = 1;
				if (npc.velocity.Y > moveInterval){ npc.rotation += (float)npc.direction * 0.1f; }else{ npc.rotation = 0f; }
				npc.spriteDirection = npc.direction;
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.noGravity = false;
				snailStatus = 0;
				//int tileX = (int)(npc.Center.X + (float)(npc.width / 2 * -npc.direction)) / 16;
				//int tileY = (int)(npc.position.Y + (float)npc.height + 8f) / 16;
				//if (Main.tile[tileX, tileY] != null && Main.tile[tileX, tileY].slope() != 1 && npc.collideY){ ai[2] -= 1f; }
				//tileX = (int)(npc.Center.X + (float)(npc.width / 2 * npc.direction)) / 16;
				//tileY = (int)(npc.position.Y + (float)npc.height - 4f) / 16;
				//if (Main.tile[num1058, scaleChange] != null && Main.tile[num1058, scaleChange].bottomSlope()){ npc.direction *= -1; }
				if (npc.collideX && npc.velocity.Y == 0f){ collisonOnX = true; ai[2] = 0f; ai[1] = 1f; npc.directionY = -1; }
				if (npc.velocity.Y == 0f)
				{
					if (npc.localAI[1] == npc.position.X)
					{
						npc.localAI[2] += 1f;
						if (npc.localAI[2] > 10f)
						{
							npc.direction = 1;
							npc.velocity.X = (float)npc.direction * moveInterval;
							npc.localAI[2] = 0f;
						}
					}else{ npc.localAI[2] = 0f; npc.localAI[1] = npc.position.X; }
				}
			}
			if (ai[2] == 0f)
			{
				npc.noGravity = true;
				if (ai[1] == 0f)
				{
					if (npc.collideY){ ai[0] = 2f; }
					if (!npc.collideY && ai[0] == 2f)
					{
						npc.direction = -npc.direction;
						ai[1] = 1f;
						ai[0] = 1f;
					}
					if (npc.collideX){ npc.directionY = -npc.directionY; ai[1] = 1f; }
				}else
				{
					if (npc.collideX){ ai[0] = 2f; }
					if (!npc.collideX && ai[0] == 2f)
					{
						npc.directionY = -npc.directionY;
						ai[1] = 0f;
						ai[0] = 1f;
					}
					if (npc.collideY){ npc.direction = -npc.direction; ai[1] = 0f; }
				}
				if (!collisonOnX)
				{
					float prevRot = npc.rotation;
					if (npc.directionY < 0)
					{
						if (npc.direction < 0)
						{
							if (npc.collideX)
							{
								npc.rotation = 1.57f;
								npc.spriteDirection = -1;
							}else if (npc.collideY)
							{
								npc.rotation = 3.14f;
								npc.spriteDirection = 1;
							}
						}else if (npc.collideY)
						{
							npc.rotation = 3.14f;
							npc.spriteDirection = -1;
						}else if (npc.collideX)
						{
							npc.rotation = 4.71f;
							npc.spriteDirection = 1;
						}
					}else 
					if (npc.direction < 0)
					{
						if (npc.collideY)
						{
							npc.rotation = 0f;
							npc.spriteDirection = -1;
						}else if (npc.collideX)
						{
							npc.rotation = 1.57f;
							npc.spriteDirection = 1;
						}
					}else if (npc.collideX){ npc.rotation = 4.71f; npc.spriteDirection = -1; }else 
					if (npc.collideY){ npc.rotation = 0f; npc.spriteDirection = 1; }
					float prevRot2 = npc.rotation;
					npc.rotation = prevRot;
					if (npc.rotation > 6.28){ npc.rotation -= 6.28f; } if (npc.rotation < 0f){ npc.rotation += 6.28f; }
					float rotDiffAbs = Math.Abs(npc.rotation - prevRot2);
					if (npc.rotation > prevRot2)
					{
						if (rotDiffAbs > 3.14){ npc.rotation += rotAmt; }else
						{
							npc.rotation -= rotAmt;
							if (npc.rotation < prevRot2){ npc.rotation = prevRot2; }
						}
					}
					if (npc.rotation < prevRot2)
					{
						if (rotDiffAbs > 3.14){ npc.rotation -= rotAmt; }else
						{
							npc.rotation += rotAmt;
							if (npc.rotation > prevRot2){ npc.rotation = prevRot2; }
						}
					}
				}

				if(npc.directionY == -1 && !npc.collideX){ snailStatus = 1; }else
				if(npc.directionY == 1 && !npc.collideX){ snailStatus = 0; }else
				if(npc.direction == 1 && !npc.collideY){ snailStatus = 3; }else
				{ snailStatus = 2; }
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.velocity.Y = moveInterval * (float)npc.directionY;
			}
		}

		public static void CollisionTest(NPC npc, ref bool left, ref bool right, ref bool up, ref bool down)
		{
			up = down = left = right = false;
			int lengthX = (npc.width / 16) + (npc.width % 16 > 0 ? 1 : 0);
			int lengthY = (npc.height / 16) + (npc.height % 16 > 0 ? 1 : 0);			
			int xLeft = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(npc.position.X / 16f) - 1)), xRight = Math.Max(0, Math.Min(Main.maxTilesX - 1, xLeft + lengthX + 1));
			int yUp = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(npc.position.Y / 16f) - 1)), yDown = Math.Max(0, Math.Min(Main.maxTilesY - 1, yUp + lengthY + 1));
			//TOP/DOWN
			for(int x2 = xLeft; x2 < xRight; x2++)
			{
				Tile tileUp = Main.tile[x2, yUp], tileDown = Main.tile[x2, yDown];
				if(tileUp != null && tileUp.nactive() && Main.tileSolid[tileUp.type] && !Main.tileSolidTop[tileUp.type]) up = true;
				if(tileDown != null && tileDown.nactive() && Main.tileSolid[tileDown.type]) down = true;		
				if(up && down) break;
			}
			//LEFT/RIGHT
			for(int y2 = yUp; y2 < yDown; y2++)
			{
				Tile tileLeft = Main.tile[xLeft, y2], tileRight = Main.tile[xRight, y2];
				if(tileLeft != null && tileLeft.nactive() && Main.tileSolid[tileLeft.type] && !Main.tileSolidTop[tileLeft.type]) left = true;
				if(tileRight != null && tileRight.nactive() && Main.tileSolid[tileRight.type] && !Main.tileSolidTop[tileRight.type]) right = true;	
				if(left && right) break;				
			}
		}

		public static void AISpore(NPC npc, ref float[] ai, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			npc.TargetClosest(true);
			AISpore(npc, ref ai, Main.player[npc.target].Center, Main.player[npc.target].width, moveIntervalX, moveIntervalY, maxSpeedX, maxSpeedY);
		}

		/*
         * A cleaned up (and edited) copy of Spore AI. (Fungi Spore, Plantera Spore, etc.) (AIStyle 50)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * target : The center of the target.
		 * targetWidth : The width of the target.
		 * moveIntervalX : The amount to move by on the X axis each tick.
		 * moveIntervalY : The amount to move by on the Y axis each tick.
		 * maxSpeedX : The maximum speed of the codable on the X axis.
		 * maxSpeedY : The maximum speed of the codable on the Y axis.
         */
		public static void AISpore(Entity codable, ref float[] ai, Vector2 target, int targetWidth = 16, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			codable.velocity.Y += moveIntervalY;
			if (codable.velocity.Y < 0f) codable.velocity.Y *= 0.99f;
			if (codable.velocity.Y > 1f) codable.velocity.Y = 1f;
			int widthHalf = targetWidth / 2;
			if (codable.position.X + codable.width < target.X - widthHalf)
			{
				if (codable.velocity.X < 0) codable.velocity.X *= 0.98f;
				codable.velocity.X += moveIntervalX;
			}else if (codable.position.X > target.X + widthHalf)
			{
				if (codable.velocity.X > 0) codable.velocity.X *= 0.98f;
				codable.velocity.X -= moveIntervalX;
			}
			if (codable.velocity.X > maxSpeedX || codable.velocity.X < -maxSpeedX) codable.velocity.X *= 0.97f;
		}

		/*
		 * *UNTESTED, MAY NOT WORK PROPERLY*
		 * 
         * A cleaned up (and edited) copy of Charger AI. (Unicorns, wolves, etc.) (AIStyle 26)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * moveInterval : How much to move each tick.
		 * maxSpeed : The maxium speed the npc can move per tick.
         * allowBoredom : If false, npc will not get 'bored' trying to harass a target and wander off.
         * ticksUntilBoredom : the amount of ticks until the npc gets 'bored' following a target.
         */
		public static void AICharger(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 6f, bool allowBoredom = true, int ticksUntilBoredom = 30)
		{
			bool isMoving = false;
			if (npc.velocity.Y == 0f && (npc.velocity.X > 0f && npc.direction < 0 || npc.velocity.X < 0f && npc.direction > 0))
			{
				isMoving = true;
				++ai[3];
			}
			if (npc.position.X == npc.oldPosition.X || ai[3] >= ticksUntilBoredom || isMoving) ++ai[3];
			else if ((ai[3] > 0f)) --ai[3];
			if (ai[3] > (ticksUntilBoredom * 10)) ai[3] = 0f;
			if (npc.justHit) ai[3] = 0f;
			if (ai[3] == ticksUntilBoredom) npc.netUpdate = true;
			Vector2 npcCenter = npc.Center;
			float distX = Main.player[npc.target].Center.X - npcCenter.X;
			float distY = Main.player[npc.target].Center.Y - npcCenter.Y;
			float dist = (float)Math.Sqrt(distX * distX + distY * distY);
			if (dist < 200f) ai[3] = 0f;
			if (!allowBoredom || ai[3] < ticksUntilBoredom)
			{
				npc.TargetClosest(true);
			}else
			{
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						++ai[0];
						if (ai[0] >= 2.0){ npc.direction *= -1; npc.spriteDirection = npc.direction; ai[0] = 0f; }
					}
				}else ai[0] = 0f;
				npc.directionY = -1;
				if (npc.direction == 0) npc.direction = 1;
			}
			if (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0))
			{
				if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0f) npc.velocity *= 0.8f;
				}else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X += moveInterval;
					if (npc.velocity.X > maxSpeed) npc.velocity.X = maxSpeed;
				}else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X -= moveInterval;
					if (npc.velocity.X < -maxSpeed) npc.velocity.X = -maxSpeed;
				}
			}
		}

		/*
         * A cleaned up (and edited) copy of Friendly AI. (Town NPCs, Bunnies, Penguins, etc.) (AIStyle 7)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * critter : If true, targets players when idle and follows them.
         * moveInterval : how much to move each tick.
		 * seekHome: If null, use normal behavior. If true or false, overrides normal behavior. Wether or not to seek a home.
		 * canTeleportHome : If true, npc will teleport to it's home tile if noone is within it's view range.
		 * canFindHouse : If true, the npc can search for a home. If false, it will only ever attempt to teleport to it's preset home.
		 * canOpenDoors : If true, the npc can open and close doors.
         */
		public static void AIFriendly(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 1f, bool critter = false, bool? seekHome = null, bool canTeleportHome = true, bool canFindHouse = true, bool canOpenDoors = true)
		{
			bool seekHouse = Main.raining;
			if (!Main.dayTime || Main.eclipse) seekHouse = true;
			if (seekHome != null) seekHouse = (bool)seekHome;
			int npcTileX = (int)((double)npc.position.X + (double)(npc.width / 2)) / 16;
			int npcTileY = (int)((double)npc.position.Y + (double)npc.height + 1.0) / 16;
			if (critter && npc.target == 255)
			{
				npc.TargetClosest(true);
				npc.direction = npc.Center.X < Main.player[npc.target].Center.X ? 1 : -1;
				npc.spriteDirection = npc.direction;
				if (npc.homeTileX == -1) npc.homeTileX = (int)(npc.Center.X / 16f);
			}
			bool isTalking = false;
			npc.directionY = -1;
			if (npc.direction == 0) npc.direction = 1;
			//Sets AI if the npc is speaking to a player
			for (int m1 = 0; m1 < 255; ++m1)
			{
				if (Main.player[m1].active && Main.player[m1].talkNPC == npc.whoAmI)
				{
					isTalking = true;
					if (ai[0] != 0f) npc.netUpdate = true;
					ai[0] = 0f; ai[1] = 300f; ai[2] = 100f;
					npc.direction = Main.player[m1].Center.X >= npc.Center.X ? 1 : -1;
				}
			}
			if (ai[3] > 0f)
			{
				npc.life = -1;
				npc.HitEffect(0, 10.0);
				npc.active = false;
				if (npc.type == 37)
					Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
			}
			//prevent a -1, -1 saving scenario
			if ((npc.type >= Main.maxNPCTypes && npc.homeTileX == -1 && npc.homeTileY == -1) || (npc.homeTileX == ushort.MaxValue && npc.homeTileY == ushort.MaxValue))
			{
				npc.homeTileX = (int)npc.Center.X / 16;
				npc.homeTileY = (int)npc.Center.Y / 16;
			}
			int homeTileY = npc.homeTileY;
			if (Main.netMode != 1 && npc.homeTileY > 0)
			{
				while (!WorldGen.SolidTile(npc.homeTileX, homeTileY) && homeTileY < Main.maxTilesY - 20)
					++homeTileY;
			}
			//handle teleporting to the home tile
			if (Main.netMode != 1 && canTeleportHome && seekHouse && ((npcTileX != npc.homeTileX || npcTileY != homeTileY) && !npc.homeless))
			{
				bool moveToHome = true;
				for (int m2 = 0; m2 < 2; ++m2)
				{
					Rectangle checkRect = new Rectangle((int)(npc.Center.X - (NPC.sWidth / 2) - NPC.safeRangeX), (int)(npc.Center.Y - (NPC.sHeight / 2) - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					if (m2 == 1)
						checkRect = new Rectangle(npc.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, homeTileY * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					for (int m3 = 0; m3 < 255; m3++)
					{
						if (Main.player[m3].active && Main.player[m3].Hitbox.Intersects(checkRect)){ moveToHome = false; break; }else 
						if (!moveToHome) break;
					}
				}
				if (moveToHome)
				{
					if (!Collision.SolidTiles(npc.homeTileX - 1, npc.homeTileX + 1, homeTileY - 3, homeTileY - 1)) //either move to preestablished home..
					{
						npc.velocity.X = 0.0f;
						npc.velocity.Y = 0.0f;
						npc.position.X = (float)(npc.homeTileX * 16 + 8 - npc.width / 2);
						npc.position.Y = (float)(homeTileY * 16 - npc.height) - 0.1f;
						npc.netUpdate = true;
					}else //or find a new one
					if(canFindHouse)
					{
						npc.homeless = true;
						WorldGen.QuickFindHome(npc.whoAmI);
					}
				}
			}
			//slow down
			if (ai[0] == 0f)
			{
				if (ai[2] > 0f) --ai[2];
				if (seekHouse && !isTalking && !critter)
				{
					if (Main.netMode != 1)
					{
						//stop at the home tile
						if (npcTileX == npc.homeTileX && npcTileY == homeTileY)
						{
							if (npc.velocity.X != 0f) npc.netUpdate = true;
							if (npc.velocity.X > 0.1f) npc.velocity.X -= 0.1f;
							else if (npc.velocity.X < -0.1f) npc.velocity.X += 0.1f;
							else npc.velocity.X = 0f;
						}else
						{
							npc.direction = npcTileX <= npc.homeTileX ? 1 : -1;
							ai[0] = 1f; ai[1] = (float)(200 + Main.rand.Next(200)); ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}else
				{
					//just stop in general
					if (npc.velocity.X > 0.1f) npc.velocity.X -= 0.1f;
					else if (npc.velocity.X < -0.1f) npc.velocity.X += 0.1f;
					else npc.velocity.X = 0f;
					if (Main.netMode != 1)
					{
						if (ai[1] > 0) --ai[1];
						if (ai[1] <= 0)
						{
							ai[0] = 1f;
							ai[1] = (float)(200 + Main.rand.Next(200));
							if (critter) ai[1] += (float)Main.rand.Next(200, 400);
							ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				if (Main.netMode == 1 || seekHouse && (npcTileX != npc.homeTileX || npcTileY != homeTileY)) return;
				//move towards the home point
				if (npcTileX < npc.homeTileX - 25 || npcTileX > npc.homeTileX + 25)
				{
					if (ai[2] != 0f) return;
					if (npcTileX < npc.homeTileX - 50 && npc.direction == -1){ npc.direction = 1; npc.netUpdate = true; }else
					{ if (npcTileX <= npc.homeTileX + 50 || npc.direction != 1) return; npc.direction = -1; npc.netUpdate = true; }
				}else
				{
					if (Main.rand.Next(80) != 0 || (double)ai[2] != 0) return;
					ai[2] = 200f;
					npc.direction *= -1;
					npc.netUpdate = true;
				}
			}else
			if (ai[0] != 1) { return; }else
			//move around within the home
			if (Main.netMode != 1 && !critter && seekHouse && npcTileX == npc.homeTileX && npcTileY == npc.homeTileY)
			{
				ai[0] = 0f; ai[1] = (float)(200 + Main.rand.Next(200)); ai[2] = 60f;
				npc.netUpdate = true;
			}else
			{
				if (Main.netMode != 1 && !npc.homeless && !Main.tileDungeon[(int)Main.tile[npcTileX, npcTileY].type] && (npcTileX < npc.homeTileX - 35 || npcTileX > npc.homeTileX + 35))
				{
					if (npc.Center.X < (npc.homeTileX * 16) && npc.direction == -1)
						ai[1] -= 5f;
					else if (npc.Center.X > (npc.homeTileX * 16) && npc.direction == 1)
						ai[1] -= 5f;
				}
				--ai[1];
				if (ai[1] <= 0f)
				{
					ai[0] = 0f; ai[1] = (float)(300 + Main.rand.Next(300));
					if (critter) ai[1] -= (float)Main.rand.Next(100);
					ai[2] = 60f; npc.netUpdate = true;
				}
				//close doors the npc has opened
				if (npc.closeDoor && ((npc.Center.X / 16f > npc.doorX + 2) || (npc.Center.X / 16f < npc.doorX - 2)))
				{
					if (WorldGen.CloseDoor(npc.doorX, npc.doorY, false))
					{
						npc.closeDoor = false;
						NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 1, (float)npc.doorX, (float)npc.doorY, (float)npc.direction, 0);
					}
					if ((npc.Center.X / 16f > npc.doorX + 4) || (npc.Center.X / 16f < npc.doorX - 4) || (npc.Center.Y / 16f > npc.doorY + 4) || (npc.Center.Y / 16f < npc.doorY - 4))
						npc.closeDoor = false;
				}
				if (npc.Center.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0) npc.velocity *= 0.8f;
				}else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X += moveInterval;
					if (npc.velocity.X > maxSpeed) npc.velocity.X = maxSpeed;
				}else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X -= moveInterval;
					if (npc.velocity.X > maxSpeed) npc.velocity.X = maxSpeed;
				}
				WalkupHalfBricks(npc);
				if (npc.velocity.Y != 0f) return;
				if (npc.position.X == ai[2]) npc.direction *= -1;
				ai[2] = -1f;
				int tileX2 = (int)((npc.Center.X + (15 * npc.direction)) / 16f);
				int tileY2 = (int)((npc.position.Y + npc.height - 16f) / 16f);
				#region denull tiles
				if (Main.tile[tileX2, tileY2] == null) Main.tile[tileX2, tileY2] = new Tile();
				if (Main.tile[tileX2, tileY2 - 1] == null) Main.tile[tileX2, tileY2 - 1] = new Tile();
				if (Main.tile[tileX2, tileY2 - 2] == null) Main.tile[tileX2, tileY2 - 2] = new Tile();
				if (Main.tile[tileX2, tileY2 - 3] == null) Main.tile[tileX2, tileY2 - 3] = new Tile();
				if (Main.tile[tileX2, tileY2 + 1] == null) Main.tile[tileX2, tileY2 + 1] = new Tile();
				if (Main.tile[tileX2 - npc.direction, tileY2 + 1] == null) Main.tile[tileX2 - npc.direction, tileY2 + 1] = new Tile();
				if (Main.tile[tileX2 + npc.direction, tileY2 - 1] == null) Main.tile[tileX2 + npc.direction, tileY2 - 1] = new Tile();
				if (Main.tile[tileX2 + npc.direction, tileY2 + 1] == null) Main.tile[tileX2 + npc.direction, tileY2 + 1] = new Tile();
				#endregion
				//Main.tile[tileX2 - npc.direction, tileY2 + 1].halfBrick();
				if (canOpenDoors && Main.tile[tileX2, tileY2 - 2].nactive() && (int)Main.tile[tileX2, tileY2 - 2].type == 10 && (Main.rand.Next(10) == 0 || seekHouse))
				{
					if (Main.netMode == 1)
						return;
					//attempt to open the door...
					if (WorldGen.OpenDoor(tileX2, tileY2 - 2, npc.direction))
					{
						npc.closeDoor = true;
						npc.doorX = tileX2;
						npc.doorY = tileY2 - 2;
						NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX2, (float)(tileY2 - 2), (float)npc.direction, 0);
						npc.netUpdate = true;
						ai[1] += 80f;
					//if that fails, attempt to open it the other way...
					}else if (WorldGen.OpenDoor(tileX2, tileY2 - 2, -npc.direction))
					{
						npc.closeDoor = true;
						npc.doorX = tileX2;
						npc.doorY = tileY2 - 2;
						NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX2, (float)(tileY2 - 2), (float)-npc.direction, 0);
						npc.netUpdate = true;
						ai[1] += 80f;
					//if it still fails, you can't open the door, so turn around
					}else
					{
						npc.direction *= -1;
						npc.netUpdate = true;
					}
				}else
				{
					if (npc.velocity.X < 0f && npc.spriteDirection == -1 || npc.velocity.X > 0f && npc.spriteDirection == 1)
					{
						if (Main.tile[tileX2, tileY2 - 2].nactive() && Main.tileSolid[Main.tile[tileX2, tileY2 - 2].type] && !Main.tileSolidTop[Main.tile[tileX2, tileY2 - 2].type])
						{
							if (npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2 - 1, tileY2 - 5, tileY2 - 1) || npc.direction == -1 && !Collision.SolidTiles(tileX2 + 1, tileX2 + 2, tileY2 - 5, tileY2 - 1))
							{
								if (!Collision.SolidTiles(tileX2, tileX2, tileY2 - 5, tileY2 - 3))
								{
									npc.velocity.Y = -6f; npc.netUpdate = true;
								}else{ npc.direction *= -1; npc.netUpdate = true; }
							}else{ npc.direction *= -1; npc.netUpdate = true; }
						}else if (Main.tile[tileX2, tileY2 - 1].nactive() && Main.tileSolid[Main.tile[tileX2, tileY2 - 1].type] && !Main.tileSolidTop[Main.tile[tileX2, tileY2 - 1].type])
						{
							if (npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2 - 1, tileY2 - 4, tileY2 - 1) || npc.direction == -1 && !Collision.SolidTiles(tileX2 + 1, tileX2 + 2, tileY2 - 4, tileY2 - 1))
							{
								if (!Collision.SolidTiles(tileX2, tileX2, tileY2 - 4, tileY2 - 2))
								{
									npc.velocity.Y = -5f; npc.netUpdate = true;
								}else{ npc.direction *= -1; npc.netUpdate = true; }
							}else{ npc.direction *= -1; npc.netUpdate = true; }
						}else if (npc.position.Y + npc.height - (tileY2 * 16f) > 20f)
						{
							if (Main.tile[tileX2, tileY2].nactive() && Main.tileSolid[(int)Main.tile[tileX2, tileY2].type] && (int)Main.tile[tileX2, tileY2].slope() == 0)
							{
								if (npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2, tileY2 - 3, tileY2 - 1) || npc.direction == -1 && !Collision.SolidTiles(tileX2, tileX2 + 2, tileY2 - 3, tileY2 - 1))
								{
									npc.velocity.Y = -4.4f;
									npc.netUpdate = true;
								}else{ npc.direction *= -1; npc.netUpdate = true; }
							}
						}
						try
						{
							#region more denulling tiles
							if (Main.tile[tileX2, tileY2 + 1] == null) Main.tile[tileX2, tileY2 + 1] = new Tile();
							if (Main.tile[tileX2 - npc.direction, tileY2 + 1] == null) Main.tile[tileX2 - npc.direction, tileY2 + 1] = new Tile();
							if (Main.tile[tileX2, tileY2 + 2] == null) Main.tile[tileX2, tileY2 + 2] = new Tile();
							if (Main.tile[tileX2 - npc.direction, tileY2 + 2] == null) Main.tile[tileX2 - npc.direction, tileY2 + 2] = new Tile();
							if (Main.tile[tileX2, tileY2 + 3] == null) Main.tile[tileX2, tileY2 + 3] = new Tile();
							if (Main.tile[tileX2 - npc.direction, tileY2 + 3] == null) Main.tile[tileX2 - npc.direction, tileY2 + 3] = new Tile();
							if (Main.tile[tileX2, tileY2 + 4] == null) Main.tile[tileX2, tileY2 + 4] = new Tile();
							if (Main.tile[tileX2 - npc.direction, tileY2 + 4] == null) Main.tile[tileX2 - npc.direction, tileY2 + 4] = new Tile();
							#endregion
							if (!critter && npcTileX >= npc.homeTileX - 35 && npcTileX <= npc.homeTileX + 35 && (!Main.tile[tileX2, tileY2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 1].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 1].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 1].type]) && (!Main.tile[tileX2, tileY2 + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 2].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 2].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 2].type]) && (!Main.tile[tileX2, tileY2 + 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 3].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 3].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 3].type]) && (!Main.tile[tileX2, tileY2 + 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 4].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 4].type]))
							{
								npc.direction *= -1;
								npc.velocity.X *= -1f;
								npc.netUpdate = true;
							}
						}catch{ }
						if ((double)npc.velocity.Y < 0f) ai[2] = npc.position.X;
					}
					if (npc.velocity.Y < 0f && npc.wet) npc.velocity.Y *= 1.2f;
					npc.velocity.Y *= 1.2f;
				}
			}
		}


		/*
		 * A cleaned up (and edited) copy of Eater of Souls AI. (EoS, Corruptor, etc.) (AIStyle 5)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * moveInterval : how much to move each tick.
		 * distanceDivider : The amount that is divided by the distance; determines velocity.
		 * bounceScalar : scalar for how big a 'bounce' is if the npc hits a tile.
		 * fleeAtDay : If true, npc will flee if it becomes day.
		 * ignoreWet : If true, npc will ignore being wet.
		 */
		public static void AIEater(NPC npc, ref float[] ai, float moveInterval = 0.022f, float distanceDivider = 4.2f, float bounceScalar = 0.7f, bool fleeAtDay = false, bool ignoreWet = false)
        {
            if (npc.target < 0 || npc.target == (int)byte.MaxValue || Main.player[npc.target].dead){ npc.TargetClosest(true); }
			float distX = Main.player[npc.target].Center.X;
			float distY = Main.player[npc.target].Center.Y;
            Vector2 npcCenter = npc.Center;
			float distDX = (int)(distX / 8f) * 8f;
			float distDY = (int)(distY / 8f) * 8f;
			npcCenter.X = (int)(npcCenter.X / 8f) * 8f;
			npcCenter.Y = (int)(npcCenter.Y / 8f) * 8f;
			float distX2 = distDX - npcCenter.X;
			float distY2 = distDY - npcCenter.Y;
			float dist = (float) Math.Sqrt(distX2 * distX2 + distY2 * distY2);
			float SpeedX1;
			float SpeedY1;
			if (dist == 0f)
			{
				SpeedX1 = npc.velocity.X;
				SpeedY1 = npc.velocity.Y;
			}else
			{
				float distScalar = distanceDivider / dist;
				SpeedX1 = distX2 * distScalar;
				SpeedY1 = distY2 * distScalar;
			}
			++ai[0];
            if (ai[0] > 0f){ npc.velocity.Y += 23f / 1000f; }else{ npc.velocity.Y -= 23f / 1000f; }
            if (ai[0] < -100f || (double)ai[0] > 100f){ npc.velocity.X += 23f / 1000f; }else{ npc.velocity.X -= 23f / 1000f; }
            if (ai[0] > 200f){ ai[0] = -200f; }
			if (dist < 150f){	npc.velocity.X += SpeedX1 * 0.007f; npc.velocity.Y += SpeedY1 * 0.007f; }
			if (Main.player[npc.target].dead)
			{
				SpeedX1 = npc.direction * distanceDivider / 2f;
				SpeedY1 = -distanceDivider / 2f;
			}
			if (npc.velocity.X < SpeedX1){ npc.velocity.X += moveInterval; }else 
            if (npc.velocity.X > SpeedX1){ npc.velocity.X -= moveInterval; }
			if (npc.velocity.Y < SpeedY1){ npc.velocity.Y += moveInterval; }else 
            if (npc.velocity.Y > SpeedY1){ npc.velocity.Y -= moveInterval; }
			npc.rotation = (float) Math.Atan2((double) SpeedY1, (double) SpeedX1) - 1.57f;
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f){ npc.velocity.X = 2f; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f){ npc.velocity.X = -2f; }
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f){ npc.velocity.Y = 2f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f){ npc.velocity.Y = -2f; }
			}
			if (!ignoreWet && npc.wet)
			{
                if(npc.velocity.Y > 0f){ npc.velocity.Y *= 0.95f; }
				npc.velocity.Y -= 0.3f;
                if(npc.velocity.Y < -2f){ npc.velocity.Y = -2f; }
			}
			if((fleeAtDay && Main.dayTime) || Main.player[npc.target].dead)
			{
				npc.velocity.Y -= moveInterval * 2f;
                if (npc.timeLeft > 10){ npc.timeLeft = 10; }
			}
			if ((npc.velocity.X <= 0f || npc.oldVelocity.X >= 0f) && (npc.velocity.X >= 0f || npc.oldVelocity.X <= 0f) && ((npc.velocity.Y <= 0f || npc.oldVelocity.Y >= 0f) && (npc.velocity.Y >= 0.0 || npc.oldVelocity.Y <= 0f)) || npc.justHit)
				return;
			npc.netUpdate = true;
        }

		/*
		 * A cleaned up (and edited) copy of Wheel AI. (Blazing Wheel) (AIStyle 21)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * moveInterval : How much to move each tick (NOTE: very high numbers can result in the AI breaking!)
		 * rotate : If true, rotates the npc like a Blazing Wheel.
		 */
		public static void AIWheel(NPC npc, ref float[] ai, float moveInterval = 6f, bool rotate = false)
        {
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.directionY = 1;
				ai[0] = 1f;
			}
			if (ai[1] == 0f)
			{
                if (rotate){ npc.rotation += (float)(npc.direction * npc.directionY) * 0.13f; }
				if (npc.collideY) ai[0] = 2f;
				if (!npc.collideY && ai[0] == 2f)
				{
					npc.direction = -npc.direction;
					ai[1] = 1f;
					ai[0] = 1f;
				}
				if (npc.collideX){ npc.directionY = -npc.directionY; ai[1] = 1f; }
			}else
			{
                if (rotate){ npc.rotation -= (float)(npc.direction * npc.directionY) * 0.13f; }
				if (npc.collideX) ai[0] = 2f;
				if (!npc.collideX && ai[0] == 2f)
				{
					npc.directionY = -npc.directionY;
					ai[1] = 0f;
					ai[0] = 1f;
				}
				if (npc.collideY){ npc.direction = -npc.direction; ai[1] = 0.0f; }
			}
			npc.velocity.X = (float) (moveInterval * npc.direction);
			npc.velocity.Y = (float) (moveInterval * npc.directionY);
        }

		/*
		 * A cleaned up (and edited) copy of Spider Wallwalking AI. (Blood Crawler, etc.) (AIStyle 40)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * ignoreSight : If true, ignores if the npc can see the player or not.
		 * moveInterval : how much to move each tick.
		 * slowSpeed : how fast the npc much go before you begin to slow it down.
		 * maxSpeed : the max speed the npc can go.
		 * distanceDivider : the amount that is divided by the distance; determins velocity.
		 * bounceScalar : scalar for how big a 'bounce' is if the npc hits a tile.
		 * transformType : if not -1, will check if there's a wall behind the npc and if there is not, will change projectile npc to the type provided.
		 */
		public static void AISpider(NPC npc, ref float[] ai, bool ignoreSight = false, float moveInterval = 0.08f, float slowSpeed = 1.5f, float maxSpeed = 3f, float distanceDivider = 2f, float bounceScalar = 0.5f, int transformType = -1)
        {
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
				npc.TargetClosest(true);
            Vector2 npcCenter = npc.Center;
            float distX = Main.player[npc.target].Center.X;
            float distY = Main.player[npc.target].Center.Y;
            float distDX = (int)(distX / 8f) * 8f;
            float distDY = (int)(distY / 8f) * 8f;
            npcCenter.X = (int)(npcCenter.X / 8f) * 8f;
            npcCenter.Y = (int)(npcCenter.Y / 8f) * 8f;
            float distX2 = distDX - npcCenter.X;
            float distY2 = distDY - npcCenter.Y;
            float dist = (float)Math.Sqrt(distX2 * distX2 + distY2 * distY2);
			float velX;
			float velY;
			if (dist == 0f)
			{
				velX = npc.velocity.X;
				velY = npc.velocity.Y;
			}else
			{
				float speedMult = distanceDivider / dist;
				velX = distX2 * speedMult;
				velY = distY2 * speedMult;
			}
			if (Main.player[npc.target].dead)
			{
				velX = (float)npc.direction * distanceDivider / 2f;
				velY = -distanceDivider / 2f;
			}
			npc.spriteDirection = -1;
			if (!ignoreSight && !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
			{
				++ai[0];
                if (ai[0] > 0f){ npc.velocity.Y += 23f / 1000f; }else{ npc.velocity.Y -= 23f / 1000f; }
                if (ai[0] < -100f || (double)ai[0] > 100f){ npc.velocity.X += 23f / 1000f; }else{ npc.velocity.X -= 23f / 1000f; }
                if (ai[0] > 200f){ ai[0] = -200f; }
				npc.velocity.X += velX * 0.007f;
				npc.velocity.Y += velY * 0.007f;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                if (npc.velocity.X > slowSpeed || npc.velocity.X < -slowSpeed){ npc.velocity.X *= 0.9f; }
				if (npc.velocity.Y > slowSpeed || npc.velocity.Y < -slowSpeed){ npc.velocity.Y *= 0.9f; }
                if (npc.velocity.X > maxSpeed){ npc.velocity.X = maxSpeed; }else
                if (npc.velocity.X < -maxSpeed){ npc.velocity.X = -maxSpeed; }
                if (npc.velocity.Y > maxSpeed){ npc.velocity.Y = maxSpeed; }else
                if (npc.velocity.Y < -maxSpeed){ npc.velocity.Y = -maxSpeed; }
			}else
			{
				if ((double)npc.velocity.X < (double)velX)
				{
					npc.velocity.X += moveInterval;
					if ((double)npc.velocity.X < 0 && (double)velX > 0)
						npc.velocity.X += moveInterval;
				}else if ((double)npc.velocity.X > (double)velX)
				{
					npc.velocity.X -= moveInterval;
					if ((double)npc.velocity.X > 0 && (double)velX < 0)
						npc.velocity.X -= moveInterval;
				}
				if ((double)npc.velocity.Y < (double)velY)
				{
					npc.velocity.Y += moveInterval;
					if ((double)npc.velocity.Y < 0 && (double)velY > 0)
						npc.velocity.Y += moveInterval;
				}else if ((double)npc.velocity.Y > (double)velY)
				{
					npc.velocity.Y -= moveInterval;
					if ((double)npc.velocity.Y > 0 && (double)velY < 0)
						npc.velocity.Y -= moveInterval;
				}
				npc.rotation = (float)Math.Atan2((double)velY, (double)velX);
			}
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f){ npc.velocity.X = 2f; }
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f){ npc.velocity.X = -2f; }
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f){ npc.velocity.Y = 2f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f){ npc.velocity.Y = -2f; }
			}
			if ((npc.velocity.X > 0f && npc.oldVelocity.X < 0f || npc.velocity.X < 0f && npc.oldVelocity.X > 0f || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f || npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
				npc.netUpdate = true;
			if(Main.netMode != 1 && transformType != -1)
            {
			    int centerTileX = (int)npc.Center.X / 16;
			    int centerTileY = (int)npc.Center.Y / 16;
			    bool wallExists = false;
			    for (int x = centerTileX - 1; x <= centerTileX + 1; ++x)
			    {
				    for (int y = centerTileY - 1; y <= centerTileY + 1; ++y)
				    {
                        if (Main.tile[x, y].wall > 0) { wallExists = true; break; }
				    }
			    }
			    if (!wallExists) npc.Transform(transformType);
            }
        }

		/*
		 * A cleaned up (and edited) copy of Skull AI. (Cursed Skull) (AIStyle 10)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * tacklePlayer : If true, the npc will occasionally charge at the player.
		 * maxDistanceAmt : The maxmimum amount of 'distance' the npc is allowed to wander to from the player.
		 * maxDistance : The maximum amount of distance from the player until the npc begins to speed up.
		 * increment : the amount to move per tick.
		 * closeIncrement : the amount to move per tick when close to the player.
		 */
		public static void AISkull(NPC npc, ref float[] ai, bool tacklePlayer = true, float maxDistanceAmt = 4f, float maxDistance = 350f, float increment = 0.011f, float closeIncrement = 0.019f)
        {
		    float distanceAmt = 1f;
		    npc.TargetClosest(true);
		    float distX = Main.player[npc.target].Center.X - npc.Center.X;
		    float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
		    float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
		    ai[1] += 1f;
		    if (ai[1] > 600f)
		    {
                if (tacklePlayer)
                {
                    increment *= 8f;
                    distanceAmt = 4f;
                    if (ai[1] > 650f){ ai[1] = 0f; }
                }else{ ai[1] = 0f; }
		    }else
			if (dist < 250f)
			{
				ai[0] += 0.9f;
                if (ai[0] > 0f){ npc.velocity.Y = npc.velocity.Y + closeIncrement; }else{ npc.velocity.Y = npc.velocity.Y - closeIncrement; }
                if (ai[0] < -100f || ai[0] > 100f) { npc.velocity.X = npc.velocity.X + closeIncrement; }else{ npc.velocity.X = npc.velocity.X - closeIncrement; }
				if (ai[0] > 200f){ ai[0] = -200f; }
			}
		    if (dist > maxDistance)
		    {
                distanceAmt = maxDistanceAmt + (maxDistanceAmt / 4f);
			    increment = 0.3f;
		    }else
			if (dist > maxDistance - (maxDistance / 7f))
			{
                distanceAmt = maxDistanceAmt - (maxDistanceAmt / 4f);
				increment = 0.2f;
			}else
            if (dist > maxDistance - (2 * (maxDistance / 7f)))
			{
                distanceAmt = (maxDistanceAmt / 2.66f);
				increment = 0.1f;
			}
		    dist = distanceAmt / dist;
		    distX *= dist; distY *= dist;
		    if (Main.player[npc.target].dead)
		    {
			    distX = (float)npc.direction * distanceAmt / 2f;
			    distY = -distanceAmt / 2f;
		    }
		    if (npc.velocity.X < distX){ npc.velocity.X = npc.velocity.X + increment; }else
			if (npc.velocity.X > distX){ npc.velocity.X = npc.velocity.X - increment; }
		    if (npc.velocity.Y < distY){ npc.velocity.Y = npc.velocity.Y + increment; }else
			if (npc.velocity.Y > distY){ npc.velocity.Y = npc.velocity.Y - increment; }
	    }

		/*
		 * A cleaned up (and edited) copy of Floater AI. (Pixie, Gastropod, etc.) (AIStyle 22)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * ignoreWet : If true, does not slow down in liquids.
		 * moveInterval : how much to move each tick.
		 * maxSpeedX/maxSpeedY : the max speed of the npc on the X and Y axis, respectively.
		 * hoverInterval : how much to hover by each tick.
		 * hoverMaxSpeed : the maximum speed to hover by.
		 * hoverHeight : the amount of tiles below an npc before it needs ground to hover over.
		 */
		public static void AIFloater(NPC npc, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit) { ai[2] = 0f; }
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - (float)tileDist && npc.position.X < ai[0] + (float)tileDist) { inRangeX = true; }
                else
                    if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0)) { inRangeX = true; }
                tileDist += 24;
                if (npc.position.Y > ai[1] - (float)tileDist && npc.position.Y < ai[1] + (float)tileDist)
                {
                    inRangeY = true;
                }
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    //i'm pretty sure projectile is never called, but it's in the original so...
                    if (ai[2] >= 30f && tileDist == 16)
                    {
                        flyUpward = true;
                    }
                    if (ai[2] >= 60f)
                    {
                        ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X = npc.velocity.X * -1f;
                        npc.collideX = false;
                    }
                }
                else
                {
                    ai[0] = npc.position.X;
                    ai[1] = npc.position.Y;
                    ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            else
            {
                ai[2] += 1f;
                if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }
            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);
            bool tileBelowEmpty = true;
            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    tileBelowEmpty = false;
                    break;
                }
            }
            if (flyUpward)
            {
                tileBelowEmpty = true;
            }
            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > maxSpeedY) { npc.velocity.Y = maxSpeedY; }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f) { npc.velocity.Y -= moveInterval; }
                if (npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
            }
            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) { npc.velocity.Y = -maxSpeedY * 0.75f; }
            }
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.4f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f) { npc.velocity.X = 1f; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f) { npc.velocity.X = -1f; }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
            }
            if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= (moveInterval * 0.5f);
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = npc.velocity.X - 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
                if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
                {
                    npc.velocity.X += (moveInterval * 0.5f);
                    if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = npc.velocity.X + 0.1f; }
                    else
                        if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - 0.05f; }
                    if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
                }
            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y = npc.velocity.Y - hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y - 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + (hoverInterval - 0.01f); }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
                if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
                {
                    npc.velocity.Y = npc.velocity.Y + hoverInterval;
                    if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y + 0.05f; }
                    else
                        if (npc.velocity.Y < 0f) { npc.velocity.Y = npc.velocity.Y - (hoverInterval - 0.01f); }
                    if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
                }
        }

		/*
		 * A cleaned up (and edited) copy of Flier AI. (Bat, Demon, etc.) (AIStyle 14)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * sporadic : If true, npc will overshoot targets.
		 * maxSpeedX/maxSpeedY : the max speed of the npc on the X and Y axis, respectively.
		 * slowdownIncrementX/slowdownIncrementY : the slowdown increment on the X and Y axis, respectively.
		 */
		public static void AIFlier(NPC npc, ref float[] ai, bool sporadic = true, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float maxSpeedX = 4f, float maxSpeedY = 1.5f, bool canBeBored = true, int timeUntilBoredom = 300)
        {
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                float max = maxSpeedX * 0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < max) { npc.velocity.X = max; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -max) { npc.velocity.X = -max; }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                float max = maxSpeedY * 0.66f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < max) { npc.velocity.Y = max; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -max) { npc.velocity.Y = -max; }
            }
            npc.TargetClosest(true);
            Action move = () =>
            {
                if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
                {
                    npc.velocity.X -= moveIntervalX;
                    if (npc.velocity.X > maxSpeedX) { npc.velocity.X -= moveIntervalX; }else
                    if (npc.velocity.X > 0f) { npc.velocity.X += moveIntervalX * 0.5f; }
                    if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
                }else
                if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
                {
                    npc.velocity.X += moveIntervalX;
                    if (npc.velocity.X < -maxSpeedX) { npc.velocity.X += moveIntervalX; }else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= moveIntervalX * 0.5f; }
                    if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
                }
                if (npc.directionY == -1 && (double)npc.velocity.Y > -maxSpeedY)
                {
                    npc.velocity.Y -= moveIntervalY;
                    if ((double)npc.velocity.Y > maxSpeedY) { npc.velocity.Y -= moveIntervalY; }else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += moveIntervalY * 0.5f; }
                    if ((double)npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
                }else
                if (npc.directionY == 1 && (double)npc.velocity.Y < maxSpeedY)
                {
                    npc.velocity.Y += moveIntervalY;
                    if ((double)npc.velocity.Y < -maxSpeedY) { npc.velocity.Y += moveIntervalY; }else
                    if (npc.velocity.Y < 0f) { npc.velocity.Y -= moveIntervalY * 0.5f; }
                    if ((double)npc.velocity.Y > maxSpeedY) { npc.velocity.Y = maxSpeedY; }
                }
            };
            if (canBeBored){ ai[0] += 1f; }
            if (canBeBored && ai[0] > timeUntilBoredom)
            {
                if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    ai[0] = 0f;
                }
                if (ai[0] > timeUntilBoredom * 2) { ai[0] = 0f; }
                npc.direction = Main.player[npc.target].Center.X < npc.Center.X ? 1 : -1;
                npc.directionY = Main.player[npc.target].Center.Y < npc.Center.Y ? 1 : -1;
                move();
            }else
            {
                move();
                if (sporadic)
                {
                    if (npc.wet)
                    {
                        if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y * 0.95f; }
                        npc.velocity.Y = npc.velocity.Y - 0.5f;
                        if (npc.velocity.Y < -maxSpeedX) { npc.velocity.Y = -maxSpeedX; }
                        npc.TargetClosest(true);
                    }
                    move();
                }
            }
        }

		/*
		 * A cleaned up (and edited) copy of Plant AI. (Man Eater, etc.) (AIStyle 13)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * checkTilePoint : If true, check if the tile the npc is connected to is there. If it isn't, kill the npc.
		 * endPoint : the end point of the npc. 
		 * isTilePos : true if the endPoint is an tile coordinates, false if it's an npc position.
		 * vineLength : the distance from the endPoint the npc can go up to.
		 * vineTimeExtend : the time until the vine's distance is extended.
		 * vineTimeMax : the time until the vine's distance is retracted (must be greater than vineTimeExtend).
		 * moveInterval : the increment to move by.
		 * speedMax : the max speed of the npc.
		 * targetOffset : A vector2 representing an 'offset' from the target, allows for some variation and misaccuracy.
		 */
		public static void AIPlant(NPC npc, ref float[] ai, bool checkTilePoint = true, Vector2 endPoint = default(Vector2), bool isTilePos = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2))
        {
            if (endPoint != default(Vector2))
            {
                ai[0] = endPoint.X;
                ai[1] = endPoint.Y;
            }
            if (checkTilePoint)
            {
                Vector2 tilePos = isTilePos ? new Vector2(ai[0], ai[1]) : new Vector2(ai[0] / 16f, ai[1] / 16f);
                int tx = (int)tilePos.X; int ty = (int)tilePos.Y;
                if (Main.tile[tx, ty] == null) { Main.tile[tx, ty] = new Tile(); }
                if (!Main.tile[tx, ty].nactive() || (!Main.tileSolid[Main.tile[tx, ty].type] || (Main.tileSolid[Main.tile[tx, ty].type] && Main.tileSolidTop[Main.tile[tx, ty].type])))
                {
                    if(npc.DeathSound != null) Main.PlaySound(npc.DeathSound, (int)npc.Center.X, (int)npc.Center.Y);
                    npc.life = -1;
                    npc.HitEffect(0, 10.0f);
                    npc.active = false;
                    return;
                }
            }
            npc.TargetClosest(true);
            ai[2] += 1f;
            if (ai[2] > vineTimeExtend)
            {
                vineLength = vineLengthLong;
                if (ai[2] > vineTimeMax) { ai[2] = 0f; }
            }
            Vector2 endPointCenter = isTilePos ? new Vector2(ai[0] * 16f + 8f, ai[1] * 16f + 8f) : new Vector2(ai[0], ai[1]);
			Vector2 playerCenter = Main.player[npc.target].Center + targetOffset;
            float distPlayerX = playerCenter.X - (float)(npc.width / 2) - endPointCenter.X;
            float distPlayerY = playerCenter.Y - (float)(npc.height / 2) - endPointCenter.Y;
            float distPlayer = (float)Math.Sqrt((double)(distPlayerX * distPlayerX + distPlayerY * distPlayerY));
            if (distPlayer > vineLength)
            {
                distPlayer = vineLength / distPlayer;
                distPlayerX *= distPlayer;
                distPlayerY *= distPlayer;
            }
            if (npc.position.X < endPointCenter.X + distPlayerX)
            {
                npc.velocity.X = npc.velocity.X + moveInterval;
                if (npc.velocity.X < 0f && distPlayerX > 0f) { npc.velocity.X = npc.velocity.X + moveInterval * 1.5f; }
            }else
            if (npc.position.X > endPointCenter.X + distPlayerX)
            {
                npc.velocity.X = npc.velocity.X - moveInterval;
                if (npc.velocity.X > 0f && distPlayerX < 0f) { npc.velocity.X = npc.velocity.X - moveInterval * 1.5f; }
            }
            if (npc.position.Y < endPointCenter.Y + distPlayerY)
            {
                npc.velocity.Y = npc.velocity.Y + moveInterval;
                if (npc.velocity.Y < 0f && distPlayerY > 0f) { npc.velocity.Y = npc.velocity.Y + moveInterval * 1.5f; }
            }else
            if (npc.position.Y > endPointCenter.Y + distPlayerY)
            {
                npc.velocity.Y = npc.velocity.Y - moveInterval;
                if (npc.velocity.Y > 0f && distPlayerY < 0f) { npc.velocity.Y = npc.velocity.Y - moveInterval * 1.5f; }
            }
            npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
            npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
            if (distPlayerX > 0f) { npc.spriteDirection = 1; npc.rotation = (float)Math.Atan2((double)distPlayerY, (double)distPlayerX); }else
            if (distPlayerX < 0f) { npc.spriteDirection = -1; npc.rotation = (float)Math.Atan2((double)distPlayerY, (double)distPlayerX) + 3.14f; }
            if (npc.collideX)
            {
                npc.netUpdate = true;
                npc.velocity.X = npc.oldVelocity.X * -0.7f;
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
            }
            if (npc.collideY)
            {
                npc.netUpdate = true;
                npc.velocity.Y = npc.oldVelocity.Y * -0.7f;
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
            }
        }

        public static void AIWorm(NPC npc, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool diggingDummy = false;			
			AIWorm(npc, ref diggingDummy, wormTypes, wormLength, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}
		
        public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
            int[] wtypes = new int[(wormTypes.Length == 1 ? 1 : wormLength)];
            wtypes[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				wtypes[wtypes.Length - 1] = wormTypes[2];
				for (int m = 1; m < wtypes.Length - 1; m++)
				{
					wtypes[m] = wormTypes[1];
				}
			}
            AIWorm(npc, ref isDigging, wtypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
        }
		
		public static void AIWorm(NPC npc, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool diggingDummy = false;
			AIWorm(npc, ref diggingDummy, wormTypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		/*
		 * A cleaned up (and edited) copy of Worm AI. (Giant Worm, EoW, Wyvern, etc.) (AIStyle 6)
		 * 
		 * wormTypes: An array of the worm npc types. The first type is for the head, the last type is for the tail, and the rest are body types in the order they appear on the worm.
		 *            If wormTypes has a length of 1, then it will spawn the head but not any other parts. This lets you make single npcs that dig like worms.
		 * partDistanceAddon : an addon to the distance between parts of the worm.
		 * maxSpeed : the fastest the worm can accellerate to.
		 * gravityResist : how much resistance on the X axis the worm has when it is out of tiles.
		 * fly : If true, acts like a Wvyern.
		 * split : If true, worm will split when parts of it die.
		 * ignoreTiles : If true, Allows the worm to move outside of tiles as if it were in them. (ignored if fly is true)
		 * spawnTileDust : If true, worm will spawn tile dust when it digs through tiles.
		 * soundEffects : If true, will produce a digging sound when nearing the player.
		 * rotateAverage : If true, takes the rotations of the the piece before and after this npc and averages them and adds to it to get a rotation. More accurate for some weirder shaped worms.
		 * onChangeType : an Action<NPC, int, bool> which is called when the worm splits and the npc changes to a head or tail. (NPC npc, int oldType, bool isHead)
		 */
		public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool singlePiece = wormTypes.Length == 1;
            int wormLength = wormTypes.Length;
            if (split)
            {
                npc.realLife = -1;
            }else
            if (npc.ai[3] > 0f) { npc.realLife = (int)npc.ai[3]; }

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) { npc.TargetClosest(true); }
            if (Main.player[npc.target].dead && npc.timeLeft > 300) { npc.timeLeft = 300; }
            if (Main.netMode != 1)
            {
				if (!singlePiece)
				{
					//spawn pieces (flying)
					if (fly && npc.type == wormTypes[0] && npc.ai[0] == 0f)
					{
						npc.ai[3] = (float)npc.whoAmI;
						npc.realLife = npc.whoAmI;
						int npcID = npc.whoAmI;

						for (int m = 1; m < wormLength - 1; m++)
						{
							int npcType = (m == wormLength ? wormTypes[wormTypes.Length - 1] : wormTypes[m]);

							int newnpcID = NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), npcType, npc.whoAmI);
							Main.npc[newnpcID].ai[3] = (float)npc.whoAmI;
							Main.npc[newnpcID].realLife = npc.whoAmI;
							Main.npc[newnpcID].ai[1] = (float)npcID;
							Main.npc[npcID].ai[0] = (float)newnpcID;
							NetMessage.SendData(23, -1, -1, NetworkText.FromLiteral(""), newnpcID, 0f, 0f, 0f, 0);
							npcID = newnpcID;
						}
						npc.netUpdate = true;
					}else //spawn pieces
					if ((npc.type == wormTypes[0] || (npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1])) && npc.ai[0] == 0f)
					{
						if (npc.type == wormTypes[0])
						{
							if (!split)
							{
								npc.ai[3] = (float)npc.whoAmI;
								npc.realLife = npc.whoAmI;
							}
							npc.ai[2] = (float)(wormLength - 1);
							int nextPiece = (wormTypes.Length <= 2 ? wormTypes[wormTypes.Length - 1] : wormTypes[1]);
							npc.ai[0] = (float)NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), nextPiece, npc.whoAmI);
						}else
						if ((npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1]) && npc.ai[2] > 0f)
						{
							npc.ai[0] = (float)NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), wormTypes[wormLength - (int)npc.ai[2]], npc.whoAmI);
						}else
						{
							npc.ai[0] = (float)NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), wormTypes[wormTypes.Length - 1], npc.whoAmI);
						}
						if (!split)
						{
							Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
							Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
						}
						Main.npc[(int)npc.ai[0]].ai[1] = (float)npc.whoAmI;
						Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;
						npc.netUpdate = true;
					}
				}
				//if npc can split, check if pieces are dead and if so split.
				if (!singlePiece && split)
                {
                    if (!Main.npc[(int)npc.ai[1]].active && !Main.npc[(int)npc.ai[0]].active) //if this is in the middle and both parts are inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type == wormTypes[0] && !Main.npc[(int)npc.ai[0]].active) //if this is the head and the bottom is inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type == wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[1]].active) //if this is the tail and the top is inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if ((npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1]) && !Main.npc[(int)npc.ai[1]].active) //if the body was just split, turn it into a head
                    {
						int oldType = npc.type;
                        npc.type = wormTypes[0];
                        int npcID = npc.whoAmI;
                        float lifePercent = (float)npc.life / (float)npc.lifeMax;
                        float lastPiece = npc.ai[0];
                        npc.SetDefaults(npc.type, -1f);
                        npc.life = (int)((float)npc.lifeMax * lifePercent);
                        npc.ai[0] = lastPiece;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                        npc.whoAmI = npcID;
						if(onChangeType != null) onChangeType(npc, oldType, true);
                    }
                    else
                        if ((npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1]) && !Main.npc[(int)npc.ai[0]].active) //if the body was just split, turn it into a tail
                        {
							int oldType = npc.type;				
                            npc.type = wormTypes[wormTypes.Length - 1];
                            int npcID = npc.whoAmI;
                            float lifePercent = (float)npc.life / (float)npc.lifeMax;
                            float lastPiece = npc.ai[1];
                            npc.SetDefaults(npc.type, -1f);
                            npc.life = (int)((float)npc.lifeMax * lifePercent);
                            npc.ai[1] = lastPiece;
                            npc.TargetClosest(true);
                            npc.netUpdate = true;
                            npc.whoAmI = npcID;
							if(onChangeType != null) onChangeType(npc, oldType, false);						
                        }
                }
                else
				if (!singlePiece)
                {
                    if (npc.type != wormTypes[0] && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type != wormTypes[wormTypes.Length - 1] && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (!npc.active && Main.netMode == 2) { NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, 0f, 0f, -1); }
            }
            int tileX = (int)(npc.position.X / 16f) - 1;
            int tileCenterX = (int)((npc.Center.X) / 16f) + 2;
            int tileY = (int)(npc.position.Y / 16f) - 1;
            int tileCenterY = (int)((npc.Center.Y) / 16f) + 2;
            if (tileX < 0) { tileX = 0; } if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; } if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            bool canMove = false;
            if (fly || ignoreTiles) { canMove = true; }
            if (!canMove || spawnTileDust)
            {
                for (int tX = tileX; tX < tileCenterX; tX++)
                {
                    for (int tY = tileY; tY < tileCenterY; tY++)
                    {
                        if (Main.tile[tX, tY] != null && ((Main.tile[tX, tY].nactive() && (Main.tileSolid[(int)Main.tile[tX, tY].type] || (Main.tileSolidTop[(int)Main.tile[tX, tY].type] && Main.tile[tX, tY].frameY == 0))) || Main.tile[tX, tY].liquid > 64))
                        {
                            Vector2 tPos;
                            tPos.X = (float)(tX * 16);
                            tPos.Y = (float)(tY * 16);
                            if (npc.position.X + (float)npc.width > tPos.X && npc.position.X < tPos.X + 16f && npc.position.Y + (float)npc.height > tPos.Y && npc.position.Y < tPos.Y + 16f)
                            {
                                canMove = true;
                                if (spawnTileDust && Main.rand.Next(100) == 0 && Main.tile[tX, tY].nactive())
                                {
                                    WorldGen.KillTile(tX, tY, true, true, false);
                                }
                            }
                        }
                    }
                }
            }
            if (!canMove && npc.type == wormTypes[0])
            {
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int playerCheckDistance = 1000;
                bool canMove2 = true;
                for (int m3 = 0; m3 < 255; m3++)
                {
                    if (Main.player[m3].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[m3].position.X - playerCheckDistance, (int)Main.player[m3].position.Y - playerCheckDistance, playerCheckDistance * 2, playerCheckDistance * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            canMove2 = false;
                            break;
                        }
                    }
                }
                if (canMove2) { canMove = true; }
            }
            if (fly)
            {
                if (npc.velocity.X < 0f) { npc.spriteDirection = 1; }else if (npc.velocity.X > 0f) { npc.spriteDirection = -1; }
            }
            Vector2 npcCenter = npc.Center;
            float playerCenterX = Main.player[npc.target].Center.X;
            float playerCenterY = Main.player[npc.target].Center.Y;
            playerCenterX = (float)((int)(playerCenterX / 16f) * 16); playerCenterY = (float)((int)(playerCenterY / 16f) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16f) * 16); npcCenter.Y = (float)((int)(npcCenter.Y / 16f) * 16);
            playerCenterX -= npcCenter.X; playerCenterY -= npcCenter.Y;
            float dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
			isDigging = canMove;
            if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    npcCenter = npc.Center;
                    playerCenterX = Main.npc[(int)npc.ai[1]].Center.X - npcCenter.X;
                    playerCenterY = Main.npc[(int)npc.ai[1]].Center.Y - npcCenter.Y;
                }catch
                {
                }
                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2((double)playerCenterY, (double)playerCenterX) + 1.57f;
                }else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
                dist = (dist - (float)npc.width - (float)partDistanceAddon) / dist;
                playerCenterX *= dist;
                playerCenterY *= dist;
                npc.velocity = default(Vector2);
                npc.position.X = npc.position.X + playerCenterX;
                npc.position.Y = npc.position.Y + playerCenterY;
                if (fly)
                {
                    if (playerCenterX < 0f) { npc.spriteDirection = 1; return;  }else
                    if (playerCenterX > 0f) { npc.spriteDirection = -1; return; }
                }
            }else
            {
                if (!canMove)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > maxSpeed) { npc.velocity.Y = maxSpeed; }
                    if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.4)
                    {
                        if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - gravityResist * 1.1f; } else { npc.velocity.X = npc.velocity.X + gravityResist * 1.1f; }
                    }
                    else
                        if (npc.velocity.Y == maxSpeed)
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist; }
                        }
                        else
                            if (npc.velocity.Y > 4f)
                            {
                                if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X + gravityResist * 0.9f; } else { npc.velocity.X = npc.velocity.X - gravityResist * 0.9f; }
                            }
                }else
                {
                    if (soundEffects && npc.soundDelay == 0)
                    {
                        float distSoundDelay = dist / 40f;
                        if (distSoundDelay < 10f) { distSoundDelay = 10f; }
                        if (distSoundDelay > 20f) { distSoundDelay = 20f; }
                        npc.soundDelay = (int)distSoundDelay;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                    }
                    dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
                    float absPlayerCenterX = Math.Abs(playerCenterX);
                    float absPlayerCenterY = Math.Abs(playerCenterY);
                    float newSpeed = maxSpeed / dist;
                    playerCenterX *= newSpeed;
                    playerCenterY *= newSpeed;
                    bool dontFall = false;
                    if (fly)
                    {
                        if (((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f) || (npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > gravityResist / 2f && dist < 300f)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed) { npc.velocity *= 1.1f; }
                        }
                        if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) < maxSpeed / 2f)
                            {
                                if (npc.velocity.X == 0f) { npc.velocity.X = npc.velocity.X - (float)npc.direction; }
                                npc.velocity.X = npc.velocity.X * 1.1f;
                            }
                            else
                                if (npc.velocity.Y > -maxSpeed) { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                        }
                    }
                    if (!dontFall)
                    {
                        if ((npc.velocity.X > 0f && playerCenterX > 0f) || (npc.velocity.X < 0f && playerCenterX < 0f) || (npc.velocity.Y > 0f && playerCenterY > 0f) || (npc.velocity.Y < 0f && playerCenterY < 0f))
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist; }
                            if (npc.velocity.Y < playerCenterY) { npc.velocity.Y = npc.velocity.Y + gravityResist; }
                            else
                                if (npc.velocity.Y > playerCenterY) { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                            if ((double)Math.Abs(playerCenterY) < (double)maxSpeed * 0.2 && ((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f)))
                            {
                                if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + gravityResist * 2f; } else { npc.velocity.Y = npc.velocity.Y - gravityResist * 2f; }
                            }
                            if ((double)Math.Abs(playerCenterX) < (double)maxSpeed * 0.2 && ((npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)))
                            {
                                if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + gravityResist * 2f; } else { npc.velocity.X = npc.velocity.X - gravityResist * 2f; }
                            }
                        }
                        else
                            if (absPlayerCenterX > absPlayerCenterY)
                            {
                                if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist * 1.1f; }
                                else
                                    if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist * 1.1f; }

                                if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
                                {
                                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + gravityResist; } else { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < playerCenterY) { npc.velocity.Y = npc.velocity.Y + gravityResist * 1.1f; }
                                else
                                    if (npc.velocity.Y > playerCenterY) { npc.velocity.Y = npc.velocity.Y - gravityResist * 1.1f; }
                                if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
                                {
                                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + gravityResist; } else { npc.velocity.X = npc.velocity.X - gravityResist; }
                                }
                            }
                    }
                }
                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                }else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                if (npc.type == wormTypes[0])
                {
                    if (canMove)
                    {
                        if (npc.localAI[0] != 1f) { npc.netUpdate = true; }
                        npc.localAI[0] = 1f;
                    }
                    else
                    {
                        if (npc.localAI[0] != 0f) { npc.netUpdate = true; }
                        npc.localAI[0] = 0f;
                    }
                    if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
                    {
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
        }

		/*
		 * A cleaned up (and edited) copy of Teleporter AI. (Fire Imps, Tim, Chaos Elementals, etc.) (AIStyle 8)
		 * 
		 * ai : A float array that stores AI data.
		 * checkGround: If true, npc checks for ground underneath where it teleports to.
		 * immobile : If true, npc's x velocity is continuously shrunken until it stops moving.
		 * distFromPlayer: The amount (in tiles) from the player to have a chance to teleport to.
		 * teleportInterval : The time until the npc attempts to teleport again.
		 * attackInterval : The time until the npc attempts to attack again. If -1, never attack.
		 * delayOnHit : If true, will delay the npc's teleporting and attacking.
		 * TeleportEffects<bool> : Action that can be used to add custom teleport effects. (if the bool is true, it's pre-teleport. If false, post-teleport.
		 * CanTeleportTo<int, int> : Action that can be used to check if the npc can teleport to a specific place.
		 * Attack : Action that can be used to have the npc periodically attack.
		 */
		public static void AITeleporter(NPC npc, ref float[] ai, bool checkGround = true, bool immobile = true, int distFromPlayer = 20, int teleportInterval = 650, int attackInterval = 100, int stopAttackInterval = 500, bool delayOnHit = true, Action<bool> TeleportEffects = null, Func<int, int, bool> CanTeleportTo = null, Action Attack = null)
        {
            npc.TargetClosest(true);
            if (immobile)
            {
                npc.velocity.X = npc.velocity.X * 0.93f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1) { npc.velocity.X = 0f; }
            }
            if (ai[0] == 0f) { ai[0] = Math.Max(0, Math.Max(teleportInterval, (teleportInterval - 150))); }
            if (ai[2] != 0f && ai[3] != 0f)
            {
                if (TeleportEffects != null) { TeleportEffects(true); }
                npc.position.X = ai[2] * 16f - (float)(npc.width / 2) + 8f;
                npc.position.Y = ai[3] * 16f - (float)npc.height;
                npc.velocity.X = 0f; npc.velocity.Y = 0f;
                ai[2] = 0f; ai[3] = 0f;
                if (TeleportEffects != null) { TeleportEffects(false); }
            }
			if (npc.justHit) { ai[0] = 0; }
			ai[0]++;
            if (attackInterval != -1 && ai[0] < stopAttackInterval && ai[0] % attackInterval == 0)
            {
                ai[1] = 30f;
                npc.netUpdate = true;
            }else
            if (ai[0] >= teleportInterval && Main.netMode != 1)
            {
                ai[0] = 1f;
                int playerTileX = (int)Main.player[npc.target].position.X / 16;
                int playerTileY = (int)Main.player[npc.target].position.Y / 16;
                int tileX = (int)npc.position.X / 16;
                int tileY = (int)npc.position.Y / 16;
                int teleportCheckCount = 0;
                bool hasTeleportPoint = false;
                //player is too far away, don't teleport.
                if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 2000f)
                {
                    teleportCheckCount = 100;
                    hasTeleportPoint = true;
                }
                while (!hasTeleportPoint && teleportCheckCount < 100)
                {
                    teleportCheckCount++;
                    int tpTileX = Main.rand.Next(playerTileX - distFromPlayer, playerTileX + distFromPlayer);
                    int tpTileY = Main.rand.Next(playerTileY - distFromPlayer, playerTileY + distFromPlayer);
                    for (int tpY = tpTileY; tpY < playerTileY + distFromPlayer; tpY++)
                    {
                        if ((tpY < playerTileY - 4 || tpY > playerTileY + 4 || tpTileX < playerTileX - 4 || tpTileX > playerTileX + 4) && (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) && (!checkGround || Main.tile[tpTileX, tpY].nactive()))
                        {
                            if ((CanTeleportTo != null && CanTeleportTo(tpTileX, tpY)) || (!Main.tile[tpTileX, tpY - 1].lava() && (!checkGround || Main.tileSolid[(int)Main.tile[tpTileX, tpY].type]) && !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1)))
                            {
                                if (attackInterval != -1) { ai[1] = 20f; }
                                ai[2] = (float)tpTileX;
                                ai[3] = (float)tpY;
                                hasTeleportPoint = true;
                                break;
                            }
                        }
                    }
                }
                npc.netUpdate = true;
            }
            if (Attack != null && attackInterval != -1 && ai[1] > 0f)
            {
                ai[1] -= 1f;
                if (ai[1] == 25f) { Attack(); }
            }
        }

        /*
         * A cleaned up (and edited) copy of Fish AI. (Goldfish, Angler Fish, etc.)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * hostile : If true, will target players.
         * ignoreNonWetPlayer : If false, npc will target players even if they are out of water.
         * ignoreWater : If true, npc will not be bound to water. (ie npc flies)
         * velMaxX/velMaxY : the max velocities on the X and Y axis, respectively.
         */
        public static void AIFish(NPC npc, ref float[] ai, bool hostile = false, bool ignoreNonWetPlayer = true, bool ignoreWater = false, float velMaxX = 3f, float velMaxY = 2f)
        {
            //if the npc is hostile and it has no direction, target the closest player.
            if (hostile && npc.direction == 0) { npc.TargetClosest(true); }
            if (ignoreWater || npc.wet)//if wet or ignore water is true...
            {
                bool hasTarget = false;
                //if hostile, get a target and check that the player found is wet.
                if (hostile)
                {
                    npc.TargetClosest(false);
                    if ((!ignoreNonWetPlayer || Main.player[npc.target].wet) && !Main.player[npc.target].dead) { hasTarget = true; }
                }
                //if the target is wet or there is no target...
                if (!hasTarget)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X *= -1f;
                        npc.direction *= -1;
                        npc.netUpdate = true;
                    }
                    if (npc.collideY)
                    {
                        npc.netUpdate = true;
                        int mult = npc.velocity.Y > 0f ? -1 : 1;
                        npc.velocity.Y = Math.Abs(npc.velocity.Y) * mult;
                        npc.directionY = 1 * mult;
                        ai[0] = 1f * mult;
                    }
                }
                //if the npc has a target that fits the requirements, attempt to move toward that target.
                if (hasTarget)
                {
                    npc.TargetClosest(true);
                    npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
                    npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * 0.1f;
                    if (npc.velocity.X > velMaxX) { npc.velocity.X = velMaxX; } if (npc.velocity.X < -velMaxX) { npc.velocity.X = -velMaxX; }
                    if (npc.velocity.Y > velMaxY) { npc.velocity.Y = velMaxY; } if (npc.velocity.Y < -velMaxY) { npc.velocity.Y = -velMaxY; }
                }
                else//otherwise, move horizontally, slowly bobbing up and down as well.
                {
                    npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
                    if (npc.velocity.X < -1f || npc.velocity.X > 1f) { npc.velocity.X = npc.velocity.X * 0.95f; }
                    if (ai[0] == -1f)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.01f;
                        if ((double)npc.velocity.Y < -0.3)
                        {
                            ai[0] = 1f;
                        }
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.01f;
                        if ((double)npc.velocity.Y > 0.3)
                        {
                            ai[0] = -1f;
                        }
                    }
                    int tileX = (int)(npc.Center.X / 16);
                    int tileY = (int)(npc.Center.Y / 16);
                    for (int m = -1; m < 3; m++)
                    {
                        if (Main.tile[tileX, tileY + m] == null) { Main.tile[tileX, tileY + m] = new Tile(); }
                    }
                    if (Main.tile[tileX, tileY - 1].liquid > 128)
                    {
                        if (Main.tile[tileX, tileY + 1].nactive() || Main.tile[tileX, tileY + 2].nactive()) { ai[0] = -1f; }
                    }
                    //if npc's y speed goes above max velocity, slow the npc down.
                    if (npc.velocity.Y > velMaxY || npc.velocity.Y < -velMaxY) { npc.velocity.Y *= 0.95f; }
                }
            }
            else
            {
                //if y velocity is 0, set the npc's velocity to a random number to get it started.
                if (Main.netMode != 1 && npc.velocity.Y == 0f)
                {
                    npc.velocity.Y = (float)Main.rand.Next(-50, -20) * 0.1f;
                    npc.velocity.X = (float)Main.rand.Next(-20, 20) * 0.1f;
                    npc.netUpdate = true;
                }
                npc.velocity.Y = npc.velocity.Y + 0.3f;
                if (npc.velocity.Y > 10f) { npc.velocity.Y = 10f; } ai[0] = 1f;
            }
            npc.rotation = npc.velocity.Y * (float)npc.direction * 0.1f;
            if ((double)npc.rotation < -0.2) { npc.rotation = -0.2f; }
            if ((double)npc.rotation > 0.2) { npc.rotation = 0.2f; }
        }

        /*
         * A cleaned up (and edited) copy of Zombie AI. (Stripped Fighter AI)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * fleeWhenDay : If true, flees when it is daytime.
         * allowBoredom : If false, npc will not get 'bored' trying to harass a target and wander off.
         * openDoors : -1 == do not interact with doors, 0 == go up to door but do not break it, 1 == attempt to break down doors, 2 == attempt to open doors.
         * velMaxX : the maximum velocity on the X axis.
         * maxJumpTilesX/maxJumpTilesY : The max tiles it can jump across and over, respectively. 
         * ticksUntilBoredom : the amount of ticks until the npc gets 'bored' following a target.
         * targetPlayers : If false, will not target players actively.
         * doorBeatCounterMax : how many beat ticks until the door is opened/broken.
         * doorCounterMax : how many ticks to iterate doorBeatCounter.
         * jumpUpPlatforms : If true, the npc will jump up if a platform is above it and it is within jumping range.
         */
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

        /*
         * A cleaned up copy of Demon Eye AI. (Flier AI)
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * fleeWhenDay : If true, npc will lose interest in players and fly away.
         * ignoreWet : If true, ignores code for forcing the npc out of water.
         * velMaxX, velMaxY : the maximum velocity on the X and Y axis, respectively.
         * bounceScalarX, bounceScalarY : scalars to increase the amount of velocity from bouncing on the X and Y axis, respectively.
         */
        public static void AIEye(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool ignoreWet = false, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f, float bounceScalarX = 1f, float bounceScalarY = 1f)
        {
            //controls the npc's bouncing when it hits a wall.
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f) { npc.velocity.X = 2f; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f) { npc.velocity.X = -2f; }
                npc.velocity.X *= bounceScalarX;
            }
            //controls the npc's bouncing when it hits a floor or ceiling.
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
                npc.velocity.Y *= bounceScalarY;
            }
            //if it should flee when it's day, and it is day, the npc's position is at or above the surface, it will flee.
            if (fleeWhenDay && Main.dayTime && (double)npc.position.Y <= Main.worldSurface * 16.0)
            {
                if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                npc.directionY = -1;
                if (npc.velocity.Y > 0f) { npc.direction = 1; }
                npc.direction = -1;
                if (npc.velocity.X > 0f) { npc.direction = 1; }
            }else
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f) { npc.direction = 1; }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f) { npc.direction = 1; }
                }
            }
            //controls momentum when going left, and clamps velocity at -velMaxX.
            if (npc.direction == -1 && npc.velocity.X > -velMaxX)
            {
                npc.velocity.X = npc.velocity.X - moveIntervalX;
                if (npc.velocity.X > 4f) { npc.velocity.X = npc.velocity.X - 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + 0.05f; }
                if (npc.velocity.X < -4f) { npc.velocity.X = -velMaxX; }
            }
            else //controls momentum when going right on the x axis and clamps velocity at velMaxX.
                if (npc.direction == 1 && npc.velocity.X < velMaxX)
                {
                    npc.velocity.X = npc.velocity.X + moveIntervalX;
                    if (npc.velocity.X < -velMaxX) { npc.velocity.X = npc.velocity.X + 0.1f; }
                    else
                        if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - 0.05f; }

                    if (npc.velocity.X > velMaxX) { npc.velocity.X = velMaxX; }
                }
            //controls momentum when going up on the Y axis and clamps velocity at -velMaxY.
            if (npc.directionY == -1 && (double)npc.velocity.Y > -velMaxY)
            {
                npc.velocity.Y = npc.velocity.Y - moveIntervalY;
                if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y = npc.velocity.Y - 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + 0.03f; }

                if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y = -velMaxY; }
            }
            else //controls momentum when going down on the Y axis and clamps velocity at velMaxY.
                if (npc.directionY == 1 && (double)npc.velocity.Y < velMaxY)
                {
                    npc.velocity.Y = npc.velocity.Y + moveIntervalY;
                    if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y = npc.velocity.Y + 0.05f; }
                    else
                        if (npc.velocity.Y < 0f) { npc.velocity.Y = npc.velocity.Y - 0.03f; }

                    if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y = velMaxY; }
                }
            if (!ignoreWet && npc.wet) //if don't ignore being wet and is wet, accelerate upwards to get out.
            {
                if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y * 0.95f; }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -velMaxY * 1.5f) { npc.velocity.Y = -velMaxY * 1.5f; }
                npc.TargetClosest(true);
                return;
            }
        }

        /*
         * A cleaned up copy of Slime AI.
         * 
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * jumpTime : the amount of cooldown after the slime has jumped.
         */
        public static void AISlime(NPC npc, ref float[] ai, bool fleeWhenDay = false, int jumpTime = 200, float jumpVelX = 2f, float jumpVelY = 6f, float jumpVelHighX = 3f, float jumpVelHighY = 8f)
        {
            //ai[0] is a timer that iterates after the npc has jumped. If it is >= 0, the npc will attempt to jump again.
            //ai[1] is used to determine what jump type to do. (if 2, large jump, else smaller jump.)
            //ai[2] is used for target updating. 
            //ai[3] is used to house the landing position of the npc for bigger jumps. This is used to make it turn around when it hits
            //      an impassible wall.

			//if (jumpTime < 100) { jumpTime = 100; }
            bool getNewTarget = false; //getNewTarget is used to iterate the 'boredom' scale. If it's night, the npc is hurt, or it's
            //below a certain depth, it will attempt to find the nearest target to it.
            if ((fleeWhenDay && !Main.dayTime) || npc.life != npc.lifeMax || (double)npc.position.Y > Main.worldSurface * 16.0)
            {
                getNewTarget = true;
            }
            if (ai[2] > 1f) { ai[2] -= 1f; }
            if (npc.wet)//if the npc is wet...
            {
                //handles the npc's 'bobbing' in water.
                if (npc.collideY) { npc.velocity.Y = -2f; }
                if (npc.velocity.Y < 0f && ai[3] == npc.position.X) { npc.direction *= -1; ai[2] = 200f; }
                if (npc.velocity.Y > 0f) { ai[3] = npc.position.X; }
                if (npc.velocity.Y > 2f) { npc.velocity.Y = npc.velocity.Y * 0.9f; }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f) { npc.velocity.Y = -4f; }
                //if ai[2] is 1f, and we should get a target, target nearby players.
                if (ai[2] == 1f && getNewTarget) { npc.TargetClosest(true); }
            }
            npc.aiAction = 0;
            //if ai[2] is 0f (just spawned)
            if (ai[2] == 0f)
            {
                ai[0] = -100f;
                ai[2] = 1f;
                npc.TargetClosest(true);
            }
            //if npc is not jumping or falling
            if (npc.velocity.Y == 0f)
            {
                if (ai[3] == npc.position.X) { npc.direction *= -1; ai[2] = 200f; }
                ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if (npc.velocity.X > -0.1f && npc.velocity.X < 0.1f){ npc.velocity.X = 0f; }
                if (getNewTarget) { ai[0] += 1f; }
                ai[0] += 1f;
                if (ai[0] >= 0f)
                {
                    npc.netUpdate = true;
                    if (ai[2] == 1f && getNewTarget) { npc.TargetClosest(true); }
                    if (ai[1] == 2f) //larger jump
                    {
						npc.velocity.Y = jumpVelHighY;
                        npc.velocity.X += jumpVelHighX * npc.direction;
                        ai[0] = -jumpTime;
                        ai[1] = 0f;
                        ai[3] = npc.position.X;
                    }else //smaller jump
                    {
                        npc.velocity.Y = jumpVelY;
                        npc.velocity.X += jumpVelX * npc.direction;
                        ai[0] = -jumpTime - 80f;
                        ai[1] += 1f;
                    }
                }else
                if (ai[0] >= -30f) { npc.aiAction = 1; return; }
            }else //handle moving the npc while in air.
            if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                    return;
                }
                npc.velocity.X = npc.velocity.X * 0.93f;
                return;
            }
        }

        #endregion

        #region Vanilla NPC AI Code Excerpts
        //Code Excerpts are pieces of code from vanilla AI that were converted into standalone methods.

		public static void WalkupHalfBricks(NPC npc)
		{
			WalkupHalfBricks(npc, ref npc.gfxOffY, ref npc.stepSpeed);
		}

		/*
		 *  Code based on vanilla halfbrick walkup code, checks for and attempts to walk over half tiles.
		 */
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

        /*
         *  Code based on vanilla jumping code, checks for and attempts to jump over tiles and gaps when needed.
         *  
         *  direction/directionY : the direction and directionY of the object jumping (usually an NPC)
         *  tileDistX/tileDistY : the tile amounts the object can jump across and over, respectively.
         *  float maxSpeedX : The maximum speed of the npc.
         */
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
										newVelocity.Y -= 0.0325f;
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
										newVelocity.Y -= 0.0325f;
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


        /*
         * Attempts to interact with a door.
         * 
         * Returns : true if it found and is trying to open a door, false otherwise.
         * doorBeatCounter : counter that goes from 0-10. When it hits 10 or more the door is opened.
         * doorCounter : counter that goes from 0-60. When it hits 60 it increments doorBeatCounter by one.
         * tickUpdater : counter that goes from 0-60+. See AIZombie() on what projectile is.
         * ticksUntilBoredom : See AIZombie() on what projectile is.
         * interactDoorStyle : 0 == hit door but don't break it, 1 == smash down door, 2 == open door.
         */
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
                            if (Main.netMode == 2 && openedDoor)
                            {
                                NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX, (float)tileY, (float)npc.direction, 0);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        #endregion


        /*
         * Checks if a space is completely devoid of solid tiles.
         */
		public static bool EmptyTiles(Rectangle rect)
		{
			int topX = rect.X / 16, topY = rect.Y / 16;
			for(int x = topX; x < topX + rect.Width; x++)
			{
				for(int y = topY; x < topY + rect.Height; y++)
				{
					Tile tile = Main.tile[x, y];
					if(tile != null && tile.nactive() && Main.tileSolid[tile.type])
					{
						return false;
					}
				}
			}
			return true;
		}
		
        /*
         * Checks if a Entity object (Player, NPC, Item or Projectile) has hit a tile on it's sides.
         * 
         * noYMovement : If true, will not calculate unless the Entity is not moving on the Y axis.
         */
        public static bool HitTileOnSide(Entity codable, int dir, bool noYMovement = true)
        {
            if (!noYMovement || codable.velocity.Y == 0f)
            {
                Vector2 dummyVec = default(Vector2);
                return HitTileOnSide(codable.position, codable.width, codable.height, dir, ref dummyVec);
            }
            return false;
        }

        /*
         * Checks if a bounding box has hit a tile on it's sides.
         * 
         * position : the position of the bounding box.
         * width : the width of the bounding box.
         * height : the height of the bounding box.
         * dir : The direction to check. 0 == left, 1 == right, 2 == up, 3 == down.
         * hitTilePos : A Vector2 that is set to the hit tile position, if it hit one.
         */
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

        /*
         * specialized version of DropItem to be used specifically for boss bags.
		 
		 * player: The player that is receiving the boss bag loot. If null, works like DropItem.
         */
        public static int DropItemBossBag(Player player, Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false)
        {
			if(player != null)
			{
				if ((float)Main.rand.NextDouble() <= chance) player.QuickSpawnItem(type, amt);
				return -2;
			}else
			{
				return DropItem(codable, type, amt, maxStack, chance, clusterItem);
			}
		}

		public static int DropItem(Entity codable, int type, int amt, int maxStack, int chance, bool clusterItem = false)
		{
			return DropItem(codable, type, amt, maxStack, (float)chance / 100f, clusterItem);
		}

        /*
         * Drops an item from a codable, and returns the item's whoAmI. Mostly convenience for mp support.
         * If it drops more then one item it will return the last item dropped's whoAmI.
         * 
         * amt : the amount of the item to drop.
         * maxStack : The max stack count per item. (only applies if clusterItem == true)
         * chance : 0-1. The percent chance of the item drop. If projectile is not 100 and the item does not drop, projectile method returns -1.
         * clusterItem : If true, it will stick the drops into stacks that fit to the item's maxStack value. If false it drops them as individual items.
         */
        public static int DropItem(Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false, bool sync = false)
        {
            int itemID = -1;
            if ((sync || Main.netMode != 1) && (float)Main.rand.NextDouble() <= chance)
            {
                if (clusterItem)
                {
                    int stackCount = 0;
                    int stackCount2 = 0;
                    while (stackCount != amt)
                    {
                        stackCount++; stackCount2++;
                        if (stackCount == amt || stackCount2 == maxStack)
                        {
                            itemID = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, stackCount2, false, 0);
							if(sync) NetMessage.SendData(21, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);
                            stackCount2 = 0;
                        }
                    }
                }else
                {
                    int count = 0;
                    while (count < amt)
                    {
                        count++;
                        itemID = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, 1, false, 0);
						if(sync) NetMessage.SendData(21, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);						
                    }
                }
            }
            return itemID;
        }



		/* THESE TWO ARE WIP - BROKEN IN TMODLOADER */
		/*public static LootRule AddLoot(object npcTypes, int type, int amtMin, int amtMax, int chance)
		{
			return AddLoot(npcTypes, type, amtMin, amtMax, (float)chance / 100f);
		}
		public static LootRule AddLoot(object npcTypes, int type, int amtMin, int amtMax, float chance)
		{
			LootRule rule = new LootRule().Chance(chance).Item(type).Stack(amtMin, amtMax);
			if (npcTypes is int) LootRule.AddFor((int)npcTypes, rule);
			else if (npcTypes is int[]) LootRule.AddFor((int[])npcTypes, rule);
			else if (npcTypes is string) LootRule.AddFor((string)npcTypes, rule);
			else if (npcTypes is string[]) LootRule.AddFor((string[])npcTypes, rule);
			else if (npcTypes is Tuple<int, int>) LootRule.AddFor((Tuple<int, int>)npcTypes, rule);
			return rule;
		}*/



        public static void DamagePlayer(Player player, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
        {
            int hitDirection = damager == null ? 0 : damager.direction;
            DamagePlayer(player, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense);
        }

        /*
         *  Damages the player by the given amount.
         * 
         *  dmgAmt : The amount of damage to inflict.
         *  knockback : The amount of knockback to inflict.
         *  hitDirection : The direction of the damage.
         *  damager : The thing actually doing damage (Player, Projectile, NPC or null)
         *  dmgVariation : If true, the damage will vary based on Main.DamageVar().
         *  hitThroughDefense : If true, boosts damage to get around player defense.
		 *  critChance: the chance to crit. (0 == no crits)
		 *  critMult: the amount to multiply the crit by.
         */
        public static void DamagePlayer(Player player, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, int critChance = 0, float critMult = 1f)
        {
            //TODO: FIX THIS
            if(hitThroughDefense){ dmgAmt += (int)(player.statDefense * 0.5f); }
            if (damager == null)
            {
                int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }
                int dmgDealt = (int)player.Hurt(PlayerDeathReason.ByOther(-1), parsedDamage, hitDirection, false, false, false, 0);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(26, -1, -1, PlayerDeathReason.LegacyDefault().GetDeathText(player.name), player.whoAmI, (float)hitDirection, (float)1, knockback, parsedDamage);
                }
            }else
            if (damager is Player)
            {
                Player subPlayer = (Player)damager;
				//bool crit = false;
				//if (critChance > 0) { crit = Main.rand.Next(1, 101) <= critChance; }
				//float mult = 2f;
                //TODO: fix these by adding in the tmodloader equivilants

				//player.ItemDamagePVP(subPlayer, hitDirection, ref dmgAmt, ref crit, ref mult);
				//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DamagePVP(player, subPlayer, hitDirection, ref dmgAmt, ref crit, ref mult); });
				//ItemDef.RunEquipMethod(player, (item) => { item.DamagePVP(player, subPlayer, hitDirection, ref dmgAmt, ref crit, ref mult); }, true, true, false, true);

                int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }

				int dmgDealt = (int)player.Hurt(PlayerDeathReason.ByPlayer(subPlayer.whoAmI), parsedDamage, hitDirection, true, false, false, 0);

				//crit = false;
				//player.ItemDealtPVP(subPlayer, hitDirection, dmgAmt, crit);
				//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DealtPVP(player, subPlayer, hitDirection, dmgAmt, crit); });
				//ItemDef.RunEquipMethod(player, (item) => { item.DealtPVP(player, subPlayer, hitDirection, dmgAmt, crit); }, true, true, false, true);

                if (Main.netMode != 0)
                {
                    NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByPlayer(subPlayer.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, (float)1, knockback, parsedDamage);
                }
                subPlayer.attackCD = (int)(subPlayer.itemAnimationMax * 0.33f);
            }else
            if (damager is Projectile)
            {
                Projectile p = (Projectile)damager;
                if(p.friendly)
                {
					//bool crit = false; float mult = 2f;
                    //TODO: fix these by adding in the tmodloader equivilants

					//p.DamagePVP(player, hitDirection, ref dmgAmt, ref crit, ref mult);

                    int parsedDamage = dmgAmt; if (dmgVariation) { parsedDamage = Main.DamageVar((float)dmgAmt); }
                    int dmgDealt = (int)player.Hurt(PlayerDeathReason.ByProjectile(p.owner, p.whoAmI), parsedDamage, hitDirection, true, false, false, 0);
					
					//crit = false;
					//p.DealtPVP(player, hitDirection, dmgDealt, crit);
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByProjectile(p.owner, p.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, (float)1, knockback, parsedDamage);
                    }
                    p.playerImmune[player.whoAmI] = 40;
                }else
                if(p.hostile)
                {
					//bool crit = false; float mult = 2f;
					//p.DamagePlayer(player, hitDirection, ref dmgAmt, ref crit, ref mult);
                    
					int parsedDamage = dmgAmt; if (dmgVariation) { parsedDamage = Main.DamageVar((float)dmgAmt); }
                    int dmgDealt = (int)player.Hurt(PlayerDeathReason.ByProjectile(-1, p.whoAmI), parsedDamage, hitDirection, false, false, false, 0);
					
					//crit = false;
					//p.DealtPlayer(player, hitDirection, dmgDealt, crit);
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByProjectile(p.owner, p.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, (float)1, knockback, parsedDamage);
                    }
                }
            }else
            if (damager is NPC)
            {
                NPC npc = (NPC)damager;

				//bool crit = false; float mult = 2f;
                //TODO: fix these by adding in the tmodloader equivilants

				//npc.DamagePlayer(player, hitDirection, ref dmgAmt, ref crit, ref mult);
				//BuffDef.RunBuffMethod(npc, (modbuff) => { modbuff.DamagePlayer(npc, player, hitDirection, ref dmgAmt, ref crit, ref mult); });
				//player.NPCDamagePlayer(npc, hitDirection, ref dmgAmt, ref crit, ref mult);
				//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DamagePlayer(player, npc, hitDirection, ref dmgAmt, ref crit, ref mult); });
				//ItemDef.RunEquipMethod(player, (item) => { item.DamagePlayer(npc, player, hitDirection, ref dmgAmt, ref crit, ref mult); }, true, true, false, true);

                int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }
                int dmgDealt = (int)player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), parsedDamage, hitDirection, false, false, false, 0);

				//npc.DealtPlayer(player, hitDirection, dmgDealt, crit);
				//BuffDef.RunBuffMethod(npc, (modbuff) => { modbuff.DealtPlayer(npc, player, hitDirection, dmgAmt, crit); });
				//player.NPCDealtPlayer(npc, hitDirection, dmgDealt, crit);
				//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DealtPlayer(player, npc, hitDirection, dmgAmt, crit); });
				//ItemDef.RunEquipMethod(player, (item) => { item.DealtPlayer(npc, player, hitDirection, dmgAmt, crit); }, true, true, false, true);

                if (Main.netMode != 0)
                {
                    NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByNPC(npc.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, (float)1, knockback, parsedDamage);
                }
            }
        }

        /*
         *  Damages the given NPC by the given amount.
         */
        public static void DamageNPC(NPC npc, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
        {
            int hitDirection = damager == null ? 0 : damager.direction;
            DamageNPC(npc, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense);
        }

        /*
         *  Damages the NPC by the given amount.
         *  
         *  dmgAmt : The amount of damage to inflict.
         *  knockback : The amount of knockback to inflict.
         *  hitDirection : The direction of the damage.
         *  damager : the thing actually doing damage (Player, Projectile or null)
         *  dmgVariation : If true, the damage will vary based on Main.DamageVar().
         *  hitThroughDefense : If true, boosts damage to get around npc defense.
         */
        public static void DamageNPC(NPC npc, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
        {
            //TODO: FIX THIS
            if (hitThroughDefense) { dmgAmt += (int)(npc.defense * 0.5f); }
            if (damager == null)
            {
                int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }
                npc.StrikeNPC(parsedDamage, knockback, hitDirection, false, false, false);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, (float)1, knockback, (float)hitDirection, parsedDamage);
                }
            }else
            if (damager is Projectile)
            {
                Projectile p = (Projectile)damager;
                if (p.owner == Main.myPlayer)
                {
					//bool crit = false;
					//float mult = 1f;
					//p.DamageNPC(npc, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult);

                    int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }
                    int resultDmg = (int)npc.StrikeNPC(parsedDamage, knockback, hitDirection, false, false, false);

					//p.DealtNPC(npc, hitDirection, resultDmg, knockback, false);
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, (float)1, knockback, (float)hitDirection, parsedDamage);
                    }
                    if (p.penetrate != 1){ npc.immune[p.owner] = 10; }
                }
            }else
            if (damager is Player)
            {
                Player player = (Player)damager;
                if (player.whoAmI == Main.myPlayer)
                {
					//bool crit = false;
					//float mult = 1f;
					//npc.DamageNPC(player, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult);
					//BuffDef.RunBuffMethod(npc, (modbuff) => { modbuff.DamageNPC(npc, player, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult); });
					//player.ItemDamageNPC(npc, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult);
					//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DamageNPC(player, npc, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult); });
					//ItemDef.RunEquipMethod(player, (item) => { item.DamageNPC(player, npc, hitDirection, ref dmgAmt, ref knockback, ref crit, ref mult); }, true, true, false, true);
                    
					int parsedDamage = dmgAmt; if (dmgVariation){ parsedDamage = Main.DamageVar((float)dmgAmt); }
                    int dmgDealt = (int)npc.StrikeNPC(parsedDamage, knockback, hitDirection, false, false, false);

					//crit = false;
					//npc.DealtNPC(player, hitDirection, dmgDealt, knockback, crit);
					//BuffDef.RunBuffMethod(npc, (modbuff) => { modbuff.DealtNPC(npc, player, hitDirection, dmgAmt, knockback, crit); });
					//player.ItemDealtNPC(npc, hitDirection, dmgDealt, knockback, crit);
					//BuffDef.RunBuffMethod(player, (modbuff) => { modbuff.DealtNPC(player, npc, hitDirection, dmgAmt, knockback, crit); });
					//ItemDef.RunEquipMethod(player, (item) => { item.DealtNPC(player, npc, hitDirection, dmgAmt, knockback, crit); }, true, true, false, true);

                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, knockback, (float)hitDirection, parsedDamage);
                    }
                    npc.immune[player.whoAmI] = player.itemAnimation;
                }
            }
        }

        /*
         * Convenience method that handles killing an NPC and having it drop loot.
         * If you want the NPC to just dissapear, use KillNPC().
         */
        public static void KillNPCWithLoot(NPC npc)
        {
            DamageNPC(npc, npc.lifeMax + npc.defense + 1, 0f, 0, null, false, true);
        }

        /*
         * Convenience method that handles killing an NPC without loot.
         */
        public static void KillNPC(NPC npc)
        {
			if(Main.netMode == 1) return;
			npc.active = false;
			int npcID = npc.whoAmI;
            Main.npc[npcID] = new NPC();
            if (Main.netMode == 2) NetMessage.SendData(23, -1, -1, null, npcID, 0f, 0f, 0f, 0, 0, 0);
        }

        /*
         * Convenience method that handles killing the projectile and removing it from the game. 
         * Can be used in Kill() methods to actually kill the projectile while you spawn other things like dust.
         */
        public static void KillProjectile(Projectile p)
        {
            if (p.owner == Main.myPlayer)
            {
                NetMessage.SendData(BaseConstants.NET_PROJ_MANUALKILL, -1, -1, NetworkText.FromLiteral(""), p.identity, (float)p.owner, 0f, 0f, 0);
            }
            p.active = false;
        }

        /*
         * Spawns a cloud of smoke given the start, width and height positions.
         * 
         * loopAmount : the amount of loops to do. Each loop produces 4 smoke gore.
         * scale : Scalar for the smoke gore.
         */
        public static void SpawnSmoke(Vector2 start, float width, float height, int loopAmount = 2, float scale = 1f)
        {
            Vector2 center = start + new Vector2(width * 0.5F, height * 0.5F);
            UnifiedRandom rand = Main.rand;
            for (int m = 0; m < loopAmount; m++)
            {
                Vector2 gorePos = new Vector2(center.X - 24f, center.Y - 24f);
                Vector2 velocityDefault = default(Vector2);
                int goreID = Gore.NewGore(gorePos, velocityDefault, Main.rand.Next(61, 64), 1f);
                Gore gore = Main.gore[goreID];
                gore.scale = scale * 1.5f;
                gore.velocity.X = rand.Next(2) == 0 ? -(gore.velocity.X + 1.5f) : gore.velocity.X + 1.5f;
                gore.velocity.Y = gore.velocity.Y + 1.5f;
                goreID = Gore.NewGore(gorePos, velocityDefault, Main.rand.Next(61, 64), 1f);
                gore = Main.gore[goreID];
                gore.scale = scale * 1.5f;
                gore.velocity.X = rand.Next(2) == 0 ? -(gore.velocity.X + 1.5f) : gore.velocity.X + 1.5f;
                gore.velocity.Y = gore.velocity.Y + 1.5f;
                goreID = Gore.NewGore(gorePos, velocityDefault, Main.rand.Next(61, 64), 1f);
                gore = Main.gore[goreID];
                gore.scale = scale * 1.5f;
                gore.velocity.X = rand.Next(2) == 0 ? -(gore.velocity.X + 1.5f) : gore.velocity.X + 1.5f;
                gore.velocity.Y = gore.velocity.Y + 1.5f;
                goreID = Gore.NewGore(gorePos, velocityDefault, Main.rand.Next(61, 64), 1f);
                gore = Main.gore[goreID];
                gore.scale = scale * 1.5f;
                gore.velocity.X = rand.Next(2) == 0 ? -(gore.velocity.X + 1.5f) : gore.velocity.X + 1.5f;
                gore.velocity.Y = gore.velocity.Y + 1.5f;
            }
        }

		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, float distance = -1, Func<Projectile, bool> CanAdd = null)
		{
			return GetProjectile(center, projType, owner, default(int[]), distance, CanAdd);
		}
		/*
		 * Gets the closest Projectile with the given type within the given distance from the center. If distance is -1, it gets the closest Projectile.
		 * 
		 * projType : If -1, check for ANY projectiles in the area. If not, check for the projectiles who match the type given.
		 * projsToExclude : An array of projectile whoAmIs to exclude from the search.
		 * distance : The distance to check.
		 */
		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = default(int[]), float distance = -1, Func<Projectile, bool> CanAdd = null)
		{
			int currentProj = -1;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (projType == -1 || proj.type == projType) && (owner == -1f || proj.owner == owner) && (distance == -1f || proj.Distance(center) < distance))
				{
					bool add = true;
					if (projsToExclude != default(int[]))
					{
						foreach (int m in projsToExclude)
						{
							if (m == proj.whoAmI) { add = false; break; }
						}
					}
					if (add && CanAdd != null && !CanAdd(proj)) { continue; }
					if (add)
					{
						distance = proj.Distance(center);
						currentProj = i;
					}
				}
			}
			return currentProj;
		}

		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return GetProjectiles(center, projType, owner, default(int[]), distance, CanAdd);
		}
		/*
		 * Gets the all Projectiles with the given type within the given distance from the center.
		 * 
         * projType : If -1, check for ANY projectiles in the area. If not, check for the projectiles who match the type given.
         * projsToExclude : An array of projectile whoAmIs to exclude from the search.
         * distance : The distance to check.
		 */
		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = default(int[]), float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> allProjs = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (projType == -1 || proj.type == projType) && (owner == -1 || proj.owner == owner) && (distance == -1 || proj.Distance(center) < distance))
				{
					bool add = true;
					if (projsToExclude != default(int[]))
					{
						foreach (int m in projsToExclude)
						{
							if (m == proj.whoAmI) { add = false; break; }
						}
					}
					if (add && CanAdd != null && !CanAdd(proj)) { continue; }
					if (add) { allProjs.Add(i); }
				}
			}
			return allProjs.ToArray();
		}


		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return GetProjectiles(center, projTypes, owner, default(int[]), distance, CanAdd);
		}

		/*
		 * Gets the all Projectiles with the given type within the given distance from the center.
		 * 
         * projType : If -1, check for ANY projectiles in the area. If not, check for the projectiles who match the type given.
         * projsToExclude : An array of projectile whoAmIs to exclude from the search.
         * distance : The distance to check.
		 */
		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, int[] projsToExclude = default(int[]), float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> allProjs = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (owner == -1 || proj.owner == owner) && (distance == -1 || proj.Distance(center) < distance))
				{
					bool isType = false;
					foreach (int type in projTypes) { if (proj.type == type) { isType = true; break; } }
					if (!isType) { continue; }
					bool add = true;
					if (projsToExclude != default(int[]))
					{
						foreach (int m in projsToExclude)
						{
							if (m == proj.whoAmI) { add = false; break; }
						}
					}
					if (add && CanAdd != null && !CanAdd(proj)) { continue; }
					if (add) { allProjs.Add(i); }
				}
			}
			return allProjs.ToArray();
		}

		/*
		 * Gets all NPCs of the given type within the given rectangle.
		 * 
		 * rect : The box to check.
		 * npcType : If -1, check for ANY npcs in the area. If not, check for the npcs who match the type given.
		 * npcsToExclude : An array of npc whoAmIs to exclude from the search.
		 */
		public static int[] GetNPCsInBox(Rectangle rect, int npcType = -1, int[] npcsToExclude = default(int[]), Func<NPC, bool> CanAdd = null)
		{
			List<int> allNPCs = new List<int>();
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy)
				{
					if (!rect.Intersects(npc.Hitbox)) continue;
					bool add = true;
					if (npcsToExclude != default(int[]))
					{
						foreach (int m in npcsToExclude)
						{
							if (m == npc.whoAmI) { add = false; break; }
						}
					}
					if (add && CanAdd != null && !CanAdd(npc)) continue;
					if (add) { allNPCs.Add(i); }
				}
			}
			return allNPCs.ToArray();
		}

        public static int GetNPC(Vector2 center, int npcType = -1, float distance = -1, Func<NPC, bool> CanAdd = null)
        {
            return GetNPC(center, npcType, default(int[]), distance, CanAdd);
        }
        /*
         * Gets the closest NPC with the given type within the given distance from the center. If distance is -1, it gets the closest NPC.
         * 
         * npcType : If -1, check for ANY npcs in the area. If not, check for the npcs who match the type given.
         * npcsToExclude : An array of npc whoAmIs to exclude from the search.
         * distance : The distance to check.
         */
		public static int GetNPC(Vector2 center, int npcType = -1, int[] npcsToExclude = default(int[]), float distance = -1, Func<NPC, bool> CanAdd = null)
        {
            int currentNPC = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && (distance == -1f || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (npcsToExclude != default(int[]))
                    {
                        foreach (int m in npcsToExclude)
                        {
                            if (m == npc.whoAmI) { add = false; break; }
                        }
                    }
					if (add && CanAdd != null && !CanAdd(npc)) { continue; }
                    if (add) 
                    {
                        distance = npc.Distance(center);
                        currentNPC = i;
                    }
                }
            }
            return currentNPC;
        }

		public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500F, Func<NPC, bool> CanAdd = null)
        {
            return GetNPCs(center, npcType, new int[0], distance, CanAdd);
        }
        /*
         * Gets all NPCs of the given type within a given distance from the center.
         * 
         * npcType : If -1, check for ANY npcs in the area. If not, check for the npcs who match the type given.
         * npcsToExclude : an array of npc whoAmIs to exclude from the search.
         * distance : the distance to check.
         */
		public static int[] GetNPCs(Vector2 center, int npcType = -1, int[] npcsToExclude = default(int[]), float distance = 500F, Func<NPC, bool> CanAdd = null)
        {
            List<int> allNPCs = new List<int>();
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && (distance == -1 || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (npcsToExclude != default(int[]))
                    {
                        foreach (int m in npcsToExclude)
                        {
                            if (m == npc.whoAmI) { add = false; break; }
                        }
                    }
					if (add && CanAdd != null && !CanAdd(npc)) { continue; }
                    if (add) { allNPCs.Add(i); }
                }
            }
            return allNPCs.ToArray();
        }

        /*
         * Gets all players colliding with the given rectangle.
         * 
         * rect : The rectangle to search.
         * playersToExclude : An array of player whoAmis that will be excluded from the search.
         */
		public static int[] GetPlayersInBox(Rectangle rect, int[] playersToExclude = default(int[]), Func<Player, bool> CanAdd = null)
        {
            List<int> allPlayers = new List<int>();
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player plr = Main.player[i];
                if (plr != null && plr.active && !plr.dead)
                {
                    if (!rect.Intersects(plr.Hitbox)) { continue; }
                    bool add = true;
                    if (playersToExclude != default(int[]))
                    {
                        foreach (int m in playersToExclude)
                        {
                            if (m == plr.whoAmI) { add = false; break; }
                        }
                    }
					if(add && CanAdd != null && !CanAdd(plr)){ continue; }
                    if (add) { allPlayers.Add(i); }
                }
            }
            return allPlayers.ToArray();
        }

        /*
         * Gets the player whoAmI connected to the player with the given name, or -1 if they aren't found.
         * 
         * aliveOnly : If true, it only returns the player whoAmI if the player is not dead.
         */
        public static int GetPlayerByName(string name, bool aliveOnly = true)
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player != null && player.active && (!aliveOnly || !player.dead))
                {
                    if (player.name == name) { return i; }
                }
            }
            return -1;
        }

		public static int GetPlayer(Vector2 center, float distance = -1, Func<Player, bool> CanAdd = null)
        {
            return GetPlayer(center, default(int[]), true, distance, CanAdd);
        }
        /*
         * Gets the closest player within the given distance from the center. If distance is -1, it gets the closest player.
         * 
         * playersToExclude : An array of player whoAmis that will be excluded from the search.
         * aliveOnly : If true, it only returns the player whoAmI if the player is not dead.
         * distance : The distance to search.
         */
		public static int GetPlayer(Vector2 center, int[] playersToExclude = default(int[]), bool activeOnly = true, float distance = -1, Func<Player, bool> CanAdd = null)
        {
            int currentPlayer = -1;
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player != null && (!activeOnly || (player.active && !player.dead)) && (distance == -1f || player.Distance(center) < distance))
                {
                    bool add = true;
                    if (playersToExclude != default(int[]))
                    {
                        foreach (int m in playersToExclude)
                        {
                            if (m == player.whoAmI) { add = false; break; }
                        }
                    }
					if (add && CanAdd != null && !CanAdd(player)) { continue; }
                    if (add) 
                    {
                        distance = player.Distance(center);
                        currentPlayer = i;
                    }
                }
            }
            return currentPlayer;
        }

		public static int[] GetPlayers(Vector2 center, float distance = 500F, Func<Player, bool> CanAdd = null)
        {
            return GetPlayers(center, default(int[]), true, distance, CanAdd);
        }
        /*
         * Gets all players within a given distance from the center.
         * 
         * playersToExclude is an array of player ids you do not want included in the array.
         * aliveOnly : If true, it only returns the player whoAmI if the player is not dead.
         */
		public static int[] GetPlayers(Vector2 center, int[] playersToExclude = default(int[]), bool aliveOnly = true, float distance = 500F, Func<Player, bool> CanAdd = null)
        {
            List<int> allPlayers = new List<int>();
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player != null && player.active && (!aliveOnly || !player.dead) && player.Distance(center) < distance)
                {
                    bool add = true;
                    if (playersToExclude != default(int[]))
                    {
                        foreach (int m in playersToExclude)
                        {
                            if (m == player.whoAmI) { add = false; break; }
                        }
                    }
					if(add && CanAdd != null && !CanAdd(player)){ continue; }
                    if (add) { allPlayers.Add(i); }
                }
            }
            return allPlayers.ToArray();
        }

        /*
         * Returns true if the player can target the given codable.
         */
        public static bool CanTarget(Player player, Entity codable)
        {
            if (codable is NPC)
            {
                NPC npc = (NPC)codable;
                return npc.life > 0 && (!npc.friendly || (npc.type == 22 && player.killGuide)) && !npc.dontTakeDamage;
            }else
            if (codable is Player)
            {
                Player player2 = (Player)codable;
                return player2.statLife > 0 && !player2.immune && (player2.hostile && (player.team == 0 || player2.team == 0 || player.team != player2.team));
            }
            return false;
        }

        /*
         * Sets the npc's target to the given target and adjusts the according variables.
         */
        public static void SetTarget(NPC npc, int target)
        {
            npc.target = target;
            if (npc.target < 0 || npc.target >= 255) { npc.target = 0; }
            npc.targetRect = Main.player[npc.target].Hitbox;
            if ((npc.target != npc.oldTarget) && !npc.collideX && !npc.collideY)
            {
                npc.netUpdate = true;
            }
        }

        /*
         * Shoots a projectile from an NPC aiming at fireTarget.
         * 
         * position/width/height : the target's position, width, and height, respectively.
         * projName : name of the projectile to fire.
         * delayTimer : a float value used to tick down before firing.
         * delayTimerMax : the amount of ticks until firing.
         * damage : how much damage to do.
         * speed : how fast the projectile flies.
         * checkCanHit : If true, check if the codable can see the target point before firing.
         * offset : offset from the center of the codable that the projectile should spawn at.
         */
        public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true, Vector2 offset = default(Vector2))
        {
            int pID = -1;
            if (damage == -1) { Projectile proj = new Projectile(); proj.SetDefaults(projType); damage = proj.damage; }
            bool properSide = (codable is NPC ? Main.netMode != 1 : codable is Projectile ? ((Projectile)codable).owner == Main.myPlayer : true);
            if (properSide)
            {
                Vector2 targetCenter = position + new Vector2(width * 0.5f, height * 0.5f);
                delayTimer--;
                if (delayTimer <= 0)
                {
                    if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
                    {
                        Vector2 fireTarget = codable.Center + offset;
                        float rot = BaseUtility.RotationTo(codable.Center, targetCenter);
                        fireTarget = BaseUtility.RotateVector(codable.Center, fireTarget, rot);
						pID = BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage, 0f, speed);
                    }
                    delayTimer = delayTimerMax;
                    if (codable is NPC) { ((NPC)codable).netUpdate = true; }
                }
            }
            return pID;
        }

        /*
         * Shoots a projectile from an NPC aiming at fireTarget.
         * 
         * projectileType : The type of projectile to be fired.
         * soundGroup / sound : The sound group and sound ID of a sound to play when shot. if either is -1, it does not produce sound.
         */
        public static int FireProjectile(Vector2 fireTarget, NPC npc, int projectileType, int damage, float knockback, float speedScalar = 1.0F, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
        {
            if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
            {
                Main.PlaySound(soundGroup, (int)npc.Center.X, (int)npc.Center.Y, sound);
            }
            if (Main.netMode != 1)
            {
                int projectileID = FireProjectile(fireTarget, npc.Center, projectileType, damage, knockback, speedScalar, hostility, owner);
                npc.netUpdate = true;
                return projectileID;
            }
            return -1;
        }

        /*
         * Shoots a projectile from another Projectile aiming at fireTarget.
         * 
         * projectileType : The type of projectile to be fired.
         * soundGroup / sound : The sound group and sound ID of a sound to play when shot. if either is -1, it does not produce sound.
         */
        public static int FireProjectile(Vector2 fireTarget, Projectile p, int projectileType, int damage, float knockback, float speedScalar = 1.0F, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
        {
            if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
            {
                Main.PlaySound(soundGroup, (int)p.Center.X, (int)p.Center.Y, sound);
            }
            if (p.owner == Main.myPlayer)
            {
                return FireProjectile(fireTarget, p.Center, projectileType, damage, knockback, speedScalar, hostility, owner);
            }
            return -1;
        }

        /*
         * Shoots a projectile from a start position aiming at fireTarget. 
         * 
         * fireTarget : The position the projectile is shooting at.
         * position : The position the projectile is shooting from.
         * projectileTypeObj : Either an int of the projectile's type, or the projectile's name, to be fired.
         * damage : How much damage the projectile should inflict.
         * knockback : How much knockback the projectile should influct.
         * speedScalar : A scalar for how fast the projectile is shot.
         * hostility : The hostility of the projectile.
         *             0 -> use default projectile hostility
         *             1 -> hurt NPCS but not Players/Townies
         *            -1 -> hurt Players/Townies but not NPCs
         *             2 -> hurt BOTH Players/Townies and NPCs
         *             3 -> hurt NEITHER Players/Townies and NPCs (inert projectile)
         */
        public static int FireProjectile(Vector2 fireTarget, Vector2 position, int projectileType, int damage, float knockback, float speedScalar = 1f, int hostility = 0, int owner = -1, Vector2 targetOffset = default(Vector2))
        {
            Vector2 rotVec = BaseUtility.RotateVector(position, position + new Vector2(speedScalar, 0f), BaseUtility.RotationTo(position, fireTarget));
            rotVec -= position;
            int projectileID = Projectile.NewProjectile(position.X, position.Y, rotVec.X, rotVec.Y, projectileType, damage, knockback, (owner != -1 ? owner : Main.myPlayer));
            Projectile proj = Main.projectile[projectileID];
			proj.velocity = rotVec;
            if (hostility != 0)
            {
				proj.friendly = (hostility == 1 || hostility == 2);
				proj.hostile = (hostility == -1 || hostility == 2);
				if (Main.netMode != 0) { MNet.SendBaseNetMessage(0, proj.owner, proj.identity, proj.friendly, proj.hostile); }
            }
			proj.netUpdate2 = true;
            return projectileID;
        }


        public static void Look(Projectile p, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            Look(p, ref p.rotation, ref p.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
        }
        public static void Look(NPC npc, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            Look(npc, ref npc.rotation, ref npc.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
        }
        /*
         * Makes the rotation value and sprite direction 'look' based on factors from the Entity.
         * lookType : the type of look code to run.
         *        0 -> Rotates the entity and changes spriteDirection based on velocity.
         *        1 -> changes spriteDirection based on velocity.
         *        2 -> Rotates the entity based on velocity.
         *        3 -> Smoothly rotate and change sprite direction based on velocity.
         *        4 -> Smoothly rotate based on velocity. 
         * rotAddon : the amount to add to the rotation. (only used by lookType 3/4)
         * rotAmount: the amount to rotate by. (only used by lookType 3/4)
         */
        public static void Look(Entity c, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            LookAt(c.position + c.velocity, c.position, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
        }

        public static void LookAt(Vector2 lookTarget, Entity c, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            int spriteDirection = (c is NPC ? ((NPC)c).spriteDirection : c is Projectile ? ((Projectile)c).spriteDirection : 0);
            float rotation = (c is NPC ? ((NPC)c).rotation : c is Projectile ? ((Projectile)c).rotation : 0f);
            LookAt(lookTarget, c.Center, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
            if (c is NPC)
            {
                ((NPC)c).spriteDirection = spriteDirection;
                ((NPC)c).rotation = rotation;
            }else
            if (c is Projectile)
            {
                ((Projectile)c).spriteDirection = spriteDirection;
                ((Projectile)c).rotation = rotation;
            }
        }

        /*
         * Makes the rotation value and sprite direction 'look' at the given target.
         * lookType : the type of look code to run.
         *        0 -> Rotate the entity and change sprite direction based on the look target.
         *        1 -> change spriteDirection based on the look target.
         *        2 -> Rotate the entity based on the look target.
         *        3 -> Smoothly rotate and change sprite direction based on the look target.
         *        4 -> Smoothly rotate based on the look target.       
         * rotAddon : the amount to add to the rotation. (only used by lookType 3/4)
         * rotAmount: the amount to rotate by. (only used by lookType 3/4)
         */
        public static void LookAt(Vector2 lookTarget, Vector2 center, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.075f, bool flipSpriteDir = false)
        {
            if (lookType == 0)
            {
                if (lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
                if (flipSpriteDir) { spriteDirection *= -1; }
                float rotX = lookTarget.X - center.X;
                float rotY = lookTarget.Y - center.Y;
                rotation = -((float)Math.Atan2((double)rotX, (double)rotY) - 1.57f + rotAddon);
                if (spriteDirection == 1) { rotation -= (float)Math.PI; }
            }else
            if (lookType == 1)
            {
                if (lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
                if (flipSpriteDir) { spriteDirection *= -1; }
            }else
            if (lookType == 2)
            {
                float rotX = lookTarget.X - center.X;
                float rotY = lookTarget.Y - center.Y;
                rotation = -((float)Math.Atan2((double)rotX, (double)rotY) - 1.57f + rotAddon);
            }else
			if (lookType == 3 || lookType == 4)
			{
				int oldDirection = spriteDirection;
				if (lookType == 3 && lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
				if (lookType == 3 && flipSpriteDir) { spriteDirection *= -1; }
				if (oldDirection != spriteDirection)
				{
					rotation += (float)Math.PI * spriteDirection;
				}
				float pi2 = (float)Math.PI * 2f;
				float rotX = lookTarget.X - center.X;
				float rotY = lookTarget.Y - center.Y;
				float rot = ((float)Math.Atan2((double)rotY, (double)rotX) + rotAddon);
				if (spriteDirection == 1) { rot += (float)Math.PI; }
				if (rot > pi2) { rot -= pi2; } else if (rot < 0) { rot += pi2; }
				if (rotation > pi2) { rotation -= pi2; } else if (rotation < 0) { rotation += pi2; }
				if (rotation < rot)
				{
					if ((double)(rot - rotation) > (float)Math.PI) { rotation -= rotAmount; } else { rotation += rotAmount; }
				}else
				if (rotation > rot)
				{
					if ((double)(rotation - rot) > (float)Math.PI) { rotation += rotAmount; } else { rotation -= rotAmount; }
				}
				if (rotation > rot - rotAmount && rotation < rot + rotAmount) { rotation = rot; }
			}
        }

		public static void RotateTo(ref float rotation, float rotDestination, float rotAmount = 0.075f)
		{
			float pi2 = (float)Math.PI * 2f;
			float rot = rotDestination;
			if (rot > pi2) { rot -= pi2; } else if (rot < 0) { rot += pi2; }
			if (rotation > pi2) { rotation -= pi2; } else if (rotation < 0) { rotation += pi2; }
			if (rotation < rot)
			{
				if ((double)(rot - rotation) > (float)Math.PI) { rotation -= rotAmount; } else { rotation += rotAmount; }
			}else
			if (rotation > rot)
			{
				if ((double)(rotation - rot) > (float)Math.PI) { rotation += rotAmount; } else { rotation -= rotAmount; }
			}
			if (rotation > rot - rotAmount && rotation < rot + rotAmount) { rotation = rot; }
		}

        public static Vector2 TraceTile(Vector2 start, float distance, float rotation, Vector2 ignoreTile, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
        {
            Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
            return Trace(start, end, ignoreTile, 1, npcCheck, tileCheck, playerCheck, 1F, ignorePlatforms);
        }

        public static Vector2 TracePlayer(Vector2 start, float distance, float rotation, int ignorePlayer, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
        {
            Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
            return Trace(start, end, ignorePlayer, 0, npcCheck, tileCheck, playerCheck, 1F, ignorePlatforms);
        }

        public static Vector2 TraceNPC(Vector2 start, float distance, float rotation, int ignoreNPC, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
        {
            Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
            return Trace(start, end, ignoreNPC, 2, npcCheck, tileCheck, playerCheck, 1F, ignorePlatforms);
        }

        public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, float Jump = 1F, bool ignorePlatforms = true)
        {
            return Trace(start, end, ignore, ignoreType, null, npcCheck, tileCheck, playerCheck, false, Jump, ignorePlatforms);
        }

        public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, float Jump = 1F, bool ignorePlatforms = true)
        {
            return Trace(start, end, ignore, ignoreType, dim, npcCheck, tileCheck, playerCheck, returnCenter, (ignorePlatforms ? new int[] { 19 } : default(int[])), Jump); //ignores wooden platforms
        }

        /* **Code edited from Yoraiz0r's 'Holowires' Mod!**
         * 
         * From the start point, it iterates to the end point. If it hits anything on the way, it will return the collision point. If not it returns the end point.
         * 
         * dim : a Rectangle instance of the collision's dimensions. Can be null.
         * npcCheck : If true, Check for npc collision while iterating.
         * tileCheck : If true, check for tile collision while iterating.
         * playerCheck : If true, check for player collision while iterating.
         * returnCenter : If true, if it hits anything it returns it's center instead of where it hit.
         * tileTypesToIgnore : An array of tile types that it will assume it can trace through.
         * Jump: The amount to iterate by.
         */
        public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, int[] tileTypesToIgnore = default(int[]), float Jump = 1F)
        {
			try
			{
            if (ignore == null) { return start; }
            if (dim == null) { dim = new Rectangle(0, 0, 1, 1); }
            if (start.X < 0) { start.X = 0; } if (start.X > Main.maxTilesX * 16) { start.X = Main.maxTilesX * 16; }
            if (start.Y < 0) { start.Y = 0; } if (start.Y > Main.maxTilesY * 16) { start.Y = Main.maxTilesY * 16; }
            if (end.X < 0) { end.X = 0; } if (end.X > Main.maxTilesX * 16) { end.X = Main.maxTilesX * 16; }
            if (end.Y < 0) { end.Y = 0; } if (end.Y > Main.maxTilesY * 16) { end.Y = Main.maxTilesY * 16; }
            Vector2 TC = new Vector2(1, 1);
            Vector2 Pstart = start;
            Vector2 Pend = end;
            Vector2 dir = Pend - Pstart;
            dir.Normalize();
            float length = Vector2.Distance(Pstart, Pend);
            float Way = 0f;
            while (Way < length)
            {
                Vector2 v = (Pstart + dir * Way) + TC;			
                Rectangle dimensions = (Rectangle)dim;
                Rectangle posRect = new Rectangle((int)v.X - (dimensions.Width == 1 ? 0 : dimensions.Width / 2), (int)v.Y - (dimensions.Height == 1 ? 0 : dimensions.Height / 2), dimensions.Width, dimensions.Height);
                if (tileCheck)
                {
                    int vecX = (int)v.X / 16;
                    int vecY = (int)v.Y / 16;
                    Rectangle rect = new Rectangle((int)v.X, (int)v.Y, 16, 16);
                    if (posRect.Intersects(rect))
                    {
                        Vector2 vec = ignoreType == 1 ? (Vector2)ignore : new Vector2(-1, -1);
                        if ((int)vec.X != vecX && (int)vec.Y != vecY)
                        {
                            Tile tile = Main.tile[vecX, vecY];
                            if (tile != null && tile.nactive())
                            {
                                bool ignoreTile = (tileTypesToIgnore == null || tileTypesToIgnore.Length <= 0 ? false : BaseUtility.InArray(tileTypesToIgnore, tile.type));
                                if (!ignoreTile && Main.tileSolid[(int)tile.type])
                                {
                                    return returnCenter ? new Vector2((vecX * 16) + 8, (vecY * 16) + 8) : v;
                                }
                            }
                        }
                    }
                }
                if (npcCheck)
                {
                    int[] npcs = GetNPCs(v, -1, 5F);
                    for (int i = 0; i < npcs.Length; i++)
                    {
                        NPC npc = Main.npc[npcs[i]];
                        if (!npc.active || npc.life <= 0) { continue; }
                        if (ignoreType == 2 && npc.whoAmI == (int)ignore) { continue; }
                        Rectangle npcRect = new Rectangle((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height);
                        if (posRect.Intersects(npcRect)) { return returnCenter ? npc.Center : v; }
                    }
                }
                if (playerCheck)
                {
                    int[] players = GetPlayers(v, 5F);
                    for (int i = 0; i < players.Length; i++)
                    {
                        Player player = Main.player[players[i]];
                        if (player.dead || !player.active) { continue; }
                        if (ignoreType == 0 && player.whoAmI == (int)ignore) { continue; }
                        Rectangle playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height);
                        if (posRect.Intersects(playerRect)) { return returnCenter ? player.Center : v; }
                    }
                }
                Way += Jump;
            }
			}catch(Exception e){ ErrorLogger.Log("TRACE ERROR: " + e.Message); ErrorLogger.Log(e.StackTrace); ErrorLogger.Log("--------"); }
            return end;
        }

        /*
         * 
         * Returns an array of points in a straight line from start to end.
         * jump : the interval between points.
         */
        public static Vector2[] GetLinePoints(Vector2 start, Vector2 end, float jump = 1f)
        {
            Vector2 dir = end - start;
            dir.Normalize();
            float length = Vector2.Distance(start, end); float way = 0f;
            float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
            List<Vector2> vList = new List<Vector2>();
            while (way < length)
            {
                vList.Add(start + dir * way);
                way += jump;
            }
            return vList.ToArray();
        }
    }
}