using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class CthulhuPortal : ModTile
    {
        public override void SetDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileSolid[Type] = false;
            soundType = 0;
            dustType = 0;
            AddMapEntry(new Color(0, 80, 100));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.Cthulhu, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        float Rotation1 = 0;
        float Rotation2 = 0;

        public override bool PreDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D PortalTex = mod.GetTexture("Tiles/CthulhuPortal_Portal");
            Texture2D PortalTex2 = mod.GetTexture("Tiles/CthulhuPortal_Portal2");
            Rotation1 -= .0008f;
            Rotation2 += .0008f;
            Tile tile = Main.tile[x, y];
            Rectangle Frame = BaseMod.BaseDrawing.GetFrame(0, 60, 60, 0, 0);
            
            BaseMod.BaseDrawing.DrawTexture(sb, PortalTex, 0, new Vector2(x, y) - Main.screenPosition, 60, 60, 0, Rotation1, 0, 1, Frame, AAColor.Cthulhu, false);
            BaseMod.BaseDrawing.DrawTexture(sb, PortalTex2, 0, new Vector2(x, y) - Main.screenPosition, 60, 60, 0, Rotation2, 0, 1, Frame, AAColor.Cthulhu, false);
            return false;
        }
    }
}