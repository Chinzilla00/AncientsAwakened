using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class CometOre : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comet Ore");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 99;
            item.rare = 1;
        }
    }
}
