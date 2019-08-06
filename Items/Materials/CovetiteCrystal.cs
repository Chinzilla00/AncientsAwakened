namespace AAMod.Items.Materials
{
    public class CovetiteCrystal : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Covetite Crystal");
            Tooltip.SetDefault(@"You have a strange desire for this crystal, 
despite you already owning it.");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
			item.maxStack = 99;
            item.rare = 6;
        }
    }
}
