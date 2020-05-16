using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class ShinyCharm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shiny Charm");
            Tooltip.SetDefault("A rare charm that allows you to make certain weapons shiny");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 36;
            item.maxStack = 99;
            item.rare = ItemRarityID.Cyan;
        }
    }
}