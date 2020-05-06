using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class WizardPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Wizard");
            Tooltip.SetDefault("'I don't care what you've become. I still have hope for you.'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("WizardPainting");
        }
    }
}