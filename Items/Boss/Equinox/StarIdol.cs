using Terraria;
using Terraria.ModLoader;

using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Items.Boss.Equinox
{
    public class StarIdol : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blessing of the Stars");
            Tooltip.SetDefault(@"It sparkles like the stars in the sky
Can only be used if there arent many radium stars in the world.");	
		}

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = ItemRarityID.Lime;
			item.expert = true;
            item.value = BaseUtility.CalcValue(0, 15, 0, 0);

			item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;		
        }

        public override bool CanUseItem(Player player)
        {
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(400f * num2);
            for (int j = 5; j < Main.maxTilesX - 5; j++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[j, num4].active() && Main.tile[j, num4].type == (ushort)ModContent.TileType<Tiles.Ore.RadiumOre>())
                    {
                        num++;
                        if (num > num3)
                        {
                            if (Main.dayTime)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.Worldtext("StarIdolInfo"), new Color(43, 178, 245));
                            }
                            else
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.Worldtext("StarIdolInfo"), new Color(0, 255, 181));
                            }
                            return false;
                        }
                    }
                    num4++;
                }
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            
            if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedEquinoxInfo"), Color.Violet);
            for (int i = 0; i < Main.maxTilesX / 50; ++i)
            {
                int X = WorldGen.genRand.Next(Main.maxTilesX / 10 * 2, (int)(Main.maxTilesX / 10 * 4.5f));
                int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                int radius = WorldGen.genRand.Next(2, 6); //Radius.
                for (int x = X - radius; x <= X + radius; x++)
                {
                    for (int y = Y - radius; y <= Y + radius; y++)
                    {
                        if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                        {
                            WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Ore.RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                        }
                    }
                }
            }
            for (int i = 0; i < Main.maxTilesX / 50; ++i)
            {
                int X = WorldGen.genRand.Next((int)(Main.maxTilesX / 10 * 5.5f), Main.maxTilesX / 10 * 8);
                int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                int radius = WorldGen.genRand.Next(2, 6); //Radius.
                for (int x = X - radius; x <= X + radius; x++)
                {
                    for (int y = Y - radius; y <= Y + radius; y++)
                    {
                        if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                        {
                            WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Ore.RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                        }
                    }
                }
            }
            return true;
		}
	}
}