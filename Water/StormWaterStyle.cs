using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class StormWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == mod.GetSurfaceBgStyleSlot("StormBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("StormWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("StormWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Water/StormDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = .7f;
			g = 0f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.Violet;
		}
	}
}