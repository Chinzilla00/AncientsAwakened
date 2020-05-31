using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class TerraWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == mod.GetSurfaceBgStyleSlot("TerraSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("TerraWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("TerraWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Water/TerraDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Globals.AAColor.TerraGlow;
		}
	}
}