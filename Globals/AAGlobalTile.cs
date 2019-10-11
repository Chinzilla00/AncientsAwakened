using AAMod.Tiles.Plants;
using AAMod.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
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

        public static Color GetIncineriteColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAPlayer.IncineriteColor, color, min, max, clamp);
        public static Color GetIncineriteColorDim(Color color) => GetIncineriteColor(color, 0.4f, 1f, false);
        public static Color GetIncineriteColorBright(Color color) => GetIncineriteColor(color, 0.6f, 1f, false);
        public static Color GetIncineriteColorBrightInvert(Color color) => GetIncineriteColor(color, 1f, 0.6f, true);

        public static Color GetZeroColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.ZeroShield, color, min, max, clamp);
        public static Color GetZeroColorDim(Color color) => GetZeroColor(color, 0.4f, 1f, false);
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

        public static Color GetDarkmatterColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.Nightcrawler, color, min, max, clamp);
        public static Color GetDarkmatterColorDim(Color color) => GetDarkmatterColor(color, 0.4f, 1f, false);
        public static Color GetDarkmatterColorBright(Color color) => GetDarkmatterColor(color, 0.6f, 1f, false);
        public static Color GetDarkmatterColorBrightInvert(Color color) => GetDarkmatterColor(color, 1f, 0.6f, true);

        public static Color GetRadiumColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.Daybringer, color, min, max, clamp);
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

        public static Color GetShenColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.Shen2, color, min, max, clamp);
        public static Color GetShenColorDim(Color color) => GetShenColor(color, 0.4f, 1f, false);
        public static Color GetShenColorBright(Color color) => GetShenColor(color, 0.6f, 1f, false);
        public static Color GetShenColorBrightInvert(Color color) => GetShenColor(color, 1f, 0.6f, true);

        public static Color GetSkyColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.Sky, color, min, max, clamp);
        public static Color GetSkyColorDim(Color color) => GetSkyColor(color, 0.4f, 1f, false);
        public static Color GetSkyColorBright(Color color) => GetSkyColor(color, 0.6f, 1f, false);
        public static Color GetSkyColorBrightInvert(Color color) => GetSkyColor(color, 1f, 0.6f, true);

        public static Color GetBlankColor(Color color, float min, float max, bool clamp) => GetTimedColor(AAColor.COLOR_WHITEFADE1, color, min, max, clamp);
        public static Color GetBlankColorDim(Color color) => GetBlankColor(color, 0.4f, 1f, false);
        public static Color GetBlankColorBright(Color color) => GetBlankColor(color, 0.6f, 1f, false);
        public static Color GetBlankColorBrightInvert(Color color) => GetBlankColor(color, 1f, 0.6f, true);

        public static Color GetRainbowColor(Color color, float min, float max, bool clamp) => GetTimedColor(Main.DiscoColor, color, min, max, clamp);
        public static Color GetRainbowColorDim(Color color) => GetRainbowColor(color, 0.4f, 1f, false);
        public static Color GetRainbowColorBright(Color color) => GetRainbowColor(color, 0.6f, 1f, false);
        public static Color GetRainbowColorBrightInvert(Color color) => GetRainbowColor(color, 1f, 0.6f, true);

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

            return base.Drop(i, j, type);
        }

        public static Color GetTimedColor(Color tColor, Color color, float min, float max, bool clamp)
        {
            Color glowColor = BaseMod.BaseUtility.ColorMult(tColor, BaseMod.BaseUtility.MultiLerp(glowTick / (float)glowMax, min, max, min));

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
            Color glowColor = Color.Lerp(tColor1, tColor2, BaseMod.BaseUtility.MultiLerp(glowTick / (float)glowMax, 0f, 1f, 0f));

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
    }
}

