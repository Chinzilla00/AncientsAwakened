
using Microsoft.Xna.Framework;
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
		public static Color GetIncineriteColorPylon(Color color) { return GetIncineriteColor(color, 0.65f, 1f, true); }
		public static Color GetIncineriteColor(Color color, float min, float max, bool clamp)
		{
			return GetTimedColor(AAPlayer.IncineriteColor, color, min, max, clamp);
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
	}
}

