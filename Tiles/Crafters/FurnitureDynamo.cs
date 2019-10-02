using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Crafters
{
    public class FurnitureDynamo : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = mod.DustType("AbyssiumDust");
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Furniture Dynamo");
            AddMapEntry(new Color(40, 0, 0), name);
            disableSmartCursor = true;
            adjTiles = new int[]
            {
                TileID.Blendomatic,
                TileID.Solidifier,
                TileID.Blendomatic,
                TileID.MeatGrinder,
                TileID.LivingLoom,
                TileID.FleshCloningVat,
                TileID.GlassKiln,
                TileID.BoneWelder,
                TileID.SteampunkBoiler,
                TileID.LihzahrdFurnace,
                TileID.HeavyWorkBench,
                TileID.Sawmill,
                TileID.IceMachine,
                TileID.SkyMill,
                TileID.HoneyDispenser,
                TileID.AlchemyTable
            };
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.Solidifier];
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("FurnitureDynamo"));
        }
    }
}