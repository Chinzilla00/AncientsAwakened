using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Apocalyptite : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Doomstone")] = true;
            Main.tileMergeDirt[Type] = false;
            SetModTree(new OroborosTree());
            soundType = 21;
            drop = mod.ItemType("Apocalyptite");   //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Apocalyptite Ore");
            AddMapEntry(new Color(70, 20, 20), name);
			minPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/ApocalyptiteTile_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
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