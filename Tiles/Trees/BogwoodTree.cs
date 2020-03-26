using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class BogwoodTree : ModTree
    {
        private Mod Mod => ModLoader.GetMod("AAMod");

        public override int DropWood()
        {
            return Mod.ItemType("Bogwood");
        }

        public override Texture2D GetTexture()
        {
            return Mod.GetTexture("Tiles/Trees/BogwoodTree");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return Mod.GetTexture("Tiles/Trees/BogwoodBranches");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return Mod.GetTexture("Tiles/Trees/BogwoodTreeTop");
        }
    }
}
