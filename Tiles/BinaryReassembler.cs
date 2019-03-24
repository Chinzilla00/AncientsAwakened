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
    public class BinaryReassembler : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = mod.DustType("DoomDust");
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Binary Reassembler");
            AddMapEntry(new Color(40, 0, 0), name);
            disableSmartCursor = true;
            adjTiles = new int[]
            {
                TileID.WorkBenches,
                TileID.Anvils,
                TileID.Furnaces,
                TileID.Hellforge,
                TileID.Bookcases,
                TileID.Sinks,
                TileID.Solidifier,
                TileID.Blendomatic,
                TileID.MeatGrinder,
                TileID.Loom,
                TileID.LivingLoom,
                TileID.FleshCloningVat,
                TileID.GlassKiln,
                TileID.BoneWelder,
                TileID.SteampunkBoiler,
                TileID.Bottles,
                TileID.LihzahrdFurnace,
                TileID.ImbuingStation,
                TileID.DyeVat,
                TileID.Kegs,
                TileID.HeavyWorkBench,
                TileID.Tables,
                TileID.Chairs,
                TileID.CookingPots,
                TileID.DemonAltar,
                TileID.Sawmill,
                TileID.CrystalBall,
                TileID.AdamantiteForge,
                TileID.MythrilAnvil,
                TileID.TinkerersWorkbench,
                TileID.Autohammer,
                TileID.IceMachine,
                TileID.SkyMill,
                TileID.HoneyDispenser,
                TileID.AlchemyTable,
                TileID.LunarCraftingStation,
                mod.TileType("HellstoneAnvil"),
                mod.TileType("HallowedAnvil"),
                mod.TileType("HallowedForge"),
                mod.TileType("QuantumFusionAccelerator"),
                mod.TileType("ACS"),
            };
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0f;
        }

        public Texture2D glowTex = null;

        public Color GetColor(Color color)
        {
            Color glowColor = AAColor.ZeroShield;
            return glowColor;
        }


        public Color White(Color color)
        {
            return Color.White;
        }


        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/BinaryReassemblerTile_Glow");
            int frameY = (tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 54) : 0);

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, frameY, false, false, false, null, White);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("BinaryReassembler"));
        }
    }
}