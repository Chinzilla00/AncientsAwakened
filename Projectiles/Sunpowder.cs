using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Walls;
using AAMod.Dusts;
using AAMod.Tiles;

namespace AAMod.Projectiles
{
    internal class Sunpowder : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
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
            int dustType = mod.DustType<BroodmotherDust>();
            projectile.velocity *= 0.95f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] == 180f)
            {
                projectile.Kill();
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                for (int num62 = 0; num62 < 30; num62++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 50);
                }
            }
            int num63 = (int)(projectile.position.X / 16f) - 1;
            int num64 = (int)((projectile.position.X + projectile.width) / 16f) + 2;
            int num65 = (int)(projectile.position.Y / 16f) - 1;
            int num66 = (int)((projectile.position.Y + projectile.height) / 16f) + 2;
            if (num63 < 0)
            {
                num63 = 0;
            }
            if (num64 > Main.maxTilesX)
            {
                num64 = Main.maxTilesX;
            }
            if (num65 < 0)
            {
                num65 = 0;
            }
            if (num66 > Main.maxTilesY)
            {
                num66 = Main.maxTilesY;
            }
            if (projectile.owner == Main.myPlayer)
            {
                Convert((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16);
            }
        }

        public void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        if (type == (ushort)mod.WallType<DepthstoneWall>())
                        {
                            Main.tile[k, l].wall = WallID.Stone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)mod.WallType<DepthsandstoneWall>())
                        {
                            Main.tile[k, l].wall = WallID.Sandstone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)mod.WallType<DepthsandHardenedWall>())
                        {
                            Main.tile[k, l].wall = WallID.HardenedSand;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<Depthstone>())
                        {
                            Main.tile[k, l].type = 1;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<MireGrass>())
                        {
                            Main.tile[k, l].type = 60;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<IndigoIce>())
                        {
                            Main.tile[k, l].type = 161;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<Depthsandstone>())
                        {
                            Main.tile[k, l].type = 396;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<Depthsand>())
                        {
                            Main.tile[k, l].type = 53;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == mod.TileType<DepthsandHardened>())
                        {
                            Main.tile[k, l].type = 397;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
            }
        }
    }
}
