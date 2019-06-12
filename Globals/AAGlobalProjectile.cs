using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles;
using AAMod.Walls;

namespace AAMod
{
    public class AAGlobalProjectile : GlobalProjectile
    {
        public static int CountProjectiles(int Type)
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    num++;
                }
            }
            return num;
        }

        public static bool AnyProjectiless(int Type)
        {
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
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
                        if (Main.tile[k, l].type == mod.TileType<InfernoGrass>() || Main.tile[k, l].type == mod.TileType<MireGrass>() || Main.tile[k, l].type == mod.TileType<Mycelium>() || Main.tile[k, l].type == mod.TileType<Doomgrass>())
                        {
                            Main.tile[k, l].type = 2;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == mod.TileType<Torchstone>() || Main.tile[k, l].type == mod.TileType<Depthstone>() || Main.tile[k, l].type == mod.TileType<DoomstoneB>())
                        {
                            Main.tile[k, l].type = 1;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == mod.TileType<Torchsand>() || Main.tile[k, l].type == mod.TileType<Depthsand>())
                        {
                            Main.tile[k, l].type = 53;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == mod.TileType<TorchsandHardened>() || Main.tile[k, l].type == mod.TileType<DepthsandHardened>())
                        {
                            Main.tile[k, l].type = 397;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == mod.TileType<Torchsandstone>() || Main.tile[k, l].type == mod.TileType<Depthsandstone>())
                        {
                            Main.tile[k, l].type = 396;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].type == mod.TileType<Torchice>() || Main.tile[k, l].type == mod.TileType<DepthIce>())
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
