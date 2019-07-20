using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TerraCrystal : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][mod.TileType("TerraWood")] = true;
            soundType = 21;
            Main.tileLighted[Type] = true;
            dustType = 107;
            AddMapEntry(new Color(39, 125, 37));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
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
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/TerraCrystal"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), AAColor.TerraGlow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.TerraGlow, 1.4f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void RandomUpdate(int i, int j)
        {
            int coordinates = WorldGen.genRand.Next(4);
            int x = 0;
            int y = 0;
            switch (coordinates)
            {
                case 0:
                    x = -1;
                    break;

                case 1:
                    x = 1;
                    break;

                case 2:
                    y = -1;
                    break;

                case 3:
                    y = 1;
                    break;
            }
            if (!Main.tile[i + x, j + y].active() && Main.rand.Next(500) == 0 && NPC.downedPlantBoss)
            {
                int num4 = 0;
                int num5 = 6;
                for (int k = i - num5; k <= i + num5; k++)
                {
                    for (int l = j - num5; l <= j + num5; l++)
                    {
                        if (Main.tile[k, l].active())
                        {
                            num4++;
                        }
                    }
                }
                if (num4 < 2)
                {
                    WorldGen.PlaceTile(i + x, j + y, mod.TileType<BiomePrism>(), false, false, -1, 0);
                    NetMessage.SendTileSquare(-1, i + x, j + y, 1, TileChangeType.None);
                }
            }
        }
    }
}