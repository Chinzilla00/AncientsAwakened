using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class MushroomTree : ModTree
    {
        private Mod mod => AAMod.instance;

        public override int DropWood()
        {
            return ItemID.Mushroom;
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Trees/MushroomTree");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Trees/MushroomTreeBranches");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return mod.GetTexture("Tiles/Trees/MushroomTreeTop");
        }
    }
}
