
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
            return GetTimedColor(Color.Lime, color, min, max, clamp);
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
                }
            }
        }
    }
}

