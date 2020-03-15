using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class TorchstoneWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("TorchstoneWall");
            AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }
    }
}