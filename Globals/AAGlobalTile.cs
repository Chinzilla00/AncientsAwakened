using AAMod.Tiles.Plants;
using AAMod.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Blocks;

namespace AAMod.Globals
{
    public class AAGlobalTile : GlobalTile
    {
        public static int glowTick = 0;
        public static int glowMax = 100;

        public override void AnimateTile()
        {
            glowTick++;
            if (glowTick >= glowMax)
            {
                glowTick = 0;
            }
        }

        #region Tile Colors

        public static Color GetIncineriteColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAPlayer.IncineriteColor, color, min, max, clamp);
        public static Color GetIncineriteColorDim(Color color) => GetIncineriteColor(color, 0.4f, 1f, false);
        public static Color GetIncineriteColorBright(Color color) => GetIncineriteColor(color, 0.6f, 1f, false);
        public static Color GetIncineriteColorBrightInvert(Color color) => GetIncineriteColor(color, 1f, 0.6f, true);

        public static Color GetZeroColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.ZeroShield, color, min, max, clamp);
        public static Color GetZeroColorDim(Color color) => GetZeroColor(color, 0.4f, .6f, false);
        public static Color GetZeroColorBright(Color color) => GetZeroColor(color, 0.6f, 1f, false);
        public static Color GetZeroColorBrightInvert(Color color) => GetZeroColor(color, 1f, 0.6f, true);

        public static Color GetTerraColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.LimeGreen, color, min, max, clamp);
        public static Color GetTerraColorDim(Color color) => GetTerraColor(color, 0.4f, 1f, false);
        public static Color GetTerraColorBright(Color color) => GetTerraColor(color, 0.6f, 1f, false);
        public static Color GetTerraColorBrightInvert(Color color) => GetTerraColor(color, 1f, 0.6f, true);

        public static Color GetTerra2Color(Color color, float min, float max, bool clamp) => GetTimedColor(Color.YellowGreen, color, min, max, clamp);
        public static Color GetTerra2ColorDim(Color color) => GetTerra2Color(color, 0.4f, 1f, false);
        public static Color GetTerra2ColorBright(Color color) => GetTerra2Color(color, 0.6f, 1f, false);
        public static Color GetTerra2ColorBrightInvert(Color color) => GetTerra2Color(color, 1f, 0.6f, true);

        public static Color GetUraniumColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.DarkSeaGreen, color, min, max, clamp);
        public static Color GetUraniumColorDim(Color color) => GetUraniumColor(color, 0.4f, 1f, false);
        public static Color GetUraniumColorBright(Color color) => GetUraniumColor(color, 0.6f, 1f, false);
        public static Color GetUraniumColorBrightInvert(Color color) => GetUraniumColor(color, 1f, 0.6f, true);

        public static Color GetStormColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.Violet, color, min, max, clamp);
        public static Color GetStormColorDim(Color color) => GetStormColor(color, 0.4f, 1f, false);
        public static Color GetStormColorBright(Color color) => GetStormColor(color, 0.6f, 1f, false);
        public static Color GetStormColorBrightInvert(Color color) => GetStormColor(color, 1f, 0.6f, true);

        public static Color GetAkumaColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.DeepSkyBlue, color, min, max, clamp);
        public static Color GetAkumaColorDim(Color color) => GetAkumaColor(color, 0.4f, 1f, false);
        public static Color GetAkumaColorBright(Color color) => GetAkumaColor(color, 0.6f, 1f, false);
        public static Color GetAkumaColorBrightInvert(Color color) => GetAkumaColor(color, 1f, 0.6f, true);

        public static Color GetDarkmatterColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.Nightcrawler, color, min, max, clamp);
        public static Color GetDarkmatterColorDim(Color color) => GetDarkmatterColor(color, 0.4f, 1f, false);
        public static Color GetDarkmatterColorBright(Color color) => GetDarkmatterColor(color, 0.6f, 1f, false);
        public static Color GetDarkmatterColorBrightInvert(Color color) => GetDarkmatterColor(color, 1f, 0.6f, true);

        public static Color GetRadiumColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.Daybringer, color, min, max, clamp);
        public static Color GetRadiumColorDim(Color color) => GetRadiumColor(color, 0.4f, 1f, false);
        public static Color GetRadiumColorBright(Color color) => GetRadiumColor(color, 0.6f, 1f, false);
        public static Color GetRadiumColorBrightInvert(Color color) => GetRadiumColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.Maroon, color, min, max, clamp);
        public static Color GetYamataColorDim(Color color) => GetYamataColor(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright(Color color) => GetYamataColor(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert(Color color) => GetYamataColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor2(Color color, float min, float max, bool clamp) => GetTimedColor(Color.Violet, color, min, max, clamp);
        public static Color GetYamataColorDim2(Color color) => GetYamataColor2(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright2(Color color) => GetYamataColor2(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert2(Color color) => GetYamataColor2(color, 1f, 0.6f, true);

        public static Color GetCthulhuColor(Color color, float min, float max, bool clamp) => GetTimedColor(Color.DarkCyan, color, min, max, clamp);
        public static Color GetCthulhuColorDim(Color color) => GetCthulhuColor(color, 0.4f, 1f, false);
        public static Color GetCthulhuColorBright(Color color) => GetCthulhuColor(color, 0.6f, 1f, false);
        public static Color GetCthulhuColorBrightInvert(Color color) => GetCthulhuColor(color, 1f, 0.6f, true);

        public static Color GetShenColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.Shen2, color, min, max, clamp);
        public static Color GetShenColorDim(Color color) => GetShenColor(color, 0.4f, 1f, false);
        public static Color GetShenColorBright(Color color) => GetShenColor(color, 0.6f, 1f, false);
        public static Color GetShenColorBrightInvert(Color color) => GetShenColor(color, 1f, 0.6f, true);

        public static Color GetSkyColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.Sky, color, min, max, clamp);
        public static Color GetSkyColorDim(Color color) => GetSkyColor(color, 0.4f, 1f, false);
        public static Color GetSkyColorBright(Color color) => GetSkyColor(color, 0.6f, 1f, false);
        public static Color GetSkyColorBrightInvert(Color color) => GetSkyColor(color, 1f, 0.6f, true);

        public static Color GetBlankColor(Color color, float min, float max, bool clamp) => GetTimedColor(Globals.AAColor.COLOR_WHITEFADE1, color, min, max, clamp);
        public static Color GetBlankColorDim(Color color) => GetBlankColor(color, 0.4f, 1f, false);
        public static Color GetBlankColorBright(Color color) => GetBlankColor(color, 0.6f, 1f, false);
        public static Color GetBlankColorBrightInvert(Color color) => GetBlankColor(color, 1f, 0.6f, true);

        public static Color GetRainbowColor(Color color, float min, float max, bool clamp) => GetTimedColor(Main.DiscoColor, color, min, max, clamp);
        public static Color GetRainbowColorDim(Color color) => GetRainbowColor(color, 0.4f, 1f, false);
        public static Color GetRainbowColorBright(Color color) => GetRainbowColor(color, 0.6f, 1f, false);
        public static Color GetRainbowColorBrightInvert(Color color) => GetRainbowColor(color, 1f, 0.6f, true);

        #endregion

        public override bool Drop(int i, int j, int type)
        {
            if (type == TileID.Dirt && TileID.Sets.BreakableWhenPlacing[TileID.Dirt]) //placing grass
            {
                return false;
            }

            if (type == TileID.Mud && TileID.Sets.BreakableWhenPlacing[TileID.Mud]) //placing grass
            {
                return false;
            }

            if (type == 28)
            {
                if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManSpawn)
                {
                    PotsDropMethod(i, j);
                }
            }

            if (Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManOre)
            {
                if(TileID.Sets.Conversion.Stone[type])
                {
                    int k = DropOreMethod(i, j, type);
                    if(k != 0) Item.NewItem(i * 16, j * 16, 32, 32, k, 1, false, 0, false, false);
                }
            }

            if (Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().AncientGoldBody)
            {
                if(TileID.Sets.Conversion.Stone[type] && Main.rand.Next(50) == 0)
                {
                    Item.NewItem(i * 16, j * 16, 32, 32, ItemID.GoldCoin, 1, false, 0, false, false);
                }
            }

            return base.Drop(i, j, type);
        }

        public static Color GetTimedColor(Color tColor, Color color, float min, float max, bool clamp)
        {
            Color glowColor = BaseUtility.ColorMult(tColor, BaseUtility.MultiLerp(glowTick / (float)glowMax, min, max, min));

            if (clamp)
            {
                if (color.R > glowColor.R) { glowColor.R = color.R; }
                if (color.G > glowColor.G) { glowColor.G = color.G; }
                if (color.B > glowColor.B) { glowColor.B = color.B; }
            }

            return glowColor;
        }

        public static Color GetGradientColor(Color tColor1, Color tColor2, Color color, bool clamp)
        {
            Color glowColor = Color.Lerp(tColor1, tColor2, BaseUtility.MultiLerp(glowTick / (float)glowMax, 0f, 1f, 0f));

            if (clamp)
            {
                if (color.R > glowColor.R)
                {
                    glowColor.R = color.R;
                }

                if (color.G > glowColor.G)
                {
                    glowColor.G = color.G;
                }

                if (color.B > glowColor.B)
                {
                    glowColor.B = color.B;
                }
            }

            return glowColor;
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (Main.tile[i, j - 1].active() && (Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar1>() || Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar2>()) && (Main.tile[i, j].type != ModContent.TileType<ChaosAltar1>() || Main.tile[i, j].type != ModContent.TileType<ChaosAltar2>()))
            {
                return false;
            }

            if (Main.tile[i, j - 1].active() && (Main.tile[i, j - 1].type == ModContent.TileType<Tiles.Boss.GreedAltar>() || Main.tile[i, j - 1].type == ModContent.TileType<Tiles.Boss.AcropolisAltar>()) && (Main.tile[i, j].type != ModContent.TileType<Tiles.Boss.GreedAltar>() || Main.tile[i, j].type != ModContent.TileType<Tiles.Boss.AcropolisAltar>()))
            {
                return false;
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanExplode(int i, int j, int type)
        {
            if (Main.tile[i, j - 1].active() && (Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar1>() || Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar2>()) && (Main.tile[i, j].type != ModContent.TileType<ChaosAltar1>() || Main.tile[i, j].type != ModContent.TileType<ChaosAltar2>()))
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

        public override bool Slope(int i, int j, int type)
        {
            if (Main.tile[i, j - 1].active() && (Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar1>() || Main.tile[i, j - 1].type == ModContent.TileType<ChaosAltar2>()) && (Main.tile[i, j].type != ModContent.TileType<ChaosAltar1>() || Main.tile[i, j].type != ModContent.TileType<ChaosAltar2>()))
            {
                return false;
            }

            if (Main.tile[i, j - 1].active() && (Main.tile[i, j - 1].type == ModContent.TileType<Tiles.Boss.GreedAltar>() || Main.tile[i, j - 1].type == ModContent.TileType<Tiles.Boss.AcropolisAltar>()) && (Main.tile[i, j].type != ModContent.TileType<Tiles.Boss.GreedAltar>() || Main.tile[i, j].type != ModContent.TileType<Tiles.Boss.AcropolisAltar>()))
            {
                return false;
            }

            return base.Slope(i, j, type);
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.tile[i, j].type == TileID.MushroomGrass)
            {
                if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(1000) == 0)
                {
                    int style = Main.rand.Next(5);

                    if (PlaceObject(i, j - 1, ModContent.TileType<MadnessShroom>(), false, style))
                    {
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<MadnessShroom>(), style, 0, -1, -1);
                    }
                }
            }

            if (Main.tile[i, j].type == TileID.Grass && Main.hardMode)
            {
                if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(800) == 0)
                {
                    if (PlaceObject(i, j - 1, ModContent.TileType<Carrot>(), false, 0))
                    {
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Carrot>(), 0, 0, -1, -1);
                    }
                }
            }

            if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManSpawn)
            {
                if(Main.rand.Next(800) == 0 && j >= WorldGen.worldSurfaceLow)
                {
                    if (Main.tile[i, j + 1].active() && Main.tileSolid[Main.tile[i, j].type] && !Main.tile[i, j - 1].lava())
                    {
                        int style = WorldGen.genRand.Next(0, 4);
                        int tiletype = 0;
                        if (j < Main.maxTilesY - 5)
                        {
                            tiletype = Main.tile[i, j + 1].type;
                        }
                        if (tiletype == 147 || tiletype == 161 || tiletype == 162)
                        {
                            style = WorldGen.genRand.Next(4, 7);
                        }
                        if (tiletype == 60)
                        {
                            style = WorldGen.genRand.Next(7, 10);
                        }
                        if (Main.wallDungeon[Main.tile[i, j].wall])
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 41 || tiletype == 43 || tiletype == 44)
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 22 || tiletype == 23 || tiletype == 25)
                        {
                            style = WorldGen.genRand.Next(16, 19);
                        }
                        if (tiletype == 199 || tiletype == 203 || tiletype == 204 || tiletype == 200)
                        {
                            style = WorldGen.genRand.Next(22, 25);
                        }
                        if (tiletype == 367)
                        {
                            style = WorldGen.genRand.Next(31, 34);
                        }
                        if (tiletype == 226)
                        {
                            style = WorldGen.genRand.Next(28, 31);
                        }
                        if (j > Main.maxTilesY - 200)
                        {
                            style = WorldGen.genRand.Next(13, 16);
                        }
                        if (WorldGen.PlacePot(i, j, 28, style))
                        {
                            NetMessage.SendObjectPlacment(-1, i, j, 28, 0, 0, -1, -1);
                        }
                    }
                }
                
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }

            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }

            return false;
        }

        public void PotsDropMethod(int i, int j)
        {
            int itemcreat = 0;
            if (WorldGen.genRand.Next(30) == 0 || (Main.rand.Next(30) == 0 && Main.expertMode))
            {
                if (WorldGen.genRand.Next(20) == 0)
                {
                    int rand = WorldGen.genRand.Next(100);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 678, 1, false, 0, false, false);
                    }
                    else
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if(rand2 == 0)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2352, 1, false, 0, false, false);
                        }
                        if(rand2 == 1)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2353, 1, false, 0, false, false);
                        }
                        if(rand2 == 2)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2756, 1, false, 0, false, false);
                        }
                    }
                }
                if(Main.rand.Next(200) == 0)
                {
                    int k = Config.LuckyPotion.Keys.Count;
                    foreach (int itempotion in Config.LuckyPotion.Keys)
			        {
                        if(Main.rand.Next(k) == 0)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, itempotion, 1, false, 0, false, false);
                            break;
                        }
                        k -= 1;
                    }
                }
                else if (Main.tile[i, j].liquid > 0)
                {
                    int rand = WorldGen.genRand.Next(3);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2354, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2355, 1, false, 0, false, false);
                    }
                    if (rand >= 2)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2356, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.worldSurface)
                {
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyironskinpotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 292, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyswiftnesspotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckycalmingpotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2324, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2325, 1, false, 0, false, false);
                    }
                    if (rand == 7 || rand == 8)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckywrathpotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2349, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 9)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.rockLayer)
                {
                    if (Main.player[Main.myPlayer].ZoneJungle)
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if (rand2 == 0)
                        {
                            if(Main.rand.Next(100) == 0)
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckysummoningpotion>();
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2328, 1, false, 0, false, false);
                            }
                        }
                        else
                        {
                            if(Main.rand.Next(100) == 0)
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckythornspotion>();
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                            }
                        }
                    }
                    else if (Main.player[Main.myPlayer].ZoneSnow)
                    {
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2359, 1, false, 0, false, false);
                        }
                    }
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyregenerationpotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 289, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyswiftnesspotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 291, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyendurancepotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2346, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 10)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.maxTilesY - 200)
                {
                    int rand = WorldGen.genRand.Next(15);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckythornspotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2327, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else
                {
                    int rand = WorldGen.genRand.Next(16);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 293, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 294, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckythornspotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 300, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        if(Main.rand.Next(100) == 0)
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckyragepotion>();
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2347, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 15)
                    {
                        if(Main.rand.Next(5) == 0)
                        {
                            if(Main.rand.Next(100) == 0)
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.luckylifeforcepotion>();
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2345, 1, false, 0, false, false);
                            }
                        }
                        else if (Main.rand.Next(2) == 0)
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                        }
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
            }
        }

        public int DropOreMethod(int x, int y, int type)
        {
            float ChanceBalance = 1;
            int SecondDrop = Config.LuckyOre.Keys.Count;
            foreach (int itemtype in Config.LuckyOre.Keys)
			{
				float chance = Config.LuckyOre[itemtype];
                chance -= Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].pick;
                chance = chance/ChanceBalance * 100f;
                if(chance < 100 && !(itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre || itemtype == ModContent.ItemType<Abyssium>() || itemtype == ModContent.ItemType<Incinerite>() || itemtype == ModContent.ItemType<Apocalyptite>()))
                {
                    if(Utils.NextFloat(Main.rand, SecondDrop) < 1)
                    {
                        int itemcreat = Item.NewItem(x * 16, y * 16, 32, 32, itemtype, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    SecondDrop -= 1;
                    continue;
                }
                else if(itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre)
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && (type == TileID.Crimstone || type == TileID.Ebonstone))
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<Abyssium>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Tiles.Depthstone>())
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<Incinerite>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Tiles.Torchstone>())
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<Apocalyptite>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Tiles.Doomstone>() && AAWorld.downedZero)
                    {
                        return itemtype;
                    }
                }
                else if(!Main.hardMode)
                {
                    if(Config.LuckyOre[itemtype] <= 500)
                    {
                        if(Utils.NextFloat(Main.rand, chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                else
                {
                    chance /= 2 * (1 + (NPC.downedPlantBoss? 1 : 0) + (NPC.downedMoonlord? 1 : 0) + (AAWorld.downedEquinox? 1 : 0) + (AAWorld.downedShen? 1 : 0));
                    int digcheck = 500 + (NPC.downedPlantBoss? 200 : 0) + (NPC.downedMoonlord? 110 : 0) + (AAWorld.downedEquinox? 20 : 0);
                    bool flag = Config.LuckyOre[itemtype] <= digcheck;
                    if(flag || AAWorld.downedShen)
                    {
                        if(Utils.NextFloat(Main.rand, chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                ChanceBalance = 1 - 1/chance;
            }
            return 0;
        }
    }
}

