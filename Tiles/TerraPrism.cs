using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BaseMod;

namespace AAMod.Tiles
{
    public class TerraPrism : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = 107;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Terra Infuser");
            AddMapEntry(new Color(40, 100, 40), name);
            disableSmartCursor = true;
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 10)
            {
                frameCounter = 0;
                if (++frame >= 10) frame = 0;
            }
        }

        public Color CurrentColor = AAGlobalTile.GetPrismColorDim(Color.White);

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = CurrentColor.R / 255f;
            g = CurrentColor.G / 255f;
            b = CurrentColor.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/TerraPrism_Glow");
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, tile.frameY + (Main.tileFrame[Type] * 54), false, false, false, null, AAGlobalTile.GetPrismColorDim);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 54, 54, mod.ItemType("TerraPrismStation"));
        }
    }
}