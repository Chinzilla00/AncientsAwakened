﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

using Terraria.ID;

namespace AAMod.Tiles.Crafters
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
            name.SetDefault("Infinity Core");
            AddMapEntry(new Color(40, 100, 40), name);
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

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/TerraPrism_Glow");
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, tile.frameY + (Main.tileFrame[Type] * 54), false, false, false, null, Globals.AAGlobalTile.GetRainbowColorBright);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 54, 54, mod.ItemType("TerraPrismStation"));
        }
    }
}