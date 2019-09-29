using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class BogwoodTree : ModTree
    {
        private Mod mod => ModLoader.GetMod("AAMod");

        public override int DropWood()
        {
            return mod.ItemType("Bogwood");
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Trees/BogwoodTree");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Trees/BogwoodBranches");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            frameWidth = 114;
            frameHeight = 98;
            yOffset += 2;
            xOffsetLeft += 16;
            return mod.GetTexture("Tiles/Trees/BogwoodTreeTop");
        }
    }
}
