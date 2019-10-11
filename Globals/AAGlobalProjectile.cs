using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles;

namespace AAMod
{
    public class AAGlobalProjectile : GlobalProjectile
    {

        public static int CountProjectiles(int type)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    num++;
                }
            }

            return num;
        }

        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.PureSpray)
            {
                Convert((int)(projectile.position.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16);
            }

            base.PostAI(projectile);
        }


        public static void Convert(int i, int j, int size = 4)
        {
            Mod mod = AAMod.instance;

            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        if (Main.tile[k, l].type == ModContent.TileType<InfernoGrass>() || Main.tile[k, l].type == ModContent.TileType<MireGrass>() || Main.tile[k, l].type == ModContent.TileType<Mycelium>() || Main.tile[k, l].type == ModContent.TileType<Doomgrass>())
                        {
                            Main.tile[k, l].type = 2;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == ModContent.TileType<Torchstone>() || Main.tile[k, l].type == ModContent.TileType<Depthstone>() || Main.tile[k, l].type == ModContent.TileType<DoomstoneB>())
                        {
                            Main.tile[k, l].type = 1;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == ModContent.TileType<Torchsand>() || Main.tile[k, l].type == ModContent.TileType<Depthsand>())
                        {
                            Main.tile[k, l].type = 53;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == ModContent.TileType<TorchsandHardened>() || Main.tile[k, l].type == ModContent.TileType<DepthsandHardened>())
                        {
                            Main.tile[k, l].type = 397;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == ModContent.TileType<Torchsandstone>() || Main.tile[k, l].type == ModContent.TileType<Depthsandstone>())
                        {
                            Main.tile[k, l].type = 396;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == ModContent.TileType<Torchice>() || Main.tile[k, l].type == ModContent.TileType<IndigoIce>())
                        {
                            Main.tile[k, l].type = 161;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }
    }
}
