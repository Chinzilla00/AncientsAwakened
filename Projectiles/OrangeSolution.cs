using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OrangeSolution : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orange Spray");
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
            int dustType = mod.DustType("OrangeSolution");
            if (projectile.owner == Main.myPlayer)
			{
				Convert((int)(projectile.position.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16, 2);
			}
			if (projectile.timeLeft > 133)
			{
				projectile.timeLeft = 133;
			}
			if (projectile.ai[0] > 7f)
			{
				float dustScale = 1f;
				if (projectile.ai[0] == 8f)
				{
					dustScale = 0.2f;
				}
				else if (projectile.ai[0] == 9f)
				{
					dustScale = 0.4f;
				}
				else if (projectile.ai[0] == 10f)
				{
					dustScale = 0.6f;
				}
				else if (projectile.ai[0] == 11f)
				{
					dustScale = 0.8f;
				}
				projectile.ai[0] += 1f;
				for (int i = 0; i < 1; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(200, 100, 0), 1f);
                    Dust dust = Main.dust[dustIndex];
					dust.noGravity = true;
					dust.scale *= 1.75f;
					dust.velocity.X = dust.velocity.X * 2f;
					dust.velocity.Y = dust.velocity.Y * 2f;
					dust.scale *= dustScale;
				}
			}
			else
			{
				projectile.ai[0] += 1f;
			}
			projectile.rotation += 0.3f * projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt((size * size) + (size * size)))
					{
						int type = Main.tile[k, l].type;
						int wall = Main.tile[k, l].wall;
						if (wall != 0)
                        if (wall == 1)
                        {
                            Main.tile[k, l].wall = (ushort)mod.WallType("TorchstonestoneWall");
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if (wall == WallID.Sandstone || wall == WallID.CrimsonSandstone || wall == WallID.CorruptSandstone || wall == WallID.HallowSandstone)
                        {
                            Main.tile[k, l].wall = (ushort)mod.WallType("TorchsandstoneWall");
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if (wall == WallID.HardenedSand || wall == WallID.CrimsonHardenedSand || wall == WallID.CorruptHardenedSand || wall == WallID.HallowHardenedSand)
                        {
                            Main.tile[k, l].wall = (ushort)mod.WallType("TorchsandHardenedWall");
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if (type == 2)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("InfernoGrass");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == 1)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("Torchstone");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == 191)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("LivingRazewood");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == 192)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("LivingRazeleaves");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == TileID.Sand || type == TileID.Crimsand || type == TileID.Ebonsand || type == TileID.Pearlsand)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("Torchsand");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == TileID.HardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.CorruptHardenedSand || type == TileID.HallowHardenedSand)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("TorchsandHardened");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == TileID.Sandstone || type == TileID.CrimsonSandstone || type == TileID.CorruptSandstone || type == TileID.HallowSandstone)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("Torchsandstone");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == TileID.IceBlock || type == TileID.FleshIce || type == TileID.CorruptIce || type == TileID.HallowedIce)
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType("Torchice");
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
			}
		}
	}
}
