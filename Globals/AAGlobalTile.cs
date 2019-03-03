
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
		public static Color GetIncineriteColorBright(Color color){ return GetIncineriteColor(color, 0.6f, 1f, false); }
		public static Color GetIncineriteColor(Color color, float min, float max, bool clamp)
		{
			return GetTimedColor(AAPlayer.IncineriteColor, color, min, max, clamp);
		}
        public static Color GetZeroColorBrightInvert(Color color) { return GetZeroColor(color, 1f, 0.6f, true); }
        public static Color GetZeroColorDim(Color color) { return GetZeroColor(color, 0.4f, 1f, false); }
        public static Color GetZeroColorBright(Color color) { return GetZeroColor(color, 0.6f, 1f, false); }
        public static Color GetZeroColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(AAPlayer.ZeroColor, color, min, max, clamp);
        }

        public static Color GetTerraColorBrightInvert(Color color) { return GetTerraColor(color, 1f, 0.6f, true); }
        public static Color GetTerraColorDim(Color color) { return GetTerraColor(color, 0.4f, 1f, false); }
        public static Color GetTerraColorBright(Color color) { return GetTerraColor(color, 0.6f, 1f, false); }
        public static Color GetTerraColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.Green, color, min, max, clamp);
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

        public static Color GetYamataColorBrightInvert(Color color) { return GetstormColor(color, 1f, 0.6f, true); }
        public static Color GetYamataColorDim(Color color) { return GetstormColor(color, 0.4f, 1f, false); }
        public static Color GetYamataColorBright(Color color) { return GetstormColor(color, 0.6f, 1f, false); }
        public static Color GetYamataColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.Maroon, color, min, max, clamp);
        }

        public static Color GetCthulhuColorBrightInvert(Color color) { return GetstormColor(color, 1f, 0.6f, true); }
        public static Color GetCthulhuColorDim(Color color) { return GetstormColor(color, 0.4f, 1f, false); }
        public static Color GetCthulhuColorBright(Color color) { return GetstormColor(color, 0.6f, 1f, false); }
        public static Color GetCthulhuColor(Color color, float min, float max, bool clamp)
        {
            return GetTimedColor(Color.DarkCyan, color, min, max, clamp);
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
            if (type == mod.TileType<Tiles.Doomstone>() && TileID.Sets.BreakableWhenPlacing[mod.TileType<Tiles.Doomstone>()]) //placing grass
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

        public static void GenAAOres(bool itemSpawn)
        {
            if (Main.netMode == 1) { AANet.SendNetMessage(AANet.GenOre, (byte)0); }
            else
            {
                Mod mod = AAMod.instance;
                float percent = (float)Main.maxTilesX / 4300f;
                int count = (int)((Main.expertMode ? 350f : 300f) * percent);
                if (itemSpawn) count = (int)(200f * percent);
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                    if (Main.tile[tilesX, tilesY].type == TileID.Mud)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EverleafRoot"));
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("AbyssiumOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == TileID.Stone)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("IncineriteOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == 59)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EventideAbyssiumOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == 1)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DaybreakIncineriteOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == 117)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)mod.TileType("HallowedOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == 397)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DynaskullOre"));
                    }
                    WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("FulguriteOre"));
                    int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    double num9 = Main.worldSurface;
                    int j2 = WorldGen.genRand.Next((int)((Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0), Main.maxTilesY - 150);

                    WorldGen.OreRunner(i2, WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 150), (double)WorldGen.genRand.Next(5, 9 + 4), WorldGen.genRand.Next(5, 9 + 4), (ushort)mod.TileType("YtriumOre"));
                    WorldGen.OreRunner(i2, WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 150), (double)WorldGen.genRand.Next(5, 9 + 3), WorldGen.genRand.Next(5, 9 + 3), (ushort)mod.TileType("UraniumOre"));
                    WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(5, 9 + 2), WorldGen.genRand.Next(5, 9 + 2), (ushort)mod.TileType("TechneciumOre"));
                }
            }
        }
    }
}

