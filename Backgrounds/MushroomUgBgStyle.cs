using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class MushroomUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneMush;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/MushroomUG2");
			textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/MushroomUG1");
			textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/MushroomUG4");
			textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/MushroomUG3");
		}
	}
}