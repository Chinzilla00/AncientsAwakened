using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class BiomePrism : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            soundType = 21;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("BiomePrism");   //put your CustomBlock name
            dustType = mod.DustType<Dusts.Glass>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Biome pRISM");
            AddMapEntry(new Color(70, 70, 70), name);
			minPick = 200;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            Tile tile2 = Main.tile[i, j - 1];
            Tile tile3 = Main.tile[i, j + 1];
            Tile tile4 = Main.tile[i - 1, j];
            Tile tile5 = Main.tile[i + 1, j];
            int num25 = -1;
            int num26 = -1;
            int num27 = -1;
            int num28 = -1;
            if (tile2 != null && tile2.nactive() && !tile2.bottomSlope())
            {
                num26 = tile2.type;
            }
            if (tile3 != null && tile3.nactive() && !tile3.halfBrick() && !tile3.topSlope())
            {
                num25 = tile3.type;
            }
            if (tile4 != null && tile4.nactive())
            {
                num27 = tile4.type;
            }
            if (tile5 != null && tile5.nactive())
            {
                num28 = tile5.type;
            }
            if (num25 >= 0 && Main.tileSolid[num25] && !Main.tileSolidTop[num25])
            {
                tile.frameY = 0;
            }
            else if (num27 >= 0 && Main.tileSolid[num27] && !Main.tileSolidTop[num27])
            {
                tile.frameY = 54;
            }
            else if (num28 >= 0 && Main.tileSolid[num28] && !Main.tileSolidTop[num28])
            {
                tile.frameY = 36;
            }
            else if (num26 >= 0 && Main.tileSolid[num26] && !Main.tileSolidTop[num26])
            {
                tile.frameY = 18;
            }
            else
            {
                WorldGen.KillTile(i, j, false, false, false);
            }
            return false;
        }
        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.active() && tile.type == Type)
            {
                Texture2D glowTex = Main.tileTexture[Type];
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetBlankColorBright);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.05f;
            g = 0.05f;
            b = 0.05f;
        }
    }
}