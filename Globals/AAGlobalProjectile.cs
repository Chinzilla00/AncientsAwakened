using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            if (projectile.bobber)
            {
                if(Main.player[projectile.owner].GetModPlayer<AAPlayer>().StripeManFish)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if(rectangle.Intersects(value) && projectile.ai[1] > 0f)
                    {
                        Item item = new Item();
                        int itemtype = 0;
                        int projectileX = (int)(projectile.Center.X / 16f);
			            int projectileY = (int)(projectile.Center.Y / 16f);
                        int WorldHeightType;
                        if ((double)projectileY < Main.worldSurface * 0.5)
                        {
                            WorldHeightType = 0;
                        }
                        else if ((double)projectileY < Main.worldSurface)
                        {
                            WorldHeightType = 1;
                        }
                        else if ((double)projectileY < Main.rockLayer)
                        {
                            WorldHeightType = 2;
                        }
                        else if (projectileY < Main.maxTilesY - 300)
                        {
                            WorldHeightType = 3;
                        }
                        else
                        {
                            WorldHeightType = 4;
                        }
                        if (Main.rand.Next(100) < 20f)
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                itemtype = 2336;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneCorrupt)
                            {
                                itemtype = 3203;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneCrimson)
                            {
                                itemtype = 3204;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneHoly)
                            {
                                itemtype = 3207;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneDungeon)
                            {
                                itemtype = 3205;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneJungle)
                            {
                                itemtype = 3208;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneSnow)
                            {
                                itemtype = mod.ItemType("IceCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneDesert)
                            {
                                itemtype = mod.ItemType("DesertCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneInferno)
                            {
                                itemtype = mod.ItemType("InfernoCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneMire)
                            {
                                itemtype = mod.ItemType("MireCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneVoid)
                            {
                                itemtype = mod.ItemType("VoidCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneHoard)
                            {
                                itemtype = ItemID.GoldenCrate;
                            }
                            else if (Main.rand.Next(3) == 0 && Main.player[projectile.owner].ZoneUnderworldHeight)
                            {
                                itemtype = mod.ItemType("HellCrate");
                            }
                            else if (Main.rand.Next(3) == 0 && WorldHeightType == 0)
                            {
                                itemtype = 3206;
                            }
                            else if (Main.rand.Next(2) == 0)
                            {
                                itemtype = 2335;
                            }
                            else
                            {
                                itemtype = 2334;
                            }
                        }
                        bool junk = false;
                        int liquidtype = 0;
                        int tileX = 0;
                        int tileY = 0;
                        while(tileX < 20 && tileY < 20)
                        {
                            if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].lava())
                            {
                                liquidtype = 1;
                            }
                            else if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].honey())
                            {
                                liquidtype = 2;
                            }
                            tileY ++;
                            if(tileY >= 20)
                            {
                                tileX ++;
                                tileY = 0;
                            }
                        }
                        while(itemtype == 0) {PlayerHooks.CatchFish(Main.player[projectile.owner], Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem], int.MaxValue, liquidtype, int.MaxValue, WorldHeightType, 0, ref itemtype, ref junk);}
                        item.SetDefaults(itemtype, false);
                        ItemLoader.CaughtFishStack(item);
						item.newAndShiny = true;
                        Item CreatItem = Main.player[projectile.owner].GetItem(projectile.owner, item, false, false);
                        if (CreatItem.stack > 0)
                        {
                            int number = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, item.type, 1, false, 0, true, false);
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                            }
                        }
                        else
                        {
                            item.position.X = projectile.Center.X - (float)(item.width / 2);
                            item.position.Y = projectile.Center.Y - (float)(item.height / 2);
                            item.active = true;
                            ItemText.NewText(item, 0, false, false);
                        }
                    }
                }
            }

            base.PostAI(projectile);
        }


        public static void Convert(int i, int j, int size = 4)
        {
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
