using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Banners
{
    public class FatPixieBanner : ModTile
	{
		public override void SetDefaults() 
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.AnchorBottom = default;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinatePadding = 0;			
            TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			dustType = -1;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) 
		{
			Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("FatPixieBanner"));
		}

		public override void NearbyEffects(int i, int j, bool closer) 
		{
			if (closer)
			{
				Player player = Main.LocalPlayer;
				player.NPCBannerBuff[mod.NPCType("FatPixie")] = true;
				player.hasBanner = true;
			}
		}
	}
}
