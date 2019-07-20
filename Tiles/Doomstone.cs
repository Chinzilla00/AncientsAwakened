using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Doomstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Apocalyptite")] = true;
            Main.tileMergeDirt[Type] = true;
            SetModTree(new OroborosTree());
            soundType = 21;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("Doomstone");   //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(21, 21, 31));
			minPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = null;
            bool glow = !AAWorld.downedZero;
            if (glow && tile != null && tile.active() && tile.type == Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/Doomstone_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return AAWorld.downedZero;
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}