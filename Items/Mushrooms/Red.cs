using Terraria.ModLoader;

namespace AAMod.Items.Mushrooms
{
    public class Red : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Alchemical Mushroom");
            Tooltip.SetDefault(@"It smells weird");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 2;
        }
    }
}