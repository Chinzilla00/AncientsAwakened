using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;
using Terraria.ID;

namespace AAMod.Tiles.Ore
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
            Main.tileValue[Type] = 860;
            Main.tileBlockLight[Type] = true;
            SetModTree(new OroborosTree());
            soundType = SoundID.Tink;
            drop = mod.ItemType("Apocalyptite");   
            dustType = mod.DustType("DoomDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Apocalyptite Ore");
            AddMapEntry(new Color(70, 20, 20), name);
			minPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.active() && tile.type == Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/ApocalyptiteTile_Glow");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
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