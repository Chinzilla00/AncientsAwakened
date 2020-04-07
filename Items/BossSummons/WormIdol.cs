namespace AAMod.Items.BossSummons
{
    public class WormIdol : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Idol");
            Tooltip.SetDefault(@"An ancient statue depicting some form of worm god.
It looks like it hasn't been touched in years");
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