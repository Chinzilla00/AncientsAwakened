using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class TorchstoneWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallLight[Type] = false;
            Main.wallHouse[Type] = false;
            drop = mod.ItemType("TorchstoneWall");
            AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }
    }
}