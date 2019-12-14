using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Crafters
{
    public class SharpeningLavaFishTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.AnchorInvalidTiles = new[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Sharpening Lava Fish");
            dustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(223, 113, 38), name);
            disableSmartCursor = false;
            adjTiles = new int[]
            {
                TileID.LunarCraftingStation,
                mod.TileType("SharpeningLavaFishTile")
            };
            animationFrameHeight = 38;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
			if (frameCounter >= 10)
			{
				frameCounter = 0;
				frame++;
				if (frame >= 4)
				{
					frame = 0;
				}
			}
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
            offsetY = 2;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .22f;
            g = .11f;
            b = .38f;
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.AddBuff(159, 36000, true);
            player.AddBuff(74, 36000, true);
			Main.PlaySound(SoundID.Item37, player.position);
        }

        public override void MouseOver(int i, int j)
        {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("SharpeningLavaFish");
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("SharpeningLavaFish"));
        }
    }
}