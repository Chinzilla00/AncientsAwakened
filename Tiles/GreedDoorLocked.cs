using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
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
			AddMapEntry(new Color(200, 200, 200), name);
			dustType = DustID.t_Lihzahrd;
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.ClosedDoor };
			openDoorID = -1;
		}

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool HasSmartInteract()
        {
			return false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
        {
			num = 1;
		}

		public override void MouseOver(int i, int j)
        {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("GreedKey");
		}

        public override void RightClick(int i, int j)
        {
            if (BaseMod.BasePlayer.HasItem(Main.LocalPlayer, mod.ItemType("GreedKey"), 1))
            {
                UnlockDoor(i, j);
            }
        }

        public void UnlockDoor(int i, int j)
        {
            int num = j;
            if (Main.tile[i, num] == null)
            {
                return;
            }
            Main.PlaySound(22, i * 16, num * 16 + 16, 1, 1f, 0f);
            for (int k = num; k <= num + 2; k++)
            {
                if (Main.tile[i, k] == null)
                {
                    Main.tile[i, k] = new Tile();
                }
                Tile expr_99 = Main.tile[i, k];
                expr_99.type = (ushort)mod.TileType<GreedDoorClosed>();
                if (Main.netMode != 1) NetMessage.SendObjectPlacment(-1, i, k, mod.TileType<GreedDoorClosed>(), 0, 0, -1, -1);
                for (int l = 0; l < 4; l++)
                {
                    Dust.NewDust(new Vector2(i * 16, k * 16), 16, 16, 11, 0f, 0f, 0, default, 1f);
                }
            }
        }
    }
}