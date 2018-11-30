using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class IceCrate : ModItem
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
            item.createTile = mod.TileType("IceCrate");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Crate");
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
                int item = Main.rand.Next(8);

                switch (item)
                {
                    case 0:
                        item = ItemID.BlizzardinaBottle;
                        break;
                    case 1:
                        item = ItemID.IceBoomerang;
                        break;
                    case 2:
                        item = ItemID.IceBlade;
                        break;
                    case 3:
                        item = ItemID.IceSkates;
                        break;
                    case 4:
                        item = ItemID.SnowballCannon;
                        break;
                    case 5:
                        item = ItemID.FlurryBoots;
                        break;
                    case 6:
                        item = ItemID.IceMirror;
                        break;
                    default:
                        item = ItemID.Fish;
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