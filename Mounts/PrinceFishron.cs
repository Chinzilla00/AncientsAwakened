using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Mounts
{
	public class PrinceFishron : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 15;
			mountData.buff = mod.BuffType("PrinceFishron");
			mountData.heightBoost = 14;
			mountData.flightTimeMax = int.MaxValue;
			mountData.fatigueMax = int.MaxValue;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 1f;
			mountData.acceleration = 0.2f;
			mountData.jumpHeight = 4;
			mountData.jumpSpeed = 3f;
			mountData.swimSpeed = 24f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 23;
			int[] array = new int[mountData.totalFrames];
			for (int num8 = 0; num8 < array.Length; num8++)
			{
				array[num8] = 12;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 2;
			mountData.bodyFrame = 3;
			mountData.yOffset = 16;
			mountData.playerHeadOffset = 31;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 8;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 14;
			mountData.runningFrameStart = 8;
			mountData.flyingFrameCount = 8;
			mountData.flyingFrameDelay = 16;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 8;
			mountData.inAirFrameDelay = 6;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = 8;
			mountData.swimFrameDelay = 4;
			mountData.swimFrameStart = 15;
			if (Main.netMode != NetmodeID.Server)
			{
				mountData.backTexture = mod.GetTexture("Mounts/PrinceFishron");
				mountData.backTextureGlow = mod.GetTexture("Mounts/PrinceFishron_Glow");
				mountData.frontTexture = null;
				mountData.frontTextureExtra = null;
				mountData.textureWidth = mountData.backTexture.Width;
				mountData.textureHeight = mountData.backTexture.Height;
			}
		}
		
		public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			if (state == 4)
			{
				mountData.runSpeed = mountData.swimSpeed;
			}
			if (state == 2)
			{
				mountData.runSpeed = 13f;
			}
			return true;
		}

		public override void UpdateEffects(Player player)
		{
			if (Math.Abs(player.velocity.X) > 4f)
			{
				Rectangle rect = player.getRect();
				Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, 15);
			}
		}
	}
}