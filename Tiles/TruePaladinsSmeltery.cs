using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class TruePaladinsSmeltery : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("True Paladin's Smeltery");
            dustType = mod.DustType<Dusts.DaybreakIncineriteDust>();
            AddMapEntry(new Color(40, 40, 40), name);
            disableSmartCursor = true;
            adjTiles = new int[]
            { TileID.WorkBenches,
              TileID.Hellforge,
              TileID.Furnaces,
              TileID.TinkerersWorkbench,
              TileID.AlchemyTable,
              TileID.Bottles,
              TileID.MythrilAnvil,
              TileID.Tables,
              TileID.DemonAltar,
              TileID.Chairs,
              TileID.Anvils,
              mod.TileType("HellstoneAnvil"),
              mod.TileType("HallowedAnvil"),
              mod.TileType("HallowedForge"),
              TileID.MythrilAnvil,
              TileID.Anvils,
              TileID.CrystalBall,
              TileID.HeavyWorkBench,
              TileID.Hellforge,
              TileID.Furnaces,
              TileID.AdamantiteForge,
              TileID.Autohammer,
              TileID.ImbuingStation
              };
            animationFrameHeight = 38;
        }

        public override void PostDraw(int i, int j, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/TruePaladinsSmeltery_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0.40f;
            b = 0.0f;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AdamantiteForge];
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("TruePaladinsSmeltery"));
        }
    }
}