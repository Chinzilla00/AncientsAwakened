namespace AAMod.Items.Materials
{
    public class SeraphFeather : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seraph Feather");
            Tooltip.SetDefault("A silvery feather from a harpy seraph");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 36;
            item.maxStack = 99;
            item.rare = 7;
        }
    }
}