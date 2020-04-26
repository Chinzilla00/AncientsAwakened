using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    public class Bogtus : ModCactus
	{
        private Mod mod => AAMod.instance;

        public override Texture2D GetTexture()
		{
			return mod.GetTexture("Tiles/Trees/Bogtus");
		}
    }
}