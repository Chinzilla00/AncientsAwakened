
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
            glowTick++; if (glowTick >= glowMax) glowTick = 0;
        }

        public static Color GetIncineriteColorBrightInvert(Color color) { return GetIncineriteColor(color, 1f, 0.6f, true); }
        public static Color GetIncineriteColorDim(Color color) { return GetIncineriteColor(color, 0.4f, 1f, false); }
        public static Color GetIncineriteColorBright(Color color) { return GetIncineriteColor(color, 0.6f, 1f, false); }
        public static Color GetIncineriteColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAPlayer.IncineriteColor, color, min, max, clamp);
        }
        public static Color GetZeroColorBrightInvert(Color color) { return GetZeroColor(color, 1f, 0.6f, true); }
        public static Color GetZeroColorDim(Color color) { return GetZeroColor(color, 0.4f, 1f, false); }
        public static Color GetZeroColorBright(Color color) { return GetZeroColor(color, 0.6f, 1f, false); }
        public static Color GetZeroColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAColor.ZeroShield, color, min, max, clamp);
        }

        public static Color GetTerraColorBrightInvert(Color color) { return GetTerraColor(color, 1f, 0.6f, true); }
        public static Color GetTerraColorDim(Color color) { return GetTerraColor(color, 0.4f, 1f, false); }
        public static Color GetTerraColorBright(Color color) { return GetTerraColor(color, 0.6f, 1f, false); }
        public static Color GetTerraColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.LimeGreen, color, min, max, clamp);
        }

        public static Color GetTerra2ColorBrightInvert(Color color) { return GetTerra2Color(color, 1f, 0.6f, true); }
        public static Color GetTerra2ColorDim(Color color) { return GetTerra2Color(color, 0.4f, 1f, false); }
        public static Color GetTerra2ColorBright(Color color) { return GetTerra2Color(color, 0.6f, 1f, false); }
        public static Color GetTerra2Color(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.YellowGreen, color, min, max, clamp);
        }

        public static Color GetUraniumColorBrightInvert(Color color) { return GetUraniumColor(color, 1f, 0.6f, true); }
        public static Color GetUraniumColorDim(Color color) { return GetUraniumColor(color, 0.4f, 1f, false); }
        public static Color GetUraniumColorBright(Color color) { return GetUraniumColor(color, 0.6f, 1f, false); }
        public static Color GetUraniumColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.DarkSeaGreen, color, min, max, clamp);
        }

        public static Color GetstormColorBrightInvert(Color color) { return GetstormColor(color, 1f, 0.6f, true); }
        public static Color GetstormColorDim(Color color) { return GetstormColor(color, 0.4f, 1f, false); }
        public static Color GetstormColorBright(Color color) { return GetstormColor(color, 0.6f, 1f, false); }
        public static Color GetstormColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.Violet, color, min, max, clamp);
        }

        public static Color GetAkumaColorBrightInvert(Color color) { return GetAkumaColor(color, 1f, 0.6f, true); }
        public static Color GetAkumaColorDim(Color color) { return GetAkumaColor(color, 0.4f, 1f, false); }
        public static Color GetAkumaColorBright(Color color) { return GetAkumaColor(color, 0.6f, 1f, false); }
        public static Color GetAkumaColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.DeepSkyBlue, color, min, max, clamp);
        }

        public static Color GetDarkmatterColorBrightInvert(Color color) { return GetDarkmatterColor(color, 1f, 0.6f, true); }
        public static Color GetDarkmatterColorDim(Color color) { return GetDarkmatterColor(color, 0.4f, 1f, false); }
        public static Color GetDarkmatterColorBright(Color color) { return GetDarkmatterColor(color, 0.6f, 1f, false); }
        public static Color GetDarkmatterColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAColor.Nightcrawler, color, min, max, clamp);
        }

        public static Color GetRadiumColorBrightInvert(Color color) { return GetRadiumColor(color, 1f, 0.6f, true); }
        public static Color GetRadiumColorDim(Color color) { return GetRadiumColor(color, 0.4f, 1f, false); }
        public static Color GetRadiumColorBright(Color color) { return GetRadiumColor(color, 0.6f, 1f, false); }
        public static Color GetRadiumColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAColor.Daybringer, color, min, max, clamp);
        }


        public static Color GetYamataColorBrightInvert(Color color) { return GetYamataColor(color, 1f, 0.6f, true); }
        public static Color GetYamataColorDim(Color color) { return GetYamataColor(color, 0.4f, 1f, false); }
        public static Color GetYamataColorBright(Color color) { return GetYamataColor(color, 0.6f, 1f, false); }
        public static Color GetYamataColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.Maroon, color, min, max, clamp);
        }

        public static Color GetCthulhuColorBrightInvert(Color color) { return GetCthulhuColor(color, 1f, 0.6f, true); }
        public static Color GetCthulhuColorDim(Color color) { return GetCthulhuColor(color, 0.4f, 1f, false); }
        public static Color GetCthulhuColorBright(Color color) { return GetCthulhuColor(color, 0.6f, 1f, false); }
        public static Color GetCthulhuColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.DarkCyan, color, min, max, clamp);
        }

        public static Color GetShenColorBrightInvert(Color color) { return GetShenColor(color, 1f, 0.6f, true); }
        public static Color GetShenColorDim(Color color) { return GetShenColor(color, 0.4f, 1f, false); }
        public static Color GetShenColorBright(Color color) { return GetShenColor(color, 0.6f, 1f, false); }
        public static Color GetShenColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAColor.Shen2, color, min, max, clamp);
        }

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
            Color glowColor = BaseMod.BaseUtility.ColorMult(tColor, BaseMod.BaseUtility.MultiLerp((float)glowTick / (float)glowMax, min, max, min));
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
            Color glowColor = Color.Lerp(tColor1, tColor2, BaseMod.BaseUtility.MultiLerp((float)glowTick / (float)glowMax, 0f, 1f, 0f));
            if (clamp)
            {
                if (color.R > glowColor.R) { glowColor.R = color.R; }
                if (color.G > glowColor.G) { glowColor.G = color.G; }
                if (color.B > glowColor.B) { glowColor.B = color.B; }
            }
            return glowColor;
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (Main.tile[i, j - 1].active() &&
                (Main.tile[i, j].type == mod.TileType<Tiles.ChaosAltar1>() || Main.tile[i, j].type == mod.TileType<Tiles.ChaosAltar2>()))
            {
                return false;
            }
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanExplode(int i, int j, int type)
        {
            if (Main.tile[i, j - 1].active() &&
                (Main.tile[i, j].type == mod.TileType<Tiles.ChaosAltar1>() || Main.tile[i, j].type == mod.TileType<Tiles.ChaosAltar2>()) && (Main.tile[i, j - 1].type != mod.TileType<Tiles.ChaosAltar1>() || Main.tile[i, j - 1].type != mod.TileType<Tiles.ChaosAltar2>()))
            {
                return false;
            }
            return base.CanExplode(i, j, type);
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.tile[i, j].type == TileID.MushroomGrass)
            {
                if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(500) == 0)
                {
                    int style = Main.rand.Next(5);
                    if (PlaceObject(i, j - 1, mod.TileType<Tiles.MadnessShroom>(), false, style))
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType<Tiles.MadnessShroom>(), style, 0, -1, -1);
                }
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
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

    public class Beanz : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            if (Main.rand.Next(15) == 1)
            {
                if (Main.tile[i, j].type == 5 && Main.tile[i, j + 1].type != 5)
                {
                    if (!Main.player[Main.myPlayer].ZoneJungle)
                    {
                        Item.NewItem(i * 16, j * 16, 26, 26, mod.ItemType("CocoaBean"), 1, false, -1, false);
                    }
                }
            }
            return true;
        }
    }
}

