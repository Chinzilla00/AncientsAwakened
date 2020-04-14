using Terraria;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Tiles.Ore;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Boss.Equinox
{
	public class StarIdol : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blessing of the Stars");	
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Unleashes the power of Grovite upon your world!" });	
		}

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 7;
			item.expert = true;
            item.value = BaseUtility.CalcValue(0, 15, 0, 0);

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;		
        }

		public override bool UseItem(Player player)
        {
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(400f * num2);
            for (int j = 5; j < Main.maxTilesX - 5; j++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[j, num4].active() && Main.tile[j, num4].type == (ushort)ModContent.TileType<RadiumOre>())
                    {
                        num++;
                        if (num > num3)
                        {
                            return false;
                        }
                    }
                    num4++;
                }
            }
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
                            WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
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
                            WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                        }
                    }
                }
            }
            return true;
		}
	}
}