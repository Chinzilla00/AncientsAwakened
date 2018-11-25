using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Bogtus : ModCactus
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("AAMod");
			}
		}

		public override Texture2D GetTexture()
		{
			return mod.GetTexture("Glowmasks/Bogtus");
		}
    }
}