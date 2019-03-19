using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DarkmatterOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            soundType = 21;
            dustType = mod.DustType("DarkmatterDust");
            drop = mod.ItemType("DarkmatterOre");
            AddMapEntry(new Color(20, 20, 21));
			minPick = 225;
        }

        public bool glow = !Main.dayTime;

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.Nightcrawler, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                Texture2D glowTex = mod.GetTexture("Glowmasks/DarkmatterOre_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetDarkmatterColorDim);
            }
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return !Main.dayTime;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}