using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Crafters
{
    public class ToxinMonkfishTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.newTile.AnchorInvalidTiles = new[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Toxin Monkfish");
            dustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(93, 163, 79), name);
            disableSmartCursor = false;
            adjTiles = new int[]
            {
                TileID.AlchemyTable,
                mod.TileType("ToxinMonkfishTile")
            };
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
			if (frameCounter >= 4)
			{
				frameCounter = 0;
				frame++;
				if (frame >= 4)
				{
					frame = 0;
				}
			}
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.36f;
            g = 0.64f;
            b = 0.31f;
        }

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.AddBuff(mod.BuffType("HydratoxinFlaskBuff"), 36000, true);
			Main.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("ToxinMonkfish");
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("ToxinMonkfish"));
        }
    }
}