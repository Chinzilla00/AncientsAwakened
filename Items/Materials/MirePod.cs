using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class MirePod : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Pod");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
        }
    }
}