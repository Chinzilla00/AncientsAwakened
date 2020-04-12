using Terraria;

namespace AAMod.Items.FishingItem.Crate
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
            AAModGlobalItem.OpenAACrate(player, 0);
        }
    }
}