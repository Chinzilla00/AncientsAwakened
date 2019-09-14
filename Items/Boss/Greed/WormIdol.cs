namespace AAMod.Items.Boss.Greed
{
    public class WormIdol : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Idol");
            Tooltip.SetDefault(@"An ancient statue depicting some form of worm god
It has a slot for an orb of some kind on the bottom...");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 11;
        }
    }
}