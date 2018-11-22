using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    //imported from my tAPI mod because I'm lazy
    public class HellCrate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Crate");
            Tooltip.SetDefault("Right click to open");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.rare = 2;
            item.maxStack = 99;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("AACrate");
            item.placeStyle = 4;
        }

        public override bool CanRightClick()
		{
			return true;
		}
        
        public override void RightClick(Player player)
        {
            if (Main.rand.Next(6) == 0)
            {
                int item = Main.rand.Next(5);
                switch (item)
                {
                    case 0:
                        item = ItemID.DarkLance;
                        break;
                    case 1:
                        item = ItemID.HellwingBow;
                        break;
                    case 2:
                        item = ItemID.Sunfury;
                        break;
                    case 3:
                        item = ItemID.FlowerofFire;
                        break;
                    case 4:
                        item = ItemID.Flamelash;
                        break;
                    default:
                        item = ItemID.Drax;
                        break;

                }



                int index = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);



                if (Main.netMode == 1)

                {

                    NetMessage.SendData(21, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);

                }

            }



            //bypass all checks and spawn defaults

            player.openCrate(4000);

        }

    }
}
