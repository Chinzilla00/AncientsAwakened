using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class IncineriteWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("IncineriteWall");
            AddMapEntry(new Color(40, 30, 10));
            dustType = mod.DustType("IncineriteDust");
        }
    }
}