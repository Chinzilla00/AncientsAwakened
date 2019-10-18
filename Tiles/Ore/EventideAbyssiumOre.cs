using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class EventideAbyssiumOre : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 840; 
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            soundType = 21;
            TileID.Sets.JungleSpecial[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("EventideAbyssiumOre");   //put your CustomBlock name
            dustType = mod.DustType("YamataDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Eventide Abyssium Ore");
            AddMapEntry(new Color(0, 0, 30), name);
			minPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.active() && tile.type == this.Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/EventideAbyssiumOre_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetYamataColorDim2);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = .2f;
            g = 0f;
            b = .5f;
        }
    }
}