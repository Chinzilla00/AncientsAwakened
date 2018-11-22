using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class HydraToxin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Toxin");
            Tooltip.SetDefault("The Essance of Chaos found from the Mire");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.maxStack = 99;
            item.rare = 3;
        }
    }
}