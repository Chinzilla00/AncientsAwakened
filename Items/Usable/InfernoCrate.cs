using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class InfernoCrate : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 2;
            item.maxStack = 99;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("InfernoCrate");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Crate");
            Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if(Main.rand.Next(6) == 0)
            {
                int item = Main.rand.Next(4);

                switch (item)
                {
                    case 0:
                        item = mod.ItemType("ScorchDagger");
                        break;
                    case 1:
                        item = mod.ItemType("DragonsBreath");
                        break;
                    case 2:
                        item = mod.ItemType("Railjaw");
                        break;
                    default:
                        item = mod.ItemType("BroodEgg");
                        break;
                }

                int index = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);
                int index1 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("IncineriteBar"), Main.rand.Next(0, 12));

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(21, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);
                    NetMessage.SendData(21, -1, -1, null, index1, 1f, 0f, 0f, 0, 0, 0);
                }
            }

            //bypass all checks and spawn defaults
            AAModGlobalItem.OpenAACrate(player, 0);
        }
    }
}