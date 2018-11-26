using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class OroborosTree : ModTree
    {
        private Mod mod
        {
            get
            {
                return ModLoader.GetMod("AAMod");
            }
        }

        public override int DropWood()
        {
            return mod.ItemType("OroborosWood");
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/OroborosTree");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/OroborosBranches");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return mod.GetTexture("Tiles/OroborosTreeTop");
        }
    }
}
