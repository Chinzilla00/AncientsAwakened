using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Mounts
{
	public class PrinceFishron : ModMountData
	{
		public override void SetDefaults()
		{
            mountData.spawnDust = 15;
            mountData.buff = 168;
            mountData.heightBoost = 20;
            mountData.flightTimeMax = 320;
            mountData.fatigueMax = 320;
            mountData.fallDamage = 0f;
            mountData.usesHover = true;
            mountData.runSpeed = 2f;
            mountData.dashSpeed = 1f;
            mountData.acceleration = 0.2f;
            mountData.jumpHeight = 4;
            mountData.jumpSpeed = 3f;
            mountData.swimSpeed = 16f;
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
            if (Main.netMode != 2)
            {
                mountData.backTexture = Main.cuteFishronMountTexture[0];
                mountData.backTextureGlow = Main.cuteFishronMountTexture[1];
                mountData.frontTexture = null;
                mountData.frontTextureExtra = null;
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }

        public override void UpdateEffects(Player player)
        {
            if (player.MountFishronSpecial)
            {
                Vector3 value3 = Colors.CurrentLiquidColor.ToVector3();
                value3 *= 0.4f;
                Point point = (player.Center + Vector2.UnitX * (float)player.direction * 20f + player.velocity * 10f).ToTileCoordinates();
                if (!WorldGen.SolidTile(point.X, point.Y))
                {
                    Lighting.AddLight(point.X, point.Y, value3.X, value3.Y, value3.Z);
                }
                else
                {
                    Lighting.AddLight(player.Center + Vector2.UnitX * (float)player.direction * 20f, value3.X, value3.Y, value3.Z);
                }
                player.meleeDamage += 0.20f;
                player.rangedDamage += 0.20f;
                player.magicDamage += 0.20f;
                player.minionDamage += 0.20f;
                player.thrownDamage += 0.20f;
            }
            player.MountFishronSpecialCounter = 60f;
        }
    }
}