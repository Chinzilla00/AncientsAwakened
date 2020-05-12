﻿
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace AAMod.Tiles.Decoration
{
    public class TerraStatue : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            dustType = DustID.Gold;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Origin = new Point16(1, 4);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Lost Hero Statue");
            AddMapEntry(new Color(100, 100, 100), name);
            disableSmartCursor = true;
        }

        public override bool NewRightClick(int i, int j)
        {
            BaseUtility.Chat(Lang.TilesInfo("TerraStatueInfo"), Color.LawnGreen);
            return true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("TerraStatue"));
        }

    }
}