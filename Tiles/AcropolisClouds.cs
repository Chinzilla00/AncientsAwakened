using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AcropolisClouds : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = DustID.BlueCrystalShard;
            AddMapEntry(new Color(30, 89, 125));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAColor.Sky, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public static Color C(Color co)
        {
            return BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, AAColor.Sky, Color.Transparent, AAColor.Sky);
        }

        public override void PostDraw(int i, int j, SpriteBatch sb)
        {
            Tile tile = Main.tile[i, j];
            if (tile != null && tile.active() && tile.type == Type)
            {
                BaseDrawing.DrawTileTexture(sb, Main.tileTexture[Type], i, j, true, false, false, null, C);
            }
        }
    }
}