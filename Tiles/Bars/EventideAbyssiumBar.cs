using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAMod.Tiles.Bars
{
    public class EventideAbyssiumBar : ModTile
    {
        public override void SetDefaults()
        {
            soundType = SoundID.Tink;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            drop = mod.ItemType("EventideAbyssium");   
            dustType = ModContent.DustType<Dusts.AbyssDust>();
            AddMapEntry(new Color(0, 0, 255));
			minPick = 0;
        }
    }
}