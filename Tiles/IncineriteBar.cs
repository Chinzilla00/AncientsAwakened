using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class IncineriteBar : ModTile
    {
        public override void SetDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            dustType = mod.DustType("IncineriteDust");
            drop = mod.ItemType("IncineriteBar");   //put your CustomBlock name
            AddMapEntry(new Color(255, 100, 0));
			minPick = 0;
        }
    }
}