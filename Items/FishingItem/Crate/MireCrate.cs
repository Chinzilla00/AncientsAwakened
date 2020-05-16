using Terraria;
using Terraria.ID;

namespace AAMod.Items.FishingItem.Crate
{
    public class MireCrate : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Green;
            item.maxStack = 99;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("MireCrate");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Crate");
            Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            AAModGlobalItem.OpenAACrate(player, 1);
        }
    }
}