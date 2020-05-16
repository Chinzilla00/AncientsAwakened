using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.DevTools.Cinematic
{
	public class CinematicThing : ModMountData
	{
		public const float speed = 1.5f;

		public override void SetDefaults()
		{
			mountData.spawnDustNoGravity = true;
			mountData.buff = mod.BuffType("CinematicBuff");
			mountData.heightBoost = 0;
			mountData.flightTimeMax = int.MaxValue;
			mountData.fatigueMax = int.MaxValue;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 3;
			mountData.dashSpeed = 3;
			mountData.acceleration = 3;
			mountData.swimSpeed = 3;
			mountData.jumpHeight = 8;
			mountData.jumpSpeed = 3;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 1;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 0;
			}
			mountData.playerYOffsets = new int[] { 0 };
			mountData.xOffset = 16;
			mountData.bodyFrame = 5;
			mountData.yOffset = 16;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 0;
			mountData.standingFrameDelay = 0;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 0;
			mountData.runningFrameDelay = 0;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 0;
			mountData.swimFrameStart = 0;
			if (Main.netMode != NetmodeID.Server)
			{
				mountData.textureWidth = mountData.backTexture.Width;
				mountData.textureHeight = mountData.backTexture.Height;
			}
		}
	}
}