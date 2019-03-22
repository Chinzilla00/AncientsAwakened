using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RadiumOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;  
            Main.tileLighted[Type] = true;
            soundType = 21;
            drop = mod.ItemType("RadiumOre");
            dustType = mod.DustType("RadiumDust");
            AddMapEntry(new Color(100, 90, 0));
			minPick = 225;
        }
        

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (Main.dayTime)
            {
                drop = mod.ItemType("RadiumOre");
            }
            else
            {
                drop = mod.ItemType("DarkmatterOre");
            }
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            if (Main.dayTime)
            {
                BaseDrawing.DrawTileTexture(spriteBatch, Main.tileTexture[Type], x, y, true, false, false);
            }
            else
            {
                BaseDrawing.DrawTileTexture(spriteBatch, mod.GetTexture("Tiles/RadiumOreDark"), x, y, true, false, false);
            }
            return false;
        }
        

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;

            Texture2D glowtex = mod.GetTexture("Glowmasks/RadiumOre_Glow");

            if (Main.dayTime)
            {
                BaseMod.BaseDrawing.DrawTileTexture(spriteBatch, glowtex, i, j, true, false, false, null, AAGlobalTile.GetRadiumColorDim);
            }
            else
            {
                BaseMod.BaseDrawing.DrawTileTexture(spriteBatch, glowtex, i, j, true, false, false, null, AAGlobalTile.GetDarkmatterColorDim);
            }

        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.dayTime)
            {
                dustType = mod.DustType("RadiumDust");
            }
            else
            {
                dustType = mod.DustType<Dusts.DarkmatterDust>();
            }
            return true;
        }


        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Main.dayTime ? 0.5f : 0f ;
            g = Main.dayTime ? .2f : 0f;
            b = 0f;
        }
    }
}