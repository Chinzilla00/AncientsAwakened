using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles.Ore
{
    public class IncineriteOre : ModTile
    {
        public Texture2D glowTex;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 340; 
            Main.tileMerge[Type][mod.TileType("Torchstone")] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            soundType = SoundID.Tink;
            drop = mod.ItemType("Incinerite");   
            dustType = mod.DustType("IncineriteDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Incinerite Ore");
            AddMapEntry(new Color(204, 102, 0), name);
			minPick = 65;
        }


        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAPlayer.IncineriteColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.active() && tile.type == Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/IncineriteOre_glow");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetIncineriteColorDim);
            }
        }
    }
}