using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Worldgeneration.Placeholder
{
    public class Placeholder1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(1, 1, 1));
		}
	}
	public class Placeholder2 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
			Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(1, 1, 1));
		}
	}
	public class Placeholder3 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
			Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(1, 1, 1));
		}
	}
	public class Placeholder4 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
			Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(1, 1, 1));
		}
	}
	public class Placeholder5 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
			Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(1, 1, 1));
		}
	}
}