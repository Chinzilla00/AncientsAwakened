using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Doomite : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Bar");
            Tooltip.SetDefault("Unsettling energy radiates from this bar");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("DoomiteBar");
            item.value = Terraria.Item.sellPrice(0, 0, 32, 0);
        }
    }
}