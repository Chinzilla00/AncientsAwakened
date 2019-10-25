using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
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
            Main.tileValue[Type] = 830; 
            soundType = 21;
            drop = mod.ItemType("RadiumOre");
            dustType = mod.DustType("RadiumDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Celestial Ore");
            AddMapEntry(new Color(160, 150, 0), name);
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
            Texture2D glowtex;
            if (Main.dayTime)
            {
                glowtex = mod.GetTexture("Glowmasks/RadiumOre_Glow");
                BaseDrawing.DrawTileTexture(spriteBatch, Main.tileTexture[Type], x, y, true, false, false);
                BaseDrawing.DrawTileTexture(spriteBatch, glowtex, x, y, true, false, false, null, AAGlobalTile.GetRadiumColorBright);
            }
            else
            {
                glowtex = mod.GetTexture("Glowmasks/DarkmatterOre_Glow");
                BaseDrawing.DrawTileTexture(spriteBatch, mod.GetTexture("Tiles/Ore/DarkmatterOre"), x, y, true, false, false);
                BaseDrawing.DrawTileTexture(spriteBatch, glowtex, x, y, true, false, false, null, AAGlobalTile.GetDarkmatterColorBright);
            }
            Tile tile = Main.tile[x, y];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(glowtex, new Vector2((x * 16) - (int)Main.screenPosition.X, (y * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Main.dayTime ? Color.Yellow : Color.DeepSkyBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.dayTime)
            {
                ModTranslation name = CreateMapEntryName();
                AddMapEntry(new Color(160, 150, 0), name);
                dustType = mod.DustType("RadiumDust");
            }
            else
            {
                ModTranslation name = CreateMapEntryName();
                AddMapEntry(new Color(0, 30, 100), name);
                dustType = ModContent.DustType<Dusts.DarkmatterDust>();
            }
            return true;
        }


        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Main.dayTime ? 0.5f : 0f ;
            g = .2f;
            b = Main.dayTime ? 0f : 0.5f;
        }
    }
}