using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class MireWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
        {
            Player player = Main.player[Main.myPlayer];

            if (Main.bgStyle == mod.GetSurfaceBgStyleSlot("MireSurfaceBgStyle") || Main.bgStyle == mod.GetSurfaceBgStyleSlot("MireDesertBgStyle") || (player.ZoneSnow && player.GetModPlayer<AAPlayer>(mod).ZoneMire))
            {
                if (!Main.dayTime || AAWorld.downedYamata || player.position.Y > Main.worldSurface * 16.0)
                {
                    return true;
                }
            }
            return false;
        }
        
		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("MireWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("MireWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Water/MireDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.DarkBlue;
		}
	}
}