using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
	// TODO: Smart Cursor Outlines and tModLoader support
	public class GreedDoorLocked : ModTile
	{
		public override void SetDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Hoard Door");
			AddMapEntry(new Color(100, 70, 0), name);
			dustType = DustID.t_Lihzahrd;
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.ClosedDoor };
        }

        public override bool HasSmartInteract() => true;

        public bool IsLockedDoor(int i, int j) => Main.tile[i, j].frameY / 54 == 2;

        public override bool CanKillTile(int i, int j, ref bool blockDamaged) => false;

        public override bool CanExplode(int i, int j) => false;

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("GreedDoor"));
		}

        public override void RightClick(int i, int j)
        {
            Player p = Main.player[Main.myPlayer];
            int num43 = mod.ItemType<Items.Usable.GreedKey>();
            for (int num44 = 0; num44 < 58; num44++)
            {
                if (p.inventory[num44].type == num43 && p.inventory[num44].stack > 0)
                {
                    p.inventory[num44].stack--;
                    if (p.inventory[num44].stack <= 0)
                    {
                        p.inventory[num44] = new Item();
                    }
                    Main.tile[i, j].type = (ushort)mod.TileType<GreedDoorClosed>();
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(52, -1, -1, null, p.whoAmI, 2f, i, j, 0, 0, 0);
                    }
                }
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            player.showItemIcon2 = -1;
            if (chest < 0)
            {
                player.showItemIconText = Language.GetTextValue("LegacyChestType.0");
            }
            else
            {
                player.showItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Golden Chest";
                if (player.showItemIconText == "Golden Chest")
                {
                    player.showItemIcon2 = ItemID.GoldenChest;
                    if (Main.tile[left, top].frameX / 36 == 1)
                        player.showItemIcon2 = mod.ItemType<Items.Usable.GreedKey>();
                    player.showItemIconText = "";
                }
            }
            player.noThrow = 2;
            player.showItemIcon = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.showItemIconText == "")
            {
                player.showItemIcon = false;
                player.showItemIcon2 = 0;
            }
        }
    }
}