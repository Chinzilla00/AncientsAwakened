using Terraria;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class AncientOreGenner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Ore Genner");
            Tooltip.SetDefault(@"Generates AA worldgen ore");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            GenOres();
            
            return true;
        }
        private void GenOres()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 1)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("IncineriteOre"));
                }
            }

            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, x);
                int tilesY = WorldGen.genRand.Next(0, y);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)mod.TileType("EverleafRoot"));
                }
            }

            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("AbyssiumOre"));
                }
            }
            
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next(0, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == TileID.IceBlock)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("RelicOre"));
                }
            }

            int amount = (int)(Main.maxTilesX * 0.4f * 0.2f);
            for (int k = 0; k < amount; k++)
            {
                int i = WorldGen.genRand.Next(0, Main.maxTilesX);
                int j = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                while (Main.tile[i, j].type != 1)
                {
                    i = WorldGen.genRand.Next(0, Main.maxTilesX);
                    j = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                }
                WorldGen.TileRunner(i, j, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), mod.TileType("PrismOre"));
            }
        }
    }
}
