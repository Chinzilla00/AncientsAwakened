using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Mounts
{
	public class BegPony : ModMountData
	{
		public override void SetDefaults()
		{
            mountData.spawnDust = DustID.Smoke;
            mountData.buff = ModContent.BuffType<Buffs.PrettyPony>();
            mountData.heightBoost = 44;
            mountData.flightTimeMax = 0;
            mountData.fallDamage = 0f;
            mountData.runSpeed = 6f;
            mountData.dashSpeed = 16f;
            mountData.acceleration = 0.5f;
            mountData.jumpHeight = 14;
            mountData.jumpSpeed = 9.01f;
            mountData.totalFrames = 16;
            int[] array = new int[mountData.totalFrames];
            for (int num6 = 0; num6 < array.Length; num6++)
            {
                array[num6] = 28;
            }
            array[3] += 2;
            array[4] += 2;
            array[7] += 2;
            array[8] += 2;
            array[12] += 2;
            array[13] += 2;
            array[15] += 4;
            mountData.playerYOffsets = array;
            mountData.xOffset = 5;
            mountData.bodyFrame = 3;
            mountData.yOffset = 3;
            mountData.playerHeadOffset = 31;
            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 7;
            mountData.runningFrameDelay = 15;
            mountData.runningFrameStart = 1;
            mountData.dashingFrameCount = 6;
            mountData.dashingFrameDelay = 40;
            mountData.dashingFrameStart = 9;
            mountData.flyingFrameCount = 6;
            mountData.flyingFrameDelay = 6;
            mountData.flyingFrameStart = 1;
            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 15;
            mountData.idleFrameCount = 0;
            mountData.idleFrameDelay = 0;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = false;
            mountData.swimFrameCount = mountData.inAirFrameCount;
            mountData.swimFrameDelay = mountData.inAirFrameDelay;
            mountData.swimFrameStart = mountData.inAirFrameStart;
            if (Main.netMode != 2)
            {
                mountData.backTexture = mod.GetTexture("Mounts/BegPony");
                mountData.backTextureExtra = null;
                mountData.frontTexture = null;
                mountData.frontTextureExtra = null;
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }

		public override void UpdateEffects(Player player)
		{
            player.doubleJumpUnicorn = true;
            if (Math.Abs(player.velocity.X) > player.mount.DashSpeed - player.mount.RunSpeed / 2f)
            {
                player.noKnockback = true;
            }
            if (player.dashDelay > 0)
            {
                player.dashDelay--;
            }
            else
            {
                float num4 = 0;
                bool flag = false;
                if (player.dashTime > 0)
                {
                    player.dashTime--;
                }
                else if (player.dashTime < 0)
                {
                    player.dashTime++;
                }
                if (player.controlRight && player.releaseRight)
                {
                    if (player.dashTime > 0)
                    {
                        num4 = 1.4f;
                        flag = true;
                        player.dashTime = 0;
                    }
                    else
                    {
                        player.dashTime = 15;
                    }
                }
                else if (player.controlLeft && player.releaseLeft)
                {
                    if (player.dashTime < 0)
                    {
                        num4 = -1.4f;
                        flag = true;
                        player.dashTime = 0;
                    }
                    else
                    {
                        player.dashTime = -15;
                    }
                }
                if (flag)
                {
                    player.velocity.X = 16.9f * num4;
                    Point point = Utils.ToTileCoordinates(player.Center + new Vector2(num4 * player.width / 2 + 2, player.gravDir * -player.height / 2f + player.gravDir * 2f));
                    Point point2 = Utils.ToTileCoordinates(player.Center + new Vector2(num4 * player.width / 2 + 2, 0f));
                    if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                    {
                        player.velocity.X /= 2f;
                    }
                    player.dashDelay = 300;
                }
            }
        }
	}
}