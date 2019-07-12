using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Bogtus : ModCactus
	{
        private Mod mod => ModLoader.GetMod("AAMod");

        public override Texture2D GetTexture()
		{
			return mod.GetTexture("Tiles/Bogtus");
		}
    }
}