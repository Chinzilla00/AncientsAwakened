using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Brick
{
    public class IncineriteWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("IncineriteWall");
            AddMapEntry(new Color(40, 30, 10));
            dustType = mod.DustType("IncineriteDust");
        }
    }
}