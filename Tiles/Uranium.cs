using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Uranium : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            soundType = 21;
            drop = mod.ItemType("Uranium");   //put your CustomBlock name
            dustType = mod.DustType("InfinityOverloadG");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Uranium Ore");
            AddMapEntry(new Color(2, 150, 0), name);
            minPick = 110;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(Color.DarkSeaGreen, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/UraniumOre_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetUraniumColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}