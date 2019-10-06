using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class DaybreakIncineriteOre : ModTile
    {

        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            //true for block to emit light
            soundType = 21;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("DaybreakIncineriteOre");   //put your CustomBlock name
            dustType = mod.DustType("AkumaADust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Daybreak Incinerite Ore");
            AddMapEntry(new Color(100, 30, 0), name);
			minPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.active() && tile.type == this.Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/DaybreakIncineriteOre_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetAkumaColorBright);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = 0.15f;
            b = 0.15f;
        }
    }
}