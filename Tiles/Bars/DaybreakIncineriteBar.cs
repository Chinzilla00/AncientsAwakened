using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Bars
{
    public class DaybreakIncineriteBar : ModTile
    {
        public override void SetDefaults()
        {
            soundType = 21;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            drop = mod.ItemType("DaybreakIncinerite");   
            dustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
            AddMapEntry(new Color(160, 100, 0));
			minPick = 0;
        }
    }
}