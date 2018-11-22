using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class HydraClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Claw");
            Tooltip.SetDefault("Don't prick yourself");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.maxStack = 99;
        }
    }
}